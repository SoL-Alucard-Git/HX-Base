namespace Aisino.Fwkp.Fptk.Form
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.MainForm;
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Framework.Startup.Login;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.CommonLibrary;
    using Aisino.Fwkp.Fptk;
    using Aisino.Fwkp.Fptk.Common.Forms;
    using Aisino.Fwkp.Fptk.Properties;
    using Aisino.Fwkp.Print;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    public class HYInvoiceForm : DockForm
    {
        private Invoice _fpxx;
        private AisinoMultiCombox _spmcBt;
        private string blueJe;
        private ToolStripButton bt_jg;
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
        private IContainer components;
        private string curSlv;
        internal List<string[]> data;
        private CustomStyleDataGrid dgFyxm;
        private string djfile;
        private DataGridViewTextBoxEditingControl EditingControl;
        private IFpManager fpm;
        internal int index;
        private bool initSuccess;
        private InvoiceHelper invMng;
        private bool isHzwm;
        private bool isHZXXB;
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
        private ILog log;
        private AisinoPNL mainPanel;
        private bool onlyShow;
        private AisinoPNL panel1;
        private AisinoPNL panel2;
        private AisinoPIC picZuofei;
        private ToolTip tip;
        private ToolStripButton tool_addrow;
        private ToolStripButton tool_close;
        private ToolStripDropDownButton tool_daoru;
        private ToolStripButton tool_delrow;
        private ToolStripMenuItem tool_drsz;
        private ToolStripMenuItem tool_drwl;
        private ToolStripMenuItem tool_drxxb;
        private ToolStripButton tool_fushu;
        private ToolStripDropDownButton tool_fushu1;
        private ToolStripButton tool_fuzhi;
        private ToolStripMenuItem tool_plzddr;
        private ToolStripButton tool_print;
        private ToolStripMenuItem tool_sgdr;
        private ToolStripButton tool_tongji;
        private ToolStripMenuItem tool_zjkj;
        private ToolStripButton tool_zuofei;
        private ToolStrip toolStrip3;
        private AisinoTXT txtBz;
        private AisinoTXT txtCcdw;
        private AisinoTXT txtCzch;
        private AisinoTXT txtQyd;
        private AisinoTXT txtYshw;
        private XmlComponentLoader xmlComponentLoader1;
        private Fpxx ykfp;

        internal HYInvoiceForm(FPLX fplx, string fpdm, string fphm)
        {
            this.log = LogUtil.GetLogger<HYInvoiceForm>();
            this.blueJe = string.Empty;
            this.initSuccess = true;
            this.tip = new ToolTip();
            this.djfile = "";
            if (base.TaxCardInstance.QYLX.ISHY)
            {
                this.HYInvoiceForm_Init(fplx, fpdm, fphm);
            }
            else
            {
                string[] textArray1 = new string[] { " 无货物运输业增值税专用发票授权。" };
                MessageManager.ShowMsgBox("INP-242155", textArray1);
            }
        }

        internal HYInvoiceForm(bool flag, int index, List<string[]> data)
        {
            this.log = LogUtil.GetLogger<HYInvoiceForm>();
            this.blueJe = string.Empty;
            this.initSuccess = true;
            this.tip = new ToolTip();
            this.djfile = "";
            this.onlyShow = true;
            this.InitializeDefault();
            this.tool_daoru.Visible = false;
            this.tool_fushu.Visible = false;
            this.tool_fuzhi.Visible = false;
            this.tool_addrow.Visible = false;
            this.tool_delrow.Visible = false;
            this.tool_daoru.Visible = false;
            this.tool_print.Enabled = true;
            this.tool_zuofei.Visible = flag;
            this.SetCmbEnabled(false);
            this.SetTxtEnabled(false);
            this.cmbSlv.FlatStyle = FlatStyle.Flat;
            this.cmbFhr.Edit=0;
            this.cmbSkr.Edit=0;
            this.dgFyxm.ReadOnly = true;
            this.dgFyxm.GridStyle = CustomStyle.invWare;
            this.data = data;
            this.index = index;
            string[] strArray = data[index];
            this.ykfp = this.fpm.GetXxfp(Invoice.ParseFPLX(strArray[0]), strArray[1], int.Parse(strArray[2]));
            this.ShowInfo(this.ykfp, strArray[3]);
        }

        private void _spmcBt_Click(object sender, EventArgs e)
        {
            this._SpmcSelect();
        }

        public void _SpmcBt_leave(object sender, EventArgs e)
        {
            if (!this.isWM())
            {
                int rowIndex = ((CustomStyleDataGrid) this._spmcBt.Parent).CurrentCell.RowIndex;
                Dictionary<SPXX, string> spxx = this._fpxx.GetSpxx(rowIndex);
                if (this._spmcBt.Text != "")
                {
                    DataTable table = this._SpmcOnAutoCompleteDataSource(this._spmcBt.Text.ToString());
                    string str = this._spmcBt.Text.Trim();
                    if ((table == null) || (table.Rows.Count == 0))
                    {
                        if (((spxx != null) && ((spxx[(SPXX)20] == null) || (spxx[(SPXX)20] == ""))) && ((spxx[(SPXX)0] != null) && (spxx[(SPXX)0] != "")))
                        {
                            string text = this._spmcBt.Text;
                            object[] objArray = new object[] { text, "", "", false };
                            object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddFYXM", objArray);
                            if (objArray2 == null)
                            {
                                this._spmcBt.Text = "";
                            }
                            this.SetSpxx(objArray2);
                        }
                    }
                    else if ((spxx[(SPXX)20] == null) || (spxx[(SPXX)20] == ""))
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            if ((table.Rows[i]["SPFL"].ToString() != "") && (str == table.Rows[i]["MC"].ToString()))
                            {
                                object[] objArray7 = new object[] { table.Rows[i]["MC"], table.Rows[i]["BM"], table.Rows[i]["SPFL"], table.Rows[i]["YHZC"], table.Rows[i]["SPFL_ZZSTSGL"], table.Rows[i]["YHZC_SLV"], table.Rows[i]["YHZCMC"] };
                                this.SetSpxx(objArray7);
                                return;
                            }
                        }
                        if (str == table.Rows[0]["MC"].ToString())
                        {
                            object[] objArray3 = new object[] { table.Rows[0]["MC"], "", table.Rows[0]["BM"], true };
                            object[] objArray4 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddFYXM", objArray3);
                            if (objArray4 == null)
                            {
                                this._spmcBt.Text = "";
                            }
                            this.SetSpxx(objArray4);
                        }
                        else
                        {
                            object[] objArray5 = new object[] { str, "", "", false };
                            object[] objArray6 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddFYXM", objArray5);
                            if (objArray6 == null)
                            {
                                this._spmcBt.Text = "";
                            }
                            this.SetSpxx(objArray6);
                        }
                    }
                }
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
                this._spmcBt.DataSource=table;
            }
        }

        private void _spmcBt_OnSelectValue(object sender, EventArgs e)
        {
            this._spmcBt.Leave -= new EventHandler(this._SpmcBt_leave);
            try
            {
                Dictionary<string, string> dictionary = this._spmcBt.SelectDict;
                if ((dictionary["SPFL"].ToString() == "") && !this.isWM())
                {
                    object[] objArray = new object[] { dictionary["MC"], "", dictionary["BM"], true };
                    object[] spxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddFYXM", objArray);
                    if (spxx == null)
                    {
                        this._spmcBt.Text = "";
                    }
                    this.SetSpxx(spxx);
                }
                else
                {
                    this._SpmcOnSelectValue(this.dgFyxm, dictionary);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                this._spmcBt.Leave += new EventHandler(this._SpmcBt_leave);
            }
        }

        private void _spmcBt_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && ((this._spmcBt.DataSource == null) || (this._spmcBt.DataSource.Rows.Count == 0)))
            {
                this._SpmcSelect();
            }
        }

        private void _spmcBt_SetAutoComplateHead()
        {
            this._spmcBt.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("名称", "MC", 160));
            this._spmcBt.ShowText="MC";
            this._spmcBt.DrawHead=true;
            this._spmcBt.AutoIndex=0;
        }

        private void _spmcBt_TextChanged(object sender, EventArgs e)
        {
            int index = this.dgFyxm.CurrentRow.Index;
            string text = this._spmcBt.Text;
            this.ShowDataGrid(this._fpxx.GetSpxx(index), index);
            if (!this._fpxx.SetSpmc(index, text))
            {
                MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
            }
        }

        private DataTable _SpmcOnAutoCompleteDataSource(string str)
        {
            object[] objArray1 = new object[] { str, 20, "MC,BM,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV,YHZCMC" };
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetFYXMMore", objArray1);
            if ((objArray != null) && (objArray.Length != 0))
            {
                return (objArray[0] as DataTable);
            }
            return null;
        }

        private void _SpmcOnSelectValue(CustomStyleDataGrid dataGrid, Dictionary<string, string> value)
        {
            object[] spxx = new object[] { value["MC"], value["BM"], value["SPFL"], value["YHZC"], value["SPFL_ZZSTSGL"], value["YHZC_SLV"], value["YHZCMC"] };
            this.SetSpxx(spxx);
        }

        private void _SpmcSelect()
        {
            try
            {
                string text = this._spmcBt.Text;
                object[] objArray = new object[] { text, 0, "MC,BM,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV,YHZCMC" };
                object[] spxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetFYXM", objArray);
                if ((!this.isWM() && (spxx != null)) && ((spxx.Length >= 2) && (spxx[2].ToString() == "")))
                {
                    objArray = new object[] { spxx[0].ToString(), "", spxx[1].ToString(), true };
                    spxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddFYXM", objArray);
                }
                this.SetSpxx(spxx);
            }
            catch (Exception exception)
            {
                this.log.Error(exception);
            }
        }

        private void addRow_Click(object sender, EventArgs e)
        {
            int index = -1;
            if (this.dgFyxm.SelectedRows.Count > 0)
            {
                index = this.dgFyxm.SelectedRows[0].Index;
                this.AddSpxx(index);
            }
            else if ((this.dgFyxm.Rows.Count > 0) && (this.dgFyxm.CurrentCell.RowIndex != (this.dgFyxm.Rows.Count - 1)))
            {
                index = this.dgFyxm.CurrentCell.RowIndex;
                this.AddSpxx(index);
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
                Spxx spxx = new Spxx("", "", this.invMng.GetSLValue(this.cmbSlv.Text));
                if (index < 0)
                {
                    if (this._fpxx.AddSpxx(spxx) >= 0)
                    {
                        int num = this.dgFyxm.Rows.Add();
                        this.ShowDataGrid(this._fpxx.GetSpxx(num), num);
                        flag = true;
                    }
                    return flag;
                }
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

        public void AutoImporthyfp(AutoImport impForm, Djfp djfp)
        {
            if ((djfp != null) && (djfp.Fpxx != null))
            {
                if ((this._fpxx.Fpdm != "0000000000") && (this._fpxx.Fphm != "00000000"))
                {
                    AutoImport.success = true;
                    Fpxx fpData = this._fpxx.GetFpData();
                    if (fpData == null)
                    {
                        string errorMessage = FPDJHelper.GetErrorMessage(this._fpxx.GetCode(), this._fpxx.Params);
                        object[] args = new object[] { base.TaxCardInstance.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djfp.Djh, 0, errorMessage };
                        AutoImport.KpResult = string.Format("[{0}] 单据号:{1},开具结果:{2},开具失败原因:{3}", args);
                    }
                    else
                    {
                        string str = "Aisino.Fwkp.Invoice" + this._fpxx.Fpdm + this._fpxx.Fphm;
                        byte[] destinationArray = new byte[0x20];
                        byte[] bytes = Encoding.Unicode.GetBytes(MD5_Crypt.GetHashStr(str));
                        Array.Copy(bytes, 0, destinationArray, 0, 0x20);
                        byte[] buffer2 = new byte[0x10];
                        Array.Copy(bytes, 0x20, buffer2, 0, 0x10);
                        byte[] inArray = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes(DateTime.Now.ToString("F")), destinationArray, buffer2);
                        fpData.gfmc = Convert.ToBase64String(AES_Crypt.Encrypt(Encoding.Unicode.GetBytes(Convert.ToBase64String(inArray) + ";" + this._fpxx.Gfmc), destinationArray, buffer2));
                        if (this._fpxx.MakeCardInvoice(fpData, false))
                        {
                            if (this.fpm.SaveXxfp(fpData))
                            {
                                Thread.Sleep(100);
                                if (this.djfile != "")
                                {
                                    new FPDJHelper().InsertYkdj(this.djfile, fpData.xsdjbh);
                                }
                                object[] objArray1 = new object[] { base.TaxCardInstance.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djfp.Djh, 1, this._fpxx.Fpdm, this._fpxx.Fphm };
                                AutoImport.KpResult = string.Format("[{0}] 单据号:{1},开具结果:{2},对应发票信息:货物运输业增值税专用发票,{3},{4}", objArray1);
                            }
                            else
                            {
                                AutoImport.KpResult = string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:发票开具成功，数据写库失败！", base.TaxCardInstance.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djfp.Djh);
                            }
                            this.GetNextFp();
                            impForm.CurFpdm = this._fpxx.Fpdm;
                            impForm.CurFphm = this._fpxx.Fphm;
                        }
                        else
                        {
                            string str2 = FPDJHelper.GetErrorMessage(this._fpxx.GetCode(), this._fpxx.Params);
                            object[] objArray2 = new object[] { base.TaxCardInstance.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djfp.Djh, 0, str2 };
                            AutoImport.KpResult = string.Format("[{0}] 单据号:{1},开具结果:{2},开具失败原因:{3}", objArray2);
                            if (this._fpxx.GetCode().StartsWith("TCD_7") || (this._fpxx.GetCode() == "A654"))
                            {
                                AutoImport.ErrorExist = true;
                            }
                        }
                    }
                }
                else
                {
                    AutoImport.success = false;
                    string str4 = FPDJHelper.GetErrorMessage(this.fpm.Code(), this._fpxx.Params);
                    object[] objArray4 = new object[] { base.TaxCardInstance.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djfp.Djh, 0, str4 };
                    AutoImport.KpResult = string.Format("[{0}] 单据号:{1},开具结果:{2},开具失败原因:{3}", objArray4);
                }
            }
        }

        private void CancelRedTK()
        {
            this.isHzwm = false;
            this.tool_fushu.Checked = false;
            this.isHzwm = false;
            if (this._fpxx.IsRed)
            {
                this.RefreshData(false);
            }
        }

        private bool CheckBlueFp(FPLX fplx, string fpdm, string strFphm)
        {
            int num;
            if (int.TryParse(strFphm, out num))
            {
                Fpxx fpxx = this.fpm.GetXxfp(fplx, fpdm, num);
                if (fpxx != null)
                {
                    if (decimal.Parse(fpxx.je).CompareTo(decimal.Parse("0.00")) < 0)
                    {
                        string[] textArray1 = new string[] { "红字货物运输业增值税专用发票填开" };
                        MessageManager.ShowMsgBox("INP-242142", textArray1);
                        return false;
                    }
                    if (fpxx.zfbz)
                    {
                        string[] textArray2 = new string[] { "红字货物运输业增值税专用发票填开" };
                        MessageManager.ShowMsgBox("INP-242143", textArray2);
                        return false;
                    }
                }
            }
            return true;
        }

        private bool CheckRedNote(string redNum)
        {
            if (redNum == null)
            {
                return false;
            }
            try
            {
                if (!this.fpm.CheckRedNum(redNum, FPLX.HYFP))
                {
                    string[] textArray1 = new string[] { redNum };
                    MessageManager.ShowMsgBox(this.fpm.Code(), textArray1);
                    return false;
                }
            }
            catch (Exception exception)
            {
                this.log.Error("校验通知单号时异常：" + exception.ToString());
                string[] textArray2 = new string[] { redNum };
                MessageManager.ShowMsgBox("9999", textArray2);
                return false;
            }
            return true;
        }

        private void ClearMainInfo()
        {
            this._fpxx.Gfmc="";
            this._fpxx.Gfsh="";
            this._fpxx.Shrmc="";
            this._fpxx.Shrsh="";
            this._fpxx.Fhrmc="";
            this._fpxx.Fhrsh="";
            this._fpxx.Bz="";
            this._fpxx.Yshwxx="";
            this._fpxx.Ccdw="";
            this._fpxx.Czch="";
            this._fpxx.Qyd_jy_ddd="";
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
            string text = this.cmbFhr.Text;
            this._fpxx.Fhr=text;
            if (this._fpxx.Fhr != text)
            {
                this.cmbFhr.Text = this._fpxx.Fhr;
                this.cmbFhr.SelectionStart=this.cmbFhr.Text.Length;
            }
        }

        private void cmbFhrsbh_TextChanged(object sender, EventArgs e)
        {
            string text = this.cmbFhrsbh.Text;
            this._fpxx.Fhrsh=text;
            if (this._fpxx.Fhrsh != text)
            {
                this.cmbFhrsbh.Text = this._fpxx.Fhrsh;
                this.cmbFhrsbh.SelectionStart=this.cmbFhrsbh.Text.Length;
            }
        }

        private void cmbSender_TextChanged(object sender, EventArgs e)
        {
            string text = this.cmbSender.Text;
            this._fpxx.Fhrmc=text;
            if (this._fpxx.Fhrmc != text)
            {
                this.cmbSender.Text = this._fpxx.Fhrmc;
                this.cmbSender.SelectionStart=this.cmbSender.Text.Length;
            }
        }

        private void cmbShr_TextChanged(object sender, EventArgs e)
        {
            string text = this.cmbShr.Text;
            this._fpxx.Shrmc=text;
            if (this._fpxx.Shrmc != text)
            {
                this.cmbShr.Text = this._fpxx.Shrmc;
                this.cmbShr.SelectionStart=this.cmbShr.Text.Length;
            }
        }

        private void cmbShrsbh_TextChanged(object sender, EventArgs e)
        {
            string text = this.cmbShrsbh.Text;
            this._fpxx.Shrsh=text;
            if (this._fpxx.Shrsh != text)
            {
                this.cmbShrsbh.Text = this._fpxx.Shrsh;
                this.cmbShrsbh.SelectionStart=this.cmbShrsbh.Text.Length;
            }
        }

        private void cmbSkr_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyUtil.SetValue("HYINV-SKR-IDX", this.cmbSkr.Text);
        }

        private void cmbSkr_TextChanged(object sender, EventArgs e)
        {
            string text = this.cmbSkr.Text;
            this._fpxx.Skr=text;
            if (this._fpxx.Skr != text)
            {
                this.cmbSkr.Text = this._fpxx.Skr;
                this.cmbSkr.SelectionStart=this.cmbSkr.Text.Length;
            }
        }

        private void cmbSlv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbSlv.SelectedItem != null)
            {
                this.curSlv = this.invMng.GetSLValue(this.cmbSlv.Text);
                this._fpxx.SetFpSLv(this.curSlv);
                for (int i = 0; i < this._fpxx.GetSpxxs().Count; i++)
                {
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
            string text = this.cmbSpf.Text;
            this._fpxx.Gfmc=text;
            if (this._fpxx.Gfmc != text)
            {
                this.cmbSpf.Text = this._fpxx.Gfmc;
                this.cmbSpf.SelectionStart=this.cmbSpf.Text.Length;
            }
        }

        private void cmbSpfsbh_TextChanged(object sender, EventArgs e)
        {
            string text = this.cmbSpfsbh.Text;
            this._fpxx.Gfsh=text;
            if (this._fpxx.Gfsh != text)
            {
                this.cmbSpfsbh.Text = this._fpxx.Gfsh;
                this.cmbSpfsbh.SelectionStart=this.cmbSpfsbh.Text.Length;
            }
        }

        private void commit_MouseDown(object sender, MouseEventArgs e)
        {
            this.CommitEditGrid();
        }

        private void CommitEditGrid()
        {
            this.dgFyxm.EndEdit();
        }

        private bool ConvertRedInv(Fpxx bluefp)
        {
            Invoice.IsGfSqdFp_Static=false;
            byte[] destinationArray = new byte[0x20];
            byte[] sourceArray = Invoice.TypeByte;
            Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
            byte[] buffer2 = new byte[0x10];
            Array.Copy(sourceArray, 0x20, buffer2, 0, 0x10);
            byte[] buffer3 = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes("KP" + DateTime.Now.ToString("F")), destinationArray, buffer2);
            Invoice invoice = new Invoice(this._fpxx.Hsjbz, bluefp, buffer3, null);
            Invoice redInvoice = invoice.GetRedInvoice(this._fpxx.Hsjbz);
            if ((redInvoice == null) || (invoice.GetCode() != "0000"))
            {
                MessageManager.ShowMsgBox(invoice.GetCode(), invoice.Params);
                return false;
            }
            this._fpxx = redInvoice;
            this._fpxx.Bmbbbh=invoice.Bmbbbh;
            this._fpxx.Fpdm=this.lblFpdm.Text;
            this._fpxx.Fphm=this.lblFphm.Text;
            this._fpxx.Kprq=this.lblKprq.Text;
            this._fpxx.Kpr=UserInfo.Yhmc;
            this._fpxx.Fhr=bluefp.fhr;
            this._fpxx.Skr=bluefp.skr;
            this._fpxx.Gfmc=invoice.Gfmc;
            this.ResetQyxx();
            if (this._fpxx.GetSpxxs().Count == 0)
            {
                this.dgFyxm.Rows.Clear();
            }
            this.ShowDataGridMxxx();
            if (this.dgFyxm.Rows.Count > 0)
            {
                this.dgFyxm.Rows[0].Cells[this.dgFyxm.ColumnCount - 1].Selected = true;
            }
            this._spmcBt.Visible = false;
            this.SetCmbEnabled(false);
            this.cmbSlv.Enabled = false;
            return true;
        }

        private void CreateHYInvoice(bool isRed, string hsjbz, FPLX fplx)
        {
            byte[] destinationArray = new byte[0x20];
            byte[] sourceArray = Invoice.TypeByte;
            Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
            byte[] buffer2 = new byte[0x10];
            Array.Copy(sourceArray, 0x20, buffer2, 0, 0x10);
            byte[] buffer3 = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes("KP" + DateTime.Now.ToString("F")), destinationArray, buffer2);
            Invoice.IsGfSqdFp_Static=false;
            this._fpxx = new Invoice(isRed, false, hsjbz.Equals("1"), fplx, buffer3, null);
            this._fpxx.Bmbbbh=this.getbmbbbh();
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;
            object obj2 = ((CustomStyleDataGrid) sender).Rows[rowIndex].Cells[columnIndex].Value;
            string str = (obj2 == null) ? "" : obj2.ToString();
            bool flag = false;
            switch (columnIndex)
            {
                case 0:
                    flag = this._fpxx.SetSpmc(rowIndex, str);
                    break;

                case 1:
                    flag = this._fpxx.SetJe(rowIndex, str);
                    break;
            }
            if (!flag)
            {
                MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
            }
            this.ShowDataGrid(this._fpxx.GetSpxx(rowIndex), rowIndex);
        }

        private void dataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            CustomStyleDataGrid grid = (CustomStyleDataGrid) sender;
            int rowIndex = e.RowIndex;
            if (e.ColumnIndex == -1)
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
                if (!owningColumn.Name.Equals("colFyxm"))
                {
                    if (combox != null)
                    {
                        combox.Visible = false;
                    }
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
                        combox.Text = (grid.CurrentCell.Value == null) ? "" : grid.CurrentCell.Value.ToString();
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
                int count = grid.Rows.Count;
                int num4 = grid.Columns.Count;
                int keyValue = e.KeyValue;
                if (((keyValue == 40) && (index == (count - 1))) && (!this.onlyShow && !grid.ReadOnly))
                {
                    this.AddSpxx(-1);
                }
                if ((((keyValue == 9) && (index == (count - 1))) && ((columnIndex == (num4 - 1)) && !this.onlyShow)) && (!grid.ReadOnly && this.AddSpxx(-1)))
                {
                    grid.CurrentCell = grid.Rows[count].Cells[0];
                }
                if (keyValue == 13)
                {
                    if (columnIndex < (num4 - 1))
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
                if ((keyValue == 0x2e) && !grid.ReadOnly)
                {
                    this.delRow_Click(null, null);
                }
            }
        }

        private void dataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CustomStyleDataGrid grid1 = (CustomStyleDataGrid) sender;
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
                this._fpxx.GetSpxx(rowIndex);
                if (this._fpxx.DelSpxx(rowIndex))
                {
                    this.dgFyxm.Rows.RemoveAt(rowIndex);
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

        internal bool FillDjxx(Djfp djfp)
        {
            bool flag = true;
            this.djfile = djfp.File;
            Fpxx fpxx = djfp.Fpxx;
            this._fpxx.DelSpxxAll();
            this._fpxx.Bmbbbh=fpxx.bmbbbh;
            this._fpxx.Shrmc=fpxx.shrmc;
            this._fpxx.Shrsh=fpxx.shrnsrsbh;
            this._fpxx.Fhrmc=fpxx.fhrmc;
            this._fpxx.Fhrsh=fpxx.fhrnsrsbh;
            this._fpxx.Gfmc=fpxx.spfmc;
            this._fpxx.Gfsh=fpxx.spfnsrsbh;
            this._fpxx.Bz=fpxx.bz;
            this._fpxx.Skr=fpxx.skr;
            this._fpxx.Fhr=fpxx.fhr;
            this._fpxx.Xsdjbh=fpxx.xsdjbh;
            foreach (Dictionary<SPXX, string> dictionary in fpxx.Mxxx)
            {
                Spxx spxx = new Spxx(dictionary[(SPXX)0], dictionary[(SPXX)2], fpxx.sLv);
                if ((dictionary[(SPXX)7] != "") && (dictionary[(SPXX)7] != null))
                {
                    dictionary[(SPXX)7] = FpManager.SetDecimals(dictionary[(SPXX)7], 2);
                }
                if (!this.isWM())
                {
                    spxx.Spbh=dictionary[(SPXX)1];
                    spxx.Flbm=dictionary[(SPXX)20];
                    spxx.Xsyh=dictionary[(SPXX)0x15];
                    spxx.Yhsm=dictionary[(SPXX)0x16];
                }
                spxx.Je=dictionary[(SPXX)7];
                spxx.Se=decimal.Multiply(decimal.Parse(spxx.Je), decimal.Parse(spxx.SLv)).ToString("F2");
                if (this._fpxx.AddSpxx(spxx) < 0)
                {
                    flag = false;
                    break;
                }
            }
            this._fpxx.Hjje=fpxx.je;
            this._fpxx.Hjse=fpxx.se;
            if (!flag)
            {
                djfp.Fpxx = null;
                string errorMessage = FPDJHelper.GetErrorMessage(this._fpxx.GetCode(), this._fpxx.Params);
                object[] args = new object[] { base.TaxCardInstance.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djfp.Djh, 0, errorMessage };
                AutoImport.KpResult = string.Format("[{0}] 单据号:{1},开具结果:{2},开具失败原因:{3}", args);
            }
            return flag;
        }

        private void FormMain_UpdateUserNameEvent(string yhmc)
        {
        }

        protected string getbmbbbh()
        {
            if (this.isWM())
            {
                return "";
            }
            return new SPFLService().GetMaxBMBBBH();
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

        private void GetNextFp()
        {
            if (this.fpm.CanInvoice(this._fpxx.Fplx))
            {
                FPLX fplx = this._fpxx.Fplx;
                string[] current = this.fpm.GetCurrent(fplx);
                if ((current != null) && (current.Length == 2))
                {
                    this.fpm.GetXfsh();
                    this.fpm.GetXfmc();
                    string str = this._fpxx.Kprq;
                    this.fpm.GetXfdzdh();
                    this.fpm.GetXfyhzh();
                    string str2 = this._fpxx.Zgswjg_dm;
                    string str3 = this._fpxx.Zgswjg_mc;
                    string str4 = this._fpxx.Jqbh;
                    string hsjbz = PropertyUtil.GetValue("HYINV-HSJBZ", "0");
                    this.CreateHYInvoice(false, hsjbz, this._fpxx.Fplx);
                    this._fpxx.Fpdm=current[0];
                    this._fpxx.Fphm=current[1];
                    this._fpxx.Kprq=str;
                    this._fpxx.Jqbh=str4;
                    this._fpxx.Zgswjg_mc=str3;
                    this._fpxx.Zgswjg_dm=str2;
                    this._fpxx.Xfsh=this.fpm.GetXfsh();
                    this._fpxx.Xfmc=this.fpm.GetXfmc();
                    this._fpxx.Kpr=UserInfo.Yhmc;
                    this._fpxx.Skr=this.cmbSkr.Text;
                    this._fpxx.Fhr=this.cmbFhr.Text;
                    this._fpxx.AddSpxx(new Spxx("", "", this.curSlv));
                }
                else
                {
                    this._fpxx.Fpdm="0000000000";
                    this._fpxx.Fphm="00000000";
                }
            }
        }

        private void GetQyLable()
        {
            this.lblFpdm.Text = this._fpxx.Fpdm;
            this.lblFphm.Text = this._fpxx.Fphm;
            this.lblKprq.Text = this._fpxx.Kprq;
            this.lblJqbh.Text = this._fpxx.Jqbh;
            this.lblCyr.Text = this._fpxx.Xfmc;
            this.lblCyrsbh.Text = this._fpxx.Xfsh;
            this.lblSwjgdm.Text = this._fpxx.Zgswjg_dm;
            this.lblSwjg.Text = this._fpxx.Zgswjg_mc;
            this.lblKpr.Text = this._fpxx.Kpr;
        }

        private void GetShrCmb()
        {
            this.cmbSpf.Text = this._fpxx.Gfmc;
            this.cmbSpfsbh.Text = this._fpxx.Gfsh;
            this.cmbShr.Text = this._fpxx.Shrmc;
            this.cmbShrsbh.Text = this._fpxx.Shrsh;
            this.cmbSender.Text = this._fpxx.Fhrmc;
            this.cmbFhrsbh.Text = this._fpxx.Fhrsh;
        }

        private SLV GetSLv(string value, int valueType)
        {
            return this.invMng.GetSLv(value, valueType, this._fpxx.Fplx, this._fpxx.GetSqSLv());
        }

        private void GetYsText()
        {
            this.txtQyd.Text = this._fpxx.Qyd_jy_ddd;
            this.txtYshw.Text = this._fpxx.Yshwxx;
            this.txtBz.Text = this._fpxx.Bz;
            this.txtCzch.Text = this._fpxx.Czch;
            this.txtCcdw.Text = this._fpxx.Ccdw;
        }

        private DataTable GfxxOnAutoCompleteDataSource(string str)
        {
            object[] objArray1 = new object[] { str, 20, "MC,SH" };
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSFHRMore", objArray1);
            if ((objArray != null) && (objArray.Length != 0))
            {
                return (objArray[0] as DataTable);
            }
            return null;
        }

        private void hsjbzButton_Click(object sender, EventArgs e)
        {
            this.SetHsjbz(this.bt_jg.Checked);
        }

        private void HYInvoiceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.onlyShow && (MessageManager.ShowMsgBox("INP-242199") != DialogResult.Yes))
            {
                e.Cancel = true;
            }
            else
            {
                FormMain.UpdateUserNameEvent -= FormMain_UpdateUserNameEvent;
            }
        }

        private void HYInvoiceForm_Init(FPLX fplx, string fpdm, string fphm)
        {
            this.InitializeDefault();
            if (base.TaxCardInstance.QYLX.ISTDQY)
            {
                this.tool_drwl.Visible = false;
            }
            else
            {
                this.tool_drwl.Visible = true;
            }
            string hsjbz = PropertyUtil.GetValue("HYINV-HSJBZ", "0");
            this.CreateHYInvoice(false, hsjbz, fplx);
            this._fpxx.Fpdm=fpdm;
            this._fpxx.Fphm=fphm;
            this._fpxx.Xfsh=this.fpm.GetXfsh();
            this._fpxx.Xfmc=this.fpm.GetXfmc();
            this._fpxx.Kprq=this.fpm.GetJskClock();
            this._fpxx.Kpr=UserInfo.Yhmc;
            this._fpxx.Skr=this.cmbSkr.Text;
            this._fpxx.Fhr=this.cmbFhr.Text;
            this._fpxx.Jqbh=this.fpm.GetMachineNum();
            string[] zgswjg = this.fpm.GetZgswjg();
            if (zgswjg != null)
            {
                this._fpxx.Zgswjg_dm=zgswjg[0];
                this._fpxx.Zgswjg_mc=zgswjg[1];
            }
            this.ResetCmbSlv();
            if (this.cmbSlv.Items.Count == 0)
            {
                this.initSuccess = false;
                string[] textArray1 = new string[] { "货物运输业增值税专用发票" };
                MessageManager.ShowMsgBox("INP-242129", textArray1);
            }
            else
            {
                this.curSlv = this.invMng.GetSLValue(this.cmbSlv.Text);
                Spxx spxx = new Spxx("", "", this.curSlv);
                if (this._fpxx.AddSpxx(spxx) < 0)
                {
                    this.initSuccess = false;
                    MessageManager.ShowMsgBox(this._fpxx.GetCode());
                }
                else
                {
                    this.ShowInvMainInfo();
                    this.SetFormTitle(fplx, fpdm);
                    this.tool_zuofei.Visible = false;
                }
            }
        }

        private void HYInvoiceForm_KeyDown(object sender, KeyEventArgs e)
        {
            Keys keyCode = e.KeyCode;
            if (this.onlyShow)
            {
                bool flag = false;
                switch (keyCode)
                {
                    case Keys.Next:
                        if (this.index < (this.data.Count - 1))
                        {
                            this.index++;
                            this.tip.Hide(this.lblTitle);
                        }
                        else
                        {
                            this.tip.Show("    已经到达本页最后一张    ", this.lblTitle, new Point(0, 0x37), 0x7d0);
                            return;
                        }
                        flag = true;
                        this.tip.Show("    正在查询后一张发票...    ", this.lblTitle, new Point(0, 0x37));
                        break;

                    case Keys.PageUp:
                        if (this.index > 0)
                        {
                            this.index--;
                            this.tip.Hide(this.lblTitle);
                        }
                        else
                        {
                            this.tip.Show("    已经到达本页第一张    ", this.lblTitle, new Point(0, 0x37), 0x7d0);
                            return;
                        }
                        flag = true;
                        this.tip.Show("    正在查询前一张发票...    ", this.lblTitle, new Point(0, 0x37));
                        break;
                }
                if (flag)
                {
                    string[] strArray = this.data[this.index];
                    FPLX fpzl = Invoice.ParseFPLX(strArray[0]);
                    if ((int)fpzl == 11)
                    {
                        this.ykfp = this.fpm.GetXxfp(fpzl, strArray[1], int.Parse(strArray[2]));
                        this.dgFyxm.Rows.Clear();
                        this.ShowInfo(this.ykfp, strArray[3]);
                        this.tip.Hide(this.lblTitle);
                    }
                    else
                    {
                        base.DialogResult = DialogResult.Ignore;
                        base.Close();
                    }
                }
            }
            else if (keyCode == Keys.Enter)
            {
                try
                {
                    if (((this.cmbSpf.Focused || this.cmbSpfsbh.Focused) || (this.cmbShr.Focused || this.cmbShrsbh.Focused)) || (this.cmbSender.Focused || this.cmbFhrsbh.Focused))
                    {
                        AisinoMultiCombox focusedCmb = this.GetFocusedCmb();
                        object[] objArray1 = new object[] { focusedCmb.Text, 1, "MC,SH" };
                        object[] khxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSFHR", objArray1);
                        this.ShfxxSetValue(sender, khxx);
                    }
                }
                catch (Exception exception)
                {
                    this.log.Error(exception);
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

        private void ImportHyFpData(Fpxx fp)
        {
            this.blueJe = string.Empty;
            bool flag = true;
            if (fp == null)
            {
                flag = false;
                MessageManager.ShowMsgBox(this.fpm.Code(), this.fpm.CodeParams());
            }
            else if (!this.CheckRedNote(fp.redNum))
            {
                flag = false;
            }
            else if (!this.CheckBlueFp(fp.fplx, fp.blueFpdm, fp.blueFphm))
            {
                flag = false;
            }
            if (!flag)
            {
                this.CancelRedTK();
            }
            else
            {
                this.RefreshData(true);
                this._fpxx.Hsjbz=false;
                this.SetHsjbz(false);
                if (this.fpm.CopyHYRedNotice(fp, this._fpxx))
                {
                    this.reset_fpxx();
                    this.ResetQyxx();
                    this.ShowInvMainInfo();
                    this.SetCmbEnabled(false);
                    this.dgFyxm.ReadOnly = true;
                    this.tool_addrow.Enabled = false;
                    this.tool_delrow.Enabled = false;
                    this._spmcBt.Visible = false;
                    this.cmbSlv.Enabled = false;
                }
                else
                {
                    MessageManager.ShowMsgBox(this.fpm.Code());
                    this.RefreshData(false);
                }
            }
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.tool_close = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_close");
            this.tool_print = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_print");
            this.tool_fuzhi = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_fuzhi");
            this.tool_zuofei = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_zuofei");
            this.bt_jg = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("bt_jg");
            this.tool_tongji = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_tongji");
            this.tool_addrow = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_addrow");
            this.tool_delrow = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_delrow");
            this.tool_fushu = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_fushu");
            this.tool_fushu1 = this.xmlComponentLoader1.GetControlByName<ToolStripDropDownButton>("tool_fushu1");
            this.tool_zjkj = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_zjkj");
            this.tool_drxxb = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_drxxb");
            this.tool_drwl = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_drwl");
            this.tool_daoru = this.xmlComponentLoader1.GetControlByName<ToolStripDropDownButton>("tool_daoru");
            this.tool_drsz = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_drsz");
            this.tool_sgdr = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_sgdr");
            this.tool_plzddr = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_plzddr");
            this.lblTitle = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblTitle");
            this.lblFpdm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblFpdm");
            this.lblFphm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblFphm");
            this.lblKprq = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblKprq");
            this.lblKpr = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblKpr");
            this.lblCyrsbh = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblCyrsbh");
            this.lblJqbh = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJqbh");
            this.lblHjse = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblHjse");
            this.lblCyr = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblCyr");
            this.lblJgdx = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJgdx");
            this.lblJgxx = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJgxx");
            this.lblSwjg = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblSwjg");
            this.lblSwjgdm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblSwjgdm");
            this.lblHjje = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblHjje");
            this.cmbFhr = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("cmbFhr");
            this.cmbSkr = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("cmbSkr");
            this.cmbSpfsbh = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("cmbSpfsbh");
            this.cmbSpf = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("cmbSpf");
            this.cmbShrsbh = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("cmbShrsbh");
            this.cmbShr = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("cmbShr");
            this.cmbFhrsbh = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("cmbFhrsbh");
            this.cmbSender = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("cmbSender");
            this.txtQyd = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtQyd");
            this.txtYshw = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtYshw");
            this.cmbSlv = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbSlv");
            this.txtCcdw = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtCcdw");
            this.txtCzch = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtCzch");
            this.txtBz = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtBz");
            this.txtBz.AcceptsTab = false;
            this.txtBz.AcceptsReturn = true;
            this.txtBz.ScrollBars = ScrollBars.Vertical;
            this.txtYshw.AcceptsTab = false;
            this.txtYshw.AcceptsReturn = true;
            this.txtYshw.ScrollBars = ScrollBars.Vertical;
            this.dgFyxm = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("dgFyxm");
            this.dgFyxm.ImeMode = ImeMode.NoControl;
            this.colFyxm = this.xmlComponentLoader1.GetControlByName<DataGridViewTextBoxColumn>("colFyxm");
            this.colJe = this.xmlComponentLoader1.GetControlByName<DataGridViewTextBoxColumn>("colJe");
            this.mainPanel = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel5");
            this.panel2 = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel2");
            this.panel1 = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel1");
            this.toolStrip3 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip3");
            ControlStyleUtil.SetToolStripStyle(this.toolStrip3);
            this.picZuofei = this.xmlComponentLoader1.GetControlByName<AisinoPIC>("picZuofei");
            this.picZuofei.Visible = false;
            this.picZuofei.BackColor = Color.Transparent;
            this.picZuofei.SizeMode = PictureBoxSizeMode.Zoom;
            this.panel1.BackgroundImage = Resources.HY;
            this.panel1.BackgroundImageLayout = ImageLayout.Zoom;
            this.panel2.AutoScroll = true;
            this.panel2.AutoScrollMinSize = new Size(0x3c4, 710);
            this.tool_close.Margin = new Padding(20, 1, 0, 2);
            this.tool_tongji.Visible = false;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(HYInvoiceForm));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x3c4, 0x2fa);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath=@"..\Config\Components\Aisino.Fwkp.Fpkj.Form.HYFPtiankai_new\Aisino.Fwkp.Fpkj.Form.HYFPtiankai_new.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x3c4, 0x2fa);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            this.MinimumSize = new Size(950, 700);
            base.Name = "HYInvoiceForm";
            base.TabText="货物运输业增值税专用发票填开";
            this.Text = "货物运输业增值税专用发票填开";
            base.ResumeLayout(false);
        }

        private void InitializeDefault()
        {
            this.fpm = new FpManager();
            this.invMng = new InvoiceHelper();
            this.InitSpmcCmb();
            this.Initialize();
            this.SetTitleFont();
            this.cmbSlv.DropDownStyle = ComboBoxStyle.DropDownList;
            this.tool_fushu1.Visible = false;
            this.tool_print.ToolTipText = this.onlyShow ? "发票打印" : "开具发票并打印";
            this.tool_zuofei.ToolTipText = "作废发票";
            this.tool_fuzhi.ToolTipText = "复制发票";
            this.bt_jg.ToolTipText = "转换含税价标志";
            this.tool_addrow.ToolTipText = "添加费用项目";
            this.tool_delrow.ToolTipText = "删除费用项目";
            this.tool_close.ToolTipText = "退出";
            this.bt_jg.CheckOnClick = true;
            this.tool_zuofei.CheckOnClick = false;
            this.tool_print.Click += new EventHandler(this.tool_print_Click);
            this.tool_print.MouseDown += new MouseEventHandler(this.tool_print_MouseDown);
            this.tool_zuofei.Click += new EventHandler(this.tool_zuofei_Click);
            this.tool_fuzhi.Click += new EventHandler(this.tool_fuzhi_Click);
            this.tool_fushu.Click += new EventHandler(this.tool_fushu_Click);
            this.tool_zjkj.Click += new EventHandler(this.tool_zjkj_Click);
            this.tool_drxxb.Click += new EventHandler(this.tool_drxxb_Click);
            this.tool_drwl.Click += new EventHandler(this.tool_drwl_Click);
            this.tool_drsz.Click += new EventHandler(this.tool_imputSet_Click);
            this.tool_sgdr.Click += new EventHandler(this.tool_manualImport_Click);
            this.tool_plzddr.Click += new EventHandler(this.tool_autoImport_Click);
            this.bt_jg.Click += new EventHandler(this.hsjbzButton_Click);
            this.tool_addrow.Click += new EventHandler(this.addRow_Click);
            this.tool_addrow.MouseDown += new MouseEventHandler(this.commit_MouseDown);
            this.tool_delrow.Click += new EventHandler(this.delRow_Click);
            this.tool_delrow.MouseDown += new MouseEventHandler(this.commit_MouseDown);
            this.tool_close.Click += new EventHandler(this.tool_close_Click);
            this.tool_tongji.Click += new EventHandler(this.tool_tongji_Click);
            this.cmbSkr.IsSelectAll=true;
            this.cmbSkr.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "YH", this.cmbSkr.Width));
            this.cmbSkr.DrawHead=false;
            this.cmbFhr.IsSelectAll=true;
            this.cmbFhr.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "YH", this.cmbFhr.Width));
            this.cmbFhr.DrawHead=false;
            for (int i = 0; i < this.dgFyxm.Columns.Count; i++)
            {
                this.dgFyxm.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            this.dgFyxm.AllowUserToAddRows = false;
            this.dgFyxm.AllowUserToDeleteRows = false;
            this.dgFyxm.StandardTab = false;
            this.dgFyxm.MultiSelect = false;
            this.dgFyxm.Columns["colFyxm"].ReadOnly = true;
            this.dgFyxm.GridStyle = CustomStyle.invWare;
            if (!this.onlyShow)
            {
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
                this.cmbSkr.OnSelectValue += this.cmbSkr_SelectedIndexChanged;
                this.cmbFhr.OnSelectValue += this.cmbFhr_SelectedIndexChanged;
                base.FormClosing += new FormClosingEventHandler(this.HYInvoiceForm_FormClosing);
                FormMain.UpdateUserNameEvent += FormMain_UpdateUserNameEvent;
            }
            base.KeyPreview = true;
            base.KeyDown += new KeyEventHandler(this.HYInvoiceForm_KeyDown);
            base.Resize += new EventHandler(this.HYInvoiceForm_Resize);
        }

        private void InitNextFp()
        {
            this.isHzwm = false;
            this.isHZXXB = false;
            if (this.fpm.CanInvoice(this._fpxx.Fplx))
            {
                FPLX fplx = this._fpxx.Fplx;
                string[] current = this.fpm.GetCurrent(fplx);
                if ((current != null) && (current.Length == 2))
                {
                    if (new StartConfirmForm(this._fpxx.Fplx, current).ShowDialog() == DialogResult.OK)
                    {
                        this._fpxx.Fpdm=current[0];
                        this._fpxx.Fphm=current[1];
                        this.RefreshData(false);
                    }
                    else
                    {
                        this.CloseHYForm();
                    }
                }
                else
                {
                    MessageManager.ShowMsgBox(this.fpm.Code(), this.fpm.CodeParams());
                    if (this.fpm.Code() != "000000")
                    {
                        this.CloseHYForm();
                    }
                }
            }
            else
            {
                MessageManager.ShowMsgBox(this.fpm.Code(), this.fpm.CodeParams());
                this.CloseHYForm();
            }
        }

        private void InitSpmcCmb()
        {
            this._spmcBt = new AisinoMultiCombox();
            this._spmcBt.IsSelectAll=true;
            this._spmcBt.Name = "SPMCBT";
            this._spmcBt.Text = "";
            this._spmcBt.Padding = new Padding(0);
            this._spmcBt.Margin = new Padding(0);
            this._spmcBt.Visible = false;
            this._spmcBt_SetAutoComplateHead();
            this._spmcBt.AutoComplate = AutoComplateStyle.HeadWork;
            this._spmcBt.OnAutoComplate += _spmcBt_OnAutoComplate;
            this._spmcBt.buttonStyle=0;
            this._spmcBt.OnButtonClick += _spmcBt_Click;
            this._spmcBt.OnSelectValue += _spmcBt_OnSelectValue;
            this._spmcBt.MouseDoubleClick += new MouseEventHandler(this._spmcBt_MouseDoubleClick);
            this._spmcBt.OnTextChanged = (EventHandler) Delegate.Combine(this._spmcBt.OnTextChanged, new EventHandler(this._spmcBt_TextChanged));
            this._spmcBt.PreviewKeyDown += new PreviewKeyDownEventHandler(this._spmcBt_PreviewKeyDown);
            this._spmcBt.Leave += new EventHandler(this._SpmcBt_leave);
        }

        public bool isWM()
        {
            bool flag = !FLBM_lock.isFlbm();
            return ((this.isHZXXB || this.isHzwm) | flag);
        }

        private bool MakeCardFp(Fpxx fp)
        {
            bool flag = false;
            try
            {
                string str = "Aisino.Fwkp.Invoice" + this._fpxx.Fpdm + this._fpxx.Fphm;
                byte[] destinationArray = new byte[0x20];
                byte[] bytes = Encoding.Unicode.GetBytes(MD5_Crypt.GetHashStr(str));
                Array.Copy(bytes, 0, destinationArray, 0, 0x20);
                byte[] buffer2 = new byte[0x10];
                Array.Copy(bytes, 0x20, buffer2, 0, 0x10);
                byte[] inArray = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes(DateTime.Now.ToString("F")), destinationArray, buffer2);
                fp.gfmc = Convert.ToBase64String(AES_Crypt.Encrypt(Encoding.Unicode.GetBytes(Convert.ToBase64String(inArray) + ";" + this._fpxx.Gfmc), destinationArray, buffer2));
                flag = this._fpxx.MakeCardInvoice(fp, false);
            }
            catch (Exception exception)
            {
                this.log.Error(exception);
            }
            if (!flag)
            {
                MessageManager.ShowMsgBox(this._fpxx.GetCode());
                if (!this._fpxx.GetCode().StartsWith("TCD_768") && !this._fpxx.GetCode().StartsWith("TCD_769"))
                {
                    return flag;
                }
                FormMain.CallUpload();
                this.CloseHYForm();
            }
            return flag;
        }

        private void PrintFP(Fpxx fp)
        {
            try
            {
                FPPrint print1 = new FPPrint(Invoice.FPLX2Str(this._fpxx.Fplx), fp.fpdm, int.Parse(fp.fphm));
                print1.Print(true);
                string str = print1.IsPrint;
                if ((str != "0000") && (str != "0005"))
                {
                    MessageManager.ShowMsgBox("INP-242116");
                }
            }
            catch (Exception exception)
            {
                this.log.Error("发票打印异常：" + exception.Message);
                MessageManager.ShowMsgBox("INP-242116");
            }
        }

        private void ptkj()
        {
            if (!this.tool_fushu.Checked)
            {
                this.tool_fushu.Checked = true;
                this.TKhzfp();
            }
            else
            {
                this.isHzwm = false;
                this.RefreshData(false);
            }
        }

        private void RefreshData(bool isRed)
        {
            this.dgFyxm.Rows.Clear();
            this.dgFyxm.ReadOnly = false;
            string hsjbz = PropertyUtil.GetValue("HYINV-HSJBZ", "0");
            string str2 = this._fpxx.Fpdm;
            string str3 = this._fpxx.Fphm;
            string str4 = this._fpxx.Kprq;
            string str5 = this._fpxx.Zgswjg_dm;
            string str6 = this._fpxx.Zgswjg_mc;
            string str7 = this._fpxx.Jqbh;
            this.CreateHYInvoice(isRed, hsjbz, this._fpxx.Fplx);
            this._fpxx.Fpdm=str2;
            this._fpxx.Fphm=str3;
            this._fpxx.Jqbh=str7;
            this._fpxx.Zgswjg_mc=str6;
            this._fpxx.Zgswjg_dm=str5;
            this._fpxx.Kprq=str4;
            this._fpxx.Xfsh=this.fpm.GetXfsh();
            this._fpxx.Xfmc=this.fpm.GetXfmc();
            this._fpxx.Kpr=UserInfo.Yhmc;
            this._fpxx.Skr=this.cmbSkr.Text;
            this._fpxx.Fhr=this.cmbFhr.Text;
            string sLValue = "";
            if (this.cmbSlv.SelectedItem != null)
            {
                sLValue = this.invMng.GetSLValue(this.cmbSlv.Text);
            }
            this._fpxx.AddSpxx(new Spxx("", "", sLValue));
            this.blueJe = string.Empty;
            this.ClearMainInfo();
            this.ShowInvMainInfo();
            this.SetHzxx();
            this.tool_addrow.Enabled = true;
            this.tool_delrow.Enabled = true;
            this.tool_fushu.Checked = isRed;
            this.bt_jg.Enabled = true;
            this.bt_jg.Checked = hsjbz.Equals("1");
            this.tool_fushu.Checked = isRed;
            if (isRed)
            {
                this.tool_fuzhi.Enabled = false;
            }
            else
            {
                this.tool_fuzhi.Enabled = true;
            }
            if (!this.onlyShow && !isRed)
            {
                this.cmbSlv.Enabled = true;
            }
            this.SetCmbEnabled(true);
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

        public void reset_fpxx()
        {
            List<Dictionary<SPXX, string>> spxxs = this._fpxx.GetSpxxs();
            for (int i = 0; i < spxxs.Count; i++)
            {
                if (spxxs[i][(SPXX)0x17] == "0")
                {
                    this._fpxx.SetYhsm(i, "出口零税");
                    continue;
                }
                if (spxxs[i][(SPXX)0x17] == "1")
                {
                    this._fpxx.SetYhsm(i, "免税");
                    continue;
                }
                if (spxxs[i][(SPXX)0x17] == "2")
                {
                    this._fpxx.SetYhsm(i, "不征税");
                    continue;
                }
                if (spxxs[i][(SPXX)0x15].ToString() == "1")
                {
                    string[] separator = new string[] { "、", ",", "，" };
                    string[] strArray = spxxs[i][(SPXX)0x16].ToString().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    bool flag = false;
                    for (int j = 0; j < strArray.Length; j++)
                    {
                        if (this.yhzc_contain_slv(strArray[j], spxxs[i][(SPXX)8], false))
                        {
                            this._fpxx.SetYhsm(i, strArray[j]);
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                    {
                        this._fpxx.SetXsyh(i, "1");
                    }
                    else
                    {
                        this._fpxx.SetXsyh(i, "0");
                    }
                    continue;
                }
                this._fpxx.SetYhsm(i, "0");
                this._fpxx.SetYhsm(i, "");
            }
        }

        private void ResetCmbSlv()
        {
            List<SLV> slvList = this.invMng.GetSlvList(this._fpxx.Fplx, this._fpxx.GetSqSLv());
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
                        if (((SLV) this.cmbSlv.Items[i]).DataValue == str)
                        {
                            num = i;
                            break;
                        }
                    }
                    this.cmbSlv.SelectedIndex = num;
                }
            }
        }

        private void ResetQyxx()
        {
            if (this._fpxx != null)
            {
                this._fpxx.Xfmc=this.fpm.GetXfmc();
                this._fpxx.Xfsh=this.fpm.GetXfsh();
                string[] zgswjg = this.fpm.GetZgswjg();
                if (zgswjg != null)
                {
                    this._fpxx.Zgswjg_dm=zgswjg[0];
                    this._fpxx.Zgswjg_mc=zgswjg[1];
                }
                this._fpxx.Jqbh=this.fpm.GetMachineNum();
            }
        }

        private void SetCmbEnabled(bool enabled)
        {
            if (!enabled)
            {
                this.cmbSpf.Edit = EditStyle.Label ;
                this.cmbSpfsbh.Edit=EditStyle.Label ;
                this.cmbShr.Edit=EditStyle.Label ;
                this.cmbShrsbh.Edit=EditStyle.Label ;
                this.cmbSender.Edit=EditStyle.Label ;
                this.cmbFhrsbh.Edit=EditStyle.Label ;
            }
            else
            {
                this.cmbSpf.Edit=EditStyle.TextBox ;
                this.cmbSpfsbh.Edit=EditStyle.TextBox;
                this.cmbShr.Edit=EditStyle.TextBox;
                this.cmbShrsbh.Edit=EditStyle.TextBox;
                this.cmbSender.Edit=EditStyle.TextBox;
                this.cmbFhrsbh.Edit=EditStyle.TextBox;
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
            this.dgFyxm.CSDGridColumnWidthChanged += dgFyxm_CSDGridColumnWidthChanged;
        }

        private void SetFormTitle(FPLX fplx, string fpdm)
        {
            string str = this.fpm.QueryXzqy(fpdm);
            if ((int)fplx == 11)
            {
                this.Text = "开具货物运输业增值税专用发票";
                if (str.Length == 2)
                {
                    str = str.Substring(0, 1) + "  " + str.Substring(1);
                }
                this.lblTitle.Text = str;
            }
        }

        protected virtual void SetGfxxControl(AisinoMultiCombox aisinoCmb, string showText)
        {
            aisinoCmb.IsSelectAll=true;
            aisinoCmb.buttonStyle=0;
            aisinoCmb.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            aisinoCmb.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", 200));
            aisinoCmb.ShowText=showText;
            aisinoCmb.DrawHead=false;
            aisinoCmb.AutoIndex=0;
            aisinoCmb.OnButtonClick  += shfxx_OnButtonClick;
            aisinoCmb.OnAutoComplate += shfxx_OnAutoComplate;
            aisinoCmb.OnSelectValue  += shfxx_OnSelectValue;
            aisinoCmb.AutoComplate = AutoComplateStyle.HeadWork;
        }

        private void SetHsjbz(bool hsjbz)
        {
            this._fpxx.Hsjbz=hsjbz;
            string str = hsjbz ? "(含税)" : "(不含税)";
            if (this.dgFyxm.Columns["colJe"] != null)
            {
                char[] separator = new char[] { '(' };
                this.dgFyxm.Columns["colJe"].HeaderText = this.dgFyxm.Columns["colJe"].HeaderText.Split(separator)[0] + str;
            }
            if (!this.onlyShow)
            {
                PropertyUtil.SetValue("HYINV-HSJBZ", hsjbz ? "1" : "0");
            }
            this.ShowDataGridMxxx();
            this.bt_jg.Checked = hsjbz;
            if (hsjbz)
            {
                this.bt_jg.Image = Resources.hanshuijiage_03;
            }
            else
            {
                this.bt_jg.Image = Resources.jiage_03;
            }
        }

        private void SetHzxx()
        {
            this.lblHjje.Text = "￥" + this._fpxx.GetHjJe();
            this.lblHjse.Text = "￥" + this._fpxx.GetHjSe();
            string hjJeHs = this._fpxx.GetHjJeHs();
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
                foreach (string str2 in objArray[0] as List<string>)
                {
                    object[] values = new object[] { str2 };
                    table.Rows.Add(values);
                }
                string str = PropertyUtil.GetValue("HYINV-SKR-IDX", "");
                this.cmbSkr.DataSource=table;
                this.cmbSkr.Text = str;
                str = PropertyUtil.GetValue("HYINV-FHR-IDX", "");
                this.cmbFhr.DataSource=table;
                this.cmbFhr.Text = str;
                this.RegTextChangedEvent();
            }
        }

        private void SetSpxx(object[] spxx)
        {
            if ((this.isWM() && (spxx != null)) && (spxx.Length > 6))
            {
                spxx[2] = "";
                spxx[3] = "";
                spxx[6] = "";
            }
            int rowIndex = this.dgFyxm.CurrentCell.RowIndex;
            if ((spxx != null) && (spxx.Length > 6))
            {
                if (!this.isWM())
                {
                    if (((spxx[3].ToString() == "是") && (spxx[6].ToString().Trim() == "")) && !this.Update_FYXM(spxx))
                    {
                        MessageBox.Show("更新数据库失败！");
                    }
                    object[] objArray1 = new object[] { spxx[2].ToString(), false };
                    object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.CanUseThisSPFLBM", objArray1);
                    if ((objArray != null) && !bool.Parse(objArray[0].ToString()))
                    {
                        this._spmcBt.Text = "";
                        string[] textArray1 = new string[] { "费用项目", "\r\n可能原因：\r\n1、当前企业没有所选税收分类编码授权。\r\n2、当前版本所选税收分类编码可用状态为不可用。" };
                        MessageManager.ShowMsgBox("INP-242207", textArray1);
                        return;
                    }
                }
                this._fpxx.SetSpmc(rowIndex, spxx[0].ToString());
                this._fpxx.SetSpbh(rowIndex, spxx[1].ToString());
                this._fpxx.SetYhsm(rowIndex, spxx[6].ToString());
                string str = spxx[2].ToString();
                if (!this.isWM() && (str.Length < 0x13))
                {
                    str = str.PadRight(0x13, '0');
                }
                this._fpxx.SetFlbm(rowIndex, str);
                this._fpxx.SetLslvbs(rowIndex, "");
                if (spxx[3].ToString() == "是")
                {
                    this._fpxx.SetXsyh(rowIndex, "1");
                }
                else
                {
                    this._fpxx.SetXsyh(rowIndex, "0");
                }
                this.ShowDataGrid(this._fpxx.GetSpxx(rowIndex), rowIndex);
                this.dgFyxm.CurrentCell = this.dgFyxm.Rows[rowIndex].Cells["colJe"];
            }
            this.dgFyxm.Focus();
        }

        private void SetTitleFont()
        {
            string name = "楷体";
            foreach (FontFamily family in FontFamily.Families)
            {
                if (family.Name.StartsWith(name))
                {
                    name = family.Name;
                    break;
                }
            }
            this.lblTitle.Font = new Font(name, 10f);
        }

        private void SetTxtEnabled(bool enabled)
        {
            this.txtQyd.Enabled = enabled;
            this.txtYshw.ReadOnly = !enabled;
            this.txtBz.ReadOnly = !enabled;
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
                    combox.DataSource=table;
                }
            }
        }

        private void shfxx_OnButtonClick(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = (AisinoMultiCombox) sender;
            try
            {
                object[] objArray1 = new object[] { combox.Text, 0, "MC,SH" };
                object[] khxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSFHR", objArray1);
                this.ShfxxSetValue(sender, khxx);
            }
            catch (Exception exception)
            {
                this.log.Error(exception);
            }
        }

        private void shfxx_OnSelectValue(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if (combox != null)
            {
                Dictionary<string, string> dictionary = combox.SelectDict;
                object[] khxx = new object[] { dictionary["MC"], dictionary["SH"] };
                this.ShfxxSetValue(sender, khxx);
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
                        return;

                    case "cmbShr":
                    case "cmbShrsbh":
                        this.cmbShr.Text = str;
                        this.cmbShrsbh.Text = str2;
                        return;

                    case "cmbSender":
                    case "cmbFhrsbh":
                        this.cmbSender.Text = str;
                        this.cmbFhrsbh.Text = str2;
                        break;
                }
            }
        }

        private void ShowCopy(Fpxx fp)
        {
            if ((this.cmbSlv.Items.Count > 0) && !this.cmbSlv.Items.Contains(this.GetSLv(fp.sLv, 0)))
            {
                string[] textArray1 = new string[] { "货物运输业增值税专用发票" };
                MessageManager.ShowMsgBox("INP-242177", textArray1);
                this.RefreshData(false);
            }
            else
            {
                this._fpxx.Bmbbbh=this.getbmbbbh();
                if (this._fpxx.GetCode() != "0000")
                {
                    MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
                    this.RefreshData(false);
                }
                else
                {
                    this.ResetQyxx();
                    this.dgFyxm.Rows.Clear();
                    this.ShowCopyMainInfo();
                    this.ShowDataGridMxxx();
                    if (this.dgFyxm.RowCount > 0)
                    {
                        this.dgFyxm.CurrentCell = this.dgFyxm[this.dgFyxm.ColumnCount - 1, this.dgFyxm.RowCount - 1];
                    }
                    if (this._spmcBt != null)
                    {
                        this._spmcBt.Visible = false;
                    }
                }
            }
        }

        private void ShowCopyMainInfo()
        {
            this.DelTextChangedEvent();
            this.GetQyLable();
            this.GetShrCmb();
            this.GetYsText();
            this.cmbSkr.Text = this._fpxx.Skr;
            this.cmbFhr.Text = this._fpxx.Fhr;
            if (this.cmbSlv.Items.Count > 0)
            {
                this.cmbSlv.Text = ((double.Parse(this._fpxx.SLv) * 100.0)).ToString("F2") + "%";
            }
            this.RegTextChangedEvent();
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
                                string str2 = spxx[(SPXX)7];
                                char[] trimChars = new char[] { '-' };
                                str2 = str2.TrimStart(trimChars);
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
            List<Dictionary<SPXX, string>> spxxs = this._fpxx.GetSpxxs();
            if (spxxs != null)
            {
                int count = spxxs.Count;
                for (int i = 0; i < count; i++)
                {
                    this.ShowDataGrid(spxxs[i], i);
                }
            }
        }

        internal void ShowImprotFp(Djfp djfp)
        {
            if ((djfp != null) && (djfp.Fpxx != null))
            {
                this.dgFyxm.Rows.Clear();
                this.ShowCopyMainInfo();
                this.ShowDataGridMxxx();
            }
            else
            {
                this.RefreshData(false);
            }
        }

        private void ShowInfo(Fpxx fp, string zfbz)
        {
            string str = PropertyUtil.GetValue("HYINV-HSJBZ", "0");
            byte[] destinationArray = new byte[0x20];
            byte[] sourceArray = Invoice.TypeByte;
            Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
            byte[] buffer2 = new byte[0x10];
            Array.Copy(sourceArray, 0x20, buffer2, 0, 0x10);
            byte[] buffer3 = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes("KP" + DateTime.Now.ToString("F")), destinationArray, buffer2);
            Invoice.IsGfSqdFp_Static=false;
            this._fpxx = new Invoice(str.Equals("1"), fp, buffer3, null);
            this.SetFormTitle(fp.fplx, fp.fpdm);
            this.GetQyLable();
            this.GetShrCmb();
            this.GetYsText();
            this.cmbFhr.Text = fp.fhr;
            this.cmbSkr.Text = fp.skr;
            this.cmbSlv.DropDownStyle = ComboBoxStyle.DropDown;
            this.cmbSlv.Text = this.GetSLv(this._fpxx.SLv, 0).ToString();
            bool flag = zfbz.Equals("1");
            this.tool_zuofei.Enabled = !flag;
            this.picZuofei.Visible = flag;
            this.SetHzxx();
            this.SetHsjbz(this._fpxx.Hsjbz);
            this.cmbSlv.Enabled = false;
        }

        private void ShowInvMainInfo()
        {
            try
            {
                this.DelTextChangedEvent();
                this.GetQyLable();
                this.GetShrCmb();
                this.GetYsText();
                this.cmbSkr.Text = this._fpxx.Skr;
                this.cmbFhr.Text = this._fpxx.Fhr;
                this.cmbSlv.Text = ((double.Parse(this._fpxx.SLv) * 100.0)).ToString("F2") + "%";
                this.SetHsjbz(this._fpxx.Hsjbz);
                if (this.dgFyxm.RowCount > 0)
                {
                    this.dgFyxm.CurrentCell = this.dgFyxm[this.dgFyxm.ColumnCount - 1, this.dgFyxm.RowCount - 1];
                }
                this.RegTextChangedEvent();
            }
            catch (Exception exception)
            {
                this.log.Error(exception);
            }
        }

        private bool TKhzfp()
        {
            this.isHzwm = false;
            HZFPTK hzfptk = new HZFPTK((FPLX)11);
            if (hzfptk.ShowDialog() == DialogResult.OK)
            {
                this.RefreshData(true);
                Fpxx blueFpxx = hzfptk.blueFpxx;
                this.isHzwm = false;
                try
                {
                    if (blueFpxx != null)
                    {
                        if ((blueFpxx.bmbbbh == null) || (blueFpxx.bmbbbh == ""))
                        {
                            this.isHzwm = true;
                            this._fpxx.Bmbbbh="";
                        }
                        else
                        {
                            this._fpxx.Bmbbbh=blueFpxx.bmbbbh;
                        }
                        this.blueJe = blueFpxx.je;
                        this.reset_fpxx();
                        if (!this.ConvertRedInv(blueFpxx))
                        {
                            this.RefreshData(false);
                            return false;
                        }
                    }
                    else
                    {
                        this.blueJe = string.Empty;
                    }
                    if (base.TaxCardInstance.QYLX.ISTLQY)
                    {
                        this._fpxx.Bz="";
                    }
                    else
                    {
                        this._fpxx.Bz=NotesUtil.GetRedZyInvNotes(hzfptk.redNum, "f");
                        this._fpxx.RedNum=hzfptk.redNum;
                    }
                    this._fpxx.BlueFpdm=hzfptk.blueFpdm;
                    this._fpxx.BlueFphm=hzfptk.blueFphm;
                    this.ShowInvMainInfo();
                }
                catch (Exception exception)
                {
                    this.log.Error(exception);
                }
            }
            else
            {
                this.RefreshData(false);
            }
            return true;
        }

        private void tool_autoImport_Click(object sender, EventArgs e)
        {
            this.isHzwm = false;
            this.isHZXXB = false;
            this._spmcBt.Leave -= new EventHandler(this._SpmcBt_leave);
            try
            {
                AutoImport import = new AutoImport((FPLX)11, this._fpxx.GetSqSLv(), this._fpxx.Zyfplx);
                if (!AutoImport.PathIsNull)
                {
                    string str = this._fpxx.Fpdm;
                    string str2 = this._fpxx.Fphm;
                    import.CurFpdm = str;
                    import.CurFphm = str2;
                    import.FPTKForm = this;
                    if (import.ShowDialog() == DialogResult.Cancel)
                    {
                        this.SetTxtEnabled(true);
                        if ((str != import.CurFpdm) || (str2 != import.CurFphm))
                        {
                            if ((import.CurFpdm == "0000000000") || (import.CurFphm == "00000000"))
                            {
                                string text1 = this.fpm.Code();
                                MessageManager.ShowMsgBox(text1, this.fpm.CodeParams());
                                if (text1 != "000000")
                                {
                                    this.CloseHYForm();
                                }
                                base.Close();
                            }
                            else
                            {
                                this.dgFyxm.Rows.Clear();
                                this.GetNextFp();
                                this.ShowInvMainInfo();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                this._spmcBt.Leave += new EventHandler(this._SpmcBt_leave);
            }
        }

        private void tool_close_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        private void tool_drwl_Click(object sender, EventArgs e)
        {
            this.isHZXXB = false;
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.HzfpHy.HZFPGetXXBInfo", null);
            if ((objArray != null) && (objArray.Length != 0))
            {
                string fileXML = objArray[0].ToString();
                if (fileXML.Length > 0)
                {
                    Fpxx fp = this.fpm.ProcessHYHZXXBxml(fileXML);
                    if ((fp != null) && (fp.bmbbbh == ""))
                    {
                        this.isHzwm = true;
                    }
                    this.isHZXXB = true;
                    this.ImportHyFpData(fp);
                }
            }
            else
            {
                this.CancelRedTK();
            }
        }

        private void tool_drxxb_Click(object sender, EventArgs e)
        {
            this.isHZXXB = false;
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "红字发票信息表|*.xml;*.dat"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Fpxx fp = this.fpm.ProcessHYRedNotice(dialog.FileName);
                if ((fp != null) && (fp.bmbbbh == ""))
                {
                    this.isHzwm = true;
                }
                this.isHZXXB = true;
                this.ImportHyFpData(fp);
            }
            else
            {
                this.isHzwm = false;
                this.CancelRedTK();
            }
        }

        private void tool_fushu_Click(object sender, EventArgs e)
        {
            this._spmcBt.Leave -= new EventHandler(this._SpmcBt_leave);
            try
            {
                if (base.TaxCardInstance.QYLX.ISTLQY)
                {
                    this.ptkj();
                }
                else
                {
                    ContextMenuStrip strip = new ContextMenuStrip();
                    ToolStripItem[] toolStripItems = new ToolStripItem[] { this.tool_zjkj, this.tool_drxxb, this.tool_drwl };
                    strip.Items.AddRange(toolStripItems);
                    strip.Show(this, new Point(this.tool_fushu.Bounds.X, this.tool_fushu.Bounds.Bottom));
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                this._spmcBt.Leave += new EventHandler(this._SpmcBt_leave);
            }
        }

        private void tool_fuzhi_Click(object sender, EventArgs e)
        {
            this.isHzwm = false;
            this.isHZXXB = false;
            this._spmcBt.Leave -= new EventHandler(this._SpmcBt_leave);
            try
            {
                int num;
                object[] objArray1 = new object[] { Invoice.FPLX2Str(this._fpxx.Fplx), (ZYFP_LX) 0 };
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FaPiaoChaXun_FPFZ", objArray1);
                if ((((objArray != null) && (objArray.Length == 3)) && ((objArray[0] != null) && (objArray[0].ToString() != ""))) && ((objArray[2] != null) && int.TryParse(objArray[2].ToString(), out num)))
                {
                    Fpxx fp = this.fpm.GetXxfp(this._fpxx.Fplx, objArray[1] as string, int.Parse(objArray[2] as string));
                    if (fp != null)
                    {
                        this._fpxx.DelSpxxAll();
                        this._fpxx.CopyFpxx(fp);
                        if (this.isWM())
                        {
                            this.ShowCopy(fp);
                        }
                        else if ((fp.bmbbbh == "") || (fp.bmbbbh == null))
                        {
                            if (new Add_SPFLBM(this._fpxx, false).ShowDialog() == DialogResult.OK)
                            {
                                this.ShowCopy(fp);
                            }
                            else
                            {
                                this.RefreshData(false);
                            }
                        }
                        else
                        {
                            this.reset_fpxx();
                            this.ShowCopy(fp);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                this._spmcBt.Leave += new EventHandler(this._SpmcBt_leave);
            }
        }

        private void tool_imputSet_Click(object sender, EventArgs e)
        {
            new ImportSet((FPLX)11).ShowDialog();
        }

        private void tool_manualImport_Click(object sender, EventArgs e)
        {
            this.isHzwm = false;
            this.isHZXXB = false;
            this._spmcBt.Leave -= new EventHandler(this._SpmcBt_leave);
            try
            {
                OpenFileDialog dialog = new OpenFileDialog {
                    Filter = "单据(*.xml)|*.xml"
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string str;
                    List<Djfp> collection = new FPDJHelper().ParseHYDjFileManual(dialog.FileName, out str, this._fpxx.GetSqSLv());
                    if (!string.IsNullOrEmpty(str))
                    {
                        string[] textArray1 = new string[] { str };
                        MessageManager.ShowMsgBox("INP-241007", textArray1);
                    }
                    else if (collection.Count > 0)
                    {
                        ManualImport import = new ManualImport((FPLX)11, 0);
                        import.DjfpList.AddRange(collection);
                        import.AllDjfpList.AddRange(collection);
                        if (import.ShowDialog() == DialogResult.OK)
                        {
                            this.RefreshData(false);
                            Djfp djfp = import.Djfp;
                            if ((djfp != null) && (djfp.Fpxx != null))
                            {
                                if (this.FillDjxx(djfp))
                                {
                                    this.SetTxtEnabled(true);
                                    this.dgFyxm.Rows.Clear();
                                    this.ShowCopyMainInfo();
                                    this.ShowDataGridMxxx();
                                    if (this.dgFyxm.RowCount > 0)
                                    {
                                        this.dgFyxm.CurrentCell = this.dgFyxm[this.dgFyxm.ColumnCount - 1, this.dgFyxm.RowCount - 1];
                                    }
                                    if (this._spmcBt != null)
                                    {
                                        this._spmcBt.Visible = false;
                                    }
                                }
                                else
                                {
                                    MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
                                    this.RefreshData(false);
                                }
                            }
                        }
                    }
                    else
                    {
                        string[] textArray2 = new string[] { "XML文件中单据信息不正确！" };
                        MessageManager.ShowMsgBox("INP-241007", textArray2);
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                this._spmcBt.Leave += new EventHandler(this._SpmcBt_leave);
            }
        }

        private void tool_print_Click(object sender, EventArgs e)
        {
            if (this._fpxx.GetSpxxs().Count == 0)
            {
                MessageManager.ShowMsgBox("INP-242120");
            }
            else if (this.onlyShow)
            {
                this.PrintFP(this.ykfp);
            }
            else if ((this._fpxx.IsRed && (this.blueJe != string.Empty)) && (Math.Abs(decimal.Parse(this._fpxx.GetHjJeNotHs())).CompareTo(decimal.Parse(this.blueJe)) > 0))
            {
                string[] textArray1 = new string[] { decimal.Negate(decimal.Parse(this.blueJe)).ToString("F2"), this._fpxx.GetHjJeNotHs() };
                MessageManager.ShowMsgBox("INP-242118", textArray1);
            }
            else
            {
                if (!FLBM_lock.isFlbm() || this.isHzwm)
                {
                    this._fpxx.Bmbbbh="";
                    int count = this._fpxx.GetSpxxs().Count;
                    for (int j = 0; j < count; j++)
                    {
                        this._fpxx.SetLslvbs(j, "");
                        this._fpxx.SetYhsm(j, "");
                        this._fpxx.SetXsyh(j, "0");
                        this._fpxx.SetFlbm(j, "");
                        this._fpxx.SetSpbh(j, "");
                    }
                }
                if (!this.isWM())
                {
                    List<Dictionary<SPXX, string>> list2 = this._fpxx.GetSpxxs();
                    for (int k = 0; k < list2.Count; k++)
                    {
                        if (list2[k][(SPXX)20] == "")
                        {
                            string[] textArray2 = new string[] { (k + 1).ToString() };
                            MessageManager.ShowMsgBox("INP-242186", textArray2);
                            return;
                        }
                    }
                }
                List<Dictionary<SPXX, string>> spxxs = this._fpxx.GetSpxxs();
                for (int i = 0; i < spxxs.Count; i++)
                {
                    this._fpxx.SetXsyh(i, "1");
                    if (!this.yhzc_contain_slv(spxxs[i][(SPXX)0x16].ToString(), this._fpxx.SLv, false))
                    {
                        this._fpxx.SetYhsm(i, "");
                        this._fpxx.SetXsyh(i, "0");
                    }
                    if (spxxs[i][(SPXX)0x16] == "")
                    {
                        this._fpxx.SetXsyh(i, "0");
                    }
                }
                Fpxx fpData = this._fpxx.GetFpData();
                if (fpData == null)
                {
                    MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
                }
                else
                {
                    if (((this._fpxx.Gfsh.Length != 15) && (this._fpxx.Gfsh.Length != 0x11)) && ((this._fpxx.Gfsh.Length != 0x12) && (this._fpxx.Gfsh.Length != 20)))
                    {
                        string[] textArray3 = new string[] { "实际受票方纳税人识别号" };
                        if (MessageManager.ShowMsgBox("INP-242203", textArray3) != DialogResult.Yes)
                        {
                            return;
                        }
                    }
                    PropertyUtil.SetValue("HYINV-SLV", this._fpxx.SLv);
                    if (this.MakeCardFp(fpData))
                    {
                        if (this.fpm.SaveXxfp(fpData))
                        {
                            if (this.djfile != "")
                            {
                                new FPDJHelper().InsertYkdj(this.djfile, fpData.xsdjbh);
                            }
                            this.PrintFP(fpData);
                            PropertyUtil.SetValue("HYINV-SKR-IDX", this.cmbSkr.Text);
                            PropertyUtil.SetValue("HYINV-FHR-IDX", this.cmbFhr.Text);
                        }
                        else
                        {
                            MessageManager.ShowMsgBox("INP-242111");
                        }
                        this.InitNextFp();
                    }
                }
            }
        }

        private void tool_print_MouseDown(object sender, MouseEventArgs e)
        {
            this.CommitEditGrid();
            this.lblTitle.Focus();
        }

        private void tool_tongji_Click(object sender, EventArgs e)
        {
            this.dgFyxm.Statistics(this);
        }

        private void tool_zjkj_Click(object sender, EventArgs e)
        {
            this.tool_fushu.Checked = true;
            this.TKhzfp();
        }

        private void tool_zuofei_Click(object sender, EventArgs e)
        {
            object[] objArray1 = new object[] { Invoice.FPLX2Str(this.ykfp.fplx), this.ykfp.fpdm, this.ykfp.fphm, 0 };
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FPYiKaiZuoFeiWenBenJieKouShareMethods", objArray1);
            if (((objArray != null) && (objArray.Length != 0)) && (objArray[0] != null))
            {
                if (Convert.ToBoolean(objArray[0]))
                {
                    MessageManager.ShowMsgBox("INP-442312");
                    this.tool_zuofei.Enabled = false;
                    this.data[this.index][3] = "1";
                }
                else
                {
                    this.tool_zuofei.Enabled = true;
                    this.data[this.index][3] = "0";
                }
            }
        }

        private void txtBz_TextChanged(object sender, EventArgs e)
        {
            string text = this.txtBz.Text;
            this._fpxx.Bz=text;
            if (this._fpxx.Bz != text)
            {
                this.txtBz.Text = this._fpxx.Bz;
                this.txtBz.SelectionStart = this.txtBz.Text.Length;
            }
        }

        private void txtCcdw_TextChanged(object sender, EventArgs e)
        {
            string text = this.txtCcdw.Text;
            this._fpxx.Ccdw=text;
            if (this._fpxx.Ccdw != text)
            {
                this.txtCcdw.Text = this._fpxx.Ccdw;
                this.txtCcdw.SelectionStart = this.txtCcdw.Text.Length;
            }
        }

        private void txtCzch_TextChanged(object sender, EventArgs e)
        {
            string text = this.txtCzch.Text;
            this._fpxx.Czch=text;
            if (this._fpxx.Czch != text)
            {
                this.txtCzch.Text = this._fpxx.Czch;
                this.txtCzch.SelectionStart = this.txtCzch.Text.Length;
            }
        }

        private void txtQyd_TextChanged(object sender, EventArgs e)
        {
            string text = this.txtQyd.Text;
            this._fpxx.Qyd_jy_ddd=text;
            if (this._fpxx.Qyd_jy_ddd != text)
            {
                this.txtQyd.Text = this._fpxx.Qyd_jy_ddd;
                this.txtQyd.SelectionStart = this.txtQyd.Text.Length;
            }
        }

        private void txtYshw_TextChanged(object sender, EventArgs e)
        {
            string text = this.txtYshw.Text;
            this._fpxx.Yshwxx=text;
            if (this._fpxx.Yshwxx != text)
            {
                this.txtYshw.Text = this._fpxx.Yshwxx;
                this.txtYshw.SelectionStart = this.txtYshw.Text.Length;
            }
        }

        public bool Update_FYXM(object[] spxx)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
                dictionary.Add("BM", spxx[2].ToString());
                spxx[3] = "否";
                IEnumerator enumerator = baseDAOSQLite.querySQL("aisino.fwkp.fptk.SelectYHZCS", dictionary).GetEnumerator();
                {
                    while (enumerator.MoveNext())
                    {
                        bool flag = false;
                        string[] separator = new string[] { "、", ",", "，" };
                        string[] strArray = ((Dictionary<string, object>) enumerator.Current)["ZZSTSGL"].ToString().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            if (strArray[i] != "按5%简易征收减按1.5%计征")
                            {
                                spxx[3] = "是";
                                spxx[6] = strArray[i];
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            spxx[3] = "否";
                            spxx[6] = "";
                        }
                    }
                }
                dictionary.Clear();
                dictionary.Add("YHZC", spxx[3].ToString());
                dictionary.Add("YHZCMC", spxx[6].ToString());
                dictionary.Add("BM", spxx[1].ToString());
                dictionary.Add("MC", spxx[0].ToString());
                baseDAOSQLite.未确认DAO方法2_疑似updateSQL("aisino.fwkp.fptk.UpdataBM_FYXM", dictionary);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool yhzc_contain_slv(string yhzc, string slv, bool flag)
        {
            if ((slv == "免税") || (slv == "不征税"))
            {
                slv = "0%";
            }
            else if (slv == "中外合作油气田")
            {
                slv = "5%";
            }
            else if (!flag)
            {
                slv = ((double.Parse(slv) * 100.0)).ToString() + "%";
            }
            string str = "aisino.fwkp.fptk.SelectYhzcs";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (Dictionary<string, object> dictionary2 in BaseDAOFactory.GetBaseDAOSQLite().querySQL(str, dictionary))
            {
                if ((dictionary2["YHZCMC"].ToString() == yhzc) && (dictionary2["SLV"].ToString() == ""))
                {
                    return true;
                }
                if ((dictionary2["YHZCMC"].ToString() == yhzc) && dictionary2["SLV"].ToString().Contains(slv))
                {
                    return true;
                }
            }
            return false;
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

