namespace Aisino.Fwkp.HzfpHy.Form
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Framework.Startup.Login;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.HzfpHy.Common;
    using Aisino.Fwkp.HzfpHy.IBLL;
    using Aisino.Fwkp.HzfpHy.Model;
    using Aisino.Fwkp.Print;
    using Factory;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    public class HySqdTianKai : DockForm
    {
        private bool blueInv_error;
        private decimal blueje = -1M;
        private AisinoPNL BuyerPan;
        private DataGridViewTextBoxEditingControl CellEdit;
        private AisinoCMB cmbSlv;
        private List<codeInfo> codeInfoList = new List<codeInfo>();
        private AisinoMultiCombox com_fhrmc;
        private AisinoMultiCombox com_fhrsh;
        private AisinoMultiCombox com_gfmc;
        private AisinoMultiCombox com_gfsbh;
        private AisinoMultiCombox com_shrmc;
        private AisinoMultiCombox com_shrsh;
        private AisinoMultiCombox com_xfmc;
        private AisinoMultiCombox com_xfsbh;
        private IContainer components;
        private CustomStyleDataGrid dataGridView1;
        private string fpdm;
        private string fphm;
        private FPLX fplx;
        private bool hsjbz;
        private Invoice inv;
        public static bool isFLBM = ((TaxCardFactory.CreateTaxCard().GetExtandParams("FLBMFlag") == "FLBM") && lpFLBM);
        private AisinoLBL lab_date;
        private AisinoLBL lab_fpdm;
        private AisinoLBL lab_fphm;
        private AisinoLBL lab_fpzl;
        private AisinoLBL lab_je;
        private AisinoLBL lab_jqbh;
        private AisinoLBL lab_kpy;
        private AisinoLBL lab_lxdh;
        private AisinoLBL lab_No;
        private AisinoLBL lab_se;
        private AisinoLBL lab_sqly;
        private static bool lpFLBM = true;
        public static string oldsh = TaxCardFactory.CreateTaxCard().OldTaxCode.Trim();
        private AisinoRDO Radio_BuyerSQ;
        private AisinoRDO Radio_BuyerSQ_Wdk;
        private AisinoRDO Radio_BuyerSQ_Wdk_1;
        private AisinoRDO Radio_BuyerSQ_Wdk_2;
        private AisinoRDO Radio_BuyerSQ_Wdk_3;
        private AisinoRDO Radio_BuyerSQ_Wdk_4;
        private AisinoRDO Radio_BuyerSQ_Ydk;
        private AisinoRDO Radio_SellerSQ;
        private AisinoRDO Radio_SellerSQ_1;
        private AisinoRDO Radio_SellerSQ_2;
        private AisinoRTX rtx_yshw;
        private List<object> SelectInfor = new List<object>();
        private AisinoPNL SellerPan;
        private AisinoMultiCombox spmcBt;
        private readonly IHZFPHY_SQD sqdDal = BLLFactory.CreateInstant<IHZFPHY_SQD>("HZFPHY_SQD");
        private readonly IHZFPHY_SQD_MX sqdMxDal = BLLFactory.CreateInstant<IHZFPHY_SQD_MX>("HZFPHY_SQD_MX");
        private InitSqdMxType SqdMxType = InitSqdMxType.Edit;
        private ToolStripButton tool_AddItem;
        private ToolStripButton tool_addRow;
        private ToolStripButton tool_bianji;
        private ToolStripButton tool_daying;
        private ToolStripButton tool_DeleteRow;
        private ToolStripButton tool_geshi;
        private ToolStripButton tool_hanshuiqiehuan;
        private ToolStripButton tool_tongji;
        private ToolStripButton tool_tuichu;
        private ToolStrip toolStrip;
        private ToolStripSeparator toolStripSeparator;
        private AisinoTXT txt_ccdw;
        private AisinoTXT txt_czch;
        private AisinoTXT txt_lxdh;
        private AisinoTXT txt_sqly;
        private XmlComponentLoader xmlComponentLoader1;

        public HySqdTianKai()
        {
            this.Initialize();
            this.com_gfmc.Edit=((EditStyle)1);
            this.com_gfmc.Edit=((EditStyle)1);
            this.com_xfmc.Edit=((EditStyle)1);
            this.com_xfsbh.Edit=((EditStyle)1);
            this.spmcBt = new AisinoMultiCombox();
            this.spmcBt.IsSelectAll=(true);
            this.spmcBt.Name = "SPMCBT";
            this.spmcBt.Text = "";
            this.spmcBt.Padding = new Padding(0);
            this.spmcBt.Margin = new Padding(0);
            this.dataGridView1.Controls.Add(this.spmcBt);
            this.spmcBt.Visible = false;
            this.spmcBt.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("名称", "MC"));
            this.dataGridView1.Columns["colJe"].HeaderText = this.hsjbz ? "金额(含税)" : "金额(不含税)";
            this.dataGridView1.Columns["colFyxm"].ReadOnly = true;
            this.dataGridView1.MultiSelect = false;
            this.spmcBt.ShowText=("MC");
            this.spmcBt.DrawHead=(true);
            this.spmcBt.AutoIndex=(1);
            this.spmcBt.AutoComplate=((AutoComplateStyle)2);
            this.spmcBt.OnAutoComplate+=(new EventHandler(this.spmcBt_OnAutoComplate));
            this.spmcBt.buttonStyle=(0);
            this.spmcBt.OnButtonClick+=(new EventHandler(this.spmcBt_Click));
            this.spmcBt.DoubleClick += new EventHandler(this.spmcBt_Click);
            this.spmcBt.OnTextChanged = (EventHandler) Delegate.Combine(this.spmcBt.OnTextChanged, new EventHandler(this.spmcBt_TextChanged));
            this.spmcBt.OnSelectValue+=(new EventHandler(this.spmcBt_OnSelectValue));
            this.spmcBt.PreviewKeyDown += new PreviewKeyDownEventHandler(this.spmcBt_PreviewKeyDown);
            this.spmcBt.Leave += new EventHandler(this.spmcBt_leave);
            this.cmbSlv.SelectedIndexChanged += new EventHandler(this.cmbSlvSelected);
            this.cmbSlv.Leave += new EventHandler(this.percentageCmbSlv_TextChanged);
            this.cmbSlv.KeyPress += new KeyPressEventHandler(this.percentageCmbSlv_KeyPress);
        }

        protected DataTable _SpmcOnAutoCompleteDataSource(CustomStyleDataGrid dataGrid, string spmc)
        {
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetFYXMMore", new object[] { spmc, 20, "MC,BM,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV,YHZCMC" });
            if ((objArray != null) && (objArray.Length > 0))
            {
                DataTable table = objArray[0] as DataTable;
                if (table.Rows.Count > 0)
                {
                    return table;
                }
            }
            return null;
        }

        private int AddRow(Spxx spxx)
        {
            if (this.dataGridView1.CurrentCell != null)
            {
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0].Cells["colFyxm"];
            }
            int num = -1;
            if (!this.inv.CanAddSpxx(1, false))
            {
                MessageManager.ShowMsgBox(this.inv.GetCode(), this.inv.Params);
                return num;
            }
            num = this.inv.AddSpxx(spxx);
            if (num != -1)
            {
                num = this.dataGridView1.Rows.Add();
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[num].Cells["colJe"];
                this.dataGridView1.EndEdit();
                this.lab_je.Text = "￥" + this.inv.GetHjJe();
                this.lab_se.Text = "￥" + this.inv.GetHjSe();
                this.codeInfoList.Add(new codeInfo());
                return num;
            }
            MessageManager.ShowMsgBox(this.inv.GetCode(), this.inv.Params);
            return num;
        }

        private void ClearForm()
        {
            this.lab_fpzl.Text = string.Empty;
            this.lab_fpdm.Text = string.Empty;
            this.lab_fphm.Text = string.Empty;
            this.com_gfmc.Edit=((EditStyle)1);
            this.com_gfmc.Edit=((EditStyle)1);
            this.com_gfmc.Edit=((EditStyle)1);
            this.com_gfmc.Edit=((EditStyle)1);
            this.com_gfmc.Text = string.Empty;
            this.com_gfsbh.Text = string.Empty;
            this.com_xfmc.Text = string.Empty;
            this.com_xfsbh.Text = string.Empty;
            this.dataGridView1.Rows.Clear();
            this.Radio_BuyerSQ.Checked = false;
            this.Radio_BuyerSQ_Ydk.Checked = false;
            this.Radio_BuyerSQ_Wdk.Checked = false;
            this.Radio_BuyerSQ_Wdk_1.Checked = false;
            this.Radio_BuyerSQ_Wdk_2.Checked = false;
            this.Radio_BuyerSQ_Wdk_3.Checked = false;
            this.Radio_BuyerSQ_Wdk_4.Checked = false;
            this.Radio_SellerSQ.Checked = false;
            this.Radio_SellerSQ_1.Checked = false;
            this.Radio_SellerSQ_2.Checked = false;
        }

        private void cmbSlvSelected(object sender, EventArgs e)
        {
            if (this.cmbSlv.SelectedValue == null)
            {
                this.cmbSlv.SelectedIndex = 0;
            }
            if (!string.IsNullOrEmpty(this.cmbSlv.SelectedValue.ToString()))
            {
                string str = this.cmbSlv.SelectedValue.ToString();
                for (int i = 0; i < this.inv.GetSpxxs().Count; i++)
                {
                    this.inv.SetSLv(i, str);
                }
                this.inv.SetFpSLv(str);
                this.lab_se.Text = "￥" + this.inv.GetHjSe();
            }
        }

        private void com_fhr_DropDown(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = (AisinoMultiCombox) sender;
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSFHR", new object[] { combox.Text, 0, "MC,SH" });
            if ((objArray != null) && (objArray.Length >= 2))
            {
                this.com_fhrmc.Text = objArray[0].ToString();
                this.com_fhrsh.Text = objArray[1].ToString();
                this.inv.Fhrmc=(objArray[0].ToString());
                this.inv.Fhrsh=(objArray[1].ToString());
            }
        }

        private void com_fhr_OnAutoComplate(object sender, EventArgs e)
        {
            string text = "";
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if ((combox != null) && combox.Name.Equals("com_fhrmc"))
            {
                text = this.com_fhrmc.Text;
            }
            else if ((combox != null) && combox.Name.Equals("com_fhrsh"))
            {
                text = this.com_fhrsh.Text;
            }
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSFHRMore", new object[] { text, 20, "MC,SH" });
            if ((objArray != null) && (objArray.Length > 0))
            {
                DataTable table = objArray[0] as DataTable;
                if ((combox != null) && (table != null))
                {
                    combox.DataSource=(table);
                }
            }
        }

        private void com_fhr_OnSelectValue(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if (combox != null)
            {
                Dictionary<string, string> dictionary = combox.SelectDict;
                this.com_fhrmc.Text = dictionary["MC"].ToString();
                this.com_fhrsh.Text = dictionary["SH"].ToString();
                this.inv.Fhrmc=(dictionary["MC"].ToString());
                this.inv.Fhrsh=(dictionary["SH"].ToString());
            }
        }

        private void com_fhrmc_TextChanged(object sender, EventArgs e)
        {
            string str = this.com_fhrmc.Text.Trim();
            this.inv.Fhrmc=(str);
            if (this.inv.Fhrmc != str)
            {
                this.com_fhrmc.Text = this.inv.Fhrmc;
                this.com_fhrmc.SelectionStart=(this.com_fhrmc.Text.Length);
            }
        }

        private void com_fhrsh_TextChanged(object sender, EventArgs e)
        {
            string str = this.com_fhrsh.Text.Trim();
            this.inv.Fhrsh=(str);
            if (this.inv.Fhrsh != str)
            {
                this.com_fhrsh.Text = this.inv.Fhrsh;
                this.com_fhrsh.SelectionStart=(this.com_fhrsh.Text.Length);
            }
        }

        private void com_gf_DropDown(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = (AisinoMultiCombox) sender;
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSFHR", new object[] { combox.Text, 0 });
            if ((objArray != null) && (objArray.Length >= 2))
            {
                this.com_gfmc.Text = objArray[0].ToString();
                this.com_gfsbh.Text = objArray[1].ToString();
                this.inv.Gfmc=(objArray[0].ToString());
                this.inv.Gfsh=(objArray[1].ToString());
            }
        }

        private void com_gfmc_TextChanged(object sender, EventArgs e)
        {
            string str = this.com_gfmc.Text.Trim();
            this.inv.Gfmc=(str);
            if (this.inv.Gfmc != str)
            {
                this.com_gfmc.Text = this.inv.Gfmc;
                this.com_gfmc.SelectionStart=(this.com_gfmc.Text.Length);
            }
        }

        private void com_gfsbh_OnAutoComplate(object sender, EventArgs e)
        {
            string text = "";
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if ((combox != null) && combox.Name.Equals("com_gfmc"))
            {
                text = this.com_gfmc.Text;
            }
            else if ((combox != null) && combox.Name.Equals("com_gfsbh"))
            {
                text = this.com_gfsbh.Text;
            }
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSFHRMore", new object[] { text, 20, "MC,SH" });
            if ((objArray != null) && (objArray.Length > 0))
            {
                DataTable table = objArray[0] as DataTable;
                if ((combox != null) && (table != null))
                {
                    combox.DataSource=(table);
                }
            }
        }

        private void com_gfsbh_OnSelectValue(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if (combox != null)
            {
                Dictionary<string, string> dictionary = combox.SelectDict;
                this.com_gfmc.Text = dictionary["MC"].ToString();
                this.com_gfsbh.Text = dictionary["SH"].ToString();
                this.inv.Gfmc=(dictionary["MC"].ToString());
                this.inv.Gfsh=(dictionary["SH"].ToString());
            }
        }

        private void com_gfsbh_TextChanged(object sender, EventArgs e)
        {
            string str = this.com_gfsbh.Text.Trim();
            this.inv.Gfsh=(str);
            if (this.inv.Gfsh != str)
            {
                this.com_gfsbh.Text = this.inv.Gfsh;
                this.com_gfsbh.SelectionStart=(this.com_gfsbh.Text.Length);
            }
        }

        private void com_shr_DropDown(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = (AisinoMultiCombox) sender;
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSFHR", new object[] { combox.Text, 0, "MC,SH" });
            if ((objArray != null) && (objArray.Length >= 2))
            {
                this.com_shrmc.Text = objArray[0].ToString();
                this.com_shrsh.Text = objArray[1].ToString();
                this.inv.Shrmc=(objArray[0].ToString());
                this.inv.Shrsh=(objArray[1].ToString());
            }
        }

        private void com_shr_OnAutoComplate(object sender, EventArgs e)
        {
            string text = "";
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if ((combox != null) && combox.Name.Equals("com_shrmc"))
            {
                text = this.com_shrmc.Text;
            }
            else if ((combox != null) && combox.Name.Equals("com_shrsh"))
            {
                text = this.com_shrsh.Text;
            }
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSFHRMore", new object[] { text, 20, "MC,SH" });
            if ((objArray != null) && (objArray.Length > 0))
            {
                DataTable table = objArray[0] as DataTable;
                if ((combox != null) && (table != null))
                {
                    combox.DataSource=(table);
                }
            }
        }

        private void com_shr_OnSelectValue(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if (combox != null)
            {
                Dictionary<string, string> dictionary = combox.SelectDict;
                this.com_shrmc.Text = dictionary["MC"].ToString();
                this.com_shrsh.Text = dictionary["SH"].ToString();
                this.inv.Shrmc=(dictionary["MC"].ToString());
                this.inv.Shrsh=(dictionary["SH"].ToString());
            }
        }

        private void com_shrmc_TextChanged(object sender, EventArgs e)
        {
            string str = this.com_shrmc.Text.Trim();
            this.inv.Shrmc=(str);
            if (this.inv.Shrmc != str)
            {
                this.com_shrmc.Text = this.inv.Shrmc;
                this.com_shrmc.SelectionStart=(this.com_shrmc.Text.Length);
            }
        }

        private void com_shrsh_TextChanged(object sender, EventArgs e)
        {
            string str = this.com_shrsh.Text.Trim();
            this.inv.Shrsh=(str);
            if (this.inv.Shrsh!= str)
            {
                this.com_shrsh.Text = this.inv.Shrsh;
                this.com_shrsh.SelectionStart=(this.com_shrsh.Text.Length);
            }
        }

        private void com_xf_DropDown(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = (AisinoMultiCombox) sender;
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSFHR", new object[] { combox.Text, 0, "MC,SH" });
            if ((objArray != null) && (objArray.Length >= 2))
            {
                this.com_xfmc.Text = objArray[0].ToString();
                this.com_xfsbh.Text = objArray[1].ToString();
                this.inv.Xfmc=(objArray[0].ToString());
                this.inv.Xfsh=(objArray[1].ToString());
            }
        }

        private void com_xfmc_TextChanged(object sender, EventArgs e)
        {
            string str = this.com_xfmc.Text.Trim();
            this.inv.Xfmc=(str);
            if (this.inv.Xfmc != str)
            {
                this.com_xfmc.Text = this.inv.Xfmc;
                this.com_xfmc.SelectionStart=(this.com_xfmc.Text.Length);
            }
        }

        private void com_xfsbh_OnAutoComplate(object sender, EventArgs e)
        {
            string text = "";
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if ((combox != null) && combox.Name.Equals("com_xfmc"))
            {
                text = this.com_xfmc.Text;
            }
            else if ((combox != null) && combox.Name.Equals("com_xfsbh"))
            {
                text = this.com_xfsbh.Text;
            }
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSFHRMore", new object[] { text, 20, "MC,SH" });
            if ((objArray != null) && (objArray.Length > 0))
            {
                DataTable table = objArray[0] as DataTable;
                if ((combox != null) && (table != null))
                {
                    combox.DataSource=(table);
                }
            }
        }

        private void com_xfsbh_OnSelectValue(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if (combox != null)
            {
                Dictionary<string, string> dictionary = combox.SelectDict;
                this.com_xfmc.Text = dictionary["MC"].ToString();
                this.com_xfsbh.Text = dictionary["SH"].ToString();
                this.inv.Xfmc=(dictionary["MC"].ToString());
                this.inv.Xfsh=(dictionary["SH"].ToString());
            }
        }

        private void com_xfsbh_TextChanged(object sender, EventArgs e)
        {
            string str = this.com_xfsbh.Text.Trim();
            this.inv.Xfsh=(str);
            if (this.inv.Xfsh != str)
            {
                this.com_xfsbh.Text = this.inv.Xfsh;
                this.com_xfsbh.SelectionStart=(this.com_xfsbh.Text.Length);
            }
        }

        private void CommitEditGrid()
        {
            this.dataGridView1.EndEdit();
        }

        private static byte[] CreateInvoiceTmp()
        {
            byte[] sourceArray = Invoice.TypeByte;
            byte[] destinationArray = new byte[0x20];
            Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
            byte[] buffer3 = new byte[0x10];
            Array.Copy(sourceArray, 0x20, buffer3, 0, 0x10);
            return AES_Crypt.Encrypt(Encoding.Unicode.GetBytes("KP" + DateTime.Now.ToString("F")), destinationArray, buffer3);
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            this.tool_bianji.Enabled = false;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.spmcBt.Visible)
            {
                this.spmcBt.Visible = false;
            }
            this.tool_bianji.Enabled = true;
            this.tool_daying.Enabled = true;
            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;
            object obj2 = this.dataGridView1.Rows[rowIndex].Cells[columnIndex].Value;
            string str = (obj2 == null) ? "" : obj2.ToString();
            bool flag = false;
            switch (columnIndex)
            {
                case 0:
                    flag = this.inv.SetSpmc(rowIndex, str);
                    break;

                case 1:
                    flag = this.inv.SetJe(rowIndex, str);
                    break;
            }
            if (!flag)
            {
                MessageManager.ShowMsgBox(this.inv.GetCode(), this.inv.Params);
            }
            this.ShowDataGrid(this.inv.GetSpxx(rowIndex), rowIndex);
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if ((e.FormattedValue.ToString() != string.Empty) && (this.dataGridView1.CurrentCell.OwningColumn.Name == "colJe"))
            {
                this.dataGridView1.Rows[e.RowIndex].ErrorText = "";
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            CustomStyleDataGrid grid = (CustomStyleDataGrid) sender;
            AisinoMultiCombox combox = grid.Controls["SPMCBT"] as AisinoMultiCombox;
            if ((grid.CurrentCell != null) && !grid.CurrentRow.ReadOnly)
            {
                DataGridViewColumn owningColumn = grid.CurrentCell.OwningColumn;
                if (!owningColumn.Name.Equals("colFyxm"))
                {
                    combox.Visible = false;
                }
                else
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
                        object obj2 = grid.CurrentCell.Value;
                        combox.Text = (obj2 == null) ? "" : obj2.ToString();
                        DataTable table = combox.DataSource;
                        if (table != null)
                        {
                            table.Clear();
                        }
                        combox.Visible = true;
                        combox.Focus();
                    }
                }
            }
            else if ((combox != null) && combox.Visible)
            {
                combox.Visible = false;
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void dataGridView1_EditingControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            string name = this.dataGridView1.CurrentCell.OwningColumn.Name;
            if (name.Equals("colJe"))
            {
                if (e.KeyChar.ToString() == "\b")
                {
                    e.Handled = false;
                    return;
                }
                if (e.KeyChar.ToString() == ".")
                {
                    DataGridViewTextBoxEditingControl control = (DataGridViewTextBoxEditingControl) sender;
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
                if ((char.IsDigit(e.KeyChar) && (e.KeyChar >= 0xff10)) && (e.KeyChar <= 0xff19))
                {
                    e.KeyChar = (char) (e.KeyChar - 0xfee0);
                }
            }
            name.Equals("colFyxm");
        }

        private void dataGridView1_EditingControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            this.CellEdit = (DataGridViewTextBoxEditingControl) e.Control;
            this.CellEdit.KeyPress += new KeyPressEventHandler(this.dataGridView1_EditingControl_KeyPress);
            this.CellEdit.PreviewKeyDown += new PreviewKeyDownEventHandler(this.dataGridView1_EditingControl_PreviewKeyDown);
        }

        private void dataGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            int index = this.dataGridView1.CurrentRow.Index;
            int columnIndex = this.dataGridView1.CurrentCell.ColumnIndex;
            int keyValue = e.KeyValue;
            int count = this.dataGridView1.Rows.Count;
            int num5 = this.dataGridView1.Columns.Count;
            if (((keyValue == 40) && (index == (count - 1))) && (this.SqdMxType != InitSqdMxType.Read))
            {
                Spxx spxx = new Spxx("", "", this.inv.SLv, "", "", "", false, this.inv.Zyfplx);
                this.AddRow(spxx);
            }
            if (keyValue == 13)
            {
                if (columnIndex < (num5 - 1))
                {
                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[index].Cells[columnIndex + 1];
                }
                else if ((index == (count - 1)) && (this.SqdMxType != InitSqdMxType.Read))
                {
                    Spxx spxx2 = new Spxx("", "", this.inv.SLv, "", "", "", false, this.inv.Zyfplx);
                    this.AddRow(spxx2);
                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[count - 1].Cells[0];
                }
                else
                {
                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[index + 1].Cells[0];
                }
            }
        }

        private void dataGridView1_RowColumnWidthChanged(object sender, EventArgs e)
        {
            this.dataGridView1_CurrentCellChanged(sender, e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private string Fraction2Percentage(string fraction)
        {
            if (fraction.IndexOf('%') > 0)
            {
                return fraction;
            }
            string str = ((Convert.ToDouble(fraction) * 100.0)).ToString() + "%";
            if (str.Equals("0%"))
            {
                return "免税";
            }
            return str;
        }

        private List<string> Fraction2Percentage_Arr(string[] fractionArr)
        {
            List<string> list = new List<string>();
            foreach (string str in fractionArr)
            {
                list.Add(this.Fraction2Percentage(str));
            }
            return list;
        }

        private string GetSelectReason()
        {
            string str = "";
            if (this.Radio_BuyerSQ.Checked)
            {
                str = "1";
                if (this.Radio_BuyerSQ_Ydk.Checked)
                {
                    return (str + "100000000");
                }
                str = str + "0";
                if (!this.Radio_BuyerSQ_Wdk.Checked)
                {
                    return (str + "00000000");
                }
                str = str + "1";
                if (this.Radio_BuyerSQ_Wdk_1.Checked)
                {
                    str = str + "1";
                }
                else
                {
                    str = str + "0";
                }
                if (this.Radio_BuyerSQ_Wdk_2.Checked)
                {
                    str = str + "1";
                }
                else
                {
                    str = str + "0";
                }
                if (this.Radio_BuyerSQ_Wdk_3.Checked)
                {
                    str = str + "1";
                }
                else
                {
                    str = str + "0";
                }
                if (this.Radio_BuyerSQ_Wdk_4.Checked)
                {
                    str = str + "1";
                }
                else
                {
                    str = str + "0";
                }
                return (str + "000");
            }
            str = "0000000";
            if (this.Radio_SellerSQ.Checked)
            {
                str = str + "1";
                if (this.Radio_SellerSQ_1.Checked)
                {
                    str = str + "1";
                }
                else
                {
                    str = str + "0";
                }
                if (this.Radio_SellerSQ_2.Checked)
                {
                    return (str + "1");
                }
                return (str + "0");
            }
            return (str + "000");
        }

        private void GetSqdInfo(string sqd)
        {
            if (!string.IsNullOrEmpty(sqd))
            {
                HZFPHY_SQD hzfphy_sqd = this.sqdDal.Select(sqd);
                if (hzfphy_sqd != null)
                {
                    this.SetSelectReason(hzfphy_sqd.SQXZ);
                    if (this.Radio_BuyerSQ.Checked)
                    {
                        Invoice.IsGfSqdFp_Static=(true);
                    }
                    else
                    {
                        Invoice.IsGfSqdFp_Static=(false);
                    }
                    byte[] buffer = CreateInvoiceTmp();
                    this.inv = new Invoice(true, false, false, (hzfphy_sqd.FPZL == "f") ? ((FPLX) 11) : ((FPLX) 2), buffer, null);
                    if (this.Radio_BuyerSQ.Checked)
                    {
                        this.inv.IsGfSqdFp=(true);
                    }
                    else
                    {
                        this.inv.IsXfSqdFp=(true);
                    }
                    this.inv.SetZyfpLx(0);
                    this.lab_No.Text = sqd;
                    this.lab_kpy.Text = hzfphy_sqd.JBR;
                    this.lab_date.Text = hzfphy_sqd.TKRQ.ToString("yyyy/MM/dd");
                    this.com_gfmc.Text = hzfphy_sqd.GFMC;
                    this.com_gfsbh.Text = hzfphy_sqd.GFSH;
                    this.inv.Gfmc=(hzfphy_sqd.GFMC);
                    this.inv.Gfsh=(hzfphy_sqd.GFSH);
                    this.com_xfmc.Text = hzfphy_sqd.XFMC;
                    this.com_xfsbh.Text = hzfphy_sqd.XFSH;
                    this.inv.Xfmc=(hzfphy_sqd.XFMC);
                    this.inv.Xfsh=(hzfphy_sqd.XFSH);
                    this.com_shrmc.Text = hzfphy_sqd.SHFMC;
                    this.com_shrsh.Text = hzfphy_sqd.SHFSH;
                    this.com_fhrmc.Text = hzfphy_sqd.FHFMC;
                    this.com_fhrsh.Text = hzfphy_sqd.FHFSH;
                    this.inv.Shrmc=(hzfphy_sqd.SHFMC);
                    this.inv.Shrsh=(hzfphy_sqd.SHFSH);
                    this.inv.Fhrmc=(hzfphy_sqd.FHFMC);
                    this.inv.Fhrsh=(hzfphy_sqd.FHFSH);
                    this.lab_jqbh.Text = hzfphy_sqd.JQBH.ToString();
                    this.rtx_yshw.Text = hzfphy_sqd.YSHWXX.ToString();
                    this.txt_ccdw.Text = hzfphy_sqd.CCDW.ToString();
                    this.txt_czch.Text = hzfphy_sqd.CZCH.ToString();
                    this.txt_sqly.Text = hzfphy_sqd.SQLY;
                    this.txt_lxdh.Text = hzfphy_sqd.SQRDH;
                    this.lab_fpdm.Text = hzfphy_sqd.FPDM;
                    this.lab_fphm.Text = hzfphy_sqd.FPHM;
                    this.lab_fpzl.Text = (hzfphy_sqd.FPZL == "f") ? "货物运输业增值税专用发票" : "普通发票";
                    string sQXZ = hzfphy_sqd.SQXZ;
                    this.inv.SetFpSLv(string.Format("{0:0.00}", hzfphy_sqd.SL));
                    this.inv.Fpdm=(hzfphy_sqd.FPDM);
                    this.inv.Fphm=(hzfphy_sqd.FPHM);
                    object[] objArray = new object[] { "f", hzfphy_sqd.FPDM, Convert.ToInt32(hzfphy_sqd.FPHM) };
                    Fpxx fpxx = ServiceFactory.InvokePubService("Aisino.Fwkp.QueryFPXX", objArray)[0] as Fpxx;
                    if ((fpxx != null) && this.Radio_SellerSQ.Checked)
                    {
                        string hjJe = new Invoice(false, fpxx, CreateInvoiceTmp(), null).GetHjJe();
                        this.blueje = Math.Abs(Convert.ToDecimal(hjJe));
                    }
                    DataTable table = this.sqdMxDal.SelectList(this.sqdh);
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        DataRow row = table.Rows[i];
                        Spxx spxx = new Spxx("", "", this.inv.SLv);
                        spxx.Je=(GetSafeData.ValidateValue<decimal>(row, "JE").ToString());
                        spxx.Spmc=(GetSafeData.ValidateValue<string>(row, "SPMC").ToString());
                        spxx.Se=(GetSafeData.ValidateValue<decimal>(row, "SE").ToString("0.00"));
                        if ((isFLBM && (GetSafeData.ValidateValue<string>(row, "FLBM") != "")) && (GetSafeData.ValidateValue<string>(row, "FLBM") != null))
                        {
                            this.codeInfoList.Add(new codeInfo());
                            this.codeInfoList[i].spbm = GetSafeData.ValidateValue<string>(row, "QYSPBM").ToString();
                            this.codeInfoList[i].flbm = GetSafeData.ValidateValue<string>(row, "FLBM").ToString();
                            this.codeInfoList[i].sfxsyhzc = GetSafeData.ValidateValue<string>(row, "SFXSYHZC").ToString();
                            this.codeInfoList[i].yhzcmc = GetSafeData.ValidateValue<string>(row, "YHZCMC").ToString();
                            this.codeInfoList[i].yhzcsl = "";
                            this.codeInfoList[i].lslbs = GetSafeData.ValidateValue<string>(row, "LSLBS").ToString();
                            spxx.Flbm=(GetSafeData.ValidateValue<string>(row, "FLBM").ToString());
                            spxx.Xsyh=(GetSafeData.ValidateValue<string>(row, "SFXSYHZC").ToString());
                            spxx.Yhsm=(GetSafeData.ValidateValue<string>(row, "YHZCMC").ToString());
                        }
                        if (this.AddRow(spxx) < 0)
                        {
                            break;
                        }
                        this.ShowDataGrid(this.inv.GetSpxx(i), i);
                    }
                    this.inv.Hsjbz=(this.hsjbz);
                    this.lab_je.Text = "￥" + hzfphy_sqd.HJJE.ToString("0.00");
                    this.lab_se.Text = "￥" + hzfphy_sqd.HJSE.ToString("0.00");
                }
            }
        }

        private void GetXfInfo(string fpzl, string fpdm, string fphm)
        {
            object[] objArray = new object[] { "f", fpdm, Convert.ToInt32(fphm) };
            Fpxx fpxx = ServiceFactory.InvokePubService("Aisino.Fwkp.QueryFPXX", objArray)[0] as Fpxx;
            if (fpxx != null)
            {
                byte[] buffer = CreateInvoiceTmp();
                Invoice.IsGfSqdFp_Static=(false);
                Invoice invoice = new Invoice(false, fpxx, buffer, null);
                this.inv = invoice.GetRedInvoice(this.hsjbz);
                if ((this.inv == null) || (invoice.GetCode() != "0000"))
                {
                    MessageManager.ShowMsgBox(invoice.GetCode(), invoice.Params);
                    this.blueInv_error = true;
                }
                else
                {
                    this.inv.IsXfSqdFp=(true);
                    this.ResetPercentageCmbSlv();
                    this.blueje = Math.Abs(Convert.ToDecimal(this.inv.GetHjJe()));
                    this.SetHsbz();
                    string str = "Aisino.Fwkp.Invoice" + invoice.Fpdm + invoice.Fphm;
                    byte[] bytes = Encoding.Unicode.GetBytes(MD5_Crypt.GetHashStr(str));
                    byte[] destinationArray = new byte[0x20];
                    Array.Copy(bytes, 0, destinationArray, 0, 0x20);
                    byte[] buffer4 = new byte[0x10];
                    Array.Copy(bytes, 0x20, buffer4, 0, 0x10);
                    byte[] buffer5 = AES_Crypt.Decrypt(Convert.FromBase64String(this.inv.Gfmc), destinationArray, buffer4, null);
                    this.com_gfmc.Text = (buffer5 == null) ? this.inv.Gfmc : Encoding.Unicode.GetString(buffer5);
                    this.com_gfsbh.Text = this.inv.Gfsh;
                    TaxCard card = TaxCardFactory.CreateTaxCard();
                    this.com_xfmc.Text = card.Corporation;
                    string str2 = card.OldTaxCode;
                    if (this.inv.Xfsh == str2.Trim())
                    {
                        this.com_xfsbh.Text = card.TaxCode;
                    }
                    else
                    {
                        this.com_xfsbh.Text = this.inv.Xfsh;
                    }
                    this.com_fhrmc.Text = this.inv.Fhrmc;
                    this.com_fhrsh.Text = this.inv.Fhrsh;
                    this.com_shrmc.Text = this.inv.Shrmc;
                    this.com_shrsh.Text = this.inv.Shrsh;
                    this.txt_ccdw.Text = this.inv.Ccdw;
                    this.txt_czch.Text = this.inv.Czch;
                    this.rtx_yshw.Text = this.inv.Yshwxx;
                    this.lab_je.Text = this.inv.GetHjJe();
                    this.lab_se.Text = this.inv.GetHjSe();
                    List<Dictionary<SPXX, string>> spxxs = this.inv.GetSpxxs();
                    if (spxxs.Count <= 0)
                    {
                        Spxx spxx = new Spxx("", "", this.cmbSlv.SelectedValue.ToString());
                        this.AddRow(spxx);
                    }
                    else
                    {
                        for (int i = 0; i < spxxs.Count; i++)
                        {
                            this.dataGridView1.Rows.Add();
                            Dictionary<SPXX, string> dictionary = spxxs[i];
                            if (isFLBM)
                            {
                                this.codeInfoList.Add(new codeInfo());
                                this.codeInfoList[i].spbm = dictionary[(SPXX)1];
                                if (dictionary[(SPXX)20].Trim() == "")
                                {
                                    lpFLBM = false;
                                    isFLBM = isFLBM && lpFLBM;
                                }
                                this.codeInfoList[i].flbm = dictionary[(SPXX)20];
                                this.codeInfoList[i].sfxsyhzc = dictionary[(SPXX)0x15];
                                this.codeInfoList[i].yhzcmc = dictionary[(SPXX)0x16];
                                this.codeInfoList[i].yhzcsl = "";
                                this.codeInfoList[i].lslbs = dictionary[(SPXX)0x17];
                            }
                            this.ShowDataGrid(dictionary, i);
                        }
                    }
                }
            }
            else
            {
                this.com_gfmc.Text = "";
                this.com_gfsbh.Text = "";
                this.com_xfmc.Text = base.TaxCardInstance.Corporation;
                this.com_xfsbh.Text = base.TaxCardInstance.TaxCode;
                byte[] buffer6 = CreateInvoiceTmp();
                Invoice.IsGfSqdFp_Static=(false);
                this.inv = new Invoice(true, false, false, this.fplx, buffer6, null);
                this.inv.IsXfSqdFp=(true);
                this.ResetPercentageCmbSlv();
                this.inv.Xfmc=(base.TaxCardInstance.Corporation);
                this.inv.Xfsh=(base.TaxCardInstance.TaxCode);
                Spxx spxx2 = new Spxx("", "", this.cmbSlv.SelectedValue.ToString());
                this.AddRow(spxx2);
            }
        }

        private string GetYYSBZ(Fpxx fpxx)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append('0', 10);
            return builder.ToString();
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.Radio_BuyerSQ = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_BuyerSQ");
            this.Radio_SellerSQ = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_SellerSQ");
            this.Radio_BuyerSQ_Ydk = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_BuyerSQ_Ydk");
            this.Radio_BuyerSQ_Wdk = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_BuyerSQ_Wdk");
            this.Radio_SellerSQ_1 = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_SellerSQ_1");
            this.Radio_SellerSQ_2 = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_SellerSQ_2");
            this.Radio_BuyerSQ_Wdk_1 = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_BuyerSQ_Wdk_1");
            this.Radio_BuyerSQ_Wdk_2 = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_BuyerSQ_Wdk_2");
            this.Radio_BuyerSQ_Wdk_3 = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_BuyerSQ_Wdk_3");
            this.Radio_BuyerSQ_Wdk_4 = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("Radio_BuyerSQ_Wdk_4");
            this.toolStrip = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip2");
            this.tool_daying = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_daying");
            this.tool_tongji = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_tongji");
            this.tool_geshi = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_geshi");
            this.tool_tuichu = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_tuichu");
            this.tool_bianji = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_bianji");
            this.tool_hanshuiqiehuan = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_hanshuiqiehuan");
            this.tool_addRow = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_addRow");
            this.tool_DeleteRow = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_DeleteRow");
            this.tool_AddItem = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_AddItem");
            this.tool_AddItem.Visible = false;
            this.toolStripSeparator = this.xmlComponentLoader1.GetControlByName<ToolStripSeparator>("toolStripSeparator3");
            this.tool_bianji.Visible = false;
            this.tool_tuichu.Margin = new Padding(20, 1, 0, 2);
            ControlStyleUtil.SetToolStripStyle(this.toolStrip);
            this.lab_fpzl = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_fpzl");
            this.lab_fphm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_fphm");
            this.lab_fpdm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_fpdm");
            this.lab_je = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblHjje");
            this.lab_se = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblHjse");
            this.lab_jqbh = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJqbh");
            this.txt_sqly = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_sqly");
            this.txt_lxdh = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_lxdh");
            this.rtx_yshw = this.xmlComponentLoader1.GetControlByName<AisinoRTX>("rtxtYshw");
            this.txt_czch = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtCzch");
            this.txt_ccdw = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtCcdw");
            this.rtx_yshw.TextChanged += new EventHandler(this.rtx_yshw_TextChanged);
            this.txt_czch.TextChanged += new EventHandler(this.txt_czch_TextChanged);
            this.txt_ccdw.TextChanged += new EventHandler(this.txt_ccdw_TextChanged);
            this.dataGridView1 = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("dgFyxm");
            this.cmbSlv = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbSlv");
            this.lab_No = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_No");
            this.lab_kpy = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_kpy");
            this.lab_date = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_date");
            this.com_gfsbh = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("com_gfsbh");
            this.com_gfsbh.IsSelectAll=(true);
            this.com_gfsbh.buttonStyle=(0);
            this.com_gfsbh.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            this.com_gfsbh.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.com_gfsbh.Width - 140));
            this.com_gfsbh.ShowText=("SH");
            this.com_gfsbh.DrawHead=(false);
            this.com_gfmc = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("com_gfmc");
            this.com_gfmc.IsSelectAll=(true);
            this.com_gfmc.buttonStyle=(0);
            this.com_gfmc.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            this.com_gfmc.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.com_gfmc.Width - 140));
            this.com_gfmc.ShowText=("MC");
            this.com_gfmc.DrawHead=(false);
            this.com_xfsbh = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("com_xfsbh");
            this.com_xfsbh.IsSelectAll=(true);
            this.com_xfsbh.buttonStyle=(0);
            this.com_xfsbh.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            this.com_xfsbh.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.com_xfsbh.Width - 140));
            this.com_xfsbh.ShowText=("SH");
            this.com_xfsbh.DrawHead=(false);
            this.com_xfmc = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("com_xfmc");
            this.com_xfmc.IsSelectAll=(true);
            this.com_xfmc.buttonStyle=(0);
            this.com_xfmc.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            this.com_xfmc.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.com_xfmc.Width - 140));
            this.com_xfmc.ShowText=("MC");
            this.com_xfmc.DrawHead=(false);
            this.com_shrsh = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("com_shrsh");
            this.com_shrsh.IsSelectAll=(true);
            this.com_shrsh.buttonStyle=(0);
            this.com_shrsh.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            this.com_shrsh.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.com_shrsh.Width - 140));
            this.com_shrsh.ShowText=("SH");
            this.com_shrsh.DrawHead=(false);
            this.com_shrmc = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("com_shrmc");
            this.com_shrmc.IsSelectAll=(true);
            this.com_shrmc.buttonStyle=(0);
            this.com_shrmc.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            this.com_shrmc.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.com_shrmc.Width - 140));
            this.com_shrmc.ShowText=("MC");
            this.com_shrmc.DrawHead=(false);
            this.com_fhrsh = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("com_fhrsh");
            this.com_fhrsh.IsSelectAll=(true);
            this.com_fhrsh.buttonStyle=(0);
            this.com_fhrsh.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            this.com_fhrsh.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.com_fhrsh.Width - 140));
            this.com_fhrsh.ShowText=("SH");
            this.com_fhrsh.DrawHead=(false);
            this.com_fhrmc = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("com_fhrmc");
            this.com_fhrmc.IsSelectAll=(true);
            this.com_fhrmc.buttonStyle=(0);
            this.com_fhrmc.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            this.com_fhrmc.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.com_fhrmc.Width - 140));
            this.com_fhrmc.ShowText=("MC");
            this.com_fhrmc.DrawHead=(false);
            this.com_gfsbh.AutoIndex=(1);
            this.com_gfsbh.OnButtonClick+=(new EventHandler(this.com_gf_DropDown));
            this.com_gfsbh.AutoComplate=((AutoComplateStyle)2);
            this.com_gfsbh.OnAutoComplate+=(new EventHandler(this.com_gfsbh_OnAutoComplate));
            this.com_gfsbh.OnSelectValue+=(new EventHandler(this.com_gfsbh_OnSelectValue));
            this.com_gfmc.AutoIndex=(1);
            this.com_gfmc.OnButtonClick+=(new EventHandler(this.com_gf_DropDown));
            this.com_gfmc.AutoComplate=((AutoComplateStyle)2);
            this.com_gfmc.OnAutoComplate+=(new EventHandler(this.com_gfsbh_OnAutoComplate));
            this.com_gfmc.OnSelectValue+=(new EventHandler(this.com_gfsbh_OnSelectValue));
            this.com_xfsbh.AutoIndex=(1);
            this.com_xfsbh.OnButtonClick+=(new EventHandler(this.com_xf_DropDown));
            this.com_xfsbh.AutoComplate=((AutoComplateStyle)2);
            this.com_xfsbh.OnAutoComplate+=(new EventHandler(this.com_xfsbh_OnAutoComplate));
            this.com_xfsbh.OnSelectValue+=(new EventHandler(this.com_xfsbh_OnSelectValue));
            this.com_xfmc.AutoIndex=(1);
            this.com_xfmc.OnButtonClick+=(new EventHandler(this.com_xf_DropDown));
            this.com_xfmc.AutoComplate=((AutoComplateStyle)2);
            this.com_xfmc.OnAutoComplate+=(new EventHandler(this.com_xfsbh_OnAutoComplate));
            this.com_xfmc.OnSelectValue+=(new EventHandler(this.com_xfsbh_OnSelectValue));
            this.com_shrsh.AutoIndex=(1);
            this.com_shrsh.OnButtonClick+=(new EventHandler(this.com_shr_DropDown));
            this.com_shrsh.AutoComplate=((AutoComplateStyle)2);
            this.com_shrsh.OnAutoComplate+=(new EventHandler(this.com_shr_OnAutoComplate));
            this.com_shrsh.OnSelectValue+=(new EventHandler(this.com_shr_OnSelectValue));
            this.com_shrmc.AutoIndex=(1);
            this.com_shrmc.OnButtonClick+=(new EventHandler(this.com_shr_DropDown));
            this.com_shrmc.AutoComplate=((AutoComplateStyle)2);
            this.com_shrmc.OnAutoComplate+=(new EventHandler(this.com_shr_OnAutoComplate));
            this.com_shrmc.OnSelectValue+=(new EventHandler(this.com_shr_OnSelectValue));
            this.com_fhrsh.AutoIndex=(1);
            this.com_fhrsh.OnButtonClick+=(new EventHandler(this.com_fhr_DropDown));
            this.com_fhrsh.AutoComplate=((AutoComplateStyle)2);
            this.com_fhrsh.OnAutoComplate+=(new EventHandler(this.com_fhr_OnAutoComplate));
            this.com_fhrsh.OnSelectValue+=(new EventHandler(this.com_fhr_OnSelectValue));
            this.com_fhrmc.AutoIndex=(1);
            this.com_fhrmc.OnButtonClick+=(new EventHandler(this.com_fhr_DropDown));
            this.com_fhrmc.AutoComplate=((AutoComplateStyle)2);
            this.com_fhrmc.OnAutoComplate+=(new EventHandler(this.com_fhr_OnAutoComplate));
            this.com_fhrmc.OnSelectValue+=(new EventHandler(this.com_fhr_OnSelectValue));
            this.com_gfmc.OnTextChanged = (EventHandler) Delegate.Combine(this.com_gfmc.OnTextChanged, new EventHandler(this.com_gfmc_TextChanged));
            this.com_gfsbh.OnTextChanged = (EventHandler) Delegate.Combine(this.com_gfsbh.OnTextChanged, new EventHandler(this.com_gfsbh_TextChanged));
            this.com_xfmc.OnTextChanged = (EventHandler) Delegate.Combine(this.com_xfmc.OnTextChanged, new EventHandler(this.com_xfmc_TextChanged));
            this.com_xfsbh.OnTextChanged = (EventHandler) Delegate.Combine(this.com_xfsbh.OnTextChanged, new EventHandler(this.com_xfsbh_TextChanged));
            this.com_fhrmc.OnTextChanged = (EventHandler) Delegate.Combine(this.com_fhrmc.OnTextChanged, new EventHandler(this.com_fhrmc_TextChanged));
            this.com_fhrsh.OnTextChanged = (EventHandler) Delegate.Combine(this.com_fhrsh.OnTextChanged, new EventHandler(this.com_fhrsh_TextChanged));
            this.com_shrmc.OnTextChanged = (EventHandler) Delegate.Combine(this.com_shrmc.OnTextChanged, new EventHandler(this.com_shrmc_TextChanged));
            this.com_shrsh.OnTextChanged = (EventHandler) Delegate.Combine(this.com_shrsh.OnTextChanged, new EventHandler(this.com_shrsh_TextChanged));
            this.com_gfsbh.KeyPress += new KeyPressEventHandler(this.KeyPressToUpper);
            this.com_xfsbh.KeyPress += new KeyPressEventHandler(this.KeyPressToUpper);
            this.com_fhrsh.KeyPress += new KeyPressEventHandler(this.KeyPressToUpper);
            this.com_shrsh.KeyPress += new KeyPressEventHandler(this.KeyPressToUpper);
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows=(false);
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.GridStyle=((CustomStyle)1);
            this.Radio_BuyerSQ.Enabled = false;
            this.Radio_BuyerSQ_Ydk.Enabled = false;
            this.Radio_BuyerSQ_Wdk.Enabled = false;
            this.Radio_BuyerSQ_Wdk_1.Enabled = false;
            this.Radio_BuyerSQ_Wdk_2.Enabled = false;
            this.Radio_BuyerSQ_Wdk_3.Enabled = false;
            this.Radio_BuyerSQ_Wdk_4.Enabled = false;
            this.Radio_SellerSQ.Enabled = false;
            this.Radio_SellerSQ_1.Enabled = false;
            this.Radio_SellerSQ_2.Enabled = false;
            this.dataGridView1.CellBeginEdit += new DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            this.dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            this.dataGridView1.PreviewKeyDown += new PreviewKeyDownEventHandler(this.dataGridView1_PreviewKeyDown);
            this.dataGridView1.DataError += new DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            this.dataGridView1.CurrentCellChanged += new EventHandler(this.dataGridView1_CurrentCellChanged);
            this.dataGridView1.ColumnWidthChanged += new DataGridViewColumnEventHandler(this.dataGridView1_RowColumnWidthChanged);
            this.dataGridView1.RowHeightChanged += new DataGridViewRowEventHandler(this.dataGridView1_RowColumnWidthChanged);
            this.tool_bianji.CheckOnClick = true;
            this.tool_hanshuiqiehuan.CheckOnClick = true;
            this.tool_tuichu.Click += new EventHandler(this.tool_tuichu_Click);
            this.tool_bianji.CheckedChanged += new EventHandler(this.tool_bianji_CheckedChanged);
            this.tool_daying.Click += new EventHandler(this.tool_daying_Click);
            this.tool_daying.MouseDown += new MouseEventHandler(this.tool_dayin_MouseDown);
            this.tool_tongji.Click += new EventHandler(this.tool_tongji_Click);
            this.tool_geshi.Click += new EventHandler(this.tool_geshi_Click);
            this.tool_hanshuiqiehuan.CheckedChanged += new EventHandler(this.tool_hanshuiqiehuan_CheckedChanged);
            this.tool_addRow.Click += new EventHandler(this.tool_addRow_Click);
            this.tool_DeleteRow.Click += new EventHandler(this.tool_DeleteRow_Click);
            this.tool_AddItem.Click += new EventHandler(this.tool_AddItem_Click);
            this.tool_bianji.Checked = true;
            this.BuyerPan = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel10");
            this.SellerPan = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel9");
            this.lab_sqly = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label27");
            this.lab_lxdh = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label26");
            this.txt_sqly.Visible = false;
            this.txt_lxdh.Visible = false;
            this.lab_sqly.Visible = false;
            this.lab_lxdh.Visible = false;
            this.tool_tongji.Visible = false;
            this.tool_geshi.Visible = false;
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(HySqdTianKai));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x442, 590);
            this.xmlComponentLoader1.TabIndex = 1;
            this.xmlComponentLoader1.Text = "开具红字货物运输业增值税专用发票信息表";
            this.xmlComponentLoader1.XMLPath=(@"..\Config\Components\Aisino.Fwkp.HzfpHy.Form.HySqdTianKai\Aisino.Fwkp.HzfpHy.Form.HySqdTianKai.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.AutoScrollMinSize = new Size(980, 0x22b);
            this.xmlComponentLoader1.AutoScrollMinSize = new Size(980, 0x22b);
            base.ClientSize = new Size(0x442, 590);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Location = new Point(0, 0);
            base.Name = "HySqdTianKai";
            this.Text = "开具红字货物运输业增值税专用发票信息表";
            base.ResumeLayout(false);
        }

        public void InitSqdMx(InitSqdMxType type, List<object> oSelectInfor)
        {
            this.codeInfoList.Clear();
            this.SqdMxType = type;
            this.InitSqdzd();
            this.hsjbz = PropertyUtil.GetValue("SQD-HSJBZ", "0") != "0";
            switch (this.SqdMxType)
            {
                case InitSqdMxType.Add:
                    this.SqdAdd(oSelectInfor);
                    return;

                case InitSqdMxType.Edit:
                    this.SqdEdit();
                    return;

                case InitSqdMxType.Read:
                    this.SqdRead();
                    return;
            }
        }

        public void InitSqdzd()
        {
            lpFLBM = true;
            isFLBM = (TaxCardFactory.CreateTaxCard().GetExtandParams("FLBMFlag") == "FLBM") && lpFLBM;
        }

        public static bool IsSWDK()
        {
            try
            {
                string str = TaxCardFactory.CreateTaxCard().TaxCode;
                ushort companyType = TaxCardFactory.CreateTaxCard().StateInfo.CompanyType;
                if ((!string.IsNullOrEmpty(str) && (str.Length == 15)) && (str.Substring(8, 2) == "DK"))
                {
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        private void KeyPressToUpper(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'a') && (e.KeyChar <= 'z'))
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper()[0];
            }
        }

        private string Percentage2Fraction(string percentage)
        {
            if (percentage.Equals("免税"))
            {
                return "0.00";
            }
            int index = percentage.IndexOf('%');
            if (index >= 0)
            {
                return string.Format("{0:0.00}", Convert.ToDouble(percentage.Substring(0, index)) / 100.0);
            }
            return string.Format("{0:0.00}", Convert.ToDouble(percentage));
        }

        private void percentageCmbSlv_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComboBox box = (ComboBox) sender;
            if ((box.Text.Length >= 4) && (box.SelectedText.Length == 0))
            {
                e.Handled = true;
            }
            if (e.KeyChar.ToString() == "\b")
            {
                if (box.Text.Length > 0)
                {
                    e.Handled = false;
                    return;
                }
                e.Handled = true;
            }
            if (e.KeyChar.ToString() == ".")
            {
                if (box.Text.IndexOf(".") >= 0)
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

        private void percentageCmbSlv_TextChanged(object sender, EventArgs e)
        {
            if (!this.Radio_SellerSQ.Checked)
            {
                string percentage = this.cmbSlv.Text.ToString();
                percentage = this.Percentage2Fraction(percentage);
                if (string.IsNullOrEmpty(percentage))
                {
                    percentage = "0.11";
                }
                if ((Convert.ToDouble(percentage) > 0.25) || (Convert.ToDouble(percentage) < 0.0))
                {
                    MessageManager.ShowMsgBox("INP-431411");
                    percentage = this.inv.GetSqSLv();
                    if (string.IsNullOrEmpty(percentage))
                    {
                        percentage = "0.11";
                    }
                    else
                    {
                        percentage = percentage.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];
                    }
                }
                else if (((percentage == "0") || (percentage == "0.00")) || (percentage == "0.0"))
                {
                    MessageManager.ShowMsgBox("INP-431380");
                    percentage = this.inv.GetSqSLv();
                    if (string.IsNullOrEmpty(percentage))
                    {
                        percentage = "0.11";
                    }
                    else
                    {
                        percentage = percentage.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];
                    }
                }
                this.cmbSlv.Text = this.Fraction2Percentage(percentage);
                for (int i = 0; i < this.inv.GetSpxxs().Count; i++)
                {
                    this.inv.SetSLv(i, percentage);
                }
                this.inv.SetFpSLv(percentage);
                this.lab_se.Text = "￥" + this.inv.GetHjSe();
            }
        }

        private void ReGetPercentageSlv()
        {
            string fraction = this.inv.SLv;
            if (this.Radio_SellerSQ.Checked)
            {
                this.cmbSlv.DropDownStyle = ComboBoxStyle.DropDownList;
                string sqSLv = this.inv.GetSqSLv();
                if (!string.IsNullOrEmpty(sqSLv))
                {
                    string[] array = sqSLv.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    Array.Sort<string>(array);
                    Array.Reverse(array);
                    string[] strArray2 = this.Fraction2Percentage_Arr(array).ToArray();
                    DataTable table = new DataTable();
                    table.Columns.Add("key");
                    table.Columns.Add("value");
                    for (int i = 0; i < array.Length; i++)
                    {
                        DataRow row = table.NewRow();
                        row["key"] = strArray2[i];
                        row["value"] = array[i];
                        table.Rows.Add(row);
                    }
                    this.cmbSlv.ValueMember = "value";
                    this.cmbSlv.DisplayMember = "key";
                    this.cmbSlv.DataSource = table;
                    this.cmbSlv.SelectedValue = fraction;
                    if ((this.cmbSlv.SelectedIndex < 0) || (this.cmbSlv.SelectedIndex >= this.cmbSlv.Items.Count))
                    {
                        this.cmbSlv.SelectedIndex = 0;
                    }
                }
            }
            else
            {
                DataTable table2 = new DataTable();
                table2.Columns.Add("key");
                table2.Columns.Add("value");
                DataRow row2 = table2.NewRow();
                row2["key"] = this.Fraction2Percentage(fraction);
                row2["value"] = fraction;
                table2.Rows.Add(row2);
                this.cmbSlv.ValueMember = "value";
                this.cmbSlv.DisplayMember = "key";
                this.cmbSlv.DataSource = table2;
                this.inv.SetFpSLv(fraction);
            }
        }

        private void ResetPercentageCmbSlv()
        {
            string str = PropertyUtil.GetValue("Aisino.Fwkp.HzfpHy.HySqdTianKai.RateRecS");
            string str2 = PropertyUtil.GetValue("Aisino.Fwkp.HzfpHy.HySqdTianKai.RateRecB");
            if (this.Radio_SellerSQ.Checked)
            {
                this.cmbSlv.DropDownStyle = ComboBoxStyle.DropDownList;
                string sqSLv = this.inv.GetSqSLv();
                string str4 = this.inv.SLv;
                if (!string.IsNullOrEmpty(sqSLv))
                {
                    string[] array = sqSLv.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    Array.Sort<string>(array);
                    Array.Reverse(array);
                    string[] strArray2 = this.Fraction2Percentage_Arr(array).ToArray();
                    DataTable table = new DataTable();
                    table.Columns.Add("key");
                    table.Columns.Add("value");
                    for (int i = 0; i < array.Length; i++)
                    {
                        DataRow row = table.NewRow();
                        row["key"] = strArray2[i];
                        row["value"] = array[i];
                        table.Rows.Add(row);
                    }
                    this.cmbSlv.ValueMember = "value";
                    this.cmbSlv.DisplayMember = "key";
                    this.cmbSlv.DataSource = table;
                    if (!string.IsNullOrEmpty(str4))
                    {
                        int index = Array.IndexOf<string>(array, str4);
                        if ((index < 0) || (index >= array.Length))
                        {
                            base.Close();
                            throw new Exception("BlueRateRevoked");
                        }
                        this.cmbSlv.SelectedValue = str4;
                        if ((this.cmbSlv.SelectedIndex >= 0) && (this.cmbSlv.SelectedIndex < this.cmbSlv.Items.Count))
                        {
                            return;
                        }
                    }
                    if (!string.IsNullOrEmpty(str) && (Array.IndexOf<string>(array, str) >= 0))
                    {
                        this.cmbSlv.SelectedValue = str;
                    }
                    if ((this.cmbSlv.SelectedIndex < 0) || (this.cmbSlv.SelectedIndex >= this.cmbSlv.Items.Count))
                    {
                        this.cmbSlv.SelectedIndex = 0;
                    }
                }
            }
            else
            {
                this.cmbSlv.DropDownStyle = ComboBoxStyle.DropDown;
                string str5 = this.inv.GetSqSLv();
                if (!string.IsNullOrEmpty(str5))
                {
                    string[] strArray3 = str5.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    Array.Sort<string>(strArray3);
                    Array.Reverse(strArray3);
                    string[] strArray4 = this.Fraction2Percentage_Arr(strArray3).ToArray();
                    DataTable table2 = new DataTable();
                    table2.Columns.Add("key");
                    table2.Columns.Add("value");
                    for (int j = 0; j < strArray3.Length; j++)
                    {
                        DataRow row2 = table2.NewRow();
                        row2["key"] = strArray4[j];
                        row2["value"] = strArray3[j];
                        table2.Rows.Add(row2);
                    }
                    this.cmbSlv.ValueMember = "value";
                    this.cmbSlv.DisplayMember = "key";
                    this.cmbSlv.DataSource = table2;
                }
                if (!string.IsNullOrEmpty(str2))
                {
                    DataTable table3 = new DataTable();
                    table3.Columns.Add("key");
                    table3.Columns.Add("value");
                    DataRow row3 = table3.NewRow();
                    row3["key"] = this.Fraction2Percentage(str2);
                    row3["value"] = str2;
                    table3.Rows.Add(row3);
                    this.cmbSlv.ValueMember = "value";
                    this.cmbSlv.DisplayMember = "key";
                    this.cmbSlv.DataSource = table3;
                    this.inv.SetFpSLv(str2);
                }
                else if (this.cmbSlv.Items.Count > 0)
                {
                    this.cmbSlv.SelectedIndex = 0;
                }
                else
                {
                    DataTable table4 = new DataTable();
                    table4.Columns.Add("key");
                    table4.Columns.Add("value");
                    DataRow row4 = table4.NewRow();
                    row4["key"] = "11%";
                    row4["value"] = 0.11;
                    table4.Rows.Add(row4);
                    this.cmbSlv.ValueMember = "value";
                    this.cmbSlv.DisplayMember = "key";
                    this.cmbSlv.DataSource = table4;
                    this.inv.SetFpSLv("0.11");
                }
            }
            if (string.IsNullOrEmpty(this.cmbSlv.Text))
            {
                MessageManager.ShowMsgBox("INP-431402");
            }
        }

        private void rtx_yshw_TextChanged(object sender, EventArgs e)
        {
            string str = this.rtx_yshw.Text.Trim();
            this.inv.Yshwxx=(str);
            if (this.inv.Yshwxx != str)
            {
                this.rtx_yshw.Text = this.inv.Yshwxx;
                this.rtx_yshw.SelectionStart = this.rtx_yshw.Text.Length;
            }
        }

        private void SelSpxx(CustomStyleDataGrid parent, int type, int showDisableSLv)
        {
            object obj2 = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["colFyxm"].Value;
            string str = (obj2 != null) ? obj2.ToString() : "";
            if (((this.inv.SLv.Length != 0) || (this.inv.GetSpxxs().Count <= 0)) && ((this.inv.GetSpxxs().Count != 1) || (this.blueje > -1M)))
            {
                double.Parse(this.inv.SLv);
            }
            object[] objArray = new object[] { str, 0 };
            object[] spxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetFYXM", objArray);
            if (spxx != null)
            {
                if (isFLBM)
                {
                    if (spxx[2].ToString().Trim() == "")
                    {
                        objArray = new object[] { spxx[0].ToString(), "", spxx[1].ToString(), true };
                        spxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddFYXM", objArray);
                    }
                    if (spxx == null)
                    {
                        this.spmcBt.Text = "";
                        return;
                    }
                    object[] objArray3 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.CanUseThisSPFLBM", new object[] { spxx[2].ToString(), false });
                    if ((objArray3 != null) && !bool.Parse(objArray3[0].ToString()))
                    {
                        MessageManager.ShowMsgBox("INP-242207", new string[] { "费用项目", "\r\n可能原因：\r\n1、当前企业没有所选税收分类编码授权。\r\n2、当前版本所选税收分类编码可用状态为不可用。" });
                        return;
                    }
                }
                this.SetSpxx(spxx);
            }
        }

        protected void SetDataGridReadOnlyColumns(string columns)
        {
            if (!string.IsNullOrEmpty(columns))
            {
                string[] strArray = columns.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (this.dataGridView1.Columns.Contains(strArray[i]))
                    {
                        this.dataGridView1.Columns[strArray[i]].ReadOnly = true;
                    }
                }
            }
        }

        private void SetHsbz()
        {
            this.tool_hanshuiqiehuan.Checked = this.hsjbz;
            this.dataGridView1.Columns["colJe"].HeaderText = this.hsjbz ? "金额(含税)" : "金额(不含税)";
            this.tool_hanshuiqiehuan.Enabled = true;
        }

        private bool SetSelectReason(string Selected)
        {
            if ((Selected.Trim().Length != 11) && (Selected.Trim().Length != 10))
            {
                return false;
            }
            try
            {
                int num;
                int[] numArray = new int[7];
                int[] numArray2 = new int[3];
                string str = "";
                for (num = 1; num <= 10; num++)
                {
                    str = Selected.Substring(num - 1, 1);
                    if (num < 8)
                    {
                        numArray[num - 1] = Convert.ToInt32(str);
                    }
                    if (num >= 8)
                    {
                        numArray2[num - 8] = Convert.ToInt32(str);
                    }
                }
                num = 0;
                this.Radio_BuyerSQ.Checked = numArray[num++] == 1;
                this.Radio_BuyerSQ_Ydk.Checked = numArray[num++] == 1;
                this.Radio_BuyerSQ_Wdk.Checked = numArray[num++] == 1;
                this.Radio_BuyerSQ_Wdk_1.Checked = numArray[num++] == 1;
                this.Radio_BuyerSQ_Wdk_2.Checked = numArray[num++] == 1;
                this.Radio_BuyerSQ_Wdk_3.Checked = numArray[num++] == 1;
                this.Radio_BuyerSQ_Wdk_4.Checked = numArray[num++] == 1;
                num = 0;
                this.Radio_SellerSQ.Checked = numArray2[num++] == 1;
                this.Radio_SellerSQ_1.Checked = numArray2[num++] == 1;
                this.Radio_SellerSQ_2.Checked = numArray2[num++] == 1;
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void SetSpxx(object[] spxx)
        {
            int rowIndex = this.dataGridView1.CurrentCell.RowIndex;
            if ((spxx != null) && (spxx.Length > 0))
            {
                this.inv.SetSpmc(rowIndex, spxx[0].ToString());
                if (isFLBM)
                {
                    if ((spxx[3].ToString() == "是") && (spxx[6].ToString().Trim() == ""))
                    {
                        this.Update_FYXM(spxx);
                    }
                    object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.CanUseThisSPFLBM", new object[] { spxx[2].ToString(), false });
                    if ((objArray != null) && !bool.Parse(objArray[0].ToString()))
                    {
                        this.spmcBt.Text = "";
                        MessageManager.ShowMsgBox("INP-242207", new string[] { "费用项目", "\r\n可能原因：\r\n1、当前企业没有所选税收分类编码授权。\r\n2、当前版本所选税收分类编码可用状态为不可用。" });
                        return;
                    }
                    this.codeInfoList[rowIndex].spbm = spxx[1].ToString();
                    string str = spxx[2].ToString();
                    while (str.Length < 0x13)
                    {
                        str = str + "0";
                    }
                    this.codeInfoList[rowIndex].flbm = str;
                    this.codeInfoList[rowIndex].sfxsyhzc = (spxx[3].ToString() == "是") ? "1" : "0";
                    this.codeInfoList[rowIndex].yhzcmc = spxx[6].ToString();
                    this.codeInfoList[rowIndex].yhzcsl = spxx[5].ToString();
                    this.codeInfoList[rowIndex].lslbs = "";
                    this.inv.SetYhsm(rowIndex, this.codeInfoList[rowIndex].yhzcmc);
                    this.inv.SetXsyh(rowIndex, this.codeInfoList[rowIndex].sfxsyhzc);
                    this.inv.SetLslvbs(rowIndex, "");
                    this.inv.SetSpbh(rowIndex, this.codeInfoList[rowIndex].spbm);
                    this.inv.SetFlbm(rowIndex, this.codeInfoList[rowIndex].flbm);
                }
                this.ShowDataGrid(this.inv.GetSpxx(rowIndex), rowIndex);
                this.SetHsbz();
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[rowIndex].Cells["colJe"];
            }
        }

        private void ShowDataGrid(Dictionary<SPXX, string> spxx, int row)
        {
            this.dataGridView1.Rows[row].Cells["colFyxm"].Value = spxx[0];
            this.dataGridView1.Rows[row].Cells["colJe"].Value = spxx[(SPXX)7];
            if (!string.IsNullOrEmpty(this.dataGridView1.Rows[row].Cells["colJe"].Value.ToString()))
            {
                double num = Convert.ToDouble(spxx[(SPXX)7]);
                this.dataGridView1.Rows[row].Cells["colJe"].Value = string.Format("{0:0.00}", num);
            }
            this.lab_je.Text = "￥" + this.inv.GetHjJe();
            this.lab_se.Text = "￥" + this.inv.GetHjSe();
        }

        private void spmcBt_Click(object sender, EventArgs e)
        {
            CustomStyleDataGrid parent = (CustomStyleDataGrid) this.spmcBt.Parent;
            this.SelSpxx(parent, 0, 0);
        }

        public virtual void spmcBt_leave(object sender, EventArgs e)
        {
            CustomStyleDataGrid parent = (CustomStyleDataGrid) this.spmcBt.Parent;
            if (isFLBM && (parent.CurrentCell != null))
            {
                int rowIndex = parent.CurrentCell.RowIndex;
                Dictionary<SPXX, string> spxx = this.inv.GetSpxx(rowIndex);
                if (((spxx != null) && ((spxx[(SPXX)20] == null) || (spxx[(SPXX)20] == ""))) && (((spxx[(SPXX)0] != null) && (spxx[(SPXX)0] != "")) && !spxx[(SPXX)0].Contains("折扣")))
                {
                    string text = this.spmcBt.Text;
                    DataTable table = this._SpmcOnAutoCompleteDataSource(parent, text);
                    if ((table == null) || (table.Rows.Count == 0))
                    {
                        if ((spxx != null) && ((spxx[(SPXX)20] == null) || (spxx[(SPXX)20] == "")))
                        {
                            object[] objArray = new object[] { text, "", "", false };
                            object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddFYXM", objArray);
                            if (objArray2 == null)
                            {
                                this.spmcBt.Text = "";
                            }
                            else if (((objArray2[3].ToString() == "0") || (objArray2[3].ToString() == "0.0")) || ((objArray2[3].ToString() == "0.00") || (objArray2[3].ToString() == "0%")))
                            {
                                MessageManager.ShowMsgBox("INP-431380");
                            }
                            else
                            {
                                this.SetSpxx(objArray2);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            if (table.Rows[i]["SPFL"].ToString() != "")
                            {
                                this.SetSpxx(new object[] { table.Rows[i]["MC"], table.Rows[i]["BM"], table.Rows[i]["SPFL"], table.Rows[i]["YHZC"], table.Rows[i]["SPFL_ZZSTSGL"], table.Rows[i]["YHZC_SLV"], table.Rows[i]["YHZCMC"] });
                                return;
                            }
                        }
                        object[] objArray3 = new object[] { table.Rows[0]["MC"], "", table.Rows[0]["BM"], true };
                        object[] objArray4 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddFYXM", objArray3);
                        if (objArray4 == null)
                        {
                            this.spmcBt.Text = "";
                        }
                        else if (((objArray4[3].ToString() == "0") || (objArray4[3].ToString() == "0.0")) || ((objArray4[3].ToString() == "0.00") || (objArray4[3].ToString() == "0%")))
                        {
                            MessageManager.ShowMsgBox("INP-431380");
                        }
                        else
                        {
                            this.SetSpxx(objArray4);
                        }
                    }
                }
            }
        }

        private void spmcBt_OnAutoComplate(object sender, EventArgs e)
        {
            string spmc = this.spmcBt.Text.Trim();
            CustomStyleDataGrid parent = (CustomStyleDataGrid) this.spmcBt.Parent;
            DataTable table = this._SpmcOnAutoCompleteDataSource(parent, spmc);
            if (table != null)
            {
                this.spmcBt.DataSource=(table);
            }
        }

        private void spmcBt_OnSelectValue(object sender, EventArgs e)
        {
            Dictionary<string, string> dictionary = this.spmcBt.SelectDict;
            this.SetSpxx(new object[] { dictionary["MC"], dictionary["BM"], dictionary["SPFL"], dictionary["YHZC"], dictionary["SPFL_ZZSTSGL"], dictionary["YHZC_SLV"], dictionary["YHZCMC"] });
        }

        private void spmcBt_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CustomStyleDataGrid parent = (CustomStyleDataGrid) this.spmcBt.Parent;
                int rowIndex = parent.CurrentCell.RowIndex;
                parent.CurrentCell = parent.Rows[rowIndex].Cells["colJe"];
            }
        }

        private void spmcBt_TextChanged(object sender, EventArgs e)
        {
            int index = this.dataGridView1.CurrentRow.Index;
            int count = this.dataGridView1.Rows.Count;
            string text = this.spmcBt.Text;
            bool flag = false;
            flag = this.inv.SetSpmc(index, text);
            this.ShowDataGrid(this.inv.GetSpxx(index), index);
            if (!text.Equals(this.inv.GetSpxx(index)[0]))
            {
                this.spmcBt.Text = this.inv.GetSpxx(index)[0];
                this.spmcBt.SelectionStart=(this.spmcBt.Text.Length);
            }
            if (!flag)
            {
                MessageManager.ShowMsgBox(this.inv.GetCode(), this.inv.Params);
            }
        }

        private void SqdAdd(List<object> SelectInfor)
        {
            this.fpdm = SelectInfor[0].ToString();
            this.fphm = SelectInfor[1].ToString();
            this.fplx = (SelectInfor[2].ToString() == "f") ? ((FPLX) 11) : ((FPLX) 2);
            this.SetSelectReason(SelectInfor[3].ToString());
            this.lab_fpdm.Text = SelectInfor[0].ToString();
            this.lab_fphm.Text = SelectInfor[1].ToString();
            this.lab_fpzl.Text = (SelectInfor[2].ToString() == "f") ? "货物运输业增值税专用发票" : "专用发票";
            DateTime cardClock = base.TaxCardInstance.GetCardClock();
            this.lab_date.Text = cardClock.ToString("yyyy/MM/dd");
            this.lab_kpy.Text = UserInfo.Yhmc;
            this.lab_jqbh.Text = base.TaxCardInstance.GetInvControlNum();
            this.sqdh = base.TaxCardInstance.TaxCode + "_" + base.TaxCardInstance.GetInvControlNum() + "_" + cardClock.ToString("yyyyMMdd") + "_" + cardClock.ToString("HHmmss");
            this.lab_No.Text = this.sqdh;
            if (SelectInfor[4].ToString() == "0")
            {
                byte[] buffer = CreateInvoiceTmp();
                Invoice.IsGfSqdFp_Static=(true);
                this.inv = new Invoice(true, false, false, this.fplx, buffer, null);
                this.inv.IsGfSqdFp=(true);
                this.ResetPercentageCmbSlv();
                Spxx spxx = new Spxx("", "", this.Percentage2Fraction(this.cmbSlv.Text.ToString()));
                this.AddRow(spxx);
                this.com_gfmc.Edit=(0);
                this.com_gfmc.Edit=((EditStyle)0);
                this.com_gfmc.Text = base.TaxCardInstance.Corporation;
                this.com_gfsbh.Text = base.TaxCardInstance.TaxCode;
                this.inv.Gfmc=(base.TaxCardInstance.Corporation);
                this.inv.Gfsh=(base.TaxCardInstance.TaxCode);
                this.tool_hanshuiqiehuan.Checked = this.hsjbz;
            }
            else
            {
                this.com_xfmc.Edit=((EditStyle)0);
                this.com_xfsbh.Edit=((EditStyle)0);
                this.GetXfInfo(SelectInfor[2].ToString(), this.fpdm, this.fphm);
            }
        }

        private void SqdEdit()
        {
            this.GetSqdInfo(this.sqdh);
            this.ReGetPercentageSlv();
            if (this.Radio_BuyerSQ.Checked)
            {
                this.com_gfmc.Edit=(0);
                this.com_gfmc.Edit=((EditStyle)0);
                if (this.Radio_BuyerSQ_Wdk.Checked)
                {
                    this.Radio_BuyerSQ_Wdk_1.Enabled = true;
                    this.Radio_BuyerSQ_Wdk_2.Enabled = true;
                    this.Radio_BuyerSQ_Wdk_3.Enabled = true;
                    this.Radio_BuyerSQ_Wdk_4.Enabled = true;
                }
            }
            else
            {
                this.com_xfmc.Edit=((EditStyle)0);
                this.com_xfsbh.Edit=((EditStyle)0);
                this.Radio_SellerSQ_1.Enabled = true;
                this.Radio_SellerSQ_2.Enabled = true;
            }
        }

        private void SqdRead()
        {
            this.tool_bianji.Visible = false;
            this.tool_addRow.Visible = false;
            this.tool_DeleteRow.Visible = false;
            this.toolStripSeparator.Visible = false;
            this.GetSqdInfo(this.sqdh);
            double num = Convert.ToDouble(this.inv.SLv) * 100.0;
            this.cmbSlv.Text = Convert.ToString(num) + "%";
            if (this.cmbSlv.Text.Equals("0%"))
            {
                this.cmbSlv.Text = "免税";
            }
            this.cmbSlv.Enabled = false;
            this.com_gfmc.Edit=(0);
            this.com_gfmc.Edit=((EditStyle)0);
            this.com_xfmc.Edit=((EditStyle)0);
            this.com_xfsbh.Edit=((EditStyle)0);
            this.com_fhrmc.Edit=(0);
            this.com_fhrsh.Edit=(0);
            this.com_shrmc.Edit=(0);
            this.com_shrsh.Edit=(0);
            this.rtx_yshw.Enabled = false;
            this.txt_ccdw.Enabled = false;
            this.txt_czch.Enabled = false;
            this.txt_lxdh.Enabled = false;
            this.txt_sqly.Enabled = false;
            this.dataGridView1.ReadOnly = true;
            this.txt_lxdh.ReadOnly = true;
            this.txt_sqly.ReadOnly = true;
        }

        private void tool_AddItem_Click(object sender, EventArgs e)
        {
            object[] objArray = null;
            ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddFYXM", objArray);
        }

        private void tool_addRow_Click(object sender, EventArgs e)
        {
            Spxx spxx = new Spxx("", "", this.inv.SLv);
            this.AddRow(spxx);
        }

        private void tool_bianji_CheckedChanged(object sender, EventArgs e)
        {
            this.dataGridView1.ReadOnly = !this.tool_bianji.Checked;
        }

        private void tool_dayin_MouseDown(object sender, MouseEventArgs e)
        {
            this.CommitEditGrid();
            this.lab_No.Focus();
        }

        private void tool_daying_Click(object sender, EventArgs e)
        {
            try
            {
                double invAmountLimit = 0.0;
                if ((this.blueje > -1M) && (this.blueje < Math.Abs(Convert.ToDecimal(this.inv.GetHjJeNotHs()))))
                {
                    MessageManager.ShowMsgBox("INP-431410");
                    return;
                }
                if (!this.Radio_BuyerSQ.Checked)
                {
                    foreach (PZSQType type in base.TaxCardInstance.SQInfo.PZSQType)
                    {
                        if (type.invType.Equals((InvoiceType) 11))
                        {
                            invAmountLimit = type.InvAmountLimit;
                        }
                    }
                }
                else
                {
                    invAmountLimit = 100000000.0;
                }
                if (Math.Abs(Convert.ToDouble(this.inv.GetHjJeNotHs())) > invAmountLimit)
                {
                    MessageManager.ShowMsgBox("INP-431409");
                    return;
                }
                if (this.Radio_BuyerSQ.Checked)
                {
                    if (this.com_xfmc.Text.Trim().Equals(string.Empty) || (this.com_xfmc.Text.Trim() == null))
                    {
                        MessageManager.ShowMsgBox("INP-431412");
                        return;
                    }
                    if (this.com_xfsbh.Text.Trim().Equals(string.Empty) || (this.com_xfsbh.Text.Trim() == null))
                    {
                        MessageManager.ShowMsgBox("INP-431413");
                        return;
                    }
                    string str = this.com_xfsbh.Text.Trim();
                    InvoiceHandler handler = new InvoiceHandler();
                    if (!handler.CheckTaxCode(str, (FPLX)11).Equals("0000"))
                    {
                        MessageManager.ShowMsgBox("INP-431414", new string[] { str });
                        return;
                    }
                }
                this.inv.Gfmc=(this.com_gfmc.Text.Trim());
                this.inv.Gfsh=(this.com_gfsbh.Text.Trim());
                this.inv.Xfmc=(this.com_xfmc.Text.Trim());
                this.inv.Xfsh=(this.com_xfsbh.Text.Trim());
                this.inv.Fhrmc=(this.com_fhrmc.Text.Trim());
                this.inv.Fhrsh=(this.com_fhrsh.Text.Trim());
                this.inv.Shrmc=(this.com_shrmc.Text.Trim());
                this.inv.Shrsh=(this.com_shrsh.Text.Trim());
                this.inv.Ccdw=(this.txt_ccdw.Text.Trim());
                this.inv.Czch=(this.txt_czch.Text.Trim());
                this.inv.Jqbh=(this.lab_jqbh.Text.Trim());
                this.inv.Yshwxx=(this.rtx_yshw.Text.Trim());
                if (this.SqdMxType != InitSqdMxType.Read)
                {
                    Fpxx fpData = this.inv.GetFpData();
                    if (fpData == null)
                    {
                        MessageManager.ShowMsgBox(this.inv.GetCode(), this.inv.Params);
                        return;
                    }
                    double num2 = 0.0;
                    if (!string.IsNullOrEmpty(fpData.sLv))
                    {
                        num2 = Convert.ToDouble(fpData.sLv);
                    }
                    HZFPHY_SQD model = new HZFPHY_SQD {
                        SQDH = this.sqdh,
                        FPDM = this.lab_fpdm.Text,
                        FPHM = this.lab_fphm.Text
                    };
                    if (this.SqdMxType == InitSqdMxType.Add)
                    {
                        model.FPZL = (this.lab_fpzl.Text == "货物运输业增值税专用发票") ? "f" : "c";
                    }
                    model.KPJH = base.TaxCardInstance.Machine;
                    if (this.SqdMxType == InitSqdMxType.Add)
                    {
                        model.REQNSRSBH = base.TaxCardInstance.TaxCode;
                        string invControlNum = base.TaxCardInstance.GetInvControlNum();
                        model.JSPH = invControlNum;
                        model.XXBBH = string.Empty;
                        model.XXBZT = string.Empty;
                        model.XXBMS = string.Empty;
                    }
                    if (this.SqdMxType == InitSqdMxType.Edit)
                    {
                        model.XXBBH = string.Empty;
                        model.XXBZT = string.Empty;
                        model.XXBMS = string.Empty;
                    }
                    model.TKRQ = Convert.ToDateTime(ToolUtil.FormatDateTimeEx(this.lab_date.Text));
                    model.SSYF = Convert.ToInt32(model.TKRQ.ToString("yyyyMM"));
                    string str4 = "Aisino.Fwkp.Invoice" + fpData.fpdm + fpData.fphm;
                    byte[] bytes = Encoding.Unicode.GetBytes(MD5_Crypt.GetHashStr(str4));
                    byte[] destinationArray = new byte[0x20];
                    Array.Copy(bytes, 0, destinationArray, 0, 0x20);
                    byte[] buffer3 = new byte[0x10];
                    Array.Copy(bytes, 0x20, buffer3, 0, 0x10);
                    byte[] buffer4 = AES_Crypt.Decrypt(Convert.FromBase64String(fpData.gfmc), destinationArray, buffer3, null);
                    model.GFMC = (buffer4 == null) ? fpData.gfmc : Encoding.Unicode.GetString(buffer4);
                    model.GFSH = fpData.gfsh;
                    model.XFMC = fpData.xfmc;
                    model.XFSH = fpData.xfsh;
                    model.FHFMC = fpData.fhrmc;
                    model.FHFSH = fpData.fhrnsrsbh;
                    model.SHFMC = fpData.shrmc;
                    model.SHFSH = fpData.shrnsrsbh;
                    model.CCDW = fpData.ccdw;
                    model.CZCH = fpData.czch;
                    model.YSHWXX = fpData.yshwxx;
                    model.JQBH = fpData.jqbh;
                    model.HJJE = Convert.ToDecimal(fpData.je);
                    model.HJSE = Convert.ToDecimal(fpData.se);
                    model.SL = num2;
                    model.JBR = UserInfo.Yhmc;
                    string selectReason = this.GetSelectReason();
                    model.SQXZ = selectReason;
                    model.SQLY = this.txt_sqly.Text.Trim();
                    model.SQRDH = this.txt_lxdh.Text.Trim();
                    model.BBBZ = 0;
                    model.YYSBZ = this.GetYYSBZ(fpData);
                    if ((isFLBM && (this.codeInfoList.Count > 0)) && (this.codeInfoList[0].flbm.Trim() != ""))
                    {
                        model.FLBMBBBH = new SPFLService().GetMaxBMBBBH();
                    }
                    if (this.SqdMxType == InitSqdMxType.Add)
                    {
                        this.sqdDal.Insert(model);
                    }
                    else if (this.SqdMxType == InitSqdMxType.Edit)
                    {
                        this.sqdDal.Updata(model);
                    }
                    this.sqdMxDal.Delete(this.sqdh);
                    List<HZFPHY_SQD_MX> models = new List<HZFPHY_SQD_MX>();
                    for (int i = 0; i < fpData.Mxxx.Count; i++)
                    {
                        Dictionary<SPXX, string> dictionary = fpData.Mxxx[i];
                        HZFPHY_SQD_MX item = new HZFPHY_SQD_MX {
                            SQDH = model.SQDH,
                            MXXH = i,
                            JE = Convert.ToDecimal(dictionary[(SPXX)7]),
                            SE = Convert.ToDecimal(dictionary[(SPXX)9]),
                            SPMC = dictionary[0],
                            HSJBZ = (dictionary[(SPXX)11] == "0") ? false : true,
                            FPHXZ = Convert.ToInt32(dictionary[(SPXX)10])
                        };
                        if (isFLBM && (this.codeInfoList.Count > 0))
                        {
                            string flbm = this.codeInfoList[i].flbm;
                            while (flbm.Length < 0x13)
                            {
                                flbm = flbm + "0";
                            }
                            item.FLBM = flbm;
                            item.QYSPBM = this.codeInfoList[i].spbm;
                            item.SFXSYHZC = this.codeInfoList[i].sfxsyhzc;
                            if (item.SFXSYHZC == "1")
                            {
                                item.YHZCMC = this.codeInfoList[i].yhzcmc;
                            }
                            else
                            {
                                item.YHZCMC = "";
                            }
                            item.LSLBS = this.codeInfoList[i].lslbs;
                        }
                        models.Add(item);
                    }
                    this.sqdMxDal.Insert(models);
                }
                if (this.Radio_BuyerSQ.Checked)
                {
                    PropertyUtil.SetValue("Aisino.Fwkp.HzfpHy.HySqdTianKai.RateRecB", this.inv.SLv);
                }
                else if (this.Radio_SellerSQ.Checked)
                {
                    PropertyUtil.SetValue("Aisino.Fwkp.HzfpHy.HySqdTianKai.RateRecS", this.inv.SLv);
                }
                new HZFPSQDPrint("1" + this.sqdh).Print(true);
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
            }
            if (this.SqdMxType != InitSqdMxType.Read)
            {
                base.Close();
            }
            if ((this.SqdMxType == InitSqdMxType.Add) && (DialogResult.Yes == MessageManager.ShowMsgBox("INP-431405")))
            {
                new HySqdInfoSelect().ShowDialog();
            }
        }

        private void tool_DeleteRow_Click(object sender, EventArgs e)
        {
            if (this.SqdMxType == InitSqdMxType.Add)
            {
                if (this.dataGridView1.SelectedCells.Count > 0)
                {
                    int rowIndex = this.dataGridView1.SelectedCells[0].RowIndex;
                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[rowIndex].Cells["colJe"];
                    if (MessageManager.ShowMsgBox("INP-431408") == DialogResult.OK)
                    {
                        this.dataGridView1.Rows.Remove(this.dataGridView1.Rows[rowIndex]);
                        this.inv.DelSpxx(rowIndex);
                    }
                }
                if (this.dataGridView1.Rows.Count == 0)
                {
                    string str = this.Percentage2Fraction(this.cmbSlv.Text);
                    Spxx spxx = new Spxx("", "", str);
                    this.AddRow(spxx);
                }
            }
            else
            {
                if (this.inv.GetSpxxs().Count == 1)
                {
                    MessageManager.ShowMsgBox("INP-431407");
                    return;
                }
                if (this.inv.GetSpxxs().Count > 1)
                {
                    int num2 = this.dataGridView1.SelectedCells[0].RowIndex;
                    if (MessageManager.ShowMsgBox("INP-431408") == DialogResult.OK)
                    {
                        this.dataGridView1.Rows.Remove(this.dataGridView1.Rows[num2]);
                        this.inv.DelSpxx(num2);
                    }
                }
            }
            this.lab_je.Text = "￥" + this.inv.GetHjJe();
            this.lab_se.Text = "￥" + this.inv.GetHjSe();
        }

        private void tool_geshi_Click(object sender, EventArgs e)
        {
            this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("dgFyxm").SetColumnStyles(this.xmlComponentLoader1.XMLPath, this);
        }

        private void tool_hanshuiqiehuan_CheckedChanged(object sender, EventArgs e)
        {
            this.hsjbz = this.tool_hanshuiqiehuan.Checked;
            this.inv.Hsjbz=(this.hsjbz);
            PropertyUtil.SetValue("SQD-HSJBZ", this.hsjbz ? "1" : "0");
            this.dataGridView1.Columns["colJe"].HeaderText = this.hsjbz ? "金额(含税)" : "金额(不含税)";
            foreach (DataGridViewRow row in (IEnumerable) this.dataGridView1.Rows)
            {
                row.Cells["colJe"].Value = this.inv.GetSpxx(row.Index)[(SPXX)7];
            }
            double num = Convert.ToDouble(this.inv.GetHjJe());
            double num2 = Convert.ToDouble(this.inv.GetHjSe());
            this.lab_je.Text = "￥" + string.Format("{0:0.00}", num);
            this.lab_se.Text = "￥" + string.Format("{0:0.00}", num2);
        }

        private void tool_tongji_Click(object sender, EventArgs e)
        {
            this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("dgFyxm").Statistics(this.dataGridView1);
        }

        private void tool_tuichu_Click(object sender, EventArgs e)
        {
            this.spmcBt.Leave -= new EventHandler(this.spmcBt_leave);
            this.dataGridView1.CancelEdit();
            base.Close();
            this.spmcBt.Leave += new EventHandler(this.spmcBt_leave);
        }

        private void txt_ccdw_TextChanged(object sender, EventArgs e)
        {
            string str = this.txt_ccdw.Text.Trim();
            this.inv.Ccdw=(str);
            if (this.inv.Ccdw != str)
            {
                this.txt_ccdw.Text = this.inv.Ccdw;
                this.txt_ccdw.SelectionStart = this.txt_ccdw.Text.Length;
            }
        }

        private void txt_czch_TextChanged(object sender, EventArgs e)
        {
            string str = this.txt_czch.Text.Trim();
            this.inv.Czch=(str);
            if (this.inv.Czch != str)
            {
                this.txt_czch.Text = this.inv.Czch;
                this.txt_czch.SelectionStart = this.txt_czch.Text.Length;
            }
        }

        public void Update_FYXM(object[] spxx)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
            dictionary.Add("BM", spxx[2].ToString());
            ArrayList list = baseDAOSQLite.querySQL("aisino.Fwkp.HzfpHy.SelectYHZCS", dictionary);
            spxx[3] = "否";
            foreach (Dictionary<string, object> dictionary2 in list)
            {
                string[] strArray = dictionary2["ZZSTSGL"].ToString().Split(new string[] { "、", ",", "，" }, StringSplitOptions.RemoveEmptyEntries);
                if (strArray.Length >= 1)
                {
                    spxx[3] = "是";
                    spxx[6] = strArray[0];
                    break;
                }
            }
        }

        public bool BlueInvErr
        {
            get
            {
                return this.blueInv_error;
            }
        }

        public string sqdh { get; set; }
    }
}

