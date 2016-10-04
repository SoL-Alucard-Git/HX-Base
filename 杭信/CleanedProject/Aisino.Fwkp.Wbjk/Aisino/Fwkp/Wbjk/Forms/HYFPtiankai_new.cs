namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.MainForm;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Framework.Startup.Login;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fptk.Common.Forms;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.DAL;
    using Aisino.Fwkp.Wbjk.Model;
    using Aisino.Fwkp.Wbjk.Properties;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    public class HYFPtiankai_new : DockForm
    {
        private InvoiceBill _fpxx;
        private AisinoMultiCombox _spmcBt;
        private AisinoLBL aisinoLBL1;
        private AisinoLBL aisinoLBL2;
        private AisinoLBL aisinoLBL5;
        private SaleBill bill = null;
        private string blueJe = string.Empty;
        private ToolStripButton bt_jg;
        private CheckBox ckbNegtive;
        private AisinoMultiCombox cmbFhr;
        private AisinoMultiCombox cmbFhrsbh;
        private AisinoMultiCombox cmbSender;
        private AisinoMultiCombox cmbShr;
        private AisinoMultiCombox cmbShrsbh;
        private AisinoMultiCombox cmbSkr;
        private AisinoCMB cmbSlv;
        private AisinoMultiCombox cmbSpf;
        private AisinoMultiCombox cmbSpfsbh;
        private DataGridViewTextBoxColumn colFyxm;
        private DataGridViewTextBoxColumn colJe;
        private IContainer components = null;
        private string curSlv;
        internal List<string[]> data;
        private DateTimePicker dateTimePicker1;
        private CustomStyleDataGrid dgFyxm;
        private DataGridViewTextBoxEditingControl EditingControl;
        internal int index;
        private bool initSuccess = true;
        private AisinoLBL lblCyr;
        private AisinoLBL lblCyrsbh;
        private AisinoLBL lblFpdm;
        private AisinoLBL lblFphm;
        private AisinoLBL lblHjje;
        private AisinoLBL lblHjse;
        private AisinoLBL lblJgdx;
        private AisinoLBL lblJgxx;
        private AisinoLBL lblJqbh;
        private AisinoLBL lblKpr;
        private AisinoLBL lblKprq;
        private AisinoLBL lblSwjg;
        private AisinoLBL lblSwjgdm;
        private AisinoLBL lblTitle;
        private ILog log = LogUtil.GetLogger<HYFPtiankai_new>();
        private bool onlyShow;
        private AisinoPNL panel1;
        private AisinoPNL panel2;
        private SaleBillCtrl saleBillBL = SaleBillCtrl.Instance;
        private TaxCard taxCard = TaxCardFactory.CreateTaxCard();
        private ToolTip tip = new ToolTip();
        private ToolStripButton tool_addrow;
        private ToolStripButton tool_close;
        private ToolStripButton tool_delrow;
        private ToolStripMenuItem tool_drwl;
        private ToolStripMenuItem tool_drxxb;
        private ToolStripMenuItem tool_zjkj;
        private ToolStrip toolStrip3;
        private ToolStripButton toolStripBtnSave;
        private AisinoTXT txtBz;
        private AisinoTXT txtCcdw;
        private AisinoTXT txtCzch;
        private AisinoTXT txtDJH;
        private AisinoTXT txtQyd;
        private AisinoTXT txtYshw;
        private Fpxx ykfp;

        internal HYFPtiankai_new(FPLX fplx, string DJBH, string EditAddFlag = "xg")
        {
            try
            {
                try
                {
                    if (base.TaxCardInstance.get_QYLX().ISHY)
                    {
                        this.HYInvoiceForm_Init(fplx, DJBH, EditAddFlag);
                    }
                    else
                    {
                        MessageManager.ShowMsgBox("INP-242155", new string[] { " 无货物运输业增值税专用发票授权。" });
                    }
                }
                catch
                {
                }
            }
            finally
            {
            }
        }

        private void _spmcBt_Click(object sender, EventArgs e)
        {
            this._SpmcSelect();
        }

        public void _spmcBt_leave(object sender, EventArgs e)
        {
            string str;
            int rowIndex;
            if (CommonTool.isSPBMVersion())
            {
                try
                {
                    str = this._spmcBt.Text.Trim();
                    if (str != "")
                    {
                        object[] objArray;
                        object[] objArray2;
                        DataTable table = new SaleBillDAL().GET_SPXX_BY_NAME(str, "f", "");
                        if (table.Rows.Count == 0)
                        {
                            objArray = new object[] { str, "MC,BM,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV" };
                            objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddFYXM", objArray);
                            if (objArray2 != null)
                            {
                                if ((objArray2.Length >= 3) && (objArray2[2].ToString().Trim() == ""))
                                {
                                    MessageBox.Show("费用项目没有分类编码！");
                                }
                                else
                                {
                                    this.SetFYXM(objArray2);
                                }
                            }
                            else
                            {
                                MessageBoxHelper.Show("费用项目不存在，必须增加！");
                            }
                        }
                        else if (table.Rows.Count == 1)
                        {
                            objArray = new object[] { str, 1, "MC,BM,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV" };
                            objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetFYXM", objArray);
                            if (objArray2 != null)
                            {
                                if (objArray2.Length >= 3)
                                {
                                    string str3 = objArray2[1].ToString().Trim();
                                    string str2 = objArray2[2].ToString().Trim();
                                    str3 = table.Rows[0]["BM"].ToString().Trim();
                                    if (table.Rows[0]["SPFL"].ToString().Trim() == "")
                                    {
                                        objArray = new object[] { str, "MC,BM,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV", str3, true };
                                        objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddFYXM", objArray);
                                        if (objArray2 != null)
                                        {
                                            this.SetFYXM(objArray2);
                                        }
                                        else
                                        {
                                            MessageBoxHelper.Show("费用项目的商品分类编码需要补全！");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBoxHelper.Show("没有找到对应的费用项目！");
                            }
                        }
                        else if (table.Rows.Count > 1)
                        {
                            string fLBM = "";
                            rowIndex = this.dgFyxm.CurrentCell.RowIndex;
                            if (rowIndex < this.bill.ListGoods.Count)
                            {
                                fLBM = this.bill.ListGoods[rowIndex].FLBM;
                            }
                            if (fLBM == "")
                            {
                                objArray = new object[] { str, 0, "MC,BM,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV" };
                                objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetFYXM", objArray);
                                if (objArray2 != null)
                                {
                                    this.SetFYXM(objArray2);
                                }
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    HandleException.HandleError(exception);
                }
            }
            else
            {
                str = this._spmcBt.Text.Trim();
                rowIndex = this.dgFyxm.CurrentCell.RowIndex;
                this.bill.ListGoods[rowIndex].SPMC = str;
            }
        }

        private void _spmcBt_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this._SpmcSelect();
        }

        private void _spmcBt_OnAutoComplate(object sender, EventArgs e)
        {
            string text = this._spmcBt.Text;
            DataTable table = this._SpmcOnAutoCompleteDataSource(text);
            if (table != null)
            {
                this._spmcBt.set_DataSource(table);
            }
        }

        private void _spmcBt_OnSelectValue(object sender, EventArgs e)
        {
            Dictionary<string, string> dictionary = this._spmcBt.get_SelectDict();
            this._SpmcOnSelectValue(this.dgFyxm, dictionary);
        }

        private void _spmcBt_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && ((this._spmcBt.get_DataSource() == null) || (this._spmcBt.get_DataSource().Rows.Count == 0)))
            {
                this._SpmcSelect();
            }
        }

        private void _spmcBt_SetAutoComplateHead()
        {
            this._spmcBt.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("名称", "MC", 160));
            this._spmcBt.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("简码", "JM", 60));
            this._spmcBt.set_ShowText("MC");
            this._spmcBt.set_DrawHead(true);
            this._spmcBt.set_AutoIndex(0);
        }

        private void _spmcBt_TextChanged(object sender, EventArgs e)
        {
            int index = this.dgFyxm.CurrentRow.Index;
            string text = this._spmcBt.Text;
            bool flag = false;
            flag = this._fpxx.SetSpmc(index, text);
            this.ShowDataGrid(this._fpxx.GetSpxx(index), index);
            if (!flag)
            {
                MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
            }
        }

        private DataTable _SpmcOnAutoCompleteDataSource(string str)
        {
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetFYXMMore", new object[] { str, 20 });
            if ((objArray != null) && (objArray.Length > 0))
            {
                return (objArray[0] as DataTable);
            }
            return null;
        }

        private void _SpmcOnSelectValue(CustomStyleDataGrid dataGrid, Dictionary<string, string> value)
        {
            this._spmcBt.Leave -= new EventHandler(this._spmcBt_leave);
            try
            {
                if (CommonTool.isSPBMVersion())
                {
                    string str = value["MC"];
                    string str2 = value["BM"];
                    string str3 = value["SPFL"];
                    if (str3 == "")
                    {
                        object[] objArray = new object[] { str, "MC,BM,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV", str2, true };
                        object[] spxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddFYXM", objArray);
                        if (spxx != null)
                        {
                            if (spxx.Length >= 3)
                            {
                                string str4 = spxx[1].ToString().Trim();
                                string str5 = spxx[0].ToString().Trim();
                                if (spxx[2].ToString().Trim() == "")
                                {
                                    MessageBoxHelper.Show("费用项目的商品分类编码需要补全！");
                                }
                                else
                                {
                                    this.SetFYXM(spxx);
                                }
                            }
                        }
                        else
                        {
                            MessageBoxHelper.Show("费用项目的商品分类编码需要补全！");
                        }
                    }
                    else
                    {
                        this.SetFYXM(new object[] { value["MC"], value["BM"], value["SPFL"], value["YHZC"], value["SPFL_ZZSTSGL"], value["YHZC_SLV"] });
                    }
                }
                else
                {
                    this.SetFYXM(new object[] { value["MC"], value["BM"], value["SPFL"], value["YHZC"], value["SPFL_ZZSTSGL"], value["YHZC_SLV"] });
                }
            }
            catch
            {
            }
            finally
            {
                this._spmcBt.Leave += new EventHandler(this._spmcBt_leave);
            }
        }

        private void _SpmcSelect()
        {
            try
            {
                string text = this._spmcBt.Text;
                object[] objArray = new object[] { text, 0, "MC,BM,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV" };
                object[] spxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetFYXM", objArray);
                if (spxx != null)
                {
                    if (CommonTool.isSPBMVersion())
                    {
                        if (spxx.Length >= 3)
                        {
                            string str2 = spxx[1].ToString().Trim();
                            string str3 = spxx[0].ToString().Trim();
                            if (spxx[2].ToString().Trim() == "")
                            {
                                objArray = new object[] { str3, "MC,BM,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV", str2, true };
                                object[] objArray3 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddFYXM", objArray);
                                if (objArray3 != null)
                                {
                                    this.SetFYXM(objArray3);
                                }
                                else
                                {
                                    MessageBoxHelper.Show("费用项目的商品分类编码需要补全！");
                                }
                            }
                            else
                            {
                                this.SetFYXM(spxx);
                            }
                        }
                    }
                    else
                    {
                        this.SetFYXM(spxx);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void addRow_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = this.dgFyxm.SelectedRows;
            int index = -1;
            bool flag = true;
            if (selectedRows.Count > 0)
            {
                index = this.dgFyxm.SelectedRows[0].Index;
                flag = this.AddSpxx(index);
            }
            else if ((this.dgFyxm.Rows.Count > 0) && (this.dgFyxm.CurrentCell.RowIndex != (this.dgFyxm.Rows.Count - 1)))
            {
                index = this.dgFyxm.CurrentCell.RowIndex;
                flag = this.AddSpxx(index);
            }
            else if (this.AddSpxx(-1))
            {
                index = this.dgFyxm.Rows.Count - 1;
                if (index == -1)
                {
                    index = 0;
                }
            }
            if (index != -1)
            {
                this.dgFyxm.CurrentCell = this.dgFyxm.Rows[index].Cells[0];
            }
        }

        private bool AddSpxx(int index)
        {
            bool flag = false;
            if (this._fpxx.CanAddSpxx(1, false))
            {
                Spxx spxx = new Spxx("", "", PresentinvMng.GetSLValue(this.cmbSlv.Text));
                if (index < 0)
                {
                    this.bill.ListGoods.Add(new Goods());
                    if (this._fpxx.AddSpxx(spxx) >= 0)
                    {
                        int num2 = this.dgFyxm.Rows.Add();
                        this.ShowDataGrid(this._fpxx.GetSpxx(num2), num2);
                        return true;
                    }
                    MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
                    return flag;
                }
                this.bill.ListGoods.Insert(index, new Goods());
                if (this._fpxx.InsertSpxx(index, spxx) >= 0)
                {
                    this.dgFyxm.Rows.Insert(index, new object[0]);
                    this.ShowDataGrid(this._fpxx.GetSpxx(index), index);
                    return true;
                }
                MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
                return flag;
            }
            MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
            return flag;
        }

        private void ckbNegtive_CheckStateChanged(object sender, EventArgs e)
        {
            this.SetHzxx();
        }

        private void ClearMainInfo()
        {
            this._fpxx.set_Gfmc("");
            this._fpxx.set_Gfsh("");
            this._fpxx.set_Shrmc("");
            this._fpxx.set_Shrsh("");
            this._fpxx.set_Fhrmc("");
            this._fpxx.set_Fhrsh("");
            this._fpxx.set_Bz("");
            this._fpxx.set_Yshwxx("");
            this._fpxx.set_Ccdw("");
            this._fpxx.set_Czch("");
            this._fpxx.set_Qyd_jy_ddd("");
        }

        private void CloseHYForm()
        {
            base.FormClosing -= new FormClosingEventHandler(this.HYInvoiceForm_FormClosing);
            base.Close();
        }

        private void cmbFhr_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyUtil.SetValue("HYINV-FHR-IDX", this.cmbFhr.Text);
        }

        private void cmbFhr_TextChanged(object sender, EventArgs e)
        {
            string str = this.cmbFhr.Text.Trim();
            this._fpxx.set_Fhr(str);
            if (this._fpxx.get_Fhr() != str)
            {
                this.cmbFhr.Text = this._fpxx.get_Fhr();
                this.cmbFhr.set_SelectionStart(this.cmbFhr.Text.Length);
            }
            this.bill.FHR = this._fpxx.get_Fhr();
        }

        private void cmbFhrsbh_TextChanged(object sender, EventArgs e)
        {
            string str = this.cmbFhrsbh.Text.Trim();
            this._fpxx.set_Fhrsh(str);
            if (this._fpxx.get_Fhrsh() != str)
            {
                this.cmbFhrsbh.Text = this._fpxx.get_Fhrsh();
                this.cmbFhrsbh.set_SelectionStart(this.cmbFhrsbh.Text.Length);
            }
            this.bill.TYDH = this._fpxx.get_Fhrsh();
        }

        private void cmbSender_TextChanged(object sender, EventArgs e)
        {
            string str = this.cmbSender.Text.Trim();
            this._fpxx.set_Fhrmc(str);
            if (this._fpxx.get_Fhrmc() != str)
            {
                this.cmbSender.Text = this._fpxx.get_Fhrmc();
                this.cmbSender.set_SelectionStart(this.cmbSender.Text.Length);
            }
            this.bill.XFDZDH = this._fpxx.get_Fhrmc();
        }

        private void cmbShr_TextChanged(object sender, EventArgs e)
        {
            string str = this.cmbShr.Text.Trim();
            this._fpxx.set_Shrmc(str);
            if (this._fpxx.get_Shrmc() != str)
            {
                this.cmbShr.Text = this._fpxx.get_Shrmc();
                this.cmbShr.set_SelectionStart(this.cmbShr.Text.Length);
            }
            this.bill.GFDZDH = this._fpxx.get_Shrmc();
        }

        private void cmbShrsbh_TextChanged(object sender, EventArgs e)
        {
            string str = this.cmbShrsbh.Text.Trim();
            this._fpxx.set_Shrsh(str);
            if (this._fpxx.get_Shrsh() != str)
            {
                this.cmbShrsbh.Text = this._fpxx.get_Shrsh();
                this.cmbShrsbh.set_SelectionStart(this.cmbShrsbh.Text.Length);
            }
            this.bill.CM = this._fpxx.get_Shrsh();
        }

        private void cmbSkr_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyUtil.SetValue("HYINV-SKR-IDX", this.cmbSkr.Text);
        }

        private void cmbSkr_TextChanged(object sender, EventArgs e)
        {
            string str = this.cmbSkr.Text.Trim();
            this._fpxx.set_Skr(str);
            if (this._fpxx.get_Skr() != str)
            {
                this.cmbSkr.Text = this._fpxx.get_Skr();
                this.cmbSkr.set_SelectionStart(this.cmbSkr.Text.Length);
            }
            this.bill.SKR = this._fpxx.get_Skr();
        }

        private void cmbSlv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbSlv.SelectedItem != null)
            {
                this.curSlv = PresentinvMng.GetSLValue(this.cmbSlv.Text);
                this._fpxx.SetFpSLv(this.curSlv);
                double result = 0.0;
                double.TryParse(this.curSlv, out result);
                this.bill.SLV = result;
                for (int i = 0; i < this._fpxx.GetSpxxs().Count; i++)
                {
                    this.bill.setSlv(i, this.curSlv);
                    if (!this._fpxx.SetSLv(i, this.curSlv))
                    {
                        MessageManager.ShowMsgBox(this._fpxx.GetCode());
                        return;
                    }
                }
                if (this.bt_jg.Checked)
                {
                    this.ShowDataGridMxxx();
                }
                this.SetHzxx();
            }
        }

        private void cmbSpf_TextChanged(object sender, EventArgs e)
        {
            string str = this.cmbSpf.Text.Trim();
            this._fpxx.set_Gfmc(str);
            if (this._fpxx.get_Gfmc() != str)
            {
                this.cmbSpf.Text = this._fpxx.get_Gfmc();
                this.cmbSpf.set_SelectionStart(this.cmbSpf.Text.Length);
            }
            this.bill.GFMC = this._fpxx.get_Gfmc();
        }

        private void cmbSpfsbh_TextChanged(object sender, EventArgs e)
        {
            string str = this.cmbSpfsbh.Text.Trim();
            this._fpxx.set_Gfsh(str);
            if (this._fpxx.get_Gfsh() != str)
            {
                this.cmbSpfsbh.Text = this._fpxx.get_Gfsh();
                this.cmbSpfsbh.set_SelectionStart(this.cmbSpfsbh.Text.Length);
            }
            this.bill.GFSH = this._fpxx.get_Gfsh();
        }

        private void commit_MouseDown(object sender, MouseEventArgs e)
        {
            this.CommitEditGrid();
        }

        private void CommitEditGrid()
        {
            this.dgFyxm.EndEdit();
        }

        private void CreateHYInvoice(bool isRed, string hsjbz, FPLX fplx)
        {
            byte[] sourceArray = Invoice.get_TypeByte();
            byte[] destinationArray = new byte[0x20];
            Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
            byte[] buffer3 = new byte[0x10];
            Array.Copy(sourceArray, 0x20, buffer3, 0, 0x10);
            string s = "DJ" + DateTime.Now.ToString("F");
            byte[] val = AES_Crypt.Encrypt(Encoding.GetEncoding("UNICODE").GetBytes(s), destinationArray, buffer3);
            Invoice.set_IsGfSqdFp_Static(false);
            this._fpxx = new InvoiceBill(isRed, false, hsjbz.Equals("1"), fplx, val);
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CustomStyleDataGrid grid = (CustomStyleDataGrid) sender;
            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;
            object obj2 = grid.Rows[rowIndex].Cells[columnIndex].Value;
            string s = (obj2 == null) ? "" : obj2.ToString();
            bool flag = false;
            switch (columnIndex)
            {
                case 0:
                    flag = this._fpxx.SetSpmc(rowIndex, s);
                    this.bill.ListGoods[rowIndex].SPMC = s;
                    break;

                case 1:
                {
                    flag = this._fpxx.SetJe(rowIndex, s);
                    double result = 0.0;
                    double.TryParse(s, out result);
                    this.bill.setJe(rowIndex, result.ToString());
                    break;
                }
            }
            string sLValue = PresentinvMng.GetSLValue(this.cmbSlv.Text);
            this.bill.setSlv(rowIndex, sLValue);
            if (!flag)
            {
                string code = this._fpxx.GetCode();
                if (this.ckbNegtive.Checked && code.Equals("A105"))
                {
                    code = "A124";
                }
                MessageManager.ShowMsgBox(code, this._fpxx.Params);
            }
            this.ShowDataGrid(this._fpxx.GetSpxx(rowIndex), rowIndex);
        }

        private void dataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
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

        private void dataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            CustomStyleDataGrid grid = (CustomStyleDataGrid) sender;
            AisinoMultiCombox combox = grid.Controls["SPMCBT"] as AisinoMultiCombox;
            if ((grid.CurrentCell != null) && !grid.CurrentRow.ReadOnly)
            {
                DataGridViewColumn owningColumn = grid.CurrentCell.OwningColumn;
                if (owningColumn.Name.Equals("colFyxm"))
                {
                    int index = owningColumn.Index;
                    int rowIndex = grid.CurrentCell.RowIndex;
                    Rectangle rectangle = grid.GetCellDisplayRectangle(index, rowIndex, false);
                    if (combox != null)
                    {
                        combox.Left = rectangle.Left;
                        combox.Top = rectangle.Top;
                        combox.Width = rectangle.Width;
                        combox.Height = rectangle.Height;
                        combox.Text = (grid.CurrentCell.Value == null) ? "" : grid.CurrentCell.Value.ToString();
                        string text = combox.Text;
                        DataTable table = combox.get_DataSource();
                        if (table != null)
                        {
                            table.Clear();
                        }
                        combox.Visible = true;
                        combox.Focus();
                    }
                }
                else if (combox != null)
                {
                    combox.Visible = false;
                }
            }
            else if (((grid.CurrentRow != null) && grid.CurrentRow.ReadOnly) && (combox != null))
            {
                combox.Visible = false;
            }
        }

        private void dataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            this.EditingControl = (DataGridViewTextBoxEditingControl) e.Control;
            this.EditingControl.KeyPress += new KeyPressEventHandler(this.EditingControl_KeyPress);
        }

        private void dataGridView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            CustomStyleDataGrid grid = sender as CustomStyleDataGrid;
            if ((grid != null) && (grid.CurrentRow != null))
            {
                int index = grid.CurrentRow.Index;
                int columnIndex = grid.CurrentCell.ColumnIndex;
                int keyValue = e.KeyValue;
                int count = grid.Rows.Count;
                int num5 = grid.Columns.Count;
                if ((keyValue == 40) && !(((index != (count - 1)) || this.onlyShow) || grid.ReadOnly))
                {
                    this.AddSpxx(-1);
                }
                if (((keyValue == 9) && ((((index == (count - 1)) && (columnIndex == (num5 - 1))) && !this.onlyShow) && !grid.ReadOnly)) && this.AddSpxx(-1))
                {
                    grid.CurrentCell = grid.Rows[count].Cells[0];
                }
                if (keyValue == 13)
                {
                    if (columnIndex < (num5 - 1))
                    {
                        grid.CurrentCell = grid.Rows[index].Cells[columnIndex + 1];
                    }
                    else if (((index == (count - 1)) && !this.onlyShow) && !grid.ReadOnly)
                    {
                        if (this.AddSpxx(-1))
                        {
                            grid.CurrentCell = grid.Rows[count].Cells[0];
                        }
                    }
                    else if (index < (count - 1))
                    {
                        grid.CurrentCell = grid.Rows[index + 1].Cells[0];
                    }
                    else
                    {
                        SendKeys.Send("{TAB}");
                    }
                }
                if (!((keyValue != 0x2e) || grid.ReadOnly))
                {
                    this.delRow_Click(null, null);
                }
            }
        }

        private void dataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CustomStyleDataGrid grid = (CustomStyleDataGrid) sender;
            this.tool_addrow.Enabled = true;
            this.tool_delrow.Enabled = true;
        }

        private void dataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (this.dgFyxm.Rows.Count == 0)
            {
                this.tool_delrow.Enabled = false;
            }
        }

        private void delRow_Click(object sender, EventArgs e)
        {
            if (this.dgFyxm.CurrentCell != null)
            {
                if (this._spmcBt.Visible)
                {
                    this._spmcBt.Visible = false;
                }
                int rowIndex = this.dgFyxm.CurrentCell.RowIndex;
                Dictionary<SPXX, string> spxx = this._fpxx.GetSpxx(rowIndex);
                if (this._fpxx.DelSpxx(rowIndex))
                {
                    this.dgFyxm.Rows.RemoveAt(rowIndex);
                    this.bill.ListGoods.RemoveAt(rowIndex);
                }
                else
                {
                    MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
                }
                this.SetHzxx();
            }
        }

        private void DelTextChangedEvent()
        {
            this.cmbSpf.OnTextChanged = (EventHandler) Delegate.Remove(this.cmbSpf.OnTextChanged, new EventHandler(this.cmbSpf_TextChanged));
            this.cmbSpfsbh.OnTextChanged = (EventHandler) Delegate.Remove(this.cmbSpfsbh.OnTextChanged, new EventHandler(this.cmbSpfsbh_TextChanged));
            this.cmbShr.OnTextChanged = (EventHandler) Delegate.Remove(this.cmbShr.OnTextChanged, new EventHandler(this.cmbShr_TextChanged));
            this.cmbShrsbh.OnTextChanged = (EventHandler) Delegate.Remove(this.cmbShrsbh.OnTextChanged, new EventHandler(this.cmbShrsbh_TextChanged));
            this.cmbSender.OnTextChanged = (EventHandler) Delegate.Remove(this.cmbSender.OnTextChanged, new EventHandler(this.cmbSender_TextChanged));
            this.cmbFhrsbh.OnTextChanged = (EventHandler) Delegate.Remove(this.cmbFhrsbh.OnTextChanged, new EventHandler(this.cmbFhrsbh_TextChanged));
            this.cmbSkr.OnTextChanged = (EventHandler) Delegate.Remove(this.cmbSkr.OnTextChanged, new EventHandler(this.cmbSkr_TextChanged));
            this.cmbFhr.OnTextChanged = (EventHandler) Delegate.Remove(this.cmbFhr.OnTextChanged, new EventHandler(this.cmbFhr_TextChanged));
            this.txtQyd.TextChanged -= new EventHandler(this.txtQyd_TextChanged);
            this.txtYshw.TextChanged -= new EventHandler(this.txtYshw_TextChanged);
            this.txtBz.TextChanged -= new EventHandler(this.txtBz_TextChanged);
            this.txtCzch.TextChanged -= new EventHandler(this.txtCzch_TextChanged);
            this.txtCcdw.TextChanged -= new EventHandler(this.txtCcdw_TextChanged);
        }

        private void dgFyxm_CSDGridColumnWidthChanged(DataGridViewColumnEventArgs e)
        {
            DataGridViewColumn column = e.Column;
            AisinoMultiCombox combox = this.dgFyxm.Controls["SPMCBT"] as AisinoMultiCombox;
            if ((column != null) && column.Name.Equals("colFyxm"))
            {
                int index = column.Index;
                int rowIndex = this.dgFyxm.CurrentCell.RowIndex;
                Rectangle rectangle = this.dgFyxm.GetCellDisplayRectangle(index, rowIndex, false);
                if (combox != null)
                {
                    combox.Left = rectangle.Left;
                    combox.Top = rectangle.Top;
                    combox.Width = rectangle.Width;
                    combox.Height = rectangle.Height;
                }
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

        private void EditingControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            AisinoMultiCombox parent = ((DataGridViewTextBoxEditingControl) sender).Parent as AisinoMultiCombox;
            if (parent != null)
            {
                CustomStyleDataGrid grid = parent.Parent as CustomStyleDataGrid;
                if ((grid != null) && grid.CurrentCell.OwningColumn.Name.Equals("colJe"))
                {
                    DataGridViewTextBoxEditingControl control = (DataGridViewTextBoxEditingControl) sender;
                    if (e.KeyChar.ToString() == "\b")
                    {
                        e.Handled = false;
                    }
                    else if (e.KeyChar.ToString() == ".")
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
                }
            }
        }

        private void FormMain_UpdateUserNameEvent(string yhmc)
        {
        }

        private double GetDoubleSLVByStringSLV(string strSlv)
        {
            double num = -1.0;
            if (strSlv != "")
            {
                if (strSlv.EndsWith("%"))
                {
                    return (Convert.ToDouble(strSlv.Replace("%", "")) / 100.0);
                }
                string str = strSlv;
                num = Convert.ToDouble(str);
                if (num > 100.0)
                {
                    num /= 100.0;
                }
            }
            return num;
        }

        private AisinoMultiCombox GetFocusedCmb()
        {
            AisinoMultiCombox cmbFhrsbh = null;
            if (this.cmbSpf.Focused)
            {
                return this.cmbSpf;
            }
            if (this.cmbSpfsbh.Focused)
            {
                return this.cmbSpfsbh;
            }
            if (this.cmbShr.Focused)
            {
                return this.cmbShr;
            }
            if (this.cmbShrsbh.Focused)
            {
                return this.cmbShrsbh;
            }
            if (this.cmbSender.Focused)
            {
                return this.cmbSender;
            }
            if (this.cmbFhrsbh.Focused)
            {
                cmbFhrsbh = this.cmbFhrsbh;
            }
            return cmbFhrsbh;
        }

        private double GetHJSE(SaleBill bill)
        {
            double num = 0.0;
            foreach (Goods goods in bill.ListGoods)
            {
                num += goods.SE;
            }
            return num;
        }

        private void GetQyLable()
        {
            this.lblFpdm.Text = this._fpxx.get_Fpdm();
            this.lblFphm.Text = this._fpxx.get_Fphm();
            this.lblKprq.Text = this._fpxx.get_Kprq();
            this.lblJqbh.Text = this._fpxx.get_Jqbh();
            this.lblCyr.Text = this._fpxx.get_Xfmc();
            this.lblCyrsbh.Text = this._fpxx.get_Xfsh();
            this.lblSwjgdm.Text = this._fpxx.get_Zgswjg_dm();
            this.lblSwjg.Text = this._fpxx.get_Zgswjg_mc();
            this.lblKpr.Text = this._fpxx.get_Kpr();
        }

        private void GetShrCmb()
        {
            this.cmbSpf.Text = this._fpxx.get_Gfmc();
            this.cmbSpfsbh.Text = this._fpxx.get_Gfsh();
            this.cmbShr.Text = this._fpxx.get_Shrmc();
            this.cmbShrsbh.Text = this._fpxx.get_Shrsh();
            this.cmbSender.Text = this._fpxx.get_Fhrmc();
            this.cmbFhrsbh.Text = this._fpxx.get_Fhrsh();
        }

        private void GetYsText()
        {
            this.txtQyd.Text = this._fpxx.get_Qyd_jy_ddd();
            this.txtYshw.Text = this._fpxx.get_Yshwxx();
            this.txtBz.Text = this._fpxx.get_Bz();
            this.txtCzch.Text = this._fpxx.get_Czch();
            this.txtCcdw.Text = this._fpxx.get_Ccdw();
        }

        private DataTable GfxxOnAutoCompleteDataSource(string str)
        {
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSFHRMore", new object[] { str, 20, "MC,SH" });
            if ((objArray != null) && (objArray.Length > 0))
            {
                return (objArray[0] as DataTable);
            }
            return null;
        }

        private void hsjbzButton_Click(object sender, EventArgs e)
        {
            this.dgFyxm.EndEdit();
            double num = this.ckbNegtive.Checked ? ((double) (-1)) : ((double) 1);
            int count = this.dgFyxm.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                string str = (this.dgFyxm.Rows[i].Cells[0].Value == null) ? "" : this.dgFyxm.Rows[i].Cells[0].Value.ToString();
                string number = (this.dgFyxm.Rows[i].Cells[1].Value == null) ? "" : this.dgFyxm.Rows[i].Cells[1].Value.ToString();
                if (i < this.bill.ListGoods.Count)
                {
                    Goods goods = this.bill.ListGoods[i];
                    goods.XSDJBH = this.bill.BH;
                    goods.XH = i + 1;
                    goods.SPMC = str;
                    double num4 = (number == "") ? 0.0 : CommonTool.Todouble(number);
                    if (this.bill.ContainTax)
                    {
                        num4 = Finacial.Div(num4, 1.0 + this.bill.SLV, 15);
                    }
                    goods.JE = Finacial.GetRound(num4, 2);
                    goods.JE = Finacial.Mul(goods.JE, num, 15);
                    goods.SE = Finacial.GetRound((double) (goods.JE * this.bill.SLV), 2);
                }
            }
            this.SetHsjbz(this.bt_jg.Checked);
        }

        private void HYInvoiceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormMain.remove_UpdateUserNameEvent(new FormMain.UpdateUserNameDelegate(this, (IntPtr) this.FormMain_UpdateUserNameEvent));
        }

        private void HYInvoiceForm_Init(FPLX fplx, string DJBH, string AddEditFlag)
        {
            if (AddEditFlag == "xg")
            {
                this.bill = this.saleBillBL.Find(DJBH);
                this.bill.IsANew = false;
                if ((this.bill.DJZT == "W") || (this.bill.KPZT != "N"))
                {
                    this.onlyShow = true;
                }
            }
            else
            {
                this.bill = new SaleBill();
            }
            this.InitializeDefault();
            if (base.TaxCardInstance.get_QYLX().ISTDQY)
            {
                this.tool_drwl.Visible = false;
            }
            else
            {
                this.tool_drwl.Visible = true;
            }
            string hsjbz = PropertyUtil.GetValue("HYINV-HSJBZ", "0");
            this.CreateHYInvoice(false, hsjbz, fplx);
            string zGJGDMMC = this.taxCard.get_SQInfo().ZGJGDMMC;
            if (!string.IsNullOrEmpty(zGJGDMMC))
            {
                string[] strArray = zGJGDMMC.Split(new char[] { ',' });
                if (strArray.Length == 2)
                {
                    this._fpxx.set_Zgswjg_dm(strArray[0]);
                    this._fpxx.set_Zgswjg_mc(strArray[1]);
                }
            }
            this._fpxx.set_Xfmc(this.taxCard.get_Corporation());
            this._fpxx.set_Xfsh(this.taxCard.get_TaxCode());
            this._fpxx.set_Fpdm("销售单据");
            this._fpxx.set_Fphm("销售单据");
            this._fpxx.set_Kpr(UserInfo.get_Yhmc());
            this._fpxx.set_Jqbh(this.taxCard.GetInvControlNum());
            this.ResetCmbSlv();
            if (this.cmbSlv.Items.Count == 0)
            {
                this.initSuccess = false;
                MessageManager.ShowMsgBox("INP-242129", new string[] { "货物运输业增值税专用发票" });
            }
            else
            {
                this.curSlv = PresentinvMng.GetSLValue(this.cmbSlv.Text);
                if (AddEditFlag != "xg")
                {
                    Spxx spxx = new Spxx("", "", this.curSlv);
                    int num = this._fpxx.AddSpxx(spxx);
                    if (num < 0)
                    {
                        this.initSuccess = false;
                        MessageManager.ShowMsgBox(this._fpxx.GetCode());
                        return;
                    }
                    this._fpxx.DelSpxx(num);
                }
                this.ShowInvMainInfo();
                this.SetFormTitle(fplx, DJBH);
                if (this.onlyShow)
                {
                    this.tool_addrow.Visible = false;
                    this.tool_delrow.Visible = false;
                    this.toolStripBtnSave.Visible = false;
                    this.SetCmbEnabled(false);
                    this.SetTxtEnabled(false);
                    this.cmbSlv.FlatStyle = FlatStyle.Flat;
                    this.cmbSlv.Enabled = false;
                    this.cmbFhr.set_Edit(0);
                    this.cmbSkr.set_Edit(0);
                    this.dgFyxm.ReadOnly = true;
                    this.dgFyxm.set_GridStyle(1);
                    this.dateTimePicker1.Enabled = false;
                }
                if (AddEditFlag == "xg")
                {
                    this.Text = "货物运输发票单据修改";
                    this.txtDJH.ReadOnly = true;
                    this.txtDJH.BorderStyle = BorderStyle.FixedSingle;
                    this.ToView();
                }
                else
                {
                    this.Text = "货物运输发票单据添加";
                    this.bill = new SaleBill();
                    this.bill.DJRQ = TaxCardValue.taxCard.GetCardClock();
                    this.dateTimePicker1.Value = TaxCardValue.taxCard.GetCardClock();
                    this.ToView();
                    this.ResetCmbSlv();
                    string sLValue = PresentinvMng.GetSLValue(this.cmbSlv.Text.Trim());
                    this.bill.SLV = CommonTool.Todouble(sLValue);
                }
            }
        }

        private void HYInvoiceForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if ((((this.cmbSpf.Focused || this.cmbSpfsbh.Focused) || (this.cmbShr.Focused || this.cmbShrsbh.Focused)) || this.cmbSender.Focused) || this.cmbFhrsbh.Focused)
                    {
                        AisinoMultiCombox focusedCmb = this.GetFocusedCmb();
                        object[] khxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSFHR", new object[] { focusedCmb.Text, 1, "MC,SH" });
                        this.ShfxxSetValue(sender, khxx);
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void HYInvoiceForm_Resize(object sender, EventArgs e)
        {
            if (this.panel1 != null)
            {
                this.panel1.Location = new Point((base.Width - this.panel1.Width) / 2, this.panel1.Location.Y);
            }
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            DataGridViewCellStyle style4 = new DataGridViewCellStyle();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(HYFPtiankai_new));
            this.tool_zjkj = new ToolStripMenuItem();
            this.tool_drxxb = new ToolStripMenuItem();
            this.tool_drwl = new ToolStripMenuItem();
            this.panel2 = new AisinoPNL();
            this.panel1 = new AisinoPNL();
            this.ckbNegtive = new CheckBox();
            this.txtDJH = new AisinoTXT();
            this.aisinoLBL1 = new AisinoLBL();
            this.aisinoLBL2 = new AisinoLBL();
            this.dateTimePicker1 = new DateTimePicker();
            this.aisinoLBL5 = new AisinoLBL();
            this.cmbSkr = new AisinoMultiCombox();
            this.cmbFhr = new AisinoMultiCombox();
            this.lblSwjgdm = new AisinoLBL();
            this.lblSwjg = new AisinoLBL();
            this.txtCcdw = new AisinoTXT();
            this.txtCzch = new AisinoTXT();
            this.lblJgdx = new AisinoLBL();
            this.cmbSlv = new AisinoCMB();
            this.lblHjje = new AisinoLBL();
            this.cmbShrsbh = new AisinoMultiCombox();
            this.cmbShr = new AisinoMultiCombox();
            this.cmbSpfsbh = new AisinoMultiCombox();
            this.cmbSpf = new AisinoMultiCombox();
            this.lblCyrsbh = new AisinoLBL();
            this.lblCyr = new AisinoLBL();
            this.lblKpr = new AisinoLBL();
            this.txtBz = new AisinoTXT();
            this.lblJgxx = new AisinoLBL();
            this.lblHjse = new AisinoLBL();
            this.lblJqbh = new AisinoLBL();
            this.txtYshw = new AisinoTXT();
            this.txtQyd = new AisinoTXT();
            this.cmbFhrsbh = new AisinoMultiCombox();
            this.cmbSender = new AisinoMultiCombox();
            this.lblKprq = new AisinoLBL();
            this.lblFphm = new AisinoLBL();
            this.lblFpdm = new AisinoLBL();
            this.lblTitle = new AisinoLBL();
            this.dgFyxm = new CustomStyleDataGrid();
            this.tool_close = new ToolStripButton();
            this.toolStripBtnSave = new ToolStripButton();
            this.tool_addrow = new ToolStripButton();
            this.tool_delrow = new ToolStripButton();
            this.toolStrip3 = new ToolStrip();
            this.bt_jg = new ToolStripButton();
            this.colFyxm = new DataGridViewTextBoxColumn();
            this.colJe = new DataGridViewTextBoxColumn();
            ToolStripDropDownButton button = new ToolStripDropDownButton();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.dgFyxm.BeginInit();
            this.toolStrip3.SuspendLayout();
            base.SuspendLayout();
            button.Alignment = ToolStripItemAlignment.Right;
            button.DisplayStyle = ToolStripItemDisplayStyle.None;
            button.DropDownItems.AddRange(new ToolStripItem[] { this.tool_zjkj, this.tool_drxxb, this.tool_drwl });
            button.ImageTransparentColor = Color.Magenta;
            button.Name = "tool_fushu1";
            button.ShowDropDownArrow = false;
            button.Size = new Size(4, 0x16);
            button.Text = "红字";
            button.TextDirection = ToolStripTextDirection.Horizontal;
            button.ToolTipText = "开具红字发票";
            this.tool_zjkj.Name = "tool_zjkj";
            this.tool_zjkj.Size = new Size(0xe8, 0x16);
            this.tool_zjkj.Text = "直接开具";
            this.tool_drxxb.Name = "tool_drxxb";
            this.tool_drxxb.Size = new Size(0xe8, 0x16);
            this.tool_drxxb.Text = "导入红字发票信息表";
            this.tool_drwl.Name = "tool_drwl";
            this.tool_drwl.Size = new Size(0xe8, 0x16);
            this.tool_drwl.Text = "导入网络下载红字发票信息表";
            this.panel2.AutoScroll = true;
            this.panel2.AutoScrollMinSize = new Size(0x3c4, 710);
            this.panel2.BackColor = Color.FromArgb(180, 0xbb, 0xc2);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 0x19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x3c4, 0x2c4);
            this.panel2.TabIndex = 3;
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = Color.White;
            this.panel1.BackgroundImage = Resources.HY;
            this.panel1.BackgroundImageLayout = ImageLayout.Zoom;
            this.panel1.Controls.Add(this.ckbNegtive);
            this.panel1.Controls.Add(this.txtDJH);
            this.panel1.Controls.Add(this.aisinoLBL1);
            this.panel1.Controls.Add(this.aisinoLBL2);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.aisinoLBL5);
            this.panel1.Controls.Add(this.cmbSkr);
            this.panel1.Controls.Add(this.cmbFhr);
            this.panel1.Controls.Add(this.lblSwjgdm);
            this.panel1.Controls.Add(this.lblSwjg);
            this.panel1.Controls.Add(this.txtCcdw);
            this.panel1.Controls.Add(this.txtCzch);
            this.panel1.Controls.Add(this.lblJgdx);
            this.panel1.Controls.Add(this.cmbSlv);
            this.panel1.Controls.Add(this.lblHjje);
            this.panel1.Controls.Add(this.cmbShrsbh);
            this.panel1.Controls.Add(this.cmbShr);
            this.panel1.Controls.Add(this.cmbSpfsbh);
            this.panel1.Controls.Add(this.cmbSpf);
            this.panel1.Controls.Add(this.lblCyrsbh);
            this.panel1.Controls.Add(this.lblCyr);
            this.panel1.Controls.Add(this.lblKpr);
            this.panel1.Controls.Add(this.txtBz);
            this.panel1.Controls.Add(this.lblJgxx);
            this.panel1.Controls.Add(this.lblHjse);
            this.panel1.Controls.Add(this.lblJqbh);
            this.panel1.Controls.Add(this.txtYshw);
            this.panel1.Controls.Add(this.txtQyd);
            this.panel1.Controls.Add(this.cmbFhrsbh);
            this.panel1.Controls.Add(this.cmbSender);
            this.panel1.Controls.Add(this.lblKprq);
            this.panel1.Controls.Add(this.lblFphm);
            this.panel1.Controls.Add(this.lblFpdm);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.dgFyxm);
            this.panel1.Location = new Point(9, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x3b1, 0x2b3);
            this.panel1.TabIndex = 0;
            this.ckbNegtive.AutoSize = true;
            this.ckbNegtive.Location = new Point(0x1e8, 0x79);
            this.ckbNegtive.Name = "ckbNegtive";
            this.ckbNegtive.Size = new Size(0x48, 0x10);
            this.ckbNegtive.TabIndex = 0x4e;
            this.ckbNegtive.Text = "负数单据";
            this.ckbNegtive.UseVisualStyleBackColor = true;
            this.txtDJH.BackColor = Color.White;
            this.txtDJH.ForeColor = Color.Black;
            this.txtDJH.Location = new Point(0xae, 0x53);
            this.txtDJH.Name = "txtDJH";
            this.txtDJH.Size = new Size(0xc9, 0x15);
            this.txtDJH.TabIndex = 0x4d;
            this.aisinoLBL1.AutoSize = true;
            this.aisinoLBL1.BackColor = Color.White;
            this.aisinoLBL1.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.aisinoLBL1.ForeColor = Color.Black;
            this.aisinoLBL1.Location = new Point(0x56, 0x53);
            this.aisinoLBL1.Name = "aisinoLBL1";
            this.aisinoLBL1.Size = new Size(0x3a, 0x15);
            this.aisinoLBL1.TabIndex = 0x4c;
            this.aisinoLBL1.Text = "单据号";
            this.aisinoLBL2.AutoSize = true;
            this.aisinoLBL2.BackColor = Color.White;
            this.aisinoLBL2.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.aisinoLBL2.ForeColor = Color.Black;
            this.aisinoLBL2.Location = new Point(0x290, 0x4f);
            this.aisinoLBL2.Name = "aisinoLBL2";
            this.aisinoLBL2.Size = new Size(0x4a, 0x15);
            this.aisinoLBL2.TabIndex = 0x4b;
            this.aisinoLBL2.Text = "单据日期";
            this.dateTimePicker1.Location = new Point(0x2df, 0x4f);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new Size(0x7b, 0x15);
            this.dateTimePicker1.TabIndex = 0x4a;
            this.aisinoLBL5.BackColor = Color.White;
            this.aisinoLBL5.Font = new Font("微软雅黑", 20f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.aisinoLBL5.ForeColor = Color.Black;
            this.aisinoLBL5.Location = new Point(0x123, 11);
            this.aisinoLBL5.Name = "aisinoLBL5";
            this.aisinoLBL5.Size = new Size(0x156, 90);
            this.aisinoLBL5.TabIndex = 0x49;
            this.aisinoLBL5.Text = "货物运输业增值税专用发票销售单据";
            this.aisinoLBL5.TextAlign = ContentAlignment.TopCenter;
            this.cmbSkr.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.cmbSkr.set_AutoComplate(0);
            this.cmbSkr.set_AutoIndex(1);
            this.cmbSkr.BackColor = Color.White;
            this.cmbSkr.set_BorderColor(Color.White);
            this.cmbSkr.set_BorderStyle(1);
            this.cmbSkr.set_ButtonAutoHide(true);
            this.cmbSkr.set_buttonStyle(1);
            this.cmbSkr.set_DataSource(null);
            this.cmbSkr.set_DrawHead(true);
            this.cmbSkr.set_Edit(1);
            this.cmbSkr.ForeColor = Color.Black;
            this.cmbSkr.set_IsSelectAll(false);
            this.cmbSkr.Location = new Point(0x93, 0x27c);
            this.cmbSkr.set_MaxIndex(8);
            this.cmbSkr.set_MaxLength(0x7fff);
            this.cmbSkr.Name = "cmbSkr";
            this.cmbSkr.set_ReadOnly(false);
            this.cmbSkr.set_SelectedIndex(-1);
            this.cmbSkr.set_SelectionStart(0);
            this.cmbSkr.set_ShowText("");
            this.cmbSkr.Size = new Size(160, 0x15);
            this.cmbSkr.TabIndex = 0x19;
            this.cmbSkr.set_UnderLineColor(Color.Transparent);
            this.cmbSkr.set_UnderLineStyle(0);
            this.cmbFhr.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.cmbFhr.set_AutoComplate(0);
            this.cmbFhr.set_AutoIndex(1);
            this.cmbFhr.BackColor = Color.White;
            this.cmbFhr.set_BorderColor(Color.White);
            this.cmbFhr.set_BorderStyle(1);
            this.cmbFhr.set_ButtonAutoHide(true);
            this.cmbFhr.set_buttonStyle(1);
            this.cmbFhr.set_DataSource(null);
            this.cmbFhr.set_DrawHead(true);
            this.cmbFhr.set_Edit(1);
            this.cmbFhr.ForeColor = Color.Black;
            this.cmbFhr.set_IsSelectAll(false);
            this.cmbFhr.Location = new Point(0x171, 0x27c);
            this.cmbFhr.set_MaxIndex(8);
            this.cmbFhr.set_MaxLength(0x7fff);
            this.cmbFhr.Name = "cmbFhr";
            this.cmbFhr.set_ReadOnly(false);
            this.cmbFhr.set_SelectedIndex(-1);
            this.cmbFhr.set_SelectionStart(0);
            this.cmbFhr.set_ShowText("");
            this.cmbFhr.Size = new Size(0x70, 0x15);
            this.cmbFhr.TabIndex = 0x1a;
            this.cmbFhr.set_UnderLineColor(Color.Transparent);
            this.cmbFhr.set_UnderLineStyle(0);
            this.lblSwjgdm.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.lblSwjgdm.AutoSize = true;
            this.lblSwjgdm.BackColor = Color.White;
            this.lblSwjgdm.ForeColor = Color.Black;
            this.lblSwjgdm.Location = new Point(0xaf, 610);
            this.lblSwjgdm.Name = "lblSwjgdm";
            this.lblSwjgdm.Size = new Size(0x3b, 12);
            this.lblSwjgdm.TabIndex = 0x17;
            this.lblSwjgdm.Text = "111010100";
            this.lblSwjg.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.lblSwjg.BackColor = Color.White;
            this.lblSwjg.ForeColor = Color.Black;
            this.lblSwjg.Location = new Point(0xab, 570);
            this.lblSwjg.Name = "lblSwjg";
            this.lblSwjg.Size = new Size(0x119, 0x26);
            this.lblSwjg.TabIndex = 0x16;
            this.lblSwjg.Text = "北京市东城区";
            this.lblSwjg.TextAlign = ContentAlignment.MiddleLeft;
            this.txtCcdw.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.txtCcdw.BackColor = Color.White;
            this.txtCcdw.ForeColor = Color.Black;
            this.txtCcdw.Location = new Point(0x18e, 0x21e);
            this.txtCcdw.Name = "txtCcdw";
            this.txtCcdw.Size = new Size(0x36, 0x15);
            this.txtCcdw.TabIndex = 0x15;
            this.txtCzch.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.txtCzch.BackColor = Color.White;
            this.txtCzch.ForeColor = Color.Black;
            this.txtCzch.Location = new Point(0x8d, 0x21e);
            this.txtCzch.Name = "txtCzch";
            this.txtCzch.Size = new Size(0xbc, 0x15);
            this.txtCzch.TabIndex = 20;
            this.lblJgdx.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.lblJgdx.AutoSize = true;
            this.lblJgdx.BackColor = Color.White;
            this.lblJgdx.ForeColor = Color.Black;
            this.lblJgdx.Location = new Point(0xaf, 0x202);
            this.lblJgdx.Name = "lblJgdx";
            this.lblJgdx.Size = new Size(0x11, 12);
            this.lblJgdx.TabIndex = 0x12;
            this.lblJgdx.Text = "零";
            this.cmbSlv.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.cmbSlv.BackColor = Color.White;
            this.cmbSlv.ForeColor = Color.Black;
            this.cmbSlv.FormattingEnabled = true;
            this.cmbSlv.Items.AddRange(new object[] { "0.05" });
            this.cmbSlv.Location = new Point(370, 0x1df);
            this.cmbSlv.Name = "cmbSlv";
            this.cmbSlv.Size = new Size(0x2f, 20);
            this.cmbSlv.TabIndex = 15;
            this.lblHjje.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.lblHjje.BackColor = Color.White;
            this.lblHjje.Font = new Font("宋体", 11.25f);
            this.lblHjje.ForeColor = Color.Black;
            this.lblHjje.Location = new Point(0x8a, 0x1d7);
            this.lblHjje.Name = "lblHjje";
            this.lblHjje.Size = new Size(0xbf, 30);
            this.lblHjje.TabIndex = 14;
            this.lblHjje.Text = "￥0.00";
            this.lblHjje.TextAlign = ContentAlignment.MiddleLeft;
            this.cmbShrsbh.set_AutoComplate(0);
            this.cmbShrsbh.set_AutoIndex(1);
            this.cmbShrsbh.BackColor = Color.White;
            this.cmbShrsbh.set_BorderColor(Color.White);
            this.cmbShrsbh.set_BorderStyle(1);
            this.cmbShrsbh.set_ButtonAutoHide(true);
            this.cmbShrsbh.set_buttonStyle(1);
            this.cmbShrsbh.set_DataSource(null);
            this.cmbShrsbh.set_DrawHead(true);
            this.cmbShrsbh.set_Edit(1);
            this.cmbShrsbh.ForeColor = Color.Black;
            this.cmbShrsbh.set_IsSelectAll(false);
            this.cmbShrsbh.Location = new Point(0xad, 0x103);
            this.cmbShrsbh.set_MaxIndex(8);
            this.cmbShrsbh.set_MaxLength(0x7fff);
            this.cmbShrsbh.Name = "cmbShrsbh";
            this.cmbShrsbh.set_ReadOnly(false);
            this.cmbShrsbh.set_SelectedIndex(-1);
            this.cmbShrsbh.set_SelectionStart(0);
            this.cmbShrsbh.set_ShowText("");
            this.cmbShrsbh.Size = new Size(0x111, 0x15);
            this.cmbShrsbh.TabIndex = 8;
            this.cmbShrsbh.set_UnderLineColor(Color.Transparent);
            this.cmbShrsbh.set_UnderLineStyle(0);
            this.cmbShr.set_AutoComplate(0);
            this.cmbShr.set_AutoIndex(1);
            this.cmbShr.BackColor = Color.White;
            this.cmbShr.set_BorderColor(Color.White);
            this.cmbShr.set_BorderStyle(1);
            this.cmbShr.set_ButtonAutoHide(true);
            this.cmbShr.set_buttonStyle(1);
            this.cmbShr.set_DataSource(null);
            this.cmbShr.set_DrawHead(true);
            this.cmbShr.set_Edit(1);
            this.cmbShr.ForeColor = Color.Black;
            this.cmbShr.set_IsSelectAll(false);
            this.cmbShr.Location = new Point(0xad, 0xeb);
            this.cmbShr.set_MaxIndex(8);
            this.cmbShr.set_MaxLength(0x7fff);
            this.cmbShr.Name = "cmbShr";
            this.cmbShr.set_ReadOnly(false);
            this.cmbShr.set_SelectedIndex(-1);
            this.cmbShr.set_SelectionStart(0);
            this.cmbShr.set_ShowText("");
            this.cmbShr.Size = new Size(0x111, 0x15);
            this.cmbShr.TabIndex = 7;
            this.cmbShr.set_UnderLineColor(Color.Transparent);
            this.cmbShr.set_UnderLineStyle(0);
            this.cmbSpfsbh.set_AutoComplate(0);
            this.cmbSpfsbh.set_AutoIndex(1);
            this.cmbSpfsbh.BackColor = Color.White;
            this.cmbSpfsbh.set_BorderColor(Color.White);
            this.cmbSpfsbh.set_BorderStyle(1);
            this.cmbSpfsbh.set_ButtonAutoHide(true);
            this.cmbSpfsbh.set_buttonStyle(1);
            this.cmbSpfsbh.set_DataSource(null);
            this.cmbSpfsbh.set_DrawHead(true);
            this.cmbSpfsbh.set_Edit(1);
            this.cmbSpfsbh.ForeColor = Color.Black;
            this.cmbSpfsbh.set_IsSelectAll(false);
            this.cmbSpfsbh.Location = new Point(0xad, 0xc6);
            this.cmbSpfsbh.set_MaxIndex(8);
            this.cmbSpfsbh.set_MaxLength(0x7fff);
            this.cmbSpfsbh.Name = "cmbSpfsbh";
            this.cmbSpfsbh.set_ReadOnly(false);
            this.cmbSpfsbh.set_SelectedIndex(-1);
            this.cmbSpfsbh.set_SelectionStart(0);
            this.cmbSpfsbh.set_ShowText("");
            this.cmbSpfsbh.Size = new Size(0x111, 0x15);
            this.cmbSpfsbh.TabIndex = 6;
            this.cmbSpfsbh.set_UnderLineColor(Color.Transparent);
            this.cmbSpfsbh.set_UnderLineStyle(0);
            this.cmbSpf.set_AutoComplate(0);
            this.cmbSpf.set_AutoIndex(1);
            this.cmbSpf.BackColor = Color.White;
            this.cmbSpf.set_BorderColor(SystemColors.Control);
            this.cmbSpf.set_BorderStyle(1);
            this.cmbSpf.set_ButtonAutoHide(true);
            this.cmbSpf.set_buttonStyle(1);
            this.cmbSpf.set_DataSource(null);
            this.cmbSpf.set_DrawHead(true);
            this.cmbSpf.set_Edit(1);
            this.cmbSpf.ForeColor = Color.Black;
            this.cmbSpf.set_IsSelectAll(false);
            this.cmbSpf.Location = new Point(0xad, 0xae);
            this.cmbSpf.set_MaxIndex(8);
            this.cmbSpf.set_MaxLength(0x7fff);
            this.cmbSpf.Name = "cmbSpf";
            this.cmbSpf.set_ReadOnly(false);
            this.cmbSpf.set_SelectedIndex(-1);
            this.cmbSpf.set_SelectionStart(0);
            this.cmbSpf.set_ShowText("");
            this.cmbSpf.Size = new Size(0x111, 0x15);
            this.cmbSpf.TabIndex = 5;
            this.cmbSpf.set_UnderLineColor(Color.Transparent);
            this.cmbSpf.set_UnderLineStyle(0);
            this.lblCyrsbh.BackColor = Color.White;
            this.lblCyrsbh.ForeColor = Color.Black;
            this.lblCyrsbh.Location = new Point(0xab, 0x95);
            this.lblCyrsbh.Name = "lblCyrsbh";
            this.lblCyrsbh.Size = new Size(0x113, 12);
            this.lblCyrsbh.TabIndex = 4;
            this.lblCyrsbh.Text = "140301201406000";
            this.lblCyr.BackColor = Color.White;
            this.lblCyr.ForeColor = Color.Black;
            this.lblCyr.Location = new Point(0xab, 0x74);
            this.lblCyr.Name = "lblCyr";
            this.lblCyr.RightToLeft = RightToLeft.No;
            this.lblCyr.Size = new Size(0x113, 0x19);
            this.lblCyr.TabIndex = 3;
            this.lblCyr.TextAlign = ContentAlignment.MiddleLeft;
            this.lblKpr.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.lblKpr.BackColor = Color.White;
            this.lblKpr.Font = new Font("宋体", 11.25f);
            this.lblKpr.ForeColor = Color.Black;
            this.lblKpr.Location = new Point(0x21d, 0x27f);
            this.lblKpr.Name = "lblKpr";
            this.lblKpr.Size = new Size(170, 15);
            this.lblKpr.TabIndex = 0x1b;
            this.lblKpr.Text = "管理员1";
            this.txtBz.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.txtBz.BackColor = Color.White;
            this.txtBz.BorderStyle = BorderStyle.None;
            this.txtBz.ForeColor = Color.Black;
            this.txtBz.Location = new Point(0x1de, 0x219);
            this.txtBz.Multiline = true;
            this.txtBz.Name = "txtBz";
            this.txtBz.Size = new Size(380, 0x58);
            this.txtBz.TabIndex = 0x18;
            this.lblJgxx.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.lblJgxx.AutoSize = true;
            this.lblJgxx.BackColor = Color.White;
            this.lblJgxx.Font = new Font("宋体", 11.25f);
            this.lblJgxx.ForeColor = Color.Black;
            this.lblJgxx.Location = new Point(0x285, 0x200);
            this.lblJgxx.Name = "lblJgxx";
            this.lblJgxx.Size = new Size(0x36, 15);
            this.lblJgxx.TabIndex = 0x13;
            this.lblJgxx.Text = "￥0.00";
            this.lblHjse.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.lblHjse.BackColor = Color.White;
            this.lblHjse.Font = new Font("宋体", 11.25f);
            this.lblHjse.ForeColor = Color.Black;
            this.lblHjse.Location = new Point(0x1ca, 0x1d8);
            this.lblHjse.Name = "lblHjse";
            this.lblHjse.RightToLeft = RightToLeft.No;
            this.lblHjse.Size = new Size(0x83, 30);
            this.lblHjse.TabIndex = 0x10;
            this.lblHjse.Text = "￥0.00";
            this.lblHjse.TextAlign = ContentAlignment.MiddleLeft;
            this.lblJqbh.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.lblJqbh.BackColor = Color.White;
            this.lblJqbh.Font = new Font("宋体", 9f);
            this.lblJqbh.ForeColor = Color.Black;
            this.lblJqbh.Location = new Point(0x292, 0x1d9);
            this.lblJqbh.Name = "lblJqbh";
            this.lblJqbh.Size = new Size(180, 30);
            this.lblJqbh.TabIndex = 0x11;
            this.lblJqbh.Text = "0000000000";
            this.lblJqbh.TextAlign = ContentAlignment.MiddleLeft;
            this.txtYshw.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.txtYshw.BackColor = Color.White;
            this.txtYshw.BorderStyle = BorderStyle.None;
            this.txtYshw.ForeColor = Color.Black;
            this.txtYshw.Location = new Point(0x255, 0x13d);
            this.txtYshw.Multiline = true;
            this.txtYshw.Name = "txtYshw";
            this.txtYshw.Size = new Size(0x105, 0x8e);
            this.txtYshw.TabIndex = 13;
            this.txtQyd.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtQyd.BackColor = Color.White;
            this.txtQyd.ForeColor = Color.Black;
            this.txtQyd.Location = new Point(230, 0x121);
            this.txtQyd.Multiline = true;
            this.txtQyd.Name = "txtQyd";
            this.txtQyd.Size = new Size(0x274, 0x15);
            this.txtQyd.TabIndex = 11;
            this.cmbFhrsbh.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.cmbFhrsbh.set_AutoComplate(0);
            this.cmbFhrsbh.set_AutoIndex(1);
            this.cmbFhrsbh.BackColor = Color.White;
            this.cmbFhrsbh.set_BorderColor(Color.White);
            this.cmbFhrsbh.set_BorderStyle(1);
            this.cmbFhrsbh.set_ButtonAutoHide(true);
            this.cmbFhrsbh.set_buttonStyle(1);
            this.cmbFhrsbh.set_DataSource(null);
            this.cmbFhrsbh.set_DrawHead(true);
            this.cmbFhrsbh.set_Edit(1);
            this.cmbFhrsbh.ForeColor = Color.Black;
            this.cmbFhrsbh.set_IsSelectAll(false);
            this.cmbFhrsbh.Location = new Point(0x229, 0x103);
            this.cmbFhrsbh.set_MaxIndex(8);
            this.cmbFhrsbh.set_MaxLength(0x7fff);
            this.cmbFhrsbh.Name = "cmbFhrsbh";
            this.cmbFhrsbh.set_ReadOnly(false);
            this.cmbFhrsbh.set_SelectedIndex(-1);
            this.cmbFhrsbh.set_SelectionStart(0);
            this.cmbFhrsbh.set_ShowText("");
            this.cmbFhrsbh.Size = new Size(0x131, 0x15);
            this.cmbFhrsbh.TabIndex = 10;
            this.cmbFhrsbh.set_UnderLineColor(Color.Transparent);
            this.cmbFhrsbh.set_UnderLineStyle(0);
            this.cmbSender.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.cmbSender.set_AutoComplate(0);
            this.cmbSender.set_AutoIndex(1);
            this.cmbSender.BackColor = Color.White;
            this.cmbSender.set_BorderColor(Color.White);
            this.cmbSender.set_BorderStyle(1);
            this.cmbSender.set_ButtonAutoHide(true);
            this.cmbSender.set_buttonStyle(1);
            this.cmbSender.set_DataSource(null);
            this.cmbSender.set_DrawHead(true);
            this.cmbSender.set_Edit(1);
            this.cmbSender.ForeColor = Color.Black;
            this.cmbSender.set_IsSelectAll(false);
            this.cmbSender.Location = new Point(0x229, 0xeb);
            this.cmbSender.set_MaxIndex(8);
            this.cmbSender.set_MaxLength(0x7fff);
            this.cmbSender.Name = "cmbSender";
            this.cmbSender.set_ReadOnly(false);
            this.cmbSender.set_SelectedIndex(-1);
            this.cmbSender.set_SelectionStart(0);
            this.cmbSender.set_ShowText("");
            this.cmbSender.Size = new Size(0x131, 0x15);
            this.cmbSender.TabIndex = 9;
            this.cmbSender.set_UnderLineColor(Color.Transparent);
            this.cmbSender.set_UnderLineStyle(0);
            this.lblKprq.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.lblKprq.AutoSize = true;
            this.lblKprq.BackColor = Color.White;
            this.lblKprq.ForeColor = Color.Black;
            this.lblKprq.Location = new Point(0x2dd, 0x53);
            this.lblKprq.Name = "lblKprq";
            this.lblKprq.Size = new Size(0x4d, 12);
            this.lblKprq.TabIndex = 2;
            this.lblKprq.Text = "2014年7月1日";
            this.lblFphm.AutoSize = true;
            this.lblFphm.BackColor = Color.White;
            this.lblFphm.Font = new Font("新宋体", 18.75f);
            this.lblFphm.ForeColor = Color.Black;
            this.lblFphm.Location = new Point(0x27f, 0x22);
            this.lblFphm.Name = "lblFphm";
            this.lblFphm.Size = new Size(0x74, 0x19);
            this.lblFphm.TabIndex = 1;
            this.lblFphm.Text = "00000000";
            this.lblFpdm.AutoSize = true;
            this.lblFpdm.BackColor = Color.White;
            this.lblFpdm.Font = new Font("新宋体", 18.75f);
            this.lblFpdm.ForeColor = Color.Black;
            this.lblFpdm.Location = new Point(0x94, 0x22);
            this.lblFpdm.Name = "lblFpdm";
            this.lblFpdm.Size = new Size(0x8e, 0x19);
            this.lblFpdm.TabIndex = 0;
            this.lblFpdm.Text = "0000000000";
            this.lblTitle.BackColor = Color.Transparent;
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 10f);
            this.lblTitle.ForeColor = Color.Crimson;
            this.lblTitle.Location = new Point(0x1bc, 0x36);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(50, 0x12);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "河  北";
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.dgFyxm.set_AborCellPainting(false);
            this.dgFyxm.set_AllowColumnHeadersVisible(true);
            this.dgFyxm.AllowUserToAddRows = false;
            this.dgFyxm.set_AllowUserToResizeRows(false);
            style.BackColor = Color.FloralWhite;
            this.dgFyxm.AlternatingRowsDefaultCellStyle = style;
            this.dgFyxm.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.dgFyxm.BackgroundColor = Color.White;
            this.dgFyxm.BorderStyle = BorderStyle.None;
            this.dgFyxm.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            this.dgFyxm.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            style2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style2.BackColor = SystemColors.Control;
            style2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            style2.ForeColor = SystemColors.WindowText;
            style2.SelectionBackColor = SystemColors.Highlight;
            style2.SelectionForeColor = SystemColors.HighlightText;
            style2.WrapMode = DataGridViewTriState.True;
            this.dgFyxm.ColumnHeadersDefaultCellStyle = style2;
            this.dgFyxm.set_ColumnHeadersHeight(0);
            this.dgFyxm.set_ColumnHeadersHeightSizeMode(DataGridViewColumnHeadersHeightSizeMode.AutoSize);
            this.dgFyxm.Columns.AddRange(new DataGridViewColumn[] { this.colFyxm, this.colJe });
            style3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style3.BackColor = Color.FromArgb(220, 230, 240);
            style3.Font = new Font("宋体", 10f);
            style3.ForeColor = Color.FromArgb(0, 0, 0);
            style3.SelectionBackColor = Color.FromArgb(0x7d, 150, 0xb9);
            style3.SelectionForeColor = Color.FromArgb(0xff, 0xff, 0xff);
            style3.WrapMode = DataGridViewTriState.False;
            this.dgFyxm.DefaultCellStyle = style3;
            this.dgFyxm.EnableHeadersVisualStyles = false;
            this.dgFyxm.GridColor = Color.Gray;
            this.dgFyxm.set_GridStyle(0);
            this.dgFyxm.Location = new Point(0x5f, 0x13a);
            this.dgFyxm.MultiSelect = false;
            this.dgFyxm.Name = "dgFyxm";
            this.dgFyxm.RowHeadersVisible = false;
            this.dgFyxm.RowHeadersWidth = 0x19;
            this.dgFyxm.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            style4.BackColor = Color.White;
            style4.SelectionBackColor = Color.SlateGray;
            this.dgFyxm.RowsDefaultCellStyle = style4;
            this.dgFyxm.RowTemplate.Height = 0x17;
            this.dgFyxm.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgFyxm.Size = new Size(0x1db, 0x9b);
            this.dgFyxm.TabIndex = 12;
            this.tool_close.Image = Resources.退出;
            this.tool_close.ImageScaling = ToolStripItemImageScaling.None;
            this.tool_close.ImageTransparentColor = Color.Magenta;
            this.tool_close.Name = "tool_close";
            this.tool_close.Size = new Size(0x34, 0x16);
            this.tool_close.Text = "退出";
            this.tool_close.Click += new EventHandler(this.tool_close_Click);
            this.toolStripBtnSave.Image = Resources.修改;
            this.toolStripBtnSave.ImageScaling = ToolStripItemImageScaling.None;
            this.toolStripBtnSave.ImageTransparentColor = Color.Magenta;
            this.toolStripBtnSave.Name = "toolStripBtnSave";
            this.toolStripBtnSave.Size = new Size(0x34, 0x16);
            this.toolStripBtnSave.Text = "保存";
            this.toolStripBtnSave.Click += new EventHandler(this.toolStripBtnSave_Click);
            this.tool_addrow.Image = Resources.加入;
            this.tool_addrow.ImageScaling = ToolStripItemImageScaling.None;
            this.tool_addrow.ImageTransparentColor = Color.Magenta;
            this.tool_addrow.Name = "tool_addrow";
            this.tool_addrow.Size = new Size(0x34, 0x16);
            this.tool_addrow.Text = "增加";
            this.tool_delrow.Image = Resources.减;
            this.tool_delrow.ImageScaling = ToolStripItemImageScaling.None;
            this.tool_delrow.ImageTransparentColor = Color.Magenta;
            this.tool_delrow.Name = "tool_delrow";
            this.tool_delrow.Size = new Size(0x34, 0x16);
            this.tool_delrow.Text = "删除";
            this.toolStrip3.BackColor = SystemColors.Control;
            this.toolStrip3.Items.AddRange(new ToolStripItem[] { this.tool_close, this.toolStripBtnSave, this.tool_addrow, button, this.tool_delrow, this.bt_jg });
            this.toolStrip3.Location = new Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new Size(0x3c4, 0x19);
            this.toolStrip3.Stretch = true;
            this.toolStrip3.TabIndex = 1;
            this.bt_jg.CheckOnClick = true;
            this.bt_jg.Image = Resources.含税;
            this.bt_jg.ImageScaling = ToolStripItemImageScaling.None;
            this.bt_jg.ImageTransparentColor = Color.Magenta;
            this.bt_jg.Name = "bt_jg";
            this.bt_jg.Size = new Size(0x34, 0x16);
            this.bt_jg.Text = "价格";
            this.colFyxm.FillWeight = 185f;
            this.colFyxm.HeaderText = "费用项目";
            this.colFyxm.Name = "colFyxm";
            this.colFyxm.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colFyxm.Width = 0xb9;
            this.colJe.FillWeight = 115f;
            this.colJe.HeaderText = "金额";
            this.colJe.Name = "colJe";
            this.colJe.Width = 0x73;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            base.ClientSize = new Size(0x3c4, 0x2dd);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.toolStrip3);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "HYFPtiankai_new";
            base.set_TabText("HYFPtiankai_new");
            this.Text = "HYFPtiankai_new";
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.dgFyxm.EndInit();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InitializeDefault()
        {
            this.InitSpmcCmb();
            this.InitializeComponent();
            this.txtDJH.TextChanged += new EventHandler(this.textBoxDJH_TextChanged);
            this.cmbSlv.DropDownStyle = ComboBoxStyle.DropDownList;
            this.tool_addrow.ToolTipText = "添加费用项目";
            this.tool_delrow.ToolTipText = "删除费用项目";
            this.tool_close.ToolTipText = "退出";
            this.tool_addrow.Click += new EventHandler(this.addRow_Click);
            this.tool_addrow.MouseDown += new MouseEventHandler(this.commit_MouseDown);
            this.tool_delrow.Click += new EventHandler(this.delRow_Click);
            this.tool_delrow.MouseDown += new MouseEventHandler(this.commit_MouseDown);
            this.tool_close.Click += new EventHandler(this.tool_close_Click);
            this.bt_jg.Click += new EventHandler(this.hsjbzButton_Click);
            this.bt_jg.ToolTipText = "转换含税价标志";
            this.bt_jg.CheckOnClick = true;
            this.cmbSkr.set_IsSelectAll(true);
            this.cmbSkr.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("", "YH", this.cmbSkr.Width));
            this.cmbSkr.set_DrawHead(false);
            this.cmbFhr.set_IsSelectAll(true);
            this.cmbFhr.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("", "YH", this.cmbFhr.Width));
            this.cmbFhr.set_DrawHead(false);
            for (int i = 0; i < this.dgFyxm.Columns.Count; i++)
            {
                this.dgFyxm.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            this.dgFyxm.AllowUserToAddRows = false;
            this.dgFyxm.AllowUserToDeleteRows = false;
            this.dgFyxm.StandardTab = false;
            this.dgFyxm.MultiSelect = false;
            this.dgFyxm.Columns["colFyxm"].ReadOnly = true;
            this.dgFyxm.set_GridStyle(1);
            if (!this.onlyShow)
            {
                Color buttonFace = SystemColors.ButtonFace;
                this.cmbSpf.set_BorderColor(buttonFace);
                this.cmbSpfsbh.set_BorderColor(buttonFace);
                this.cmbShr.set_BorderColor(buttonFace);
                this.cmbShrsbh.set_BorderColor(buttonFace);
                this.cmbSender.set_BorderColor(buttonFace);
                this.cmbFhrsbh.set_BorderColor(buttonFace);
                this.cmbSkr.set_BorderColor(buttonFace);
                this.cmbFhr.set_BorderColor(buttonFace);
                this.SetGfxxControl(this.cmbSpf, "MC");
                this.SetGfxxControl(this.cmbSpfsbh, "SH");
                this.SetGfxxControl(this.cmbShr, "MC");
                this.SetGfxxControl(this.cmbShrsbh, "SH");
                this.SetGfxxControl(this.cmbSender, "MC");
                this.SetGfxxControl(this.cmbFhrsbh, "SH");
                this.SetDataGridPropEven();
                this.dgFyxm.Controls.Add(this._spmcBt);
                this.RegTextChangedEvent();
                this.cmbSlv.SelectedIndexChanged += new EventHandler(this.cmbSlv_SelectedIndexChanged);
                this.SetSkrAndFhr();
                this.cmbSkr.add_OnSelectValue(new EventHandler(this.cmbSkr_SelectedIndexChanged));
                this.cmbFhr.add_OnSelectValue(new EventHandler(this.cmbFhr_SelectedIndexChanged));
                base.FormClosing += new FormClosingEventHandler(this.HYInvoiceForm_FormClosing);
                FormMain.add_UpdateUserNameEvent(new FormMain.UpdateUserNameDelegate(this, (IntPtr) this.FormMain_UpdateUserNameEvent));
                this.ckbNegtive.CheckStateChanged += new EventHandler(this.ckbNegtive_CheckStateChanged);
            }
            base.KeyPreview = true;
            base.KeyDown += new KeyEventHandler(this.HYInvoiceForm_KeyDown);
            base.Resize += new EventHandler(this.HYInvoiceForm_Resize);
        }

        private void InitSpmcCmb()
        {
            this._spmcBt = new AisinoMultiCombox();
            this._spmcBt.set_IsSelectAll(true);
            this._spmcBt.Name = "SPMCBT";
            this._spmcBt.Text = "";
            this._spmcBt.Padding = new Padding(0);
            this._spmcBt.Margin = new Padding(0);
            this._spmcBt.Visible = false;
            this._spmcBt_SetAutoComplateHead();
            this._spmcBt.set_AutoComplate(2);
            this._spmcBt.add_OnAutoComplate(new EventHandler(this._spmcBt_OnAutoComplate));
            this._spmcBt.set_buttonStyle(0);
            this._spmcBt.add_OnButtonClick(new EventHandler(this._spmcBt_Click));
            this._spmcBt.MouseDoubleClick += new MouseEventHandler(this._spmcBt_MouseDoubleClick);
            this._spmcBt.OnTextChanged = (EventHandler) Delegate.Combine(this._spmcBt.OnTextChanged, new EventHandler(this._spmcBt_TextChanged));
            this._spmcBt.add_OnSelectValue(new EventHandler(this._spmcBt_OnSelectValue));
            this._spmcBt.PreviewKeyDown += new PreviewKeyDownEventHandler(this._spmcBt_PreviewKeyDown);
            this._spmcBt.Leave += new EventHandler(this._spmcBt_leave);
        }

        private void RegTextChangedEvent()
        {
            this.cmbSpf.OnTextChanged = (EventHandler) Delegate.Combine(this.cmbSpf.OnTextChanged, new EventHandler(this.cmbSpf_TextChanged));
            this.cmbSpfsbh.OnTextChanged = (EventHandler) Delegate.Combine(this.cmbSpfsbh.OnTextChanged, new EventHandler(this.cmbSpfsbh_TextChanged));
            this.cmbShr.OnTextChanged = (EventHandler) Delegate.Combine(this.cmbShr.OnTextChanged, new EventHandler(this.cmbShr_TextChanged));
            this.cmbShrsbh.OnTextChanged = (EventHandler) Delegate.Combine(this.cmbShrsbh.OnTextChanged, new EventHandler(this.cmbShrsbh_TextChanged));
            this.cmbSender.OnTextChanged = (EventHandler) Delegate.Combine(this.cmbSender.OnTextChanged, new EventHandler(this.cmbSender_TextChanged));
            this.cmbFhrsbh.OnTextChanged = (EventHandler) Delegate.Combine(this.cmbFhrsbh.OnTextChanged, new EventHandler(this.cmbFhrsbh_TextChanged));
            this.cmbSkr.OnTextChanged = (EventHandler) Delegate.Combine(this.cmbSkr.OnTextChanged, new EventHandler(this.cmbSkr_TextChanged));
            this.cmbFhr.OnTextChanged = (EventHandler) Delegate.Combine(this.cmbFhr.OnTextChanged, new EventHandler(this.cmbFhr_TextChanged));
            this.txtQyd.TextChanged += new EventHandler(this.txtQyd_TextChanged);
            this.txtYshw.TextChanged += new EventHandler(this.txtYshw_TextChanged);
            this.txtBz.TextChanged += new EventHandler(this.txtBz_TextChanged);
            this.txtCzch.TextChanged += new EventHandler(this.txtCzch_TextChanged);
            this.txtCcdw.TextChanged += new EventHandler(this.txtCcdw_TextChanged);
        }

        private void ResetCmbSlv()
        {
            try
            {
                this.cmbSlv.SelectedIndexChanged -= new EventHandler(this.cmbSlv_SelectedIndexChanged);
                List<SLV> slvList = PresentinvMng.GetSlvList(this._fpxx.get_Fplx(), this._fpxx.GetSqSLv());
                this.cmbSlv.Items.Clear();
                this.cmbSlv.Items.AddRange(slvList.ToArray());
                if (this.cmbSlv.Items.Count > 0)
                {
                    string str = PropertyUtil.GetValue("HYINV-SLV", "");
                    if (str == "")
                    {
                        this.cmbSlv.SelectedIndex = 0;
                    }
                    else
                    {
                        int num = 0;
                        for (int i = 0; i < this.cmbSlv.Items.Count; i++)
                        {
                            if (((SLV) this.cmbSlv.Items[i]).get_DataValue() == str)
                            {
                                num = i;
                                break;
                            }
                        }
                        this.cmbSlv.SelectedIndex = num;
                    }
                }
            }
            catch
            {
            }
            finally
            {
                this.cmbSlv.SelectedIndexChanged += new EventHandler(this.cmbSlv_SelectedIndexChanged);
            }
        }

        private bool SaveFpData()
        {
            this.dgFyxm.EndEdit();
            string str = "";
            if (this.ToModel() != 0)
            {
                return false;
            }
            for (int i = 0; i < this.bill.ListGoods.Count; i++)
            {
                FatchSaleBill bill = new FatchSaleBill();
                if (bill.CheckSlvIsYHZCSLV(this.bill.SLV.ToString(), this.bill.ListGoods[i].XSYHSM))
                {
                    this.bill.ListGoods[i].XSYH = true;
                }
                else
                {
                    this.bill.ListGoods[i].XSYH = false;
                    this.bill.ListGoods[i].XSYHSM = "";
                }
            }
            str = this.saleBillBL.Save(this.bill);
            if (str == "0")
            {
                string str2 = this.saleBillBL.CheckBill(this.bill);
                base.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageManager.ShowMsgBox(str);
            }
            return true;
        }

        private void SetCmbEnabled(bool enabled)
        {
            if (!enabled)
            {
                this.cmbSpf.set_Edit(0);
                this.cmbShr.set_Edit(0);
                this.cmbShrsbh.set_Edit(0);
                this.cmbSender.set_Edit(0);
                this.cmbFhrsbh.set_Edit(0);
                this.cmbSpfsbh.set_Edit(0);
            }
            else
            {
                this.cmbSpf.set_Edit(1);
                this.cmbSpfsbh.set_Edit(1);
                this.cmbShr.set_Edit(1);
                this.cmbShrsbh.set_Edit(1);
                this.cmbSender.set_Edit(1);
                this.cmbFhrsbh.set_Edit(1);
            }
        }

        private void SetDataGridPropEven()
        {
            this.dgFyxm.StandardTab = false;
            this.dgFyxm.PreviewKeyDown += new PreviewKeyDownEventHandler(this.dataGridView_PreviewKeyDown);
            this.dgFyxm.CellEndEdit += new DataGridViewCellEventHandler(this.dataGridView_CellEndEdit);
            this.dgFyxm.CellMouseDown += new DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseDown);
            this.dgFyxm.CurrentCellChanged += new EventHandler(this.dataGridView_CurrentCellChanged);
            this.dgFyxm.RowsAdded += new DataGridViewRowsAddedEventHandler(this.dataGridView_RowsAdded);
            this.dgFyxm.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.dataGridView_RowsRemoved);
            this.dgFyxm.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.dataGridView_EditingControlShowing);
            this.dgFyxm.add_CSDGridColumnWidthChanged(new CustomStyleDataGrid.CSDGridColumnWidthChangedHandler(this, (IntPtr) this.dgFyxm_CSDGridColumnWidthChanged));
        }

        private void SetFormTitle(FPLX fplx, string fpdm)
        {
        }

        private void SetFPLabel()
        {
            this._fpxx.set_Fpdm(this.lblFpdm.Text);
            this._fpxx.set_Fphm(this.lblFphm.Text);
            this._fpxx.set_Kprq(this.lblKprq.Text);
            this._fpxx.set_Fhr(this.cmbFhr.Text);
            this._fpxx.set_Skr(this.cmbSkr.Text);
            this._fpxx.set_Kpr(this.lblKpr.Text);
        }

        private void SetFYXM(object[] spxx)
        {
            this._spmcBt.Leave -= new EventHandler(this._spmcBt_leave);
            int rowIndex = this.dgFyxm.CurrentCell.RowIndex;
            if (CommonTool.isSPBMVersion() && (spxx.Length >= 3))
            {
                string str = spxx[2].ToString().Trim();
                if (str == "")
                {
                    MessageBox.Show("您选择的费用项目没有分类编码，请重新选择！");
                    this._spmcBt.Text = "";
                    return;
                }
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.CanUseThisSPFLBM", new object[] { str, true });
                if ((objArray != null) && !bool.Parse(objArray[0].ToString()))
                {
                    MessageManager.ShowMsgBox("INP-242207", new string[] { "费用项目", "\r\n可能原因：\r\n1、当前企业没有所选税收分类编码授权。\r\n2、当前版本所选税收分类编码可用状态为不可用。" });
                    this._spmcBt.Text = "";
                    return;
                }
            }
            if ((spxx != null) && (spxx.Length > 0))
            {
                string str6;
                string str2 = spxx[0].ToString();
                this._fpxx.SetSpmc(rowIndex, str2);
                this.ShowDataGrid(this._fpxx.GetSpxx(rowIndex), rowIndex);
                if (CommonTool.isSPBMVersion())
                {
                    string str3 = "";
                    string str4 = "";
                    bool flag2 = false;
                    string str5 = "";
                    str6 = "";
                    string str7 = "";
                    string str8 = "";
                    if (spxx.Length >= 2)
                    {
                        str6 = spxx[1].ToString().Trim();
                    }
                    if (spxx.Length >= 3)
                    {
                        str3 = spxx[2].ToString().Trim();
                    }
                    if (spxx.Length >= 4)
                    {
                        string str9 = spxx[3].ToString().Trim();
                        if (((str9 == "是") || (str9 == "1")) || (str9.ToUpper() == "TRUE"))
                        {
                            flag2 = true;
                        }
                    }
                    if (spxx.Length >= 5)
                    {
                        str5 = spxx[4].ToString().Trim();
                    }
                    if (spxx.Length >= 6)
                    {
                        str8 = spxx[5].ToString().Trim();
                    }
                    while (this.bill.ListGoods.Count < (rowIndex + 1))
                    {
                        this.bill.ListGoods.Add(new Goods());
                    }
                    this.bill.ListGoods[rowIndex].SPBM = str6;
                    this.bill.ListGoods[rowIndex].SPMC = str2;
                    this.bill.ListGoods[rowIndex].FLBM = str3;
                    this.bill.ListGoods[rowIndex].FLMC = str4;
                    this.bill.ListGoods[rowIndex].XSYH = flag2;
                    this.bill.ListGoods[rowIndex].XSYHSM = str5;
                    this.bill.ListGoods[rowIndex].LSLVBS = str7;
                }
                else
                {
                    str6 = "";
                    if (spxx.Length >= 2)
                    {
                        str6 = spxx[1].ToString().Trim();
                    }
                    while (this.bill.ListGoods.Count < (rowIndex + 1))
                    {
                        this.bill.ListGoods.Add(new Goods());
                    }
                    this.bill.ListGoods[rowIndex].SPBM = str6;
                    this.bill.ListGoods[rowIndex].SPMC = str2;
                }
                this.dgFyxm.CurrentCell = this.dgFyxm.Rows[rowIndex].Cells["colJe"];
            }
            this.dgFyxm.Focus();
            this._spmcBt.Leave += new EventHandler(this._spmcBt_leave);
        }

        protected virtual void SetGfxxControl(AisinoMultiCombox aisinoCmb, string showText)
        {
            aisinoCmb.set_IsSelectAll(true);
            aisinoCmb.set_buttonStyle(0);
            aisinoCmb.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            aisinoCmb.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", 200));
            aisinoCmb.set_ShowText(showText);
            aisinoCmb.set_DrawHead(false);
            aisinoCmb.set_AutoIndex(0);
            aisinoCmb.add_OnButtonClick(new EventHandler(this.shfxx_OnButtonClick));
            aisinoCmb.add_OnAutoComplate(new EventHandler(this.shfxx_OnAutoComplate));
            aisinoCmb.add_OnSelectValue(new EventHandler(this.shfxx_OnSelectValue));
            aisinoCmb.set_AutoComplate(2);
        }

        private void SetHsjbz(bool hsjbz)
        {
            if (this.bt_jg.Checked != hsjbz)
            {
                this.bt_jg.Checked = hsjbz;
            }
            this._fpxx.set_Hsjbz(hsjbz);
            string str = hsjbz ? "(含税)" : "(不含税)";
            if (this.dgFyxm.Columns["colJe"] != null)
            {
                this.dgFyxm.Columns["colJe"].HeaderText = this.dgFyxm.Columns["colJe"].HeaderText.Split(new char[] { '(' })[0] + str;
            }
            if (!this.onlyShow)
            {
                PropertyUtil.SetValue("HYINV-HSJBZ", hsjbz ? "1" : "0");
            }
            this.ShowDataGridMxxx();
        }

        private void SetHzxx()
        {
            string str = "";
            if (this.ckbNegtive.Checked)
            {
                str = "-";
            }
            string hjJe = this._fpxx.GetHjJe();
            this.lblHjje.Text = "￥" + (("0.00" == hjJe) ? "" : str) + hjJe;
            string hjSe = this._fpxx.GetHjSe();
            this.lblHjse.Text = "￥" + (("0.00" == hjSe) ? "" : str) + hjSe;
            string hjJeHs = this._fpxx.GetHjJeHs();
            hjJeHs = (("0.00" == hjJeHs) ? "" : str) + hjJeHs;
            this.lblJgxx.Text = "￥" + hjJeHs;
            this.lblJgdx.Text = (hjJeHs == "0.00") ? "零" : ToolUtil.RMBToDaXie(decimal.Parse(hjJeHs));
        }

        private void SetSkrAndFhr()
        {
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Xtgl.UserRoleService", null);
            if (objArray != null)
            {
                this.DelTextChangedEvent();
                DataTable table = new DataTable();
                DataColumn column = new DataColumn("YH");
                table.Columns.Add(column);
                List<string> list = objArray[0] as List<string>;
                foreach (string str in list)
                {
                    table.Rows.Add(new object[] { str });
                }
                string str2 = PropertyUtil.GetValue("HYINV-SKR-IDX", "");
                this.cmbSkr.set_DataSource(table);
                this.cmbSkr.Text = str2;
                str2 = PropertyUtil.GetValue("HYINV-FHR-IDX", "");
                this.cmbFhr.set_DataSource(table);
                this.cmbFhr.Text = str2;
                this.RegTextChangedEvent();
            }
        }

        private void SetTxtEnabled(bool enabled)
        {
            this.txtQyd.Enabled = enabled;
            this.txtYshw.Enabled = enabled;
            this.txtBz.Enabled = enabled;
            this.txtCcdw.Enabled = enabled;
            this.txtCzch.Enabled = enabled;
            if (!enabled)
            {
                this.txtQyd.BorderStyle = BorderStyle.None;
                this.txtYshw.BorderStyle = BorderStyle.None;
                this.txtBz.BorderStyle = BorderStyle.None;
                this.txtCcdw.BorderStyle = BorderStyle.None;
                this.txtCzch.BorderStyle = BorderStyle.None;
            }
        }

        private void shfxx_OnAutoComplate(object sender, EventArgs e)
        {
            string text = "";
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if (combox != null)
            {
                text = combox.Text;
                DataTable table = this.GfxxOnAutoCompleteDataSource(text);
                if (table != null)
                {
                    combox.set_DataSource(table);
                }
            }
        }

        private void shfxx_OnButtonClick(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = (AisinoMultiCombox) sender;
            try
            {
                object[] khxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSFHR", new object[] { combox.Text, 0, "MC,SH" });
                this.ShfxxSetValue(sender, khxx);
            }
            catch (Exception)
            {
            }
        }

        private void shfxx_OnSelectValue(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if (combox != null)
            {
                Dictionary<string, string> dictionary = combox.get_SelectDict();
                this.ShfxxSetValue(sender, new object[] { dictionary["MC"], dictionary["SH"] });
            }
        }

        private void ShfxxSetValue(object sender, object[] khxx)
        {
            if ((khxx != null) && (khxx.Length >= 2))
            {
                string str = khxx[0].ToString();
                string str2 = khxx[1].ToString();
                switch (((AisinoMultiCombox) sender).Name)
                {
                    case "cmbSpf":
                    case "cmbSpfsbh":
                        this.cmbSpf.Text = str;
                        this.cmbSpfsbh.Text = str2;
                        break;

                    case "cmbShr":
                    case "cmbShrsbh":
                        this.cmbShr.Text = str;
                        this.cmbShrsbh.Text = str2;
                        break;

                    case "cmbSender":
                    case "cmbFhrsbh":
                        this.cmbSender.Text = str;
                        this.cmbFhrsbh.Text = str2;
                        break;
                }
            }
        }

        private void ShowCopyMainInfo()
        {
            this.DelTextChangedEvent();
            this.GetQyLable();
            this.GetShrCmb();
            this.GetYsText();
            this.cmbSkr.Text = this._fpxx.get_Skr();
            this.cmbFhr.Text = this._fpxx.get_Fhr();
            if (this.cmbSlv.Items.Count > 0)
            {
            }
            this.RegTextChangedEvent();
        }

        private void ShowDataGrid(Goods spxx, int index)
        {
            if (spxx != null)
            {
                this.dgFyxm.CurrentCellChanged -= new EventHandler(this.dataGridView_CurrentCellChanged);
                while (index > this.dgFyxm.Rows.Count)
                {
                    this.dgFyxm.Rows.Add();
                }
                DataGridViewRow row = this.dgFyxm.Rows[index];
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    string name = this.dgFyxm.Columns[i].Name;
                    try
                    {
                        switch (name)
                        {
                            case "colFyxm":
                                row.Cells[name].Value = this.bill.ListGoods[index].SPMC;
                                break;

                            case "colJe":
                            {
                                string str2 = this.bill.getJe(index).TrimStart(new char[] { '-' });
                                row.Cells[name].Value = str2;
                                break;
                            }
                        }
                    }
                    catch (ArgumentException exception)
                    {
                        this.log.Error("设置数据表格内容异常", exception);
                    }
                }
                this.SetHzxx();
                this.dgFyxm.CurrentCellChanged += new EventHandler(this.dataGridView_CurrentCellChanged);
            }
        }

        private void ShowDataGrid(Dictionary<SPXX, string> spxx, int index)
        {
            if (spxx != null)
            {
                this.dgFyxm.CurrentCellChanged -= new EventHandler(this.dataGridView_CurrentCellChanged);
                while ((this.dgFyxm.Rows.Count - 1) < index)
                {
                    this.dgFyxm.Rows.Add();
                }
                DataGridViewRow row = this.dgFyxm.Rows[index];
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    string name = this.dgFyxm.Columns[i].Name;
                    try
                    {
                        switch (name)
                        {
                            case "colFyxm":
                                row.Cells[name].Value = spxx[0];
                                break;

                            case "colJe":
                            {
                                string str2 = spxx[7];
                                str2 = str2.TrimStart(new char[] { '-' });
                                row.Cells[name].Value = str2;
                                break;
                            }
                        }
                    }
                    catch (ArgumentException exception)
                    {
                        this.log.Error("设置数据表格内容异常", exception);
                    }
                }
                this.SetHzxx();
                this.dgFyxm.CurrentCellChanged += new EventHandler(this.dataGridView_CurrentCellChanged);
            }
        }

        private void ShowDataGridMxxx()
        {
            if (this.bill.JEHJ < 0.0)
            {
                this.ckbNegtive.Checked = true;
            }
            if (PropertyUtil.GetValue("HYINV-HSJBZ", "0").Trim() == "1")
            {
                this.bill.ContainTax = true;
            }
            else
            {
                this.bill.ContainTax = false;
            }
            double num = this.ckbNegtive.Checked ? ((double) (-1)) : ((double) 1);
            for (int i = 0; i < this.bill.ListGoods.Count; i++)
            {
                this.ShowDataGrid(this.bill.GetGood(i), i);
            }
        }

        private void ShowInvMainInfo()
        {
            try
            {
                this.DelTextChangedEvent();
                this.GetQyLable();
                this.GetShrCmb();
                this.GetYsText();
                this.cmbSkr.Text = this._fpxx.get_Skr();
                this.cmbFhr.Text = this._fpxx.get_Fhr();
                this.SetHsjbz(this._fpxx.get_Hsjbz());
                if (this.dgFyxm.RowCount > 0)
                {
                    this.dgFyxm.CurrentCell = this.dgFyxm[this.dgFyxm.ColumnCount - 1, this.dgFyxm.RowCount - 1];
                }
                this.RegTextChangedEvent();
            }
            catch (Exception)
            {
            }
        }

        private void textBoxDJH_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string text = this.txtDJH.Text;
                for (int i = ToolUtil.GetByteCount(text); i > 50; i = ToolUtil.GetByteCount(text))
                {
                    int length = text.Length;
                    text = text.Substring(0, length - 1);
                }
                this.txtDJH.Text = text;
                this.txtDJH.SelectionStart = this.txtDJH.Text.Length;
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private int ToModel()
        {
            this.bill.BH = this.txtDJH.Text.Trim();
            this.bill.GFMC = this.cmbSpf.Text.Trim();
            this.bill.GFSH = this.cmbSpfsbh.Text.Trim();
            this.bill.GFDZDH = this.cmbShr.Text.Trim();
            this.bill.XFDZDH = this.cmbSender.Text.Trim();
            this.bill.CM = this.cmbShrsbh.Text.Trim();
            this.bill.TYDH = this.cmbFhrsbh.Text.Trim();
            this.bill.QYD = this.txtCzch.Text.Trim();
            this.bill.DW = this.txtCcdw.Text.Trim();
            this.bill.XFYHZH = this.txtQyd.Text.Trim();
            this.bill.FHR = this.cmbFhr.Text.Trim();
            this.bill.SKR = this.cmbSkr.Text.Trim();
            this.bill.YSHWXX = this.txtYshw.Text.Trim();
            this.bill.DJRQ = this.dateTimePicker1.Value.Date;
            this._fpxx.set_Gfmc(this.bill.GFMC);
            this._fpxx.set_Gfsh(this.bill.GFSH);
            this._fpxx.set_Shrmc(this.bill.GFDZDH);
            this._fpxx.set_Shrsh(this.bill.CM);
            this._fpxx.set_Fhrmc(this.bill.XFDZDH);
            this._fpxx.set_Fhrsh(this.bill.TYDH);
            this._fpxx.set_Qyd_jy_ddd(this.bill.XFYHZH);
            this._fpxx.set_Czch(this.bill.QYD);
            this._fpxx.set_Ccdw(this.bill.DW);
            this._fpxx.set_Yshwxx(this.bill.YSHWXX);
            this._fpxx.set_Bz(this.bill.BZ);
            this._fpxx.set_Fhr(this.bill.FHR);
            this._fpxx.set_Skr(this.bill.SKR);
            if (this.bill.IsANew)
            {
                this.bill.DJYF = this.bill.DJRQ.Month;
            }
            string sLValue = PresentinvMng.GetSLValue(this.cmbSlv.Text.Trim());
            this.bill.SLV = CommonTool.Todouble(sLValue);
            this.bill.BZ = this.txtBz.Text.Trim();
            this.bill.DJZL = "f";
            this.bill.XFDZDH = this._fpxx.get_Fhrmc();
            this.bill.ContainTax = this._fpxx.get_Hsjbz();
            double num = 0.0;
            if (this._fpxx.GetFpData() == null)
            {
                MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
                return 1;
            }
            double num2 = this.ckbNegtive.Checked ? ((double) (-1)) : ((double) 1);
            int count = this.dgFyxm.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                string str2 = (this.dgFyxm.Rows[i].Cells[0].Value == null) ? "" : this.dgFyxm.Rows[i].Cells[0].Value.ToString();
                string number = (this.dgFyxm.Rows[i].Cells[1].Value == null) ? "" : this.dgFyxm.Rows[i].Cells[1].Value.ToString();
                if (i < this.bill.ListGoods.Count)
                {
                    Goods goods = this.bill.ListGoods[i];
                    goods.XSDJBH = this.bill.BH;
                    goods.XH = i + 1;
                    goods.SPMC = str2;
                    double num5 = (number == "") ? 0.0 : CommonTool.Todouble(number);
                    if (this.bill.ContainTax)
                    {
                        num5 = Finacial.Div(num5, 1.0 + this.bill.SLV, 15);
                    }
                    goods.JE = Finacial.GetRound(num5, 2);
                    goods.JE = Finacial.Mul(goods.JE, num2, 15);
                    goods.SE = Finacial.GetRound((double) (goods.JE * this.bill.SLV), 2);
                    num += goods.JE;
                }
            }
            this.bill.JEHJ = num;
            return 0;
        }

        private void tool_close_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        private void toolStripBtnSave_Click(object sender, EventArgs e)
        {
            this.SaveFpData();
        }

        private void ToView()
        {
            this.txtDJH.Text = this.bill.BH;
            this.cmbSpf.Text = this.bill.GFMC;
            this.cmbSpfsbh.Text = this.bill.GFSH;
            this.cmbShr.Text = this.bill.GFDZDH;
            this.cmbSender.Text = this.bill.XFDZDH;
            this.cmbShrsbh.Text = this.bill.CM;
            this.cmbFhrsbh.Text = this.bill.TYDH;
            this.txtCzch.Text = this.bill.QYD;
            this.txtCcdw.Text = this.bill.DW;
            this.txtQyd.Text = this.bill.XFYHZH;
            this.cmbFhr.Text = this.bill.FHR;
            this.cmbSkr.Text = this.bill.SKR;
            this.txtYshw.Text = this.bill.YSHWXX;
            this.dateTimePicker1.Value = this.bill.DJRQ;
            this._fpxx.set_Gfmc(this.bill.GFMC);
            this._fpxx.set_Gfsh(this.bill.GFSH);
            this._fpxx.set_Shrmc(this.bill.GFDZDH);
            this._fpxx.set_Shrsh(this.bill.CM);
            this._fpxx.set_Fhrmc(this.bill.XFDZDH);
            this._fpxx.set_Fhrsh(this.bill.TYDH);
            this._fpxx.set_Qyd_jy_ddd(this.bill.XFYHZH);
            this._fpxx.set_Czch(this.bill.QYD);
            this._fpxx.set_Ccdw(this.bill.DW);
            this._fpxx.set_Yshwxx(this.bill.YSHWXX);
            this._fpxx.set_Bz(this.bill.BZ);
            this._fpxx.set_Fhr(this.bill.FHR);
            this._fpxx.set_Skr(this.bill.SKR);
            this._fpxx.SetFpSLv(this.bill.SLV.ToString());
            string slvStr = this.bill.SLV.ToString();
            string sqSLv = this._fpxx.GetSqSLv();
            SLV slv = PresentinvMng.GetSlvList(11, slvStr)[0];
            if (!sqSLv.Contains(slvStr))
            {
                List<SLV> slvList = PresentinvMng.GetSlvList(11, slvStr);
                this.cmbSlv.Items.Add(slvList[0]);
            }
            this.cmbSlv.SelectedItem = slv.get_ShowValue();
            this.cmbSlv.Text = slv.get_ShowValue();
            this._fpxx.SetFpSLv(slvStr);
            this.txtBz.Text = this.bill.BZ;
            if (this.bill.JEHJ < 0.0)
            {
                this.ckbNegtive.Checked = true;
            }
            double num = this.ckbNegtive.Checked ? ((double) (-1)) : ((double) 1);
            int count = this.bill.ListGoods.Count;
            for (int i = 0; i < count; i++)
            {
                string sPMC = this.bill.GetGood(i).SPMC;
                int byteCount = ToolUtil.GetByteCount(sPMC);
                if ((byteCount > 12) && (this.bill.ListGoods.Count > 10))
                {
                    while (byteCount > 12)
                    {
                        int length = sPMC.Length;
                        sPMC = sPMC.Substring(0, length - 1);
                        byteCount = ToolUtil.GetByteCount(sPMC);
                    }
                }
                string str4 = Finacial.Mul(num, this.bill.ListGoods[i].JE, 2).ToString("0.00");
                string str5 = Finacial.Mul(num, this.bill.ListGoods[i].SE, 2).ToString();
                this.dgFyxm.Rows.Add();
                Spxx spxx = new Spxx(sPMC, "", slvStr);
                int num6 = this._fpxx.AddSpxx(spxx);
                if (num6 < 0)
                {
                    if (this._fpxx.GetCode() != "A032")
                    {
                        this.initSuccess = false;
                        MessageManager.ShowMsgBox(this._fpxx.GetCode());
                    }
                }
                else
                {
                    this._fpxx.SetSe(num6, str5);
                    string code = this._fpxx.GetCode();
                    this._fpxx.SetJe(num6, str4);
                    string str8 = this._fpxx.GetCode();
                }
            }
            this.SetHsjbz(this._fpxx.get_Hsjbz());
            this.SetHzxx();
            if ((this.Text == "货物运输发票单据修改") && (this.dgFyxm.Rows.Count > this.bill.ListGoods.Count))
            {
                this.dgFyxm.Rows.RemoveAt(this.dgFyxm.Rows.Count - 1);
            }
            this.lblFphm.Text = ShowString.ShowKPZT(this.bill.KPZT);
            this.lblFpdm.Text = ShowString.ShowDJZT(this.bill.DJZT);
        }

        private void txtBz_TextChanged(object sender, EventArgs e)
        {
            string str = this.txtBz.Text.Trim();
            this._fpxx.set_Bz(str);
            if (this._fpxx.get_Bz() != str)
            {
                this.txtBz.Text = this._fpxx.get_Bz();
                this.txtBz.SelectionStart = this.txtBz.Text.Length;
            }
            this.bill.BZ = this._fpxx.get_Bz();
        }

        private void txtCcdw_TextChanged(object sender, EventArgs e)
        {
            string str = this.txtCcdw.Text.Trim();
            this._fpxx.set_Ccdw(str);
            if (this._fpxx.get_Ccdw() != str)
            {
                this.txtCcdw.Text = this._fpxx.get_Ccdw();
                this.txtCcdw.SelectionStart = this.txtCcdw.Text.Length;
            }
            this.bill.DW = this._fpxx.get_Ccdw();
        }

        private void txtCzch_TextChanged(object sender, EventArgs e)
        {
            string str = this.txtCzch.Text.Trim();
            this._fpxx.set_Czch(str);
            if (this._fpxx.get_Czch() != str)
            {
                this.txtCzch.Text = this._fpxx.get_Czch();
                this.txtCzch.SelectionStart = this.txtCzch.Text.Length;
            }
            this.bill.QYD = this._fpxx.get_Czch();
        }

        private void txtQyd_TextChanged(object sender, EventArgs e)
        {
            string str = this.txtQyd.Text.Trim();
            this._fpxx.set_Qyd_jy_ddd(str);
            if (this._fpxx.get_Qyd_jy_ddd() != str)
            {
                this.txtQyd.Text = this._fpxx.get_Qyd_jy_ddd();
                this.txtQyd.SelectionStart = this.txtQyd.Text.Length;
            }
            this.bill.XFYHZH = this._fpxx.get_Qyd_jy_ddd();
        }

        private void txtYshw_TextChanged(object sender, EventArgs e)
        {
            string str = this.txtYshw.Text.Trim();
            this._fpxx.set_Yshwxx(str);
            if (this._fpxx.get_Yshwxx() != str)
            {
                this.txtYshw.Text = this._fpxx.get_Yshwxx();
                this.txtYshw.SelectionStart = this.txtYshw.Text.Length;
            }
            this.bill.YSHWXX = this._fpxx.get_Yshwxx();
        }

        public bool InitSuccess
        {
            get
            {
                return this.initSuccess;
            }
        }
    }
}

