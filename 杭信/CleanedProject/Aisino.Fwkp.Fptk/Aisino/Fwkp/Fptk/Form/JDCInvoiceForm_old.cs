namespace Aisino.Fwkp.Fptk.Form
{
    using Aisino.Framework.MainForm;
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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;
    using VCertificate;
    using VehCertI;

    public class JDCInvoiceForm_old : DockForm
    {
        private Invoice _fpxx;
        private string blueJe;
        private AisinoMultiCombox cmbCllx;
        private AisinoCMB cmbFdjh;
        private AisinoMultiCombox cmbGhdw;
        private AisinoMultiCombox cmbSfzh;
        private AisinoCMB cmbSlv;
        private IContainer components;
        internal List<string[]> data;
        private IFpManager fpm;
        internal int index;
        private bool initSuccess;
        private InvoiceHelper jdcManager;
        private AisinoLBL lblBT1;
        private AisinoLBL lblBT2;
        private AisinoLBL lblBT3;
        private AisinoLBL lblBT4;
        private AisinoLBL lblBT5;
        private AisinoLBL lblBT6;
        private AisinoLBL lblBT7;
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
        private bool onlyShow;
        private AisinoPNL panel1;
        private AisinoPNL panel2;
        private AisinoPIC picZuofei;
        private ToolTip tip;
        private ToolStripMenuItem tool_autokh;
        private ToolStripButton tool_close;
        private ToolStripButton tool_fushu;
        private ToolStripButton tool_fuzhi;
        private ToolStripMenuItem tool_guochan;
        private ToolStripMenuItem tool_jinkou;
        private ToolStripDropDownButton tool_kehu;
        private ToolStripMenuItem tool_manukh;
        private ToolStripButton tool_piaozhong;
        private ToolStripButton tool_print;
        private ToolStripDropDownButton tool_saomiao;
        private ToolStripMenuItem tool_set;
        private ToolStripButton tool_sfz;
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
        private Fpxx ykfp;

        public JDCInvoiceForm_old(FPLX fplx, string fpdm, string fphm)
        {
            this.log = LogUtil.GetLogger<JDCInvoiceForm_new>();
            this.blueJe = string.Empty;
            this.initSuccess = true;
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

        internal JDCInvoiceForm_old(bool flag, int index, List<string[]> data)
        {
            this.log = LogUtil.GetLogger<JDCInvoiceForm_new>();
            this.blueJe = string.Empty;
            this.initSuccess = true;
            this.tip = new ToolTip();
            this.onlyShow = true;
            this.InitializeDefault();
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
            this.txtDz.Text = "";
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
            object[] objArray1 = new object[] { str, 20, "MC,CPXH,CD,SCCJMC" };
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
                this.cmbCllx.Text = bmxx[0].ToString();
                this.txtCpxh.Text = bmxx[1].ToString();
                this.txtCd.Text = bmxx[2].ToString();
                this.txtCjmc.Text = bmxx[3].ToString();
                this.RegTextChangedEvent();
            }
        }

        private void cmbcl_OnAutoComplate(object sender, EventArgs e)
        {
            string text = "";
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
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
                object[] objArray1 = new object[] { combox.Text, 0, "MC,CPXH,CD,SCCJMC" };
                object[] bmxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetCL", objArray1);
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
                object[] bmxx = new object[] { dictionary["MC"], dictionary["CPXH"], dictionary["CD"], dictionary["SCCJMC"] };
                this.ClxxSetValue(bmxx);
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
        private void GetFPText()
        {
            this.DelTextChangedEvent();
            this.cmbGhdw.Text = this._fpxx.Gfmc;
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

        private SLV GetSLv(string value, int valueType)
        {
            return this.jdcManager.GetSLv(value, valueType, this._fpxx.Fplx, this._fpxx.GetSqSLv());
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
                gfxx[1].ToString();
                string str2 = gfxx[2].ToString();
                this.cmbGhdw.Text = str;
                this.cmbSfzh.Text = str2;
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
            this.tool_saomiao = this.xmlComponentLoader1.GetControlByName<ToolStripDropDownButton>("tool_saomiao");
            this.tool_guochan = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_guochan");
            this.tool_jinkou = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_jinkou");
            this.tool_set = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_set");
            this.tool_sfz = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_sfz");
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
            this.lblBT7 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBT7");
            this.toolStrip3 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip3");
            ControlStyleUtil.SetToolStripStyle(this.toolStrip3);
            this.panel1 = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel1");
            this.panel2 = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel2");
            this.picZuofei = this.xmlComponentLoader1.GetControlByName<AisinoPIC>("picZuofei");
            this.picZuofei.Visible = false;
            this.picZuofei.BackColor = System.Drawing.Color.Transparent;
            this.picZuofei.SizeMode = PictureBoxSizeMode.Zoom;
            this.panel1.BackgroundImage = Resources.JDCO;
            this.panel1.BackgroundImageLayout = ImageLayout.Zoom;
            this.panel2.AutoScroll = true;
            this.panel2.AutoScrollMinSize = new Size(0x3c4, 710);
            this.tool_close.Margin = new Padding(20, 1, 0, 2);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(JDCInvoiceForm_old));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = System.Drawing.Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x3c4, 0x2fa);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath=@"..\Config\Components\Aisino.Fwkp.Fpkj.Form.JDCFPtiankai_old_new\Aisino.Fwkp.Fpkj.Form.JDCFPtiankai_old_new.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x3c4, 0x2fa);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            this.MinimumSize = new Size(840, 540);
            base.Name = "JDCInvoiceForm_old";
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
            if (!this.onlyShow)
            {
                this.SetAutoKHChecked();
                this.SetGhfCmb(this.cmbGhdw, "MC");
                this.SetGhfCmb(this.cmbSfzh, "SFZH");
                this.SetCllxCmb(this.cmbCllx, "LXMC");
                this.RegTextChangedEvent();
                this.cmbSlv.SelectedIndexChanged += new EventHandler(this.cmbSlv_SelectedIndexChanged);
                this.txtJsxx.LostFocus += new EventHandler(this.txtJsxx_LostFocus);
                base.FormClosing += new FormClosingEventHandler(this.JDCInvoiceForm_FormClosing);
            }
            base.KeyPreview = true;
            base.KeyDown += new KeyEventHandler(this.JDCInvoiceForm_KeyDown);
            base.Resize += new EventHandler(this.JDCInvoiceForm_old_Resize);
            this.tool_sfz.ToolTipText = "读取身份证信息";
            if (!RegisterManager.CheckRegFile("ERJS"))
            {
                this.tool_saomiao.Visible = false;
                this.tool_sfz.Visible = false;
            }
        }

        private void InitNextFp()
        {
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
            this.CreateJDCInvoice(false, fplx);
            this._fpxx.Jdc_ver_new=false;
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
            this.ResetCmbSlv();
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
                    if (((int)fpzl == 12) && (str.Substring(4, 1) == "1"))
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

        private void JDCInvoiceForm_old_Resize(object sender, EventArgs e)
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
            this.cmbSfzh.Edit=EditStyle.TextBox;
            this.cmbCllx.Edit=EditStyle.TextBox;
        }

        private void RegTextChangedEvent()
        {
            this.cmbGhdw.OnTextChanged = (EventHandler) Delegate.Combine(this.cmbGhdw.OnTextChanged, new EventHandler(this.cmbGhdw_TextChanged));
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
                string str = PropertyUtil.GetValue("JDCINVOLD-SLV", "");
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
            this._fpxx.Gfsh="";
            this._fpxx.Xfmc=this.lblXhdwmc.Text;
            this._fpxx.Xfsh=this.lblNsrsbh.Text;
            this._fpxx.Zgswjg_mc=this.lblSwjg.Text;
            this._fpxx.Zgswjg_dm=this.lblSwjgdm.Text;
            bool flag = this._fpxx.SetFpSLv(this.jdcManager.GetSLValue(this.cmbSlv.Text));
            if (flag)
            {
                flag = this._fpxx.SetJshj(this.txtJsxx.Text);
            }
            if (!flag)
            {
                MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
            }
            return flag;
        }

        private void SaveKH(bool autoSave)
        {
            object[] objArray = new object[] { this.cmbGhdw.Text, "", this.cmbSfzh.Text };
            if (autoSave)
            {
                ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddGHDWAuto", objArray);
            }
            else
            {
                ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddGHDW", objArray);
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
                foreach (string str2 in value.Split(separator, StringSplitOptions.None))
                {
                    list.Add(str2);
                }
                if (list.Count > 0)
                {
                    this.DelTextChangedEvent();
                    string str3 = "";
                    if (type == 1)
                    {
                        this.txtCjmc.Text = list[3];
                        this.txtHgzh.Text = list[1];
                        this.cmbCllx.Text = list[5];
                        if (flag)
                        {
                            str3 = str3 + list[6];
                        }
                        if (flag2)
                        {
                            str3 = str3 + list[5];
                        }
                        if (flag3)
                        {
                            str3 = str3 + list[7];
                        }
                        this.txtCpxh.Text = str3;
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
                            str3 = str3 + list[1];
                        }
                        if (flag2)
                        {
                            str3 = str3 + list[2];
                        }
                        if (flag3)
                        {
                            str3 = str3 + list[3];
                        }
                        this.txtCpxh.Text = str3;
                        this.txtClsbh.Text = list[4];
                        this.cmbFdjh.Text = list[5];
                        this.txtDw.Text = "";
                        this.txtXcrs.Text = "";
                        this.txtJkzmh.Text = list[8];
                    }
                    this.RegTextChangedEvent();
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
            this.lblBT7.Visible = visible;
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
        }

        private void SetCmbLabel()
        {
            this.cmbGhdw.Edit=0;
            this.cmbSfzh.Edit=0;
            this.cmbCllx.Edit=0;
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
            aisinoCmb.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", 200));
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

        private void ShowCopyMainInfo()
        {
            this.GetQyLabel();
            this.GetFPText();
            if (this.cmbSlv.Items.Count > 0)
            {
                this.cmbSlv.SelectedItem = this.GetSLv(this._fpxx.SLv, 0);
            }
            this.SetHzxx();
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
            this.cmbSlv.Text = this.GetSLv(this._fpxx.SLv, 0).ToString();
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
            if (this.cmbSlv.Items.Count > 0)
            {
                this.cmbSlv.SelectedItem = this.GetSLv(this._fpxx.SLv, 0);
            }
            this.lblJsdx.Text = "零";
            this.lblSe.Text = "￥0.00";
            this.RegTextChangedEvent();
        }

        private void TipOverLen(Fpxx fp)
        {
            string str = fp.sfzhm.Trim();
            this.cmbSfzh.Text = "";
            this.cmbSfzh.Text = str;
            string str2 = fp.cpxh.Trim();
            this.txtCpxh.Text = "";
            this.txtCpxh.Text = str2;
            string str3 = "";
            if (str != this.cmbSfzh.Text)
            {
                str3 = str3 + "身份证号码/组织机构代码,";
            }
            if (str2 != this.txtCpxh.Text)
            {
                str3 = str3 + "厂牌型号,";
            }
            if (str3 != "")
            {
                char[] trimChars = new char[] { ',' };
                str3 = str3.TrimEnd(trimChars);
                string[] textArray1 = new string[] { "旧机动车发票", "新机动车发票", str3 };
                MessageManager.ShowMsgBox("INP-242176", textArray1);
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
            if (!this.tool_fushu.Checked)
            {
                this.tool_fushu.Checked = true;
                HZFPTK_PP hzfptk_pp = new HZFPTK_PP((FPLX)12);
                if (hzfptk_pp.ShowDialog() == DialogResult.OK)
                {
                    this.RefreshData(true);
                    Fpxx blueFpxx = hzfptk_pp.blueFpxx;
                    if (blueFpxx != null)
                    {
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
                        Invoice invoice = new Invoice(this._fpxx.Hsjbz, blueFpxx, buffer3, null);
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
                this.RefreshData(false);
                this.SetTxtEnabled(true);
            }
        }

        private void tool_fuzhi_Click(object sender, EventArgs e)
        {
            int num;
            object[] objArray1 = new object[] { Invoice.FPLX2Str(this._fpxx.Fplx), (ZYFP_LX) 0 };
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FaPiaoChaXun_FPFZ", objArray1);
            if ((((objArray != null) && (objArray.Length == 3)) && ((objArray[0] != null) && (objArray[0].ToString() != ""))) && ((objArray[2] != null) && int.TryParse(objArray[2].ToString(), out num)))
            {
                Fpxx fp = this.fpm.GetXxfp(this._fpxx.Fplx, objArray[1].ToString(), num);
                if (fp != null)
                {
                    if ((this.cmbSlv.Items.Count > 0) && !this.cmbSlv.Items.Contains(this.GetSLv(fp.sLv, 0)))
                    {
                        string[] textArray1 = new string[] { "机动车销售统一发票" };
                        MessageManager.ShowMsgBox("INP-242177", textArray1);
                    }
                    else
                    {
                        this._fpxx.DelSpxxAll();
                        this._fpxx.CopyFpxx(fp);
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

        private void tool_piaozhong_Click(object sender, EventArgs e)
        {
            if (new JDCVersionSet("", "", JDCVersion.Old).ShowDialog() == DialogResult.OK)
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
            else if (this.SaveFpData() && ((!this._fpxx.IsRed || (this.blueJe == "")) || this.jdcManager.CheckRedJe(this.blueJe, this._fpxx)))
            {
                Fpxx fpData = this._fpxx.GetFpData();
                if (fpData == null)
                {
                    MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
                }
                else
                {
                    PropertyUtil.SetValue("JDCINVOLD-SLV", this._fpxx.SLv);
                    PropertyUtil.SetValue("JDCINV-KHYH", this._fpxx.Khyh);
                    PropertyUtil.SetValue("JDCINV-YHZH", this._fpxx.Zh);
                    if (this.MakeCardFp(fpData))
                    {
                        if (this.fpm.SaveXxfp(fpData))
                        {
                            this.PrintFP(fpData);
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

        private void tool_print_MouseDown(object sender, MouseEventArgs e)
        {
            this.lblTitle.Focus();
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

        public bool InitSuccess
        {
            get
            {
                return this.initSuccess;
            }
        }
    }
}

