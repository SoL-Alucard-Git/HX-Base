namespace Aisino.Fwkp.Fptk.Form
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.MainForm;
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Registry;
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
    using Microsoft.Win32;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using VCertificate;
    using VehCertI;

    public class JDCInvoiceForm_new : DockForm
    {
        private Invoice _fpxx;
        private string blueJe;
        private AisinoMultiCombox cmbCllx;
        private AisinoCMB cmbFdjh;
        private AisinoMultiCombox cmbGhdw;
        private AisinoMultiCombox cmbGhdwsh;
        private AisinoMultiCombox cmbSfzh;
        private AisinoCMB cmbSlv;
        private IContainer components;
        internal List<string[]> data;
        private string djfile;
        private string flbm;
        private IFpManager fpm;
        internal int index;
        private bool initSuccess;
        private bool isHzwm;
        private InvoiceHelper jdcManager;
        private AisinoLBL lblBT1;
        private AisinoLBL lblBT2;
        private AisinoLBL lblBT3;
        private AisinoLBL lblBT4;
        private AisinoLBL lblBT5;
        private AisinoLBL lblBT6;
        private AisinoLBL lblDM;
        private AisinoLBL lblFpdm;
        private AisinoLBL lblFphm;
        private AisinoLBL lblHM;
        private AisinoLBL lblJgxx;
        private AisinoLBL lblJqbh;
        private AisinoLBL lblJsdx;
        private AisinoLBL lblKpr;
        private AisinoLBL lblKprq;
        private AisinoLBL lblMW;
        private AisinoLBL lblNsrsbh;
        private AisinoLBL lblSe;
        private AisinoLBL lblSwjg;
        private AisinoLBL lblSwjgdm;
        private AisinoLBL lblTitle;
        private AisinoLBL lblXhdwmc;
        private ILog log;
        private string Lslvbs;
        private bool onlyShow;
        private AisinoPNL panel1;
        private AisinoPNL panel2;
        private AisinoPIC picZuofei;
        private string spbh;
        private ToolTip tip;
        private ToolStripMenuItem tool_autokh;
        private ToolStripButton tool_close;
        private ToolStripDropDownButton tool_daoru;
        private ToolStripMenuItem tool_drsz;
        private ToolStripButton tool_fushu;
        private ToolStripButton tool_fuzhi;
        private ToolStripMenuItem tool_guochan;
        private ToolStripMenuItem tool_jinkou;
        private ToolStripDropDownButton tool_kehu;
        private ToolStripMenuItem tool_manukh;
        private ToolStripButton tool_piaozhong;
        private ToolStripMenuItem tool_plzddr;
        private ToolStripButton tool_print;
        private ToolStripDropDownButton tool_saomiao;
        private ToolStripMenuItem tool_set;
        private ToolStripButton tool_sfz;
        private ToolStripMenuItem tool_sgdr;
        private ToolStripButton tool_zuofei;
        private ToolStrip toolStrip3;
        private AisinoTXT txtCd;
        private AisinoTXT txtCjmc;
        private AisinoTXT txtClsbh;
        private AisinoTXT txtCpxh;
        private AisinoTXT txtDh;
        private AisinoTXT txtDw;
        private AisinoTXT txtDz;
        private AisinoTXT txtHgzh;
        private AisinoTXT txtJkzmh;
        private AisinoTXT txtJsxx;
        private AisinoTXT txtKhyh;
        private AisinoTXT txtSjdh;
        private AisinoTXT txtXcrs;
        private AisinoTXT txtZh;
        private XmlComponentLoader xmlComponentLoader1;
        private string xsyh;
        private string yhsm;
        private Fpxx ykfp;

        internal JDCInvoiceForm_new(FPLX fplx, string fpdm, string fphm)
        {
            this.log = LogUtil.GetLogger<JDCInvoiceForm_new>();
            this.blueJe = string.Empty;
            this.initSuccess = true;
            this.flbm = "";
            this.spbh = "";
            this.yhsm = "";
            this.xsyh = "";
            this.Lslvbs = "";
            this.djfile = "";
            this.tip = new ToolTip();
            if (base.TaxCardInstance.QYLX.ISJDC)
            {
                this.JDCInvoiceForm_Init(fplx, fpdm, fphm);
            }
            else
            {
                string[] textArray1 = new string[] { " 无机动车销售统一发票授权。" };
                MessageManager.ShowMsgBox("INP-242156", textArray1);
            }
        }

        internal JDCInvoiceForm_new(bool flag, int index, List<string[]> data)
        {
            this.log = LogUtil.GetLogger<JDCInvoiceForm_new>();
            this.blueJe = string.Empty;
            this.initSuccess = true;
            this.flbm = "";
            this.spbh = "";
            this.yhsm = "";
            this.xsyh = "";
            this.Lslvbs = "";
            this.djfile = "";
            this.tip = new ToolTip();
            this.onlyShow = true;
            this.InitializeDefault();
            this.tool_daoru.Visible = false;
            this.tool_fushu.Visible = false;
            this.tool_fuzhi.Visible = false;
            this.tool_print.Enabled = true;
            this.tool_zuofei.Visible = flag;
            this.tool_kehu.Visible = false;
            this.tool_piaozhong.Visible = false;
            this.tool_saomiao.Visible = false;
            this.tool_sfz.Visible = false;
            this.SetBtVisible(false);
            this.SetCmbLabel();
            this.SetTxtEnabled(false);
            this.data = data;
            this.index = index;
            string[] strArray = data[index];
            this.ykfp = this.fpm.GetXxfp(Invoice.ParseFPLX(strArray[0]), strArray[1], int.Parse(strArray[2]));
            this.ShowInfo(this.ykfp, strArray[3]);
        }

        public void AutoImportjdcfp(AutoImport impForm, Djfp djfp)
        {
            if ((djfp != null) && (djfp.Fpxx != null))
            {
                if ((this._fpxx.Fpdm != "000000000000") && (this._fpxx.Fphm != "00000000"))
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
                        if (fpData.sLv == "免税")
                        {
                            fpData.sLv = "0.00";
                        }
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
                                AutoImport.KpResult = string.Format("[{0}] 单据号:{1},开具结果:{2},对应发票信息:机动车销售统一发票,{3},{4}", objArray1);
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

        private bool check_port(string str_com)
        {
            List<string> list = new List<string>();
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Hardware\DeviceMap\SerialComm");
            if (key != null)
            {
                foreach (string str in key.GetValueNames())
                {
                    string item = (string) key.GetValue(str);
                    list.Add(item);
                }
            }
            if (list.Count < 0)
            {
                return false;
            }
            if (list.IndexOf(str_com) == -1)
            {
                return false;
            }
            return true;
        }

        private void ClearMainInfo()
        {
            this.DelTextChangedEvent();
            this.cmbGhdw.Text = "";
            this.cmbGhdwsh.Text = "";
            this.cmbSfzh.Text = "";
            this.cmbCllx.Text = "";
            this.txtCpxh.Text = "";
            this.txtCd.Text = "";
            this.txtHgzh.Text = "";
            this.txtJkzmh.Text = "";
            this.txtSjdh.Text = "";
            this.cmbFdjh.Text = "";
            this.txtClsbh.Text = "";
            this.txtCjmc.Text = "";
            this.txtDh.Text = "";
            this.txtZh.Text = "";
            this.txtDz.Text = this._fpxx.Dz;
            this.txtKhyh.Text = "";
            this.txtDw.Text = "";
            this.txtXcrs.Text = "";
            this._fpxx.SetJshj("0.00");
            this.SetHzxx();
            this.RegTextChangedEvent();
        }

        private void CloseJDCForm()
        {
            base.FormClosing -= new FormClosingEventHandler(this.JDCInvoiceForm_FormClosing);
            base.Close();
        }

        private DataTable ClxxOnAutoCompleteDataSource(string str)
        {
            object[] objArray1 = new object[] { str, 20, "MC,CPXH,CD,SCCJMC,BM,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV,YHZCMC" };
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetCLMore", objArray1);
            if ((objArray != null) && (objArray.Length != 0))
            {
                return (objArray[0] as DataTable);
            }
            return null;
        }

        private void ClxxSetValue(object[] bmxx)
        {
            if (bmxx != null)
            {
                this.DelTextChangedEvent();
                if ((!this.isWM() && (bmxx[6].ToString() == "是")) && ((bmxx[9].ToString() == "") && !this.Update_CL(bmxx)))
                {
                    MessageBox.Show("更新数据库失败！");
                }
                this.cmbCllx.Text = bmxx[0].ToString();
                this.txtCpxh.Text = bmxx[1].ToString();
                this.txtCd.Text = bmxx[2].ToString();
                this.txtCjmc.Text = bmxx[3].ToString();
                if (bmxx.Length >= 9)
                {
                    if (!this.isWM())
                    {
                        this.flbm = bmxx[5].ToString();
                        object[] objArray1 = new object[] { bmxx[5].ToString(), false };
                        object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.CanUseThisSPFLBM", objArray1);
                        if ((objArray != null) && !bool.Parse(objArray[0].ToString()))
                        {
                            this.flbm = "";
                            this.cmbCllx.Text = "";
                            string[] textArray1 = new string[] { "车辆类型", "\r\n可能原因：\r\n1、当前企业没有所选税收分类编码授权。\r\n2、当前版本所选税收分类编码可用状态为不可用。" };
                            MessageManager.ShowMsgBox("INP-242207", textArray1);
                            return;
                        }
                        if (this.flbm.Length < 0x13)
                        {
                            this.flbm = this.flbm.PadRight(0x13, '0');
                        }
                        this.spbh = bmxx[4].ToString();
                        this.yhsm = bmxx[9].ToString();
                        this.Lslvbs = "";
                    }
                    else
                    {
                        this.spbh = "";
                        this.yhsm = "";
                        this.Lslvbs = "";
                        this.flbm = "";
                        this.xsyh = "";
                    }
                    if (bmxx[6].ToString() == "是")
                    {
                        this.xsyh = "1";
                    }
                    else
                    {
                        this.xsyh = "0";
                    }
                    this.SetSlvcbm();
                    this.cmbSlv.SelectedIndex = 0;
                }
                this.RegTextChangedEvent();
            }
        }

        private void cmbcl_OnAutoComplate(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            string text = combox.Text;
            if (combox != null)
            {
                text = combox.Text;
                DataTable table = this.ClxxOnAutoCompleteDataSource(text);
                if (table != null)
                {
                    combox.DataSource=table;
                }
            }
        }

        private void cmbcl_OnButtonClick(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = (AisinoMultiCombox) sender;
            try
            {
                object[] objArray1 = new object[] { combox.Text, 0, "MC,CPXH,CD,SCCJMC,BM,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV,YHZCMC" };
                object[] bmxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetCL", objArray1);
                if ((!this.isWM() && (bmxx != null)) && ((bmxx.Length >= 5) && (bmxx[5].ToString() == "")))
                {
                    object[] objArray2 = new object[] { bmxx[0].ToString(), "", bmxx[4].ToString(), true };
                    bmxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddCL", objArray2);
                }
                this.ClxxSetValue(bmxx);
            }
            catch (Exception exception)
            {
                this.log.Error(exception);
            }
        }

        private void cmbcl_OnSelectValue(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if (combox != null)
            {
                Dictionary<string, string> dictionary = combox.SelectDict;
                if ((dictionary["SPFL"].ToString() == "") && !this.isWM())
                {
                    object[] objArray = new object[] { dictionary["MC"], "", dictionary["BM"], true };
                    object[] bmxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddCL", objArray);
                    if (bmxx == null)
                    {
                        combox.Text = "";
                    }
                    this.ClxxSetValue(bmxx);
                }
                else
                {
                    object[] objArray3 = new object[] { dictionary["MC"], dictionary["CPXH"], dictionary["CD"], dictionary["SCCJMC"], dictionary["BM"], dictionary["SPFL"], dictionary["YHZC"], dictionary["SPFL_ZZSTSGL"], dictionary["YHZC_SLV"], dictionary["YHZCMC"] };
                    this.ClxxSetValue(objArray3);
                }
            }
        }

        private void cmbCllx_TextChanged(object sender, EventArgs e)
        {
            string text = this.cmbCllx.Text;
            this._fpxx.Cllx=text;
            if (this._fpxx.Cllx != text)
            {
                this.cmbCllx.Text = this._fpxx.Cllx;
                this.cmbCllx.SelectionStart=this.cmbCllx.Text.Length;
            }
        }

        private void cmbFdjh_TextChanged(object sender, EventArgs e)
        {
            string text = this.cmbFdjh.Text;
            this._fpxx.Fdjhm=text;
            if (this._fpxx.Fdjhm != text)
            {
                this.cmbFdjh.Text = this._fpxx.Fdjhm;
                this.cmbFdjh.SelectionStart = this.cmbFdjh.Text.Length;
            }
        }

        private void cmbGhdw_TextChanged(object sender, EventArgs e)
        {
            string text = this.cmbGhdw.Text;
            this._fpxx.Gfmc=text;
            if (this._fpxx.Gfmc != text)
            {
                this.cmbGhdw.Text = this._fpxx.Gfmc;
                this.cmbGhdw.SelectionStart=this.cmbGhdw.Text.Length;
            }
        }

        private void cmbGhdwsh_TextChanged(object sender, EventArgs e)
        {
            string text = this.cmbGhdwsh.Text;
            this._fpxx.Gfsh=text;
            if (this._fpxx.Gfsh != text)
            {
                this.cmbGhdwsh.Text = this._fpxx.Gfsh;
                this.cmbGhdwsh.SelectionStart=this.cmbGhdwsh.Text.Length;
            }
        }

        private void cmbSfzh_TextChanged(object sender, EventArgs e)
        {
            string text = this.cmbSfzh.Text;
            this._fpxx.Sfzh_zzjgdm=text;
            if (this._fpxx.Sfzh_zzjgdm != text)
            {
                this.cmbSfzh.Text = this._fpxx.Sfzh_zzjgdm;
                this.cmbSfzh.SelectionStart=this.cmbSfzh.Text.Length;
            }
        }

        private void cmbSlv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbSlv.SelectedItem != null)
            {
                if (!this._fpxx.SetFpSLv(this.jdcManager.GetSLValue(this.cmbSlv.Text)))
                {
                    MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
                }
                else
                {
                    this.SetHzxx();
                    this.SetZeroSlvbz(this.cmbSlv.SelectedItem.ToString());
                }
            }
        }

        private void cmbxx_OnAutoComplate(object sender, EventArgs e)
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

        private void cmbxx_OnButtonClick(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = (AisinoMultiCombox) sender;
            try
            {
                object[] objArray1 = new object[] { combox.Text, 0, "MC,SH,IDCOC" };
                object[] gfxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetGHDW", objArray1);
                this.GfxxSetValue(gfxx);
            }
            catch (Exception exception)
            {
                this.log.Error(exception);
            }
        }

        private void cmbxx_OnSelectValue(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if (combox != null)
            {
                Dictionary<string, string> dictionary = combox.SelectDict;
                object[] gfxx = new object[] { dictionary["MC"], dictionary["SH"], dictionary["IDCOC"] };
                this.GfxxSetValue(gfxx);
            }
        }

        public void ComCL_leave(object sender, EventArgs e)
        {
            if (!this.isWM())
            {
                AisinoMultiCombox combox = sender as AisinoMultiCombox;
                if (combox.Text != "")
                {
                    string str = combox.Text.Trim();
                    DataTable table = this.ClxxOnAutoCompleteDataSource(combox.Text.ToString());
                    if ((table == null) || (table.Rows.Count == 0))
                    {
                        if ((this.flbm == null) || (this.flbm == ""))
                        {
                            string text = combox.Text;
                            object[] objArray = new object[] { text, "", "", false };
                            object[] bmxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddCL", objArray);
                            if (bmxx == null)
                            {
                                combox.Text = "";
                            }
                            this.ClxxSetValue(bmxx);
                        }
                    }
                    else if ((this.flbm == null) || (this.flbm == ""))
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            if ((table.Rows[i]["SPFL"].ToString() != "") && (str == table.Rows[i]["MC"].ToString()))
                            {
                                object[] objArray7 = new object[] { table.Rows[i]["MC"], table.Rows[i]["CPXH"], table.Rows[i]["CD"], table.Rows[i]["SCCJMC"], table.Rows[i]["BM"], table.Rows[i]["SPFL"], table.Rows[i]["YHZC"], table.Rows[i]["SPFL_ZZSTSGL"], table.Rows[i]["YHZC_SLV"], table.Rows[i]["YHZCMC"] };
                                this.ClxxSetValue(objArray7);
                                return;
                            }
                        }
                        if (str == table.Rows[0]["MC"].ToString())
                        {
                            object[] objArray3 = new object[] { table.Rows[0]["MC"], "", table.Rows[0]["BM"], true };
                            object[] objArray4 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddSP", objArray3);
                            if (objArray4 == null)
                            {
                                combox.Text = "";
                            }
                            this.ClxxSetValue(objArray4);
                        }
                        else
                        {
                            object[] objArray5 = new object[] { str, "", "", false };
                            object[] objArray6 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddSP", objArray5);
                            if (objArray6 == null)
                            {
                                combox.Text = "";
                            }
                            this.ClxxSetValue(objArray6);
                        }
                    }
                }
            }
        }

        private void CreateJDCInvoice(bool isRed, FPLX fplx)
        {
            byte[] destinationArray = new byte[0x20];
            byte[] sourceArray = Invoice.TypeByte;
            Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
            byte[] buffer2 = new byte[0x10];
            Array.Copy(sourceArray, 0x20, buffer2, 0, 0x10);
            byte[] buffer3 = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes("KP" + DateTime.Now.ToString("F")), destinationArray, buffer2);
            Invoice.IsGfSqdFp_Static=false;
            this._fpxx = new Invoice(isRed, false, false, fplx, buffer3, null);
            this._fpxx.Bmbbbh=this.getbmbbbh();
            this._fpxx.Xfsh=this.fpm.GetXfsh();
            this._fpxx.Xfmc=this.fpm.GetXfmc();
        }

        [DllImport("termb.dll", CharSet=CharSet.Auto)]
        public static extern int CVR_Authenticate();
        [DllImport("termb.dll", CharSet=CharSet.Auto)]
        public static extern int CVR_CloseComm();
        [DllImport("termb.dll", CharSet=CharSet.Auto)]
        public static extern int CVR_InitComm(int Port);
        [DllImport("termb.dll", CharSet=CharSet.Auto)]
        public static extern int CVR_Read_Content(int Active);
        private void DelTextChangedEvent()
        {
            this.cmbGhdw.OnTextChanged = (EventHandler) Delegate.Remove(this.cmbGhdw.OnTextChanged, new EventHandler(this.cmbGhdw_TextChanged));
            this.cmbGhdwsh.OnTextChanged = (EventHandler) Delegate.Remove(this.cmbGhdwsh.OnTextChanged, new EventHandler(this.cmbGhdwsh_TextChanged));
            this.cmbSfzh.OnTextChanged = (EventHandler) Delegate.Remove(this.cmbSfzh.OnTextChanged, new EventHandler(this.cmbSfzh_TextChanged));
            this.cmbCllx.OnTextChanged = (EventHandler) Delegate.Remove(this.cmbCllx.OnTextChanged, new EventHandler(this.cmbCllx_TextChanged));
            this.cmbFdjh.TextChanged -= new EventHandler(this.cmbFdjh_TextChanged);
            this.txtCpxh.TextChanged -= new EventHandler(this.txtCpxh_TextChanged);
            this.txtCd.TextChanged -= new EventHandler(this.txtCd_TextChanged);
            this.txtHgzh.TextChanged -= new EventHandler(this.txtHgzh_TextChanged);
            this.txtJkzmh.TextChanged -= new EventHandler(this.txtJkzmh_TextChanged);
            this.txtSjdh.TextChanged -= new EventHandler(this.txtSjdh_TextChanged);
            this.txtClsbh.TextChanged -= new EventHandler(this.txtClsbh_TextChanged);
            this.txtCjmc.TextChanged -= new EventHandler(this.txtCjmc_TextChanged);
            this.txtDz.TextChanged -= new EventHandler(this.txtDz_TextChanged);
            this.txtKhyh.TextChanged -= new EventHandler(this.txtKhyh_TextChanged);
            this.txtZh.TextChanged -= new EventHandler(this.txtZh_TextChanged);
            this.txtDh.TextChanged -= new EventHandler(this.txtDh_TextChanged);
            this.txtXcrs.TextChanged -= new EventHandler(this.txtXcrs_TextChanged);
            this.txtDw.TextChanged -= new EventHandler(this.txtDw_TextChanged);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        [DllImport("DecodeXMLFIle.dll", CallingConvention=CallingConvention.StdCall)]
        public static extern int EncodeFile(byte[] InFile, byte[] key, byte[] OutFile, byte[] info);
        internal bool FillDjxx(Djfp djfp)
        {
            bool flag = true;
            this.djfile = djfp.File;
            Fpxx fpxx = djfp.Fpxx;
            this._fpxx.Bmbbbh=fpxx.bmbbbh;
            this._fpxx.Gfmc=fpxx.gfmc;
            this._fpxx.Gfsh=fpxx.gfsh;
            this._fpxx.Sfzh_zzjgdm=fpxx.sfzhm;
            this._fpxx.Cllx=fpxx.cllx;
            this._fpxx.Cpxh=fpxx.cpxh;
            this._fpxx.Cd=fpxx.cd;
            this._fpxx.Hgzh=fpxx.hgzh;
            this._fpxx.Jkzmsh=fpxx.jkzmsh;
            this._fpxx.Sjdh=fpxx.sjdh;
            this._fpxx.Fdjhm=fpxx.fdjhm;
            this._fpxx.Clsbdh_cjhm=fpxx.clsbdh;
            this._fpxx.Dh=fpxx.xfdh;
            this._fpxx.Zh=fpxx.xfzh;
            this._fpxx.Dz=fpxx.xfdz;
            this._fpxx.Khyh=fpxx.xfyh;
            this._fpxx.Dw=fpxx.dw;
            this._fpxx.Xcrs=fpxx.xcrs;
            this._fpxx.Sccjmc=fpxx.sccjmc;
            this._fpxx.Zyspmc=fpxx.zyspmc;
            this._fpxx.Zyspsm=fpxx.zyspsm;
            this._fpxx.Skr=fpxx.skr;
            this._fpxx.Hjje=fpxx.je;
            this._fpxx.Hjse=fpxx.se;
            this._fpxx.Xsdjbh=fpxx.xsdjbh;
            bool flag2 = this._fpxx.SetFpSLv(this.jdcManager.GetSLValue(fpxx.sLv));
            if (flag2)
            {
                flag2 = this._fpxx.SetJshj(decimal.Add(decimal.Parse(fpxx.je), decimal.Parse(fpxx.se)).ToString());
            }
            if (!flag2)
            {
                flag = false;
            }
            if (!flag)
            {
                djfp.Fpxx = null;
                string errorMessage = FPDJHelper.GetErrorMessage(this._fpxx.GetCode(), this._fpxx.Params);
                object[] args = new object[] { base.TaxCardInstance.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djfp.Djh, 0, errorMessage };
                AutoImport.KpResult = string.Format("[{0}] 单据号:{1},开具结果:{2},开具失败原因:{3}", args);
            }
            return flag;
        }

        protected string getbmbbbh()
        {
            if (this.isWM())
            {
                return "";
            }
            return new SPFLService().GetMaxBMBBBH();
        }

        private void GetFPText()
        {
            this.DelTextChangedEvent();
            this.cmbGhdw.Text = this._fpxx.Gfmc;
            this.cmbGhdwsh.Text = this._fpxx.Gfsh;
            this.cmbSfzh.Text = this._fpxx.Sfzh_zzjgdm;
            this.cmbCllx.Text = this._fpxx.Cllx;
            this.txtCpxh.Text = this._fpxx.Cpxh;
            this.txtCd.Text = this._fpxx.Cd;
            this.txtHgzh.Text = this._fpxx.Hgzh;
            this.txtJkzmh.Text = this._fpxx.Jkzmsh;
            this.txtSjdh.Text = this._fpxx.Sjdh;
            this.cmbFdjh.Text = this._fpxx.Fdjhm;
            this.txtClsbh.Text = this._fpxx.Clsbdh_cjhm;
            this.txtCjmc.Text = this._fpxx.Sccjmc;
            this.txtDh.Text = this._fpxx.Dh;
            this.txtZh.Text = this._fpxx.Zh;
            this.txtDz.Text = this._fpxx.Dz;
            this.txtKhyh.Text = this._fpxx.Khyh;
            this.txtDw.Text = this._fpxx.Dw;
            this.txtXcrs.Text = this._fpxx.Xcrs;
            this.RegTextChangedEvent();
        }

        private void GetNextFp()
        {
            if (this.fpm.CanInvoice(this._fpxx.Fplx))
            {
                FPLX fplx = this._fpxx.Fplx;
                string[] current = this.fpm.GetCurrent(fplx);
                if ((current != null) && (current.Length == 2))
                {
                    string str = this._fpxx.Kprq;
                    string str2 = this._fpxx.Zgswjg_dm;
                    string str3 = this._fpxx.Zgswjg_mc;
                    string str4 = this._fpxx.Jqbh;
                    bool flag = this._fpxx.Jdc_ver_new;
                    this.CreateJDCInvoice(false, this._fpxx.Fplx);
                    this._fpxx.Fpdm=current[0];
                    this._fpxx.Fphm=current[1];
                    this._fpxx.Jqbh=str4;
                    this._fpxx.Kprq=str;
                    this._fpxx.Zgswjg_mc=str3;
                    this._fpxx.Zgswjg_dm=str2;
                    this._fpxx.Kpr=UserInfo.Yhmc;
                    this._fpxx.Jdc_ver_new=flag;
                    this._fpxx.Dz=this.txtDz.Text;
                    this._fpxx.Dh=this.txtDh.Text;
                    this._fpxx.Khyh=this.txtKhyh.Text;
                    this._fpxx.Zh=this.txtZh.Text;
                }
                else
                {
                    this._fpxx.Fpdm="000000000000";
                    this._fpxx.Fphm="00000000";
                }
            }
        }

        private void GetOtherText()
        {
            this.cmbGhdwsh.Text = this._fpxx.Gfsh;
            this.cmbGhdw.Text = this._fpxx.Gfmc;
            this.cmbSfzh.Text = this._fpxx.Sfzh_zzjgdm;
            this.cmbCllx.Text = this._fpxx.Cllx;
            this.txtCpxh.Text = this._fpxx.Cpxh;
            this.txtHgzh.Text = this._fpxx.Hgzh;
            this.txtJkzmh.Text = this._fpxx.Jkzmsh;
            this.txtSjdh.Text = this._fpxx.Sjdh;
            this.cmbFdjh.Text = this._fpxx.Fdjhm;
            this.txtClsbh.Text = this._fpxx.Clsbdh_cjhm;
            this.txtDw.Text = this._fpxx.Dw;
            this.txtDz.Text = this._fpxx.Dz;
            this.txtDh.Text = this._fpxx.Dh;
            this.txtZh.Text = this._fpxx.Zh;
            this.txtKhyh.Text = this._fpxx.Khyh;
            this.txtXcrs.Text = this._fpxx.Xcrs;
            this.txtJsxx.Text = "0.00";
            this.txtKhyh.Text = "0.00";
        }

        [DllImport("termb.dll", CallingConvention=CallingConvention.StdCall, CharSet=CharSet.Ansi)]
        public static extern int GetPeopleIDCode(ref byte strTmp, ref int strLen);
        [DllImport("termb.dll", CharSet=CharSet.Ansi)]
        public static extern int GetPeopleName(ref byte strTmp, ref int strLen);
        private void GetQyLabel()
        {
            this.lblXhdwmc.Text = this._fpxx.Xfmc;
            this.lblNsrsbh.Text = this._fpxx.Xfsh;
            this.lblSwjg.Text = this._fpxx.Zgswjg_mc;
            this.lblSwjgdm.Text = this._fpxx.Zgswjg_dm;
            this.lblKpr.Text = this._fpxx.Kpr;
        }

        protected string GetSL(string text)
        {
            decimal num;
            if ((text.EndsWith("%") && (text != "0%")) && decimal.TryParse(text.Substring(0, text.Length - 1), out num))
            {
                text = decimal.Divide(num, decimal.Parse("100")).ToString();
            }
            if ((!(text == "0%") && !(text == "免税")) && !(text == "不征税"))
            {
                return text;
            }
            return "0.00";
        }

        private string[] getSYslv()
        {
            string flbm = this.flbm;
            if (flbm.Length != 0x13)
            {
                return new string[0];
            }
            int num = flbm.Length - 1;
            while (flbm[num] == '0')
            {
                num--;
            }
            int count = (flbm.Length - num) - 1;
            if ((count % 2) != 0)
            {
                count--;
            }
            flbm = flbm.Remove(0x13 - count, count);
            object[] objArray = new object[] { flbm };
            object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.GetSLV_BY_BM", objArray);
            if ((objArray2 == null) || (objArray2.Length == 0))
            {
                return new string[0];
            }
            DataTable table = objArray2[0] as DataTable;
            if (table.Rows.Count == 0)
            {
                return new string[0];
            }
            DataRow row = table.Rows[0];
            string[] separator = new string[] { "、", ",", "，" };
            string[] source = row["SLV"].ToString().Split(separator, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == "1.5%_5%")
                {
                    source[i] = "1.5%";
                }
            }
            string[] textArray2 = new string[] { "、", ",", "，" };
            string[] strArray2 = row["YHZC_SLV"].ToString().Split(textArray2, StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < strArray2.Length; j++)
            {
                if (strArray2[j] == "1.5%_5%")
                {
                    strArray2[j] = "1.5%";
                }
            }
            if ((source.Length + strArray2.Length) == 0)
            {
                return new string[0];
            }
            List<string> list = new List<string>();
            list.AddRange(source.ToList<string>());
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

        private void GetTaxLabel()
        {
            this.lblFpdm.Text = this._fpxx.Fpdm;
            this.lblFphm.Text = this._fpxx.Fphm;
            this.lblJqbh.Text = this._fpxx.Jqbh;
            this.lblKprq.Text = this._fpxx.Kprq;
            this.lblDM.Text = this._fpxx.Fpdm;
            this.lblHM.Text = this._fpxx.Fphm;
        }

        private DataTable GfxxOnAutoCompleteDataSource(string str)
        {
            object[] objArray1 = new object[] { str, 20, "MC,SH,IDCOC" };
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetGHDWMore", objArray1);
            if ((objArray != null) && (objArray.Length != 0))
            {
                return (objArray[0] as DataTable);
            }
            return null;
        }

        private void GfxxSetValue(object[] gfxx)
        {
            if (gfxx != null)
            {
                string str = gfxx[0].ToString();
                string str2 = gfxx[1].ToString();
                string str3 = gfxx[2].ToString();
                this.cmbGhdw.Text = str;
                this.cmbGhdwsh.Text = str2;
                this.cmbSfzh.Text = str3;
            }
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.tool_close = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_close");
            this.tool_print = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_print");
            this.tool_fuzhi = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_fuzhi");
            this.tool_zuofei = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_zuofei");
            this.tool_kehu = this.xmlComponentLoader1.GetControlByName<ToolStripDropDownButton>("tool_kehu");
            this.tool_autokh = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_autokh");
            this.tool_manukh = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_manukh");
            this.tool_fushu = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_fushu");
            this.tool_piaozhong = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_piaozhong");
            this.tool_piaozhong.Visible = false;
            this.tool_saomiao = this.xmlComponentLoader1.GetControlByName<ToolStripDropDownButton>("tool_saomiao");
            this.tool_guochan = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_guochan");
            this.tool_jinkou = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_jinkou");
            this.tool_set = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_set");
            this.tool_sfz = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_sfz");
            this.tool_daoru = this.xmlComponentLoader1.GetControlByName<ToolStripDropDownButton>("tool_daoru");
            this.tool_drsz = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_drsz");
            this.tool_sgdr = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_sgdr");
            this.tool_plzddr = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_plzddr");
            this.lblTitle = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblTitle");
            this.lblFpdm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblFpdm");
            this.lblFphm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblFphm");
            this.lblJqbh = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJqbh");
            this.lblKprq = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblKprq");
            this.lblKpr = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblKpr");
            this.lblNsrsbh = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblNsrsbh");
            this.lblXhdwmc = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblXhdwmc");
            this.lblJsdx = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJsdx");
            this.lblSe = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblSe");
            this.lblSwjgdm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblSwjgdm");
            this.lblSwjg = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblSwjg");
            this.cmbGhdwsh = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("cmbGhdwsh");
            this.cmbGhdw = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("cmbGhdw");
            this.cmbCllx = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("cmbCllx");
            this.cmbFdjh = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbFdjh");
            this.cmbSfzh = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("cmbSfzh");
            this.txtClsbh = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtClsbh");
            this.txtCpxh = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtCpxh");
            this.txtSjdh = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtSjdh");
            this.txtHgzh = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtHgzh");
            this.txtJkzmh = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtJkzmh");
            this.txtCd = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtCd");
            this.cmbSlv = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbSlv");
            this.txtCjmc = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtCjmc");
            this.txtDz = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtDz");
            this.txtKhyh = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtKhyh");
            this.txtZh = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtZh");
            this.txtDh = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtDh");
            this.txtJsxx = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtJsxx");
            this.txtXcrs = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtXcrs");
            this.lblJgxx = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJgxx");
            this.txtDw = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtDw");
            this.lblMW = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblMW");
            this.lblDM = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblDM");
            this.lblHM = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblHM");
            this.lblBT1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBT1");
            this.lblBT2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBT2");
            this.lblBT3 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBT3");
            this.lblBT4 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBT4");
            this.lblBT5 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBT5");
            this.lblBT6 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBT6");
            this.toolStrip3 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip3");
            ControlStyleUtil.SetToolStripStyle(this.toolStrip3);
            this.panel2 = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel2");
            this.panel1 = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel1");
            this.picZuofei = this.xmlComponentLoader1.GetControlByName<AisinoPIC>("picZuofei");
            this.picZuofei.Visible = false;
            this.picZuofei.BackColor = System.Drawing.Color.Transparent;
            this.picZuofei.SizeMode = PictureBoxSizeMode.Zoom;
            this.panel1.BackgroundImage = Resources.JDCN;
            this.panel1.BackgroundImageLayout = ImageLayout.Zoom;
            this.panel2.AutoScroll = true;
            this.panel2.AutoScrollMinSize = new Size(0x3c4, 710);
            this.tool_close.Margin = new Padding(20, 1, 0, 2);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(JDCInvoiceForm_new));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = System.Drawing.Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x3c4, 0x2fa);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath=@"..\Config\Components\Aisino.Fwkp.Fpkj.Form.JDCFPtiankai_new_new\Aisino.Fwkp.Fpkj.Form.JDCFPtiankai_new_new.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x3c4, 0x2fa);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            this.MinimumSize = new Size(840, 540);
            base.Name = "JDCInvoiceForm_new";
            base.TabText="机动车销售统一发票填开";
            this.Text = "机动车销售统一发票填开";
            base.ResumeLayout(false);
        }

        private void InitializeDefault()
        {
            this.fpm = new FpManager();
            this.jdcManager = new InvoiceHelper();
            this.Initialize();
            this.SetTitleFont();
            this.cmbFdjh.Items.Add("无");
            this.cmbSlv.DropDownStyle = ComboBoxStyle.DropDownList;
            this.tool_print.ToolTipText = this.onlyShow ? "发票打印" : "开具发票并打印";
            this.tool_zuofei.ToolTipText = "作废发票";
            this.tool_fuzhi.ToolTipText = "复制发票";
            this.tool_fushu.ToolTipText = "开具销项红字";
            this.tool_close.ToolTipText = "退出";
            this.tool_zuofei.CheckOnClick = false;
            this.tool_autokh.CheckOnClick = true;
            this.tool_print.MouseDown += new MouseEventHandler(this.tool_print_MouseDown);
            this.tool_print.Click += new EventHandler(this.tool_print_Click);
            this.tool_zuofei.Click += new EventHandler(this.tool_zuofei_Click);
            this.tool_fuzhi.Click += new EventHandler(this.tool_fuzhi_Click);
            this.tool_fushu.Click += new EventHandler(this.tool_fushu_Click);
            this.tool_close.Click += new EventHandler(this.tool_close_Click);
            this.tool_manukh.Click += new EventHandler(this.tool_kehu_Click);
            this.tool_autokh.Click += new EventHandler(this.tool_autokh_Click);
            this.tool_piaozhong.Click += new EventHandler(this.tool_piaozhong_Click);
            this.tool_guochan.Click += new EventHandler(this.tool_guochan_Click);
            this.tool_jinkou.Click += new EventHandler(this.tool_jinkou_Click);
            this.tool_set.Click += new EventHandler(this.tool_set_Click);
            this.tool_sfz.Click += new EventHandler(this.tool_sfz_Click);
            this.tool_drsz.Click += new EventHandler(this.tool_imputSet_Click);
            this.tool_sgdr.Click += new EventHandler(this.tool_manualImport_Click);
            this.tool_plzddr.Click += new EventHandler(this.tool_autoImport_Click);
            if (!this.onlyShow)
            {
                this.SetAutoKHChecked();
                this.SetGhfCmb(this.cmbGhdw, "MC");
                this.SetGhfCmb(this.cmbGhdwsh, "SH");
                this.SetGhfCmb(this.cmbSfzh, "IDCOC");
                this.SetCllxCmb(this.cmbCllx, "LXMC");
                this.RegTextChangedEvent();
                this.cmbSlv.SelectedIndexChanged += new EventHandler(this.cmbSlv_SelectedIndexChanged);
                this.txtJsxx.LostFocus += new EventHandler(this.txtJsxx_LostFocus);
                base.FormClosing += new FormClosingEventHandler(this.JDCInvoiceForm_FormClosing);
            }
            base.KeyPreview = true;
            base.KeyDown += new KeyEventHandler(this.JDCInvoiceForm_KeyDown);
            base.Resize += new EventHandler(this.JDCInvoiceForm_new_Resize);
            this.tool_sfz.ToolTipText = "读取身份证信息";
            if (!RegisterManager.CheckRegFile("ERJS"))
            {
                this.tool_saomiao.Visible = false;
                this.tool_sfz.Visible = false;
            }
        }

        private void InitNextFp()
        {
            this.isHzwm = false;
            if (this.fpm.CanInvoice(this._fpxx.Fplx))
            {
                FPLX fplx = this._fpxx.Fplx;
                string[] current = this.fpm.GetCurrent(fplx);
                if ((current == null) || (current.Length != 2))
                {
                    MessageManager.ShowMsgBox(this.fpm.Code(), this.fpm.CodeParams());
                    if (this.fpm.Code() != "000000")
                    {
                        this.CloseJDCForm();
                    }
                }
                else if (new StartConfirmForm(this._fpxx.Fplx, current).ShowDialog() != DialogResult.OK)
                {
                    this.CloseJDCForm();
                }
                else
                {
                    this._fpxx.Fpdm=current[0];
                    this._fpxx.Fphm=current[1];
                    this.RefreshData(false);
                    if (!this.txtCpxh.Enabled)
                    {
                        this.SetTxtEnabled(true);
                    }
                }
            }
            else
            {
                MessageManager.ShowMsgBox(this.fpm.Code(), this.fpm.CodeParams());
                this.CloseJDCForm();
            }
        }

        private bool InitSFZinfo()
        {
            int num3;
            int num = 0;
            int num2 = 0;
            for (num3 = 0x3e9; num3 <= 0x3f8; num3++)
            {
                num = CVR_InitComm(num3);
                if (num == 1)
                {
                    break;
                }
            }
            if (num != 1)
            {
                for (num3 = 1; num3 <= 4; num3++)
                {
                    num2 = CVR_InitComm(num3);
                    if (num2 == 1)
                    {
                        break;
                    }
                }
            }
            if ((num != 1) && (num2 != 1))
            {
                MessageManager.ShowMsgBox("INP-242191");
                return false;
            }
            return true;
        }

        public bool isWM()
        {
            bool flag = !FLBM_lock.isFlbm();
            return (this.isHzwm | flag);
        }

        private void JDCInvoiceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.txtJsxx.LostFocus -= new EventHandler(this.txtJsxx_LostFocus);
            if (!this.onlyShow && (MessageManager.ShowMsgBox("INP-242199") != DialogResult.Yes))
            {
                e.Cancel = true;
            }
        }

        private void JDCInvoiceForm_Init(FPLX fplx, string fpdm, string fphm)
        {
            this.InitializeDefault();
            this.SetFormTitle(fplx, fpdm);
            this.tool_zuofei.Visible = false;
            this.tool_piaozhong.Visible = false;
            this.CreateJDCInvoice(false, fplx);
            this._fpxx.Jdc_ver_new=true;
            this._fpxx.Jqbh=this.fpm.GetMachineNum();
            this._fpxx.Dz=this.fpm.GetXfdz();
            this._fpxx.Dh=this.fpm.GetXfdh();
            this._fpxx.Zh=this.fpm.GetXfyhzh();
            this._fpxx.Khyh=this.fpm.GetXfyhzh();
            string[] zgswjg = this.fpm.GetZgswjg();
            if (zgswjg != null)
            {
                this._fpxx.Zgswjg_dm=zgswjg[0];
                this._fpxx.Zgswjg_mc=zgswjg[1];
            }
            this._fpxx.Fpdm=fpdm;
            this._fpxx.Fphm=fphm;
            this._fpxx.Kprq=this.fpm.GetJskClock();
            this._fpxx.Kpr=UserInfo.Yhmc;
            this.SetSlvcbm();
            if (this.cmbSlv.Items.Count == 0)
            {
                this.initSuccess = false;
                string[] textArray1 = new string[] { "机动车销售统一发票" };
                MessageManager.ShowMsgBox("INP-242129", textArray1);
            }
            else
            {
                this.ShowInitInvInfo();
            }
        }

        private void JDCInvoiceForm_KeyDown(object sender, KeyEventArgs e)
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
                    string str = strArray[5];
                    if (((int)fpzl == 12) && (str.Substring(4, 1) == "2"))
                    {
                        this.ykfp = this.fpm.GetXxfp(fpzl, strArray[1], int.Parse(strArray[2]));
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
        }

        private void JDCInvoiceForm_new_Resize(object sender, EventArgs e)
        {
            if (this.panel1 != null)
            {
                this.panel1.Location = new Point((base.Width - this.panel1.Width) / 2, this.panel1.Location.Y);
            }
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
                this.CloseJDCForm();
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

        private void RefreshData(bool isRed)
        {
            string str = this._fpxx.Kprq;
            string str2 = this._fpxx.Fpdm;
            string str3 = this._fpxx.Fphm;
            string str4 = this._fpxx.Zgswjg_dm;
            string str5 = this._fpxx.Zgswjg_mc;
            string str6 = this._fpxx.Jqbh;
            bool flag = this._fpxx.Jdc_ver_new;
            this.CreateJDCInvoice(isRed, this._fpxx.Fplx);
            this._fpxx.Fpdm=str2;
            this._fpxx.Fphm=str3;
            this._fpxx.Jqbh=str6;
            this._fpxx.Zgswjg_mc=str5;
            this._fpxx.Zgswjg_dm=str4;
            this._fpxx.Kprq=str;
            this._fpxx.Kpr=UserInfo.Yhmc;
            this._fpxx.Jdc_ver_new=flag;
            if (!this.tool_fushu.Checked)
            {
                this._fpxx.Dz=this.txtDz.Text;
                this._fpxx.Dh=this.txtDh.Text;
                this._fpxx.Khyh=this.txtKhyh.Text;
                this._fpxx.Zh=this.txtZh.Text;
                this._fpxx.Dz=this.fpm.GetXfdz();
                this._fpxx.Dh=this.fpm.GetXfdh();
            }
            this._fpxx.SetFpSLv(this.jdcManager.GetSLValue(this.cmbSlv.Text));
            this.cmbSlv.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ClearMainInfo();
            this.ShowInitInvInfo();
            this.tool_fushu.Checked = isRed;
            if (isRed)
            {
                this.tool_fuzhi.Enabled = false;
            }
            else
            {
                this.tool_fuzhi.Enabled = true;
            }
            this.cmbGhdw.Edit=EditStyle.TextBox;
            this.cmbGhdwsh.Edit=EditStyle.TextBox;
            this.cmbSfzh.Edit=EditStyle.TextBox;
            this.cmbCllx.Edit=EditStyle.TextBox;
        }

        private void RegTextChangedEvent()
        {
            this.cmbGhdw.OnTextChanged = (EventHandler) Delegate.Combine(this.cmbGhdw.OnTextChanged, new EventHandler(this.cmbGhdw_TextChanged));
            this.cmbGhdwsh.OnTextChanged = (EventHandler) Delegate.Combine(this.cmbGhdwsh.OnTextChanged, new EventHandler(this.cmbGhdwsh_TextChanged));
            this.cmbSfzh.OnTextChanged = (EventHandler) Delegate.Combine(this.cmbSfzh.OnTextChanged, new EventHandler(this.cmbSfzh_TextChanged));
            this.cmbCllx.OnTextChanged = (EventHandler) Delegate.Combine(this.cmbCllx.OnTextChanged, new EventHandler(this.cmbCllx_TextChanged));
            this.cmbFdjh.TextChanged += new EventHandler(this.cmbFdjh_TextChanged);
            this.txtCpxh.TextChanged += new EventHandler(this.txtCpxh_TextChanged);
            this.txtCd.TextChanged += new EventHandler(this.txtCd_TextChanged);
            this.txtHgzh.TextChanged += new EventHandler(this.txtHgzh_TextChanged);
            this.txtJkzmh.TextChanged += new EventHandler(this.txtJkzmh_TextChanged);
            this.txtSjdh.TextChanged += new EventHandler(this.txtSjdh_TextChanged);
            this.txtClsbh.TextChanged += new EventHandler(this.txtClsbh_TextChanged);
            this.txtCjmc.TextChanged += new EventHandler(this.txtCjmc_TextChanged);
            this.txtDz.TextChanged += new EventHandler(this.txtDz_TextChanged);
            this.txtKhyh.TextChanged += new EventHandler(this.txtKhyh_TextChanged);
            this.txtZh.TextChanged += new EventHandler(this.txtZh_TextChanged);
            this.txtDh.TextChanged += new EventHandler(this.txtDh_TextChanged);
            this.txtXcrs.TextChanged += new EventHandler(this.txtXcrs_TextChanged);
            this.txtDw.TextChanged += new EventHandler(this.txtDw_TextChanged);
        }

        private void ResetCmbSlv()
        {
            List<SLV> slvList = this.jdcManager.GetSlvList(this._fpxx.Fplx, this._fpxx.GetSqSLv());
            this.cmbSlv.Items.Clear();
            this.cmbSlv.Items.AddRange(slvList.ToArray());
            if (this.cmbSlv.Items.Count > 0)
            {
                string str = PropertyUtil.GetValue("JDCINVNEW-SLV", "");
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

        private bool SaveFpData()
        {
            this.SetFpText();
            this._fpxx.Xfmc=this.lblXhdwmc.Text;
            this._fpxx.Xfsh=this.lblNsrsbh.Text;
            this._fpxx.Zgswjg_mc=this.lblSwjg.Text;
            this._fpxx.Zgswjg_dm=this.lblSwjgdm.Text;
            bool flag1 = this._fpxx.SetFpSLv(this.jdcManager.GetSLValue(this.cmbSlv.Text));
            if (flag1)
            {
                this._fpxx.SetJshj(this.txtJsxx.Text);
            }
            if (!flag1)
            {
                MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
            }
            return flag1;
        }

        private void SaveKH(bool autoSave)
        {
            object[] objArray = new object[] { this.cmbGhdw.Text, this.cmbGhdwsh.Text, this.cmbSfzh.Text };
            if (autoSave)
            {
                ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddGHDWAuto", objArray);
            }
            else
            {
                ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddGHDW", objArray);
            }
        }

        private void Set_flxx(string skr, string zyspmc, string zyspsm)
        {
            this.yhsm = "";
            this.xsyh = "";
            this.Lslvbs = "";
            this.spbh = "";
            this.flbm = "";
            string str = "#%";
            string[] separator = new string[] { str };
            string[] strArray = skr.Split(separator, StringSplitOptions.None);
            if (strArray.Length == 2)
            {
                this.xsyh = strArray[0];
                this.Lslvbs = strArray[1];
            }
            string[] textArray2 = new string[] { str };
            strArray = zyspsm.Split(textArray2, StringSplitOptions.None);
            if (strArray.Length == 2)
            {
                this.yhsm = strArray[1];
                this.spbh = strArray[0];
            }
            this.flbm = zyspmc;
            this.xsyh = "0";
            string[] textArray3 = new string[] { "、", ",", "，" };
            string[] strArray2 = this.yhsm.Split(textArray3, StringSplitOptions.RemoveEmptyEntries);
            bool flag = false;
            for (int i = 0; i < strArray2.Length; i++)
            {
                if (this.yhzc_contain_slv(strArray2[i], this._fpxx.SLv, false))
                {
                    this.yhsm = strArray2[i];
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                this.xsyh = "1";
            }
            else
            {
                this.yhsm = "";
            }
        }

        private void set_saomiao_info(string value, int type)
        {
            if ((value != null) && (value.Length > 0))
            {
                bool flag = false;
                bool flag2 = false;
                bool flag3 = true;
                int result = 0;
                if (int.TryParse(PropertyUtil.GetValue("CPXH_SET"), out result))
                {
                    if ((result & 4) > 0)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    if ((result & 2) > 0)
                    {
                        flag2 = true;
                    }
                    else
                    {
                        flag2 = false;
                    }
                    if ((result & 1) > 0)
                    {
                        flag3 = true;
                    }
                    else
                    {
                        flag3 = false;
                    }
                }
                string str = "#%";
                List<string> list = new List<string>();
                string[] separator = new string[] { str };
                foreach (string str3 in value.Split(separator, StringSplitOptions.None))
                {
                    list.Add(str3);
                }
                this.DelTextChangedEvent();
                string str2 = "";
                if (type == 1)
                {
                    this.txtCjmc.Text = list[3];
                    this.txtHgzh.Text = list[1];
                    this.cmbCllx.Text = list[5];
                    if (flag)
                    {
                        str2 = str2 + list[6];
                    }
                    if (flag2)
                    {
                        str2 = str2 + list[5];
                    }
                    if (flag3)
                    {
                        str2 = str2 + list[7];
                    }
                    this.txtCpxh.Text = str2;
                    this.txtClsbh.Text = list[12];
                    this.cmbFdjh.Text = list[14];
                    this.txtDw.Text = list[0x24];
                    this.txtXcrs.Text = list[40];
                    this.txtJkzmh.Text = "";
                }
                else if (type == 2)
                {
                    this.txtCjmc.Text = list[0];
                    this.txtHgzh.Text = "";
                    this.cmbCllx.Text = list[2];
                    if (flag)
                    {
                        str2 = str2 + list[1];
                    }
                    if (flag2)
                    {
                        str2 = str2 + list[2];
                    }
                    if (flag3)
                    {
                        str2 = str2 + list[3];
                    }
                    this.txtCpxh.Text = str2;
                    this.txtClsbh.Text = list[4];
                    this.cmbFdjh.Text = list[5];
                    this.txtDw.Text = "";
                    this.txtXcrs.Text = "";
                    this.txtJkzmh.Text = list[8];
                }
                this.RegTextChangedEvent();
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("MC", this.cmbCllx.Text);
                dictionary.Add("CPXH", this.txtCpxh.Text);
                dictionary.Add("CD", this.txtCd.Text);
                dictionary.Add("SCCJMC", this.txtCjmc.Text);
                ArrayList list2 = BaseDAOFactory.GetBaseDAOSQLite().querySQL("aisino.fwkp.fptk.GeClxx", dictionary);
                if ((list2 != null) && (list2.Count > 0))
                {
                    List<object> list3 = new List<object>();
                    foreach (Dictionary<string, object> dictionary2 in list2)
                    {
                        list3.Add(dictionary2["MC"].ToString());
                        list3.Add(dictionary2["CPXH"].ToString());
                        list3.Add(dictionary2["CD"].ToString());
                        list3.Add(dictionary2["SCCJMC"].ToString());
                        list3.Add(dictionary2["BM"].ToString());
                        list3.Add(dictionary2["SPFL"].ToString());
                        list3.Add(dictionary2["YHZC"].ToString());
                        list3.Add(dictionary2["WJ"].ToString());
                        list3.Add(dictionary2["KJM"].ToString());
                        list3.Add(dictionary2["YHZCMC"].ToString());
                    }
                    object[] bmxx = list3.ToArray();
                    this.ClxxSetValue(bmxx);
                }
                else
                {
                    string[] textArray2 = new string[] { this.cmbCllx.Text, " #% ", this.txtCpxh.Text, " #% ", this.txtCd.Text, " #% ", this.txtCjmc.Text };
                    string str4 = string.Concat(textArray2);
                    object[] objArray1 = new object[] { str4 };
                    object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddCL", objArray1);
                    if (objArray2 == null)
                    {
                        this.cmbCllx.Text = "";
                    }
                    this.ClxxSetValue(objArray2);
                }
            }
        }

        private void SetAutoKHChecked()
        {
            string str = PropertyUtil.GetValue("INV-AUTOSAVEKH", "0");
            this.tool_autokh.Checked = str != "0";
        }

        private void SetBtVisible(bool visible)
        {
            this.lblBT1.Visible = visible;
            this.lblBT2.Visible = visible;
            this.lblBT3.Visible = visible;
            this.lblBT4.Visible = visible;
            this.lblBT5.Visible = visible;
            this.lblBT6.Visible = visible;
        }

        private void SetCllxCmb(AisinoMultiCombox aisinoCmb, string showText)
        {
            aisinoCmb.IsSelectAll=true;
            aisinoCmb.buttonStyle=0;
            aisinoCmb.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", 100));
            aisinoCmb.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "CPXH", 100));
            aisinoCmb.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "CD", 80));
            aisinoCmb.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SCCJMC", 120));
            aisinoCmb.ShowText=showText;
            aisinoCmb.DrawHead=false;
            aisinoCmb.AutoIndex=0;
            aisinoCmb.OnButtonClick  += cmbcl_OnButtonClick;
            aisinoCmb.OnAutoComplate += cmbcl_OnAutoComplate;
            aisinoCmb.OnSelectValue  += cmbcl_OnSelectValue;
            aisinoCmb.AutoComplate = AutoComplateStyle.HeadWork;
            aisinoCmb.Leave += new EventHandler(this.ComCL_leave);
        }

        private void SetCmbLabel()
        {
            this.cmbGhdw.Edit=0;
            this.cmbGhdwsh.Edit=0;
            this.cmbSfzh.Edit=0;
            this.cmbCllx.Edit=0;
        }

        private void SetCurSlv()
        {
            if (this._fpxx.SLv != "")
            {
                if (FLBM_lock.isFlbm() && (this._fpxx.Bmbbbh != ""))
                {
                    if (this.Lslvbs == "1")
                    {
                        this.cmbSlv.Text = "免税";
                    }
                    else if (this.Lslvbs == "2")
                    {
                        this.cmbSlv.Text = "不征税";
                    }
                    else
                    {
                        double num = double.Parse(this._fpxx.SLv) * 100.0;
                        string str = num.ToString() + "%";
                        this.cmbSlv.Text = str;
                    }
                }
                else if ((this._fpxx.SLv != "") && (double.Parse(this._fpxx.SLv) == 0.0))
                {
                    if (FLBM_lock.isFlbm())
                    {
                        this.cmbSlv.Text = "0%";
                    }
                    else
                    {
                        this.cmbSlv.Text = "免税";
                    }
                }
                else
                {
                    string str2 = ((double.Parse(this._fpxx.SLv) * 100.0)).ToString() + "%";
                    this.cmbSlv.Text = str2;
                }
            }
        }

        private void SetFormTitle(FPLX fplx, string fpdm)
        {
            string str = this.fpm.QueryXzqy(fpdm);
            if ((int)fplx == 12)
            {
                this.Text = "开具机动车销售统一发票";
                if (str.Length == 2)
                {
                    str = str.Substring(0, 1) + "  " + str.Substring(1);
                }
                this.lblTitle.Text = str;
            }
        }

        private void SetFpText()
        {
            this._fpxx.Gfmc=this.cmbGhdw.Text.Trim();
            this._fpxx.Gfsh=this.cmbGhdwsh.Text.Trim();
            this._fpxx.Sfzh_zzjgdm=this.cmbSfzh.Text.Trim();
            this._fpxx.Cllx=this.cmbCllx.Text.Trim();
            this._fpxx.Cpxh=this.txtCpxh.Text.Trim();
            this._fpxx.Cd=this.txtCd.Text.Trim();
            this._fpxx.Hgzh=this.txtHgzh.Text.Trim();
            this._fpxx.Jkzmsh=this.txtJkzmh.Text.Trim();
            this._fpxx.Sjdh=this.txtSjdh.Text.Trim();
            this._fpxx.Fdjhm=this.cmbFdjh.Text.Trim();
            this._fpxx.Clsbdh_cjhm=this.txtClsbh.Text.Trim();
            this._fpxx.Sccjmc=this.txtCjmc.Text;
            this._fpxx.Dh=this.txtDh.Text.Trim();
            this._fpxx.Dz=this.txtDz.Text.Trim();
            this._fpxx.Zh=this.txtZh.Text.Trim();
            this._fpxx.Khyh=this.txtKhyh.Text;
            this._fpxx.Dw=this.txtDw.Text.Trim();
            this._fpxx.Xcrs=this.txtXcrs.Text.Trim();
        }

        private void SetGhfCmb(AisinoMultiCombox aisinoCmb, string showText)
        {
            aisinoCmb.IsSelectAll=true;
            aisinoCmb.buttonStyle=0;
            aisinoCmb.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", 100));
            aisinoCmb.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 100));
            aisinoCmb.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "IDCOC", 100));
            aisinoCmb.ShowText=showText;
            aisinoCmb.DrawHead=false;
            aisinoCmb.AutoIndex=0;
            aisinoCmb.OnButtonClick  += cmbxx_OnButtonClick;
            aisinoCmb.OnAutoComplate += cmbxx_OnAutoComplate;
            aisinoCmb.OnSelectValue  += cmbxx_OnSelectValue;
            aisinoCmb.AutoComplate = AutoComplateStyle.HeadWork;
        }

        private void SetHzxx()
        {
            string hjJeHs = this._fpxx.GetHjJeHs();
            this.txtJsxx.Text = hjJeHs;
            this.lblJsdx.Text = (hjJeHs == "0.00") ? "零" : ToolUtil.RMBToDaXie(decimal.Parse(hjJeHs));
            this.lblJgxx.Text = "￥" + this._fpxx.GetHjJe();
            this.lblSe.Text = "￥" + this._fpxx.GetHjSe();
        }

        private void SetSlvcbm()
        {
            string str = ",";
            string[] separator = new string[] { str };
            string[] source = this._fpxx.GetSqSLv().Split(separator, StringSplitOptions.None);
            bool flag = false;
            if (source.Contains<string>("0.000") || source.Contains<string>("0"))
            {
                flag = true;
            }
            string[] strArray2 = this.getSYslv();
            List<string> list1 = new List<string>();
            list1.AddRange(this.yhzc2slv(this.yhsm));
            list1.AddRange(source.ToList<string>());
            list1.AddRange(strArray2.ToList<string>());
            List<double> list = new List<double>();
            using (List<string>.Enumerator enumerator = list1.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    double result = 0.0;
                    if (double.TryParse(enumerator.Current, out result))
                    {
                        list.Add(result);
                    }
                }
            }
            this.cmbSlv.Items.Clear();
            list.Sort();
            list.Reverse();
            list = list.GroupBy<double, double>((serializeClass.staticFunc_9)).Select<IGrouping<double, double>, double>((serializeClass.staticFunc_10)).ToList<double>();
            List<SLV> list2 = new List<SLV>();
            foreach (double num2 in list)
            {
                if (num2 == 0.0)
                {
                    if (!FLBM_lock.isFlbm())
                    {
                        list2.Add(new SLV((FPLX)12, 0, "0.00", "免税", "免税"));
                    }
                    else
                    {
                        if (this.yhsm.Contains("出口零税") | flag)
                        {
                            list2.Add(new SLV((FPLX)12, 0, "0.00", "0%", "0%"));
                        }
                        if (this.yhsm.Contains("免税"))
                        {
                            list2.Add(new SLV((FPLX)12, 0, "0.00", "免税", "免税"));
                        }
                        if (this.yhsm.Contains("不征税"))
                        {
                            list2.Add(new SLV((FPLX)12, 0, "0.00", "不征税", "不征税"));
                        }
                    }
                }
                else if (num2 != 0.015)
                {
                    string str2 = ((num2 * 100.0)).ToString() + "%";
                    list2.Add(new SLV((FPLX)12, 0, num2.ToString(), str2, str2));
                }
            }
            this.cmbSlv.Items.Clear();
            this.cmbSlv.Items.AddRange(list2.ToArray());
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

        private void SetTXTBorder(BorderStyle style)
        {
            this.txtCpxh.BorderStyle = style;
            this.txtCd.BorderStyle = style;
            this.txtHgzh.BorderStyle = style;
            this.txtJkzmh.BorderStyle = style;
            this.txtSjdh.BorderStyle = style;
            this.txtClsbh.BorderStyle = style;
            this.txtCjmc.BorderStyle = style;
            this.txtDz.BorderStyle = style;
            this.txtKhyh.BorderStyle = style;
            this.txtZh.BorderStyle = style;
            this.txtDh.BorderStyle = style;
            this.txtJsxx.BorderStyle = style;
            this.txtXcrs.BorderStyle = style;
            this.txtDw.BorderStyle = style;
        }

        private void SetTxtEnabled(bool enabled)
        {
            this.cmbSlv.Enabled = enabled;
            this.cmbFdjh.Enabled = enabled;
            this.txtCpxh.Enabled = enabled;
            this.txtCd.Enabled = enabled;
            this.txtHgzh.Enabled = enabled;
            this.txtSjdh.Enabled = enabled;
            this.txtJkzmh.Enabled = enabled;
            this.txtClsbh.Enabled = enabled;
            this.txtCjmc.Enabled = enabled;
            this.txtDz.Enabled = enabled;
            this.txtKhyh.Enabled = enabled;
            this.txtZh.Enabled = enabled;
            this.txtDh.Enabled = enabled;
            this.txtJsxx.Enabled = enabled;
            this.txtXcrs.Enabled = enabled;
            this.txtDw.Enabled = enabled;
            if (!enabled)
            {
                this.SetTXTBorder(BorderStyle.None);
            }
            else
            {
                this.SetTXTBorder(BorderStyle.Fixed3D);
            }
            this.tool_saomiao.Enabled = enabled;
            this.tool_sfz.Enabled = enabled;
        }

        private void SetZeroSlvbz(string str)
        {
            if (str == "0%")
            {
                if (this.yhsm == "出口零税")
                {
                    this.Lslvbs = "0";
                    this.xsyh = "1";
                }
                else
                {
                    this.Lslvbs = "3";
                    if (((this.yhsm == "免税") || (this.yhsm == "不征税")) || !this.yhzc_contain_slv(this.yhsm, str, true))
                    {
                        this.xsyh = "0";
                    }
                    else
                    {
                        this.xsyh = "1";
                    }
                }
            }
            else if (str == "免税")
            {
                this.Lslvbs = "1";
                this.xsyh = "1";
            }
            else if (str == "不征税")
            {
                this.Lslvbs = "2";
                this.xsyh = "1";
            }
            else if (this.yhzc_contain_slv(this.yhsm, str, true))
            {
                this.xsyh = "1";
                this.Lslvbs = "";
            }
            else
            {
                this.xsyh = "0";
                this.Lslvbs = "";
            }
        }

        private void ShowCopyMainInfo()
        {
            this.SetSlvcbm();
            this.GetTaxLabel();
            this.GetQyLabel();
            this.GetFPText();
            this.SetCurSlv();
            this.SetHzxx();
        }

        internal void ShowImprotFp(Djfp djfp)
        {
            if ((djfp != null) && (djfp.Fpxx != null))
            {
                this.ShowCopyMainInfo();
            }
            else
            {
                this.RefreshData(false);
            }
        }

        private void ShowInfo(Fpxx fp, string zfbz)
        {
            DateTime time;
            this.SetFormTitle(fp.fplx, fp.fpdm);
            if (DateTime.TryParse(fp.kprq, out time))
            {
                fp.kprq = time.ToString("yyyy年MM月dd日");
            }
            byte[] destinationArray = new byte[0x20];
            byte[] sourceArray = Invoice.TypeByte;
            Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
            byte[] buffer2 = new byte[0x10];
            Array.Copy(sourceArray, 0x20, buffer2, 0, 0x10);
            byte[] buffer3 = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes("KP" + DateTime.Now.ToString("F")), destinationArray, buffer2);
            Invoice.IsGfSqdFp_Static=false;
            this._fpxx = new Invoice(false, fp, buffer3, null);
            this.GetTaxLabel();
            this.GetQyLabel();
            this.GetFPText();
            DateTime time2 = new DateTime(0x7dd, 9, 10, 8, 0x22, 30);
            TimeSpan span = (TimeSpan) (DateTime.Now - time2);
            byte[] buffer4 = AES_Crypt.Encrypt(ToolUtil.GetBytes(span.TotalSeconds.ToString("F1")), new byte[] { 
                0xff, 0x42, 0xae, 0x95, 11, 0x51, 0xca, 0x15, 0x21, 140, 0x4f, 170, 220, 0x92, 170, 0xed, 
                0xfd, 0xeb, 0x4e, 13, 0xac, 0x80, 0x52, 0xff, 0x45, 0x90, 0x85, 0xca, 0xcb, 0x9f, 0xaf, 0xbd
             }, new byte[] { 0xf2, 0x1f, 0xac, 0x5b, 0x2c, 0xc0, 0xa9, 0xd0, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 });
            fp.Get_Print_Dj(null, 0, buffer4);
            this.lblMW.Text = fp.mw;
            this.cmbSlv.DropDownStyle = ComboBoxStyle.DropDown;
            this.Set_flxx(fp.skr, fp.zyspmc, fp.zyspsm);
            this.SetCurSlv();
            this.SetHzxx();
            bool flag = zfbz.Equals("1");
            this.tool_zuofei.Enabled = !flag;
            this.picZuofei.Visible = flag;
        }

        private void ShowInitInvInfo()
        {
            this.DelTextChangedEvent();
            this.GetTaxLabel();
            this.GetQyLabel();
            this.txtDz.Text = this._fpxx.Dz;
            this.txtDh.Text = this._fpxx.Dh;
            string str = PropertyUtil.GetValue("JDCINV-YHZH", this._fpxx.Zh);
            this.txtZh.Text = str;
            string str2 = PropertyUtil.GetValue("JDCINV-KHYH", this._fpxx.Khyh);
            this.txtKhyh.Text = str2;
            this.SetCurSlv();
            this.lblJsdx.Text = "零";
            this.lblSe.Text = "￥0.00";
            if (this.cmbSlv.Items.Count > 0)
            {
                this.cmbSlv.SelectedIndex = 0;
            }
            this.RegTextChangedEvent();
        }

        private void TipOverLen(Fpxx fp)
        {
            string str = fp.gfmc.Trim();
            this.cmbGhdw.Text = "";
            this.cmbGhdw.Text = str;
            string str2 = fp.dw.Trim();
            this.txtDw.Text = "";
            this.txtDw.Text = str2;
            string str3 = "";
            if (str != this.cmbGhdw.Text)
            {
                str3 = str3 + "购货单位名称,";
            }
            if (str2 != this.txtDw.Text)
            {
                str3 = str3 + "吨位,";
            }
            if (str3 != "")
            {
                char[] trimChars = new char[] { ',' };
                str3 = str3.TrimEnd(trimChars);
                string[] textArray1 = new string[] { "新机动车发票", "旧机动车发票", str3 };
                MessageManager.ShowMsgBox("INP-242176", textArray1);
            }
        }

        private void tool_autoImport_Click(object sender, EventArgs e)
        {
            this.cmbCllx.Leave -= new EventHandler(this.ComCL_leave);
            try
            {
                AutoImport import = new AutoImport((FPLX)12, this._fpxx.GetSqSLv(), this._fpxx.Zyfplx);
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
                            if ((import.CurFpdm == "000000000000") || (import.CurFphm == "00000000"))
                            {
                                string text1 = this.fpm.Code();
                                MessageManager.ShowMsgBox(text1, this.fpm.CodeParams());
                                if (text1 != "000000")
                                {
                                    this.CloseJDCForm();
                                }
                                base.Close();
                            }
                            else
                            {
                                this.GetNextFp();
                                this.ClearMainInfo();
                                this.ShowInitInvInfo();
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
                this.cmbCllx.Leave += new EventHandler(this.ComCL_leave);
            }
        }

        private void tool_autokh_Click(object sender, EventArgs e)
        {
            PropertyUtil.SetValue("INV-AUTOSAVEKH", this.tool_autokh.Checked ? "1" : "0");
        }

        private void tool_close_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        private void tool_fushu_Click(object sender, EventArgs e)
        {
            this.cmbCllx.Leave -= new EventHandler(this.ComCL_leave);
            try
            {
                if (!this.tool_fushu.Checked)
                {
                    this.tool_fushu.Checked = true;
                    HZFPTK_PP hzfptk_pp = new HZFPTK_PP((FPLX)12);
                    if (hzfptk_pp.ShowDialog() == DialogResult.OK)
                    {
                        this.RefreshData(true);
                        Fpxx blueFpxx = hzfptk_pp.blueFpxx;
                        this.isHzwm = false;
                        if (blueFpxx != null)
                        {
                            if ((blueFpxx.bmbbbh == null) || (blueFpxx.bmbbbh == ""))
                            {
                                this._fpxx.Bmbbbh="";
                                this.isHzwm = true;
                            }
                            else
                            {
                                this._fpxx.Bmbbbh=blueFpxx.bmbbbh;
                            }
                            this.Set_flxx(blueFpxx.skr, blueFpxx.zyspmc, blueFpxx.zyspsm);
                            this.SetSlvcbm();
                            if (!this._fpxx.SetFpSLv(blueFpxx.sLv))
                            {
                                MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
                                this.RefreshData(false);
                                return;
                            }
                            this.SetCmbLabel();
                            this.SetTxtEnabled(false);
                            this.blueJe = blueFpxx.je;
                            bool flag = this._fpxx.Jdc_ver_new;
                            Invoice.IsGfSqdFp_Static=false;
                            byte[] destinationArray = new byte[0x20];
                            byte[] sourceArray = Invoice.TypeByte;
                            Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
                            byte[] buffer2 = new byte[0x10];
                            Array.Copy(sourceArray, 0x20, buffer2, 0, 0x10);
                            byte[] buffer3 = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes("KP" + DateTime.Now.ToString("F")), destinationArray, buffer2);
                            Invoice invoice = new Invoice(false, blueFpxx, buffer3, null);
                            Invoice redInvoice = invoice.GetRedInvoice(false);
                            if ((redInvoice == null) || (invoice.GetCode() != "0000"))
                            {
                                MessageManager.ShowMsgBox(invoice.GetCode(), invoice.Params);
                            }
                            this._fpxx = redInvoice;
                            this._fpxx.Fpdm=this.lblFpdm.Text;
                            this._fpxx.Fphm=this.lblFphm.Text;
                            this._fpxx.Kprq=this.lblKprq.Text;
                            this._fpxx.Jdc_ver_new=flag;
                            this._fpxx.Gfmc=invoice.Gfmc;
                            this.ResetQyxx();
                            this._fpxx.Kpr=blueFpxx.kpr;
                            this._fpxx.SetFpSLv(blueFpxx.sLv);
                        }
                        else
                        {
                            this.blueJe = string.Empty;
                            this._fpxx.SetFpSLv(this.jdcManager.GetSLValue(this.cmbSlv.Text));
                        }
                        this._fpxx.Bz=NotesUtil.GetRedInvNotes(hzfptk_pp.blueFpdm, hzfptk_pp.blueFphm);
                        this._fpxx.BlueFpdm=hzfptk_pp.blueFpdm;
                        this._fpxx.BlueFphm=hzfptk_pp.blueFphm;
                        this.ShowCopyMainInfo();
                    }
                    else
                    {
                        this.RefreshData(false);
                        this.SetTxtEnabled(true);
                    }
                }
                else
                {
                    this.isHzwm = false;
                    this.RefreshData(false);
                    this.SetTxtEnabled(true);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                this.cmbCllx.Leave += new EventHandler(this.ComCL_leave);
            }
        }

        private void tool_fuzhi_Click(object sender, EventArgs e)
        {
            this.cmbCllx.Leave -= new EventHandler(this.ComCL_leave);
            try
            {
                int num;
                object[] objArray1 = new object[] { Invoice.FPLX2Str(this._fpxx.Fplx), (ZYFP_LX) 0 };
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FaPiaoChaXun_FPFZ", objArray1);
                if ((((objArray != null) && (objArray.Length == 3)) && ((objArray[0] != null) && (objArray[0].ToString() != ""))) && ((objArray[2] != null) && int.TryParse(objArray[2].ToString(), out num)))
                {
                    Fpxx fp = this.fpm.GetXxfp(this._fpxx.Fplx, objArray[1].ToString(), num);
                    if (fp != null)
                    {
                        if ((fp.bmbbbh == "") || (fp.bmbbbh == null))
                        {
                            this._fpxx.DelSpxxAll();
                            this._fpxx.CopyFpxx(fp);
                            this._fpxx.Bmbbbh=this.getbmbbbh();
                            if (this.isWM())
                            {
                                this.TipOverLen(fp);
                                this.ResetQyxx();
                                this.ShowCopyMainInfo();
                            }
                            else if (this._fpxx.GetCode() != "0000")
                            {
                                MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
                                this.RefreshData(false);
                            }
                            else
                            {
                                this.TipOverLen(fp);
                                this.ResetQyxx();
                                this.ShowCopyMainInfo();
                                string str = this.cmbSlv.SelectedItem.ToString();
                                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                dictionary.Add("MC", this.cmbCllx.Text);
                                dictionary.Add("CPXH", this.txtCpxh.Text);
                                dictionary.Add("CD", this.txtCd.Text);
                                dictionary.Add("SCCJMC", this.txtCjmc.Text);
                                ArrayList list = BaseDAOFactory.GetBaseDAOSQLite().querySQL("aisino.fwkp.fptk.GeClxx", dictionary);
                                if ((list != null) && (list.Count > 0))
                                {
                                    List<object> list2 = new List<object>();
                                    foreach (Dictionary<string, object> dictionary2 in list)
                                    {
                                        list2.Add(dictionary2["MC"].ToString());
                                        list2.Add(dictionary2["CPXH"].ToString());
                                        list2.Add(dictionary2["CD"].ToString());
                                        list2.Add(dictionary2["SCCJMC"].ToString());
                                        list2.Add(dictionary2["BM"].ToString());
                                        list2.Add(dictionary2["SPFL"].ToString());
                                        list2.Add(dictionary2["YHZC"].ToString());
                                        list2.Add(dictionary2["WJ"].ToString());
                                        list2.Add(dictionary2["KJM"].ToString());
                                        list2.Add(dictionary2["YHZCMC"].ToString());
                                    }
                                    object[] bmxx = list2.ToArray();
                                    this.ClxxSetValue(bmxx);
                                }
                                else
                                {
                                    string[] textArray1 = new string[] { this.cmbCllx.Text, " #% ", this.txtCpxh.Text, " #% ", this.txtCd.Text, " #% ", this.txtCjmc.Text };
                                    string str2 = string.Concat(textArray1);
                                    object[] objArray4 = new object[] { str2 };
                                    object[] objArray3 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddCL", objArray4);
                                    if (objArray3 == null)
                                    {
                                        this.cmbCllx.Text = "";
                                        this._fpxx.Cllx="";
                                        this.RefreshData(this._fpxx.IsRed);
                                    }
                                    else
                                    {
                                        this.ClxxSetValue(objArray3);
                                        this.cmbSlv.Text = str;
                                    }
                                }
                            }
                        }
                        else
                        {
                            this._fpxx.DelSpxxAll();
                            this._fpxx.CopyFpxx(fp);
                            this._fpxx.Bmbbbh=this.getbmbbbh();
                            this.Set_flxx(this._fpxx.Skr, this._fpxx.Zyspmc, this._fpxx.Zyspsm);
                            this.SetSlvcbm();
                            if (this._fpxx.GetCode() != "0000")
                            {
                                MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
                                this.RefreshData(false);
                            }
                            else
                            {
                                this.TipOverLen(fp);
                                this.ResetQyxx();
                                this.ShowCopyMainInfo();
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
                this.cmbCllx.Leave += new EventHandler(this.ComCL_leave);
            }
        }

        private void tool_guochan_Click(object sender, EventArgs e)
        {
            string str = PropertyUtil.GetValue("COM_SET");
            if (((str != null) && (str.Length > 0)) && this.check_port(str))
            {
                VehCert cert1 = (VehCert) Activator.CreateInstance(System.Type.GetTypeFromCLSID(new Guid("77910FEF-6F36-48DA-B5EC-787B0D07BB56")));
                cert1.Veh_Connect = str;
                cert1.ViewBarcodeInfo("PER4-54LD-WXQI-DK87");
                string str2 = cert1.Veh_Tmxx;
                this.set_saomiao_info(str2, 1);
            }
        }

        private void tool_imputSet_Click(object sender, EventArgs e)
        {
            new ImportSet((FPLX)12).ShowDialog();
        }

        private void tool_jinkou_Click(object sender, EventArgs e)
        {
            string str = PropertyUtil.GetValue("COM_SET");
            if (((str != null) && (str.Length > 0)) && this.check_port(str))
            {
                VCertificateI ei1 = (VCertificateI) Activator.CreateInstance(System.Type.GetTypeFromCLSID(new Guid("C637D003-9E2D-4E63-8A5D-C19B99297181")));
                ei1.Connect = str;
                ei1.ViewBarcodeInfoByWindow();
                string barcodeInfo = ei1.BarcodeInfo;
                this.set_saomiao_info(barcodeInfo, 2);
            }
        }

        private void tool_kehu_Click(object sender, EventArgs e)
        {
            this.SaveKH(false);
        }

        private void tool_manualImport_Click(object sender, EventArgs e)
        {
            this.cmbCllx.Leave -= new EventHandler(this.ComCL_leave);
            try
            {
                OpenFileDialog dialog = new OpenFileDialog {
                    Filter = "单据(*.xml)|*.xml"
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string str;
                    List<Djfp> collection = new FPDJHelper().ParseJDCDjFileManual(dialog.FileName, out str, this._fpxx.GetSqSLv());
                    if (!string.IsNullOrEmpty(str))
                    {
                        string[] textArray1 = new string[] { str };
                        MessageManager.ShowMsgBox("INP-241007", textArray1);
                    }
                    else if (collection.Count > 0)
                    {
                        ManualImport import = new ManualImport((FPLX)12, 0);
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
                                    this.Set_flxx(this._fpxx.Skr, this._fpxx.Zyspmc, this._fpxx.Zyspsm);
                                    this.SetTxtEnabled(true);
                                    this.ShowCopyMainInfo();
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
                this.cmbCllx.Leave += new EventHandler(this.ComCL_leave);
            }
        }

        private void tool_piaozhong_Click(object sender, EventArgs e)
        {
            if (new JDCVersionSet("", "", JDCVersion.New).ShowDialog() == DialogResult.OK)
            {
                base.DialogResult = DialogResult.Ignore;
                base.Close();
            }
        }

        private void tool_print_Click(object sender, EventArgs e)
        {
            if (this.onlyShow)
            {
                this.PrintFP(this.ykfp);
            }
            else
            {
                if (!this._fpxx.IsRed)
                {
                    IFpManager manager = new FpManager();
                    if ((this._fpxx.Clsbdh_cjhm.Length != 0) && !manager.CheckCJH(this._fpxx.Clsbdh_cjhm))
                    {
                        string[] textArray1 = new string[] { this._fpxx.Clsbdh_cjhm };
                        if (MessageManager.ShowMsgBox("INP-242204", textArray1) != DialogResult.Yes)
                        {
                            return;
                        }
                    }
                }
                if ((this.SaveFpData() && (((this._fpxx.Gfsh != null) && (this._fpxx.Gfsh.Trim() != "")) || (MessageManager.ShowMsgBox("INP-242166") == DialogResult.Yes))) && ((!this._fpxx.IsRed || (this.blueJe == "")) || this.jdcManager.CheckRedJe(this.blueJe, this._fpxx)))
                {
                    this.yhsm = (this.xsyh == "1") ? this.yhsm : "";
                    this.xsyh = (this.yhsm == "") ? "0" : "1";
                    this._fpxx.Zyspmc=(this.flbm == null) ? "" : this.flbm;
                    this._fpxx.Zyspsm=this.spbh + "#%" + this.yhsm;
                    this._fpxx.Skr=this.xsyh + "#%" + this.Lslvbs;
                    if (this.isWM())
                    {
                        this._fpxx.Bmbbbh="";
                        this._fpxx.Zyspmc="";
                        this._fpxx.Zyspsm="";
                        this._fpxx.Skr="";
                    }
                    if (this._fpxx.Zyspsm.EndsWith("\r\n"))
                    {
                        this._fpxx.Zyspsm=this._fpxx.Zyspsm.Remove(this._fpxx.Zyspsm.Length - 1);
                    }
                    Fpxx fpData = this._fpxx.GetFpData();
                    if (fpData == null)
                    {
                        MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
                    }
                    else
                    {
                        PropertyUtil.SetValue("JDCINVNEW-SLV", this._fpxx.SLv);
                        PropertyUtil.SetValue("JDCINV-KHYH", this._fpxx.Khyh);
                        PropertyUtil.SetValue("JDCINV-YHZH", this._fpxx.Zh);
                        if (fpData.sLv == "免税")
                        {
                            fpData.sLv = "0.00";
                        }
                        if (this.MakeCardFp(fpData))
                        {
                            if (this.fpm.SaveXxfp(fpData))
                            {
                                if (this.djfile != "")
                                {
                                    new FPDJHelper().InsertYkdj(this.djfile, fpData.xsdjbh);
                                }
                                this.PrintFP(fpData);
                                this.flbm = "";
                                this.spbh = "";
                                this.yhsm = "";
                                this.xsyh = "";
                                this.Lslvbs = "";
                                if (this.tool_autokh.Checked)
                                {
                                    this.SaveKH(true);
                                }
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
        }

        private void tool_print_MouseDown(object sender, MouseEventArgs e)
        {
            this.lblTitle.Focus();
        }

        private void tool_saomiao_click(object sender, EventArgs e)
        {
            ContextMenuStrip strip = new ContextMenuStrip();
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.tool_guochan, this.tool_jinkou, this.tool_set };
            strip.Items.AddRange(toolStripItems);
            strip.Show(this, new Point(this.tool_saomiao.Bounds.X, this.tool_saomiao.Bounds.Bottom));
        }

        private void tool_set_Click(object sender, EventArgs e)
        {
            new QiangSet().ShowDialog();
        }

        private void tool_sfz_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.InitSFZinfo())
                {
                    if (CVR_Authenticate() == 1)
                    {
                        if (CVR_Read_Content(4) == 1)
                        {
                            byte[] buffer = new byte[30];
                            int strLen = 0x24;
                            GetPeopleIDCode(ref buffer[0], ref strLen);
                            strLen = 30;
                            byte[] buffer1 = new byte[30];
                            GetPeopleName(ref buffer1[0], ref strLen);
                            string str = ToolUtil.GetString(buffer).Replace("\0", "").Trim();
                            string str2 = ToolUtil.GetString(buffer1).Replace("\0", "").Trim();
                            this.cmbSfzh.Text = str;
                            this.cmbGhdw.Text = str2;
                        }
                        else
                        {
                            MessageManager.ShowMsgBox("INP-242192");
                        }
                    }
                    else
                    {
                        MessageManager.ShowMsgBox("INP-242193");
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageManager.ShowMsgBox(exception1.ToString());
            }
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

        private void txtCd_TextChanged(object sender, EventArgs e)
        {
            string text = this.txtCd.Text;
            this._fpxx.Cd=text;
            if (this._fpxx.Cd != text)
            {
                this.txtCd.Text = this._fpxx.Cd;
                this.txtCd.SelectionStart = this.txtCd.Text.Length;
            }
        }

        private void txtCjmc_TextChanged(object sender, EventArgs e)
        {
            string text = this.txtCjmc.Text;
            this._fpxx.Sccjmc=text;
            if (this._fpxx.Sccjmc != text)
            {
                this.txtCjmc.Text = this._fpxx.Sccjmc;
                this.txtCjmc.SelectionStart = this.txtCjmc.Text.Length;
            }
        }

        private void txtClsbh_TextChanged(object sender, EventArgs e)
        {
            string text = this.txtClsbh.Text;
            this._fpxx.Clsbdh_cjhm=text;
            if (this._fpxx.Clsbdh_cjhm != text)
            {
                this.txtClsbh.Text = this._fpxx.Clsbdh_cjhm;
                this.txtClsbh.SelectionStart = this.txtClsbh.Text.Length;
            }
        }

        private void txtCpxh_TextChanged(object sender, EventArgs e)
        {
            string text = this.txtCpxh.Text;
            this._fpxx.Cpxh=text;
            if (this._fpxx.Cpxh != text)
            {
                this.txtCpxh.Text = this._fpxx.Cpxh;
                this.txtCpxh.SelectionStart = this.txtCpxh.Text.Length;
            }
        }

        private void txtDh_TextChanged(object sender, EventArgs e)
        {
            string text = this.txtDh.Text;
            this._fpxx.Dh=text;
            if (this._fpxx.Dh != text)
            {
                this.txtDh.Text = this._fpxx.Dh;
                this.txtDh.SelectionStart = this.txtDh.Text.Length;
            }
        }

        private void txtDw_TextChanged(object sender, EventArgs e)
        {
            string text = this.txtDw.Text;
            this._fpxx.Dw=text;
            if (this._fpxx.Dw != text)
            {
                this.txtDw.Text = this._fpxx.Dw;
                this.txtDw.SelectionStart = this.txtDw.Text.Length;
            }
        }

        private void txtDz_TextChanged(object sender, EventArgs e)
        {
            string text = this.txtDz.Text;
            this._fpxx.Dz=text;
            if (this._fpxx.Dz != text)
            {
                this.txtDz.Text = this._fpxx.Dz;
                this.txtDz.SelectionStart = this.txtDz.Text.Length;
            }
        }

        private void txtHgzh_TextChanged(object sender, EventArgs e)
        {
            string text = this.txtHgzh.Text;
            this._fpxx.Hgzh=text;
            if (this._fpxx.Hgzh != text)
            {
                this.txtHgzh.Text = this._fpxx.Hgzh;
                this.txtHgzh.SelectionStart = this.txtHgzh.Text.Length;
            }
        }

        private void txtJkzmh_TextChanged(object sender, EventArgs e)
        {
            string text = this.txtJkzmh.Text;
            this._fpxx.Jkzmsh=text;
            if (this._fpxx.Jkzmsh != text)
            {
                this.txtJkzmh.Text = this._fpxx.Jkzmsh;
                this.txtJkzmh.SelectionStart = this.txtJkzmh.Text.Length;
            }
        }

        private void txtJsxx_LostFocus(object sender, EventArgs e)
        {
            if (this._fpxx.SetJshj(this.txtJsxx.Text))
            {
                this.SetHzxx();
            }
            else
            {
                this.txtJsxx.LostFocus -= new EventHandler(this.txtJsxx_LostFocus);
                MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
                this.txtJsxx.Focus();
                this.txtJsxx.LostFocus += new EventHandler(this.txtJsxx_LostFocus);
            }
        }

        private void txtKhyh_TextChanged(object sender, EventArgs e)
        {
            string text = this.txtKhyh.Text;
            this._fpxx.Khyh=text;
            if (this._fpxx.Khyh != text)
            {
                this.txtKhyh.Text = this._fpxx.Khyh;
                this.txtKhyh.SelectionStart = this.txtKhyh.Text.Length;
            }
        }

        private void txtSjdh_TextChanged(object sender, EventArgs e)
        {
            string text = this.txtSjdh.Text;
            this._fpxx.Sjdh=text;
            if (this._fpxx.Sjdh != text)
            {
                this.txtSjdh.Text = this._fpxx.Sjdh;
                this.txtSjdh.SelectionStart = this.txtSjdh.Text.Length;
            }
        }

        private void txtXcrs_TextChanged(object sender, EventArgs e)
        {
            string text = this.txtXcrs.Text;
            this._fpxx.Xcrs=text;
            if (this._fpxx.Xcrs != text)
            {
                this.txtXcrs.Text = this._fpxx.Xcrs;
                this.txtXcrs.SelectionStart = this.txtXcrs.Text.Length;
            }
        }

        private void txtZh_TextChanged(object sender, EventArgs e)
        {
            string text = this.txtZh.Text;
            this._fpxx.Zh=text;
            if (this._fpxx.Zh != text)
            {
                this.txtZh.Text = this._fpxx.Zh;
                this.txtZh.SelectionStart = this.txtZh.Text.Length;
            }
        }

        public bool Update_CL(object[] bmxx)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
                dictionary.Add("BM", bmxx[5].ToString());
                bmxx[6] = "否";
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
                                bmxx[6] = "是";
                                bmxx[9] = strArray[i];
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            bmxx[6] = "否";
                            bmxx[9] = "";
                        }
                    }
                }
                dictionary.Clear();
                dictionary.Add("YHZC", bmxx[6].ToString());
                dictionary.Add("YHZCMC", bmxx[9].ToString());
                dictionary.Add("BM", bmxx[4].ToString());
                dictionary.Add("MC", bmxx[0].ToString());
                baseDAOSQLite.未确认DAO方法2_疑似updateSQL("aisino.fwkp.fptk.UpdataBM_CL", dictionary);
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
                string[] separator = new string[] { "、", ",", "，" };
                string[] source = dictionary2["SLV"].ToString().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < source.Length; i++)
                {
                    if (source[i] == "1.5%_5%")
                    {
                        source[i] = "1.5%";
                    }
                }
                if ((dictionary2["YHZCMC"].ToString() == yhzc) && (dictionary2["SLV"].ToString() == ""))
                {
                    return true;
                }
                if ((dictionary2["YHZCMC"].ToString() == yhzc) && source.Contains<string>(slv))
                {
                    return true;
                }
            }
            return false;
        }

        public List<string> yhzc2slv(string yhzc)
        {
            List<string> list = new List<string>();
            if (yhzc != "")
            {
                string str = "aisino.fwkp.fptk.SelectYhzcs";
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                foreach (Dictionary<string, object> dictionary2 in BaseDAOFactory.GetBaseDAOSQLite().querySQL(str, dictionary))
                {
                    if (dictionary2["YHZCMC"].ToString() == yhzc)
                    {
                        string[] separator = new string[] { "、", ",", "，" };
                        string[] strArray = dictionary2["SLV"].ToString().Split(separator, StringSplitOptions.RemoveEmptyEntries);
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

        public bool InitSuccess
        {
            get
            {
                return this.initSuccess;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class serializeClass
        {
            public static readonly JDCInvoiceForm_new.serializeClass instance = new JDCInvoiceForm_new.serializeClass();
            public static Func<double, double> staticFunc_9;
            public static Func<IGrouping<double, double>, double> staticFunc_10;

            internal double slvlistFunc_9(double p)
            {
                return p;
            }

            internal double slvlistFunc_10(IGrouping<double, double> p)
            {
                return p.Key;
            }
        }
    }
}

