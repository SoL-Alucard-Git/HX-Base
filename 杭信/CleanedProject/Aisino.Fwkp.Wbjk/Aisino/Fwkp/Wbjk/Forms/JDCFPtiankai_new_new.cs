namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Framework.Startup.Login;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fptk.Common.Forms;
    using Aisino.Fwkp.Print;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.DAL;
    using Aisino.Fwkp.Wbjk.Model;
    using Aisino.Fwkp.Wbjk.Properties;
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
    using BusinessObject;
    public class JDCFPtiankai_new_new : BaseForm
    {
        private Invoice _fpxx;
        private AisinoLBL aisinoLBL1;
        private AisinoLBL aisinoLBL2;
        private AisinoLBL aisinoLBL3;
        private AisinoLBL aisinoLBL4;
        private AisinoLBL aisinoLBL5;
        private SaleBill bill = null;
        private string blueJe = string.Empty;
        private string CL_CLBM = "";
        private string CL_CLLXName = "";
        private string CL_CPXXName = "";
        private string CL_FLBM = "";
        private string CL_LSLVBS = "";
        private double CL_SLV = -1.0;
        private string CL_SPFL_ZZSTSGL = "";
        private bool CL_XSYH = false;
        private string CL_YHZC_SLV = "";
        private AisinoMultiCombox cmbCllx;
        private AisinoCMB cmbFdjh;
        private AisinoMultiCombox cmbGhdw;
        private AisinoMultiCombox cmbGhdwsh;
        private AisinoMultiCombox cmbSfzh;
        private AisinoCMB cmbSlv;
        private IContainer components = null;
        internal List<string[]> data;
        private DateTimePicker dateTimePicker1;
        internal int index;
        private bool initSuccess = true;
        private bool JustAddFlag = false;
        private AisinoLBL label1;
        private Label label2;
        private AisinoLBL label21;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private AisinoLBL lblDM;
        private AisinoLBL lblFpdm;
        private AisinoLBL lblFphm;
        private AisinoLBL lblHM;
        private AisinoLBL lblJgxx;
        private AisinoLBL lblJqbh;
        private AisinoLBL lblJsdx;
        private AisinoLBL lblKpr;
        private AisinoLBL lblNsrsbh;
        private AisinoLBL lblSe;
        private AisinoLBL lblSwjg;
        private AisinoLBL lblSwjgdm;
        private AisinoLBL lblTitle;
        private AisinoLBL lblXhdwmc;
        private ILog log = LogUtil.GetLogger<JDCFPtiankai_new_new>();
        private bool onlyShow;
        private AisinoPNL panel1;
        private AisinoPNL panel2;
        private SaleBillCtrl saleBillBL = SaleBillCtrl.Instance;
        private TaxCard taxCard = TaxCardFactory.CreateTaxCard();
        private ToolTip tip = new ToolTip();
        private string TmpComment1 = "";
        private string TmpComment2 = "";
        private ToolStrip toolStrip3;
        private ToolStripButton toolStripBtnCancel;
        private ToolStripButton toolStripBtnSave;
        public AisinoTXT txt_bz;
        private AisinoTXT txtCd;
        private AisinoTXT txtCjmc;
        private AisinoTXT txtClsbh;
        private AisinoTXT txtCpxh;
        private AisinoTXT txtDh;
        private AisinoTXT txtDJH;
        private AisinoTXT txtDw;
        private AisinoTXT txtDz;
        private AisinoTXT txtHgzh;
        private AisinoTXT txtJkzmh;
        private AisinoTXT txtJsxx;
        private AisinoTXT txtKhyh;
        private AisinoTXT txtSjdh;
        private AisinoTXT txtXcrs;
        private AisinoTXT txtZh;
        private string xsdjbh = "";
        private Fpxx ykfp;

        internal JDCFPtiankai_new_new(FPLX fplx, string DJBH, string EditFlag = "xg")
        {
            try
            {
                try
                {
                    if (base.TaxCardInstance.get_QYLX().ISJDC)
                    {
                        this.JDCInvoiceForm_Init(fplx, DJBH, EditFlag);
                    }
                    else
                    {
                        MessageManager.ShowMsgBox("INP-242156", new string[] { " 无机动车销售统一发票授权。" });
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

        private bool check_port(string str_com)
        {
            List<string> list = new List<string>();
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Hardware\DeviceMap\SerialComm");
            if (key != null)
            {
                string[] valueNames = key.GetValueNames();
                foreach (string str in valueNames)
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
            this.txtDz.Text = this._fpxx.get_Dz();
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
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetCLMore", new object[] { str, 20, "MC,CPXH,CD,SCCJMC,BM,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV,YHZCMC" });
            if ((objArray != null) && (objArray.Length > 0))
            {
                return (objArray[0] as DataTable);
            }
            return null;
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
                    combox.set_DataSource(table);
                }
            }
        }

        private void cmbcl_OnButtonClick(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = (AisinoMultiCombox) sender;
            try
            {
                object[] bmxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetCL", new object[] { combox.Text, 0, "MC,CPXH,CD,SCCJMC,BM,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV,YHZCMC" });
                if (CommonTool.isSPBMVersion())
                {
                    string str = bmxx[0].ToString();
                    string str2 = bmxx[4].ToString();
                    if (bmxx[5].ToString() == "")
                    {
                        object[] objArray2 = new object[] { str, "MC,CPXH,CD,SCCJMC,BM,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV,YHZCMC", str2, true };
                        object[] objArray3 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddCL", objArray2);
                        if (objArray3 != null)
                        {
                            this.SetClxxValue(objArray3);
                        }
                        else
                        {
                            MessageBoxHelper.Show("商品分类编码需要补全！");
                        }
                    }
                    else
                    {
                        this.SetClxxValue(bmxx);
                    }
                }
                else
                {
                    this.SetClxxValue(bmxx);
                }
            }
            catch (Exception)
            {
            }
        }

        private void cmbcl_OnSelectValue(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if (combox != null)
            {
                Dictionary<string, string> dictionary = combox.get_SelectDict();
                if (CommonTool.isSPBMVersion())
                {
                    string str = dictionary["MC"];
                    string str2 = dictionary["BM"];
                    string str3 = dictionary["SPFL"];
                    if (str3 == "")
                    {
                        object[] objArray = new object[] { str, "MC,CPXH,CD,SCCJMC,BM,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV,YHZCMC", str2, true };
                        object[] bmxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddCL", objArray);
                        if (bmxx != null)
                        {
                            this.SetClxxValue(bmxx);
                        }
                        else
                        {
                            MessageBoxHelper.Show("车辆类型分类编码需要补全！");
                        }
                    }
                    else
                    {
                        this.SetClxxValue(new object[] { dictionary["MC"], dictionary["CPXH"], dictionary["CD"], dictionary["SCCJMC"], dictionary["BM"], dictionary["SPFL"], dictionary["YHZC"], dictionary["SPFL_ZZSTSGL"], dictionary["YHZC_SLV"], dictionary["YHZCMC"] });
                    }
                }
                else
                {
                    this.SetClxxValue(new object[] { dictionary["MC"], dictionary["CPXH"], dictionary["CD"], dictionary["SCCJMC"], dictionary["BM"], dictionary["SPFL"], dictionary["YHZC"], dictionary["SPFL_ZZSTSGL"], dictionary["YHZC_SLV"], dictionary["YHZCMC"] });
                }
            }
        }

        private void cmbCllx_Leave(object sender, EventArgs e)
        {
            if (CommonTool.isSPBMVersion())
            {
                try
                {
                    try
                    {
                        string mC = this.cmbCllx.Text.Trim();
                        if (mC != "")
                        {
                            string cPXH = this.txtCpxh.Text.Trim();
                            if ((this.CL_FLBM.Trim() == "") || (this.CL_CLLXName.Trim() != mC.Trim()))
                            {
                                object[] objArray;
                                object[] objArray2;
                                DataTable table = new SaleBillDAL().GET_SPXX_BY_NAME(mC, "j", cPXH);
                                if (table.Rows.Count == 0)
                                {
                                    objArray = new object[] { mC, "MC,CPXH,CD,SCCJMC,BM,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV,YHZCMC" };
                                    objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddCL", objArray);
                                    if (objArray2 != null)
                                    {
                                        if ((objArray2.Length >= 6) && (objArray2[5].ToString().Trim() == ""))
                                        {
                                            MessageBox.Show("车辆类型无商品分类编码！");
                                        }
                                        else
                                        {
                                            this.SetClxxValue(objArray2);
                                        }
                                    }
                                    else
                                    {
                                        MessageBoxHelper.Show("该车辆类型不存在，必须增加！");
                                    }
                                }
                                else if (table.Rows.Count == 1)
                                {
                                    objArray = new object[] { mC, 1, "MC,CPXH,CD,SCCJMC,BM,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV,YHZCMC" };
                                    objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetCL", objArray);
                                    if (objArray2 != null)
                                    {
                                        string str4 = objArray2[4].ToString().Trim();
                                        string str3 = objArray2[5].ToString().Trim();
                                        str4 = table.Rows[0]["BM"].ToString().Trim();
                                        if (table.Rows[0]["SPFL"].ToString().Trim() == "")
                                        {
                                            objArray = new object[] { mC, "MC,CPXH,CD,SCCJMC,BM,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV,YHZCMC", str4, true };
                                            objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddCL", objArray);
                                            if (objArray2 != null)
                                            {
                                                this.SetClxxValue(objArray2);
                                            }
                                            else
                                            {
                                                MessageBoxHelper.Show("商品分类编码需要补全！");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBoxHelper.Show("没有找到该车辆类型！");
                                        this.cmbCllx.Text = this.CL_CLLXName.Trim();
                                    }
                                }
                                else if ((table.Rows.Count > 1) && (this.CL_FLBM == ""))
                                {
                                    objArray = new object[] { mC, 0, "MC,CPXH,CD,SCCJMC,BM,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV,YHZCMC" };
                                    objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetCL", objArray);
                                    if (objArray2 != null)
                                    {
                                        this.SetClxxValue(objArray2);
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
                finally
                {
                }
            }
        }

        private void cmbCllx_TextChanged(object sender, EventArgs e)
        {
            string str = this.cmbCllx.Text.Trim();
            this._fpxx.set_Cllx(str);
            if (this._fpxx.get_Cllx() != str)
            {
                this.cmbCllx.Text = this._fpxx.get_Cllx();
                this.cmbCllx.set_SelectionStart(this.cmbCllx.Text.Length);
            }
        }

        private void cmbFdjh_TextChanged(object sender, EventArgs e)
        {
            string str = this.cmbFdjh.Text.Trim();
            this._fpxx.set_Fdjhm(str);
            if (this._fpxx.get_Fdjhm() != str)
            {
                this.cmbFdjh.Text = this._fpxx.get_Fdjhm();
                this.cmbFdjh.SelectionStart = this.cmbFdjh.Text.Length;
            }
        }

        private void cmbGhdw_TextChanged(object sender, EventArgs e)
        {
            string str = this.cmbGhdw.Text.Trim();
            this._fpxx.set_Gfmc(str);
            if (this._fpxx.get_Gfmc() != str)
            {
                this.cmbGhdw.Text = this._fpxx.get_Gfmc();
                this.cmbGhdw.set_SelectionStart(this.cmbGhdw.Text.Length);
            }
        }

        private void cmbGhdwsh_TextChanged(object sender, EventArgs e)
        {
            string text = this.cmbGhdwsh.Text;
            this._fpxx.set_Gfsh(text);
            if (this._fpxx.get_Gfsh() != text)
            {
                this.cmbGhdwsh.Text = this._fpxx.get_Gfsh();
                this.cmbGhdwsh.set_SelectionStart(this.cmbGhdwsh.Text.Length);
            }
        }

        private void cmbSfzh_TextChanged(object sender, EventArgs e)
        {
            string str = this.cmbSfzh.Text.Trim();
            this._fpxx.set_Sfzh_zzjgdm(str);
            if (this._fpxx.get_Sfzh_zzjgdm() != str)
            {
                this.cmbSfzh.Text = this._fpxx.get_Sfzh_zzjgdm();
                this.cmbSfzh.set_SelectionStart(this.cmbSfzh.Text.Length);
            }
        }

        private void cmbSlv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbSlv.SelectedItem != null)
            {
                if (CommonTool.isSPBMVersion())
                {
                    this.SetYHZCContent(this.cmbSlv.Text.Trim());
                }
                bool flag = this._fpxx.SetFpSLv(PresentinvMng.GetSLValue(this.cmbSlv.Text));
                double result = 0.0;
                double.TryParse(PresentinvMng.GetSLValue(this.cmbSlv.Text), out result);
                this.bill.SLV = result;
                this.SetHzxx();
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
                    combox.set_DataSource(table);
                }
            }
        }

        private void cmbxx_OnButtonClick(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = (AisinoMultiCombox) sender;
            try
            {
                object[] gfxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetGHDW", new object[] { combox.Text, 0, "MC,SH,IDCOC" });
                this.GfxxSetValue(gfxx);
                this.JustAddFlag = true;
            }
            catch (Exception)
            {
            }
        }

        private void cmbxx_OnSelectValue(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if (combox != null)
            {
                Dictionary<string, string> dictionary = combox.get_SelectDict();
                this.GfxxSetValue(new object[] { dictionary["MC"], dictionary["SH"], dictionary["IDCOC"] });
            }
        }

        private void CreateJDCInvoice(bool isRed, FPLX fplx)
        {
            byte[] sourceArray = Invoice.get_TypeByte();
            byte[] destinationArray = new byte[0x20];
            Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
            byte[] buffer3 = new byte[0x10];
            Array.Copy(sourceArray, 0x20, buffer3, 0, 0x10);
            byte[] buffer4 = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes("DJ" + DateTime.Now.ToString("F")), destinationArray, buffer3);
            Invoice.set_IsGfSqdFp_Static(false);
            this._fpxx = new Invoice(isRed, false, false, fplx, buffer4, null);
        }

        private void DelTextChangedEvent()
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FillComboxSLV()
        {
            List<SLV> slvList = PresentinvMng.GetSlvList(this._fpxx.get_Fplx(), this._fpxx.GetSqSLv());
            this.cmbSlv.Items.Clear();
            foreach (SLV slv in slvList)
            {
                if (!this.cmbSlv.Items.Contains(slv.get_ComboxValue()))
                {
                    this.cmbSlv.Items.Add(slv.get_ComboxValue());
                }
            }
            if (CommonTool.isSPBMVersion())
            {
                string str7;
                SaleBillDAL ldal = new SaleBillDAL();
                bool flag = this.CL_XSYH;
                string str = this.CL_SPFL_ZZSTSGL;
                string fLBM = this.CL_FLBM;
                if (flag)
                {
                    List<double> list2 = new List<double>();
                    string[] strArray = str.Split(new char[] { '、' });
                    foreach (string str3 in strArray)
                    {
                        List<double> list3 = ldal.GET_YHZCSLV_BY_YHZCMC(str3);
                        foreach (double num in list3)
                        {
                            if (!list2.Contains(num))
                            {
                                list2.Add(num);
                            }
                        }
                    }
                    foreach (double num2 in list2)
                    {
                        if (num2 == 0.0)
                        {
                            string str4 = "出口零税";
                            string str5 = "免税";
                            string str6 = "不征税";
                            if (str.Contains(str4) && !this.cmbSlv.Items.Contains("0%"))
                            {
                                this.cmbSlv.Items.Add("0%");
                            }
                            if (str.Contains(str5))
                            {
                                this.cmbSlv.Items.Add(str5);
                            }
                            if (str.Contains(str6))
                            {
                                this.cmbSlv.Items.Add(str6);
                            }
                        }
                        else
                        {
                            str7 = Convert.ToString(Math.Round((double) (num2 * 100.0), 1)) + "%";
                            if (!this.cmbSlv.Items.Contains(str7))
                            {
                                this.cmbSlv.Items.Add(str7);
                            }
                        }
                    }
                }
                if (fLBM.Trim() != "")
                {
                    List<double> list4 = ldal.GET_YHZCSYSLV_BY_FLBM(fLBM);
                    foreach (double num in list4)
                    {
                        str7 = Convert.ToString((double) (Math.Round(num, 3) * 100.0)) + "%";
                        if (!this.cmbSlv.Items.Contains(str7))
                        {
                            this.cmbSlv.Items.Add(str7);
                        }
                    }
                }
            }
        }

        private void GetFPText()
        {
            this.DelTextChangedEvent();
            this.cmbGhdw.Text = this._fpxx.get_Gfmc();
            this.cmbGhdwsh.Text = this._fpxx.get_Gfsh();
            this.cmbSfzh.Text = this._fpxx.get_Sfzh_zzjgdm();
            this.cmbCllx.Text = this._fpxx.get_Cllx();
            this.txtCpxh.Text = this._fpxx.get_Cpxh();
            this.txtCd.Text = this._fpxx.get_Cd();
            this.txtHgzh.Text = this._fpxx.get_Hgzh();
            this.txtJkzmh.Text = this._fpxx.get_Jkzmsh();
            this.txtSjdh.Text = this._fpxx.get_Sjdh();
            this.cmbFdjh.Text = this._fpxx.get_Fdjhm();
            this.txtClsbh.Text = this._fpxx.get_Clsbdh_cjhm();
            this.txtCjmc.Text = this._fpxx.get_Sccjmc();
            this.txtDh.Text = this._fpxx.get_Dh();
            this.txtZh.Text = this._fpxx.get_Zh();
            this.txtDz.Text = this._fpxx.get_Dz();
            this.txtKhyh.Text = this._fpxx.get_Khyh();
            this.txtDw.Text = this._fpxx.get_Dw();
            this.txtXcrs.Text = this._fpxx.get_Xcrs();
            this.RegTextChangedEvent();
        }

        private void GetQyLabel()
        {
            this.lblXhdwmc.Text = this._fpxx.get_Xfmc();
            this.lblNsrsbh.Text = this._fpxx.get_Xfsh();
            this.lblSwjg.Text = this._fpxx.get_Zgswjg_mc();
            this.lblSwjgdm.Text = this._fpxx.get_Zgswjg_dm();
            this.lblKpr.Text = this._fpxx.get_Kpr();
        }

        private void GetTaxLabel()
        {
            this.lblFpdm.Text = this._fpxx.get_Fpdm();
            this.lblFphm.Text = this._fpxx.get_Fphm();
            this.lblJqbh.Text = this._fpxx.get_Jqbh();
            this.lblDM.Text = this._fpxx.get_Fpdm();
            this.lblHM.Text = this._fpxx.get_Fphm();
        }

        private DataTable GfxxOnAutoCompleteDataSource(string str)
        {
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetGHDWMore", new object[] { str, 20, "MC,SH,IDCOC" });
            if ((objArray != null) && (objArray.Length > 0))
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

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(JDCFPtiankai_new_new));
            this.toolStrip3 = new ToolStrip();
            this.toolStripBtnCancel = new ToolStripButton();
            this.toolStripBtnSave = new ToolStripButton();
            this.panel2 = new AisinoPNL();
            this.panel1 = new AisinoPNL();
            this.aisinoLBL4 = new AisinoLBL();
            this.txt_bz = new AisinoTXT();
            this.aisinoLBL2 = new AisinoLBL();
            this.txtDJH = new AisinoTXT();
            this.aisinoLBL1 = new AisinoLBL();
            this.dateTimePicker1 = new DateTimePicker();
            this.lblJgxx = new AisinoLBL();
            this.label7 = new Label();
            this.label6 = new Label();
            this.txtJsxx = new AisinoTXT();
            this.label5 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.lblSwjgdm = new AisinoLBL();
            this.lblSwjg = new AisinoLBL();
            this.lblSe = new AisinoLBL();
            this.label21 = new AisinoLBL();
            this.lblJsdx = new AisinoLBL();
            this.label1 = new AisinoLBL();
            this.lblKpr = new AisinoLBL();
            this.txtXcrs = new AisinoTXT();
            this.txtDw = new AisinoTXT();
            this.cmbSlv = new AisinoCMB();
            this.txtDz = new AisinoTXT();
            this.txtKhyh = new AisinoTXT();
            this.txtZh = new AisinoTXT();
            this.lblNsrsbh = new AisinoLBL();
            this.txtDh = new AisinoTXT();
            this.lblXhdwmc = new AisinoLBL();
            this.txtCjmc = new AisinoTXT();
            this.cmbFdjh = new AisinoCMB();
            this.txtClsbh = new AisinoTXT();
            this.txtSjdh = new AisinoTXT();
            this.txtJkzmh = new AisinoTXT();
            this.txtHgzh = new AisinoTXT();
            this.txtCd = new AisinoTXT();
            this.txtCpxh = new AisinoTXT();
            this.cmbCllx = new AisinoMultiCombox();
            this.cmbGhdwsh = new AisinoMultiCombox();
            this.cmbGhdw = new AisinoMultiCombox();
            this.cmbSfzh = new AisinoMultiCombox();
            this.lblHM = new AisinoLBL();
            this.lblDM = new AisinoLBL();
            this.lblJqbh = new AisinoLBL();
            this.lblFphm = new AisinoLBL();
            this.lblFpdm = new AisinoLBL();
            this.label4 = new Label();
            this.aisinoLBL3 = new AisinoLBL();
            this.aisinoLBL5 = new AisinoLBL();
            this.lblTitle = new AisinoLBL();
            this.toolStrip3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.toolStrip3.BackColor = SystemColors.Control;
            this.toolStrip3.Items.AddRange(new ToolStripItem[] { this.toolStripBtnCancel, this.toolStripBtnSave });
            this.toolStrip3.Location = new Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new Size(0x3c4, 0x19);
            this.toolStrip3.Stretch = true;
            this.toolStrip3.TabIndex = 3;
            this.toolStripBtnCancel.Image = Resources.退出;
            this.toolStripBtnCancel.ImageScaling = ToolStripItemImageScaling.None;
            this.toolStripBtnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnCancel.Name = "toolStripBtnCancel";
            this.toolStripBtnCancel.Size = new Size(0x34, 0x16);
            this.toolStripBtnCancel.Text = "退出";
            this.toolStripBtnCancel.Click += new EventHandler(this.toolStripBtnCancel_Click);
            this.toolStripBtnSave.Image = Resources.修改;
            this.toolStripBtnSave.ImageScaling = ToolStripItemImageScaling.None;
            this.toolStripBtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnSave.Name = "toolStripBtnSave";
            this.toolStripBtnSave.Size = new Size(0x34, 0x16);
            this.toolStripBtnSave.Text = "保存";
            this.toolStripBtnSave.Click += new EventHandler(this.toolStripBtnSave_Click);
            this.panel2.AutoScroll = true;
            this.panel2.AutoScrollMinSize = new Size(0x3c4, 710);
            this.panel2.BackColor = System.Drawing.Color.FromArgb(180, 0xbb, 0xc2);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 0x19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x3c4, 0x2c4);
            this.panel2.TabIndex = 5;
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImage = Resources.JDCN;
            this.panel1.BackgroundImageLayout = ImageLayout.Zoom;
            this.panel1.Controls.Add(this.aisinoLBL4);
            this.panel1.Controls.Add(this.txt_bz);
            this.panel1.Controls.Add(this.aisinoLBL2);
            this.panel1.Controls.Add(this.txtDJH);
            this.panel1.Controls.Add(this.aisinoLBL1);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.lblJgxx);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtJsxx);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblSwjgdm);
            this.panel1.Controls.Add(this.lblSwjg);
            this.panel1.Controls.Add(this.lblSe);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.lblJsdx);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblKpr);
            this.panel1.Controls.Add(this.txtXcrs);
            this.panel1.Controls.Add(this.txtDw);
            this.panel1.Controls.Add(this.cmbSlv);
            this.panel1.Controls.Add(this.txtDz);
            this.panel1.Controls.Add(this.txtKhyh);
            this.panel1.Controls.Add(this.txtZh);
            this.panel1.Controls.Add(this.lblNsrsbh);
            this.panel1.Controls.Add(this.txtDh);
            this.panel1.Controls.Add(this.lblXhdwmc);
            this.panel1.Controls.Add(this.txtCjmc);
            this.panel1.Controls.Add(this.cmbFdjh);
            this.panel1.Controls.Add(this.txtClsbh);
            this.panel1.Controls.Add(this.txtSjdh);
            this.panel1.Controls.Add(this.txtJkzmh);
            this.panel1.Controls.Add(this.txtHgzh);
            this.panel1.Controls.Add(this.txtCd);
            this.panel1.Controls.Add(this.txtCpxh);
            this.panel1.Controls.Add(this.cmbCllx);
            this.panel1.Controls.Add(this.cmbGhdwsh);
            this.panel1.Controls.Add(this.cmbGhdw);
            this.panel1.Controls.Add(this.cmbSfzh);
            this.panel1.Controls.Add(this.lblHM);
            this.panel1.Controls.Add(this.lblDM);
            this.panel1.Controls.Add(this.lblJqbh);
            this.panel1.Controls.Add(this.lblFphm);
            this.panel1.Controls.Add(this.lblFpdm);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.aisinoLBL3);
            this.panel1.Controls.Add(this.aisinoLBL5);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Location = new Point(9, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x3b1, 0x2b3);
            this.panel1.TabIndex = 4;
            this.aisinoLBL4.AutoSize = true;
            this.aisinoLBL4.BackColor = System.Drawing.Color.White;
            this.aisinoLBL4.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.aisinoLBL4.ForeColor = System.Drawing.Color.Black;
            this.aisinoLBL4.Location = new Point(420, 0x93);
            this.aisinoLBL4.Name = "aisinoLBL4";
            this.aisinoLBL4.Size = new Size(0x1a, 0x54);
            this.aisinoLBL4.TabIndex = 0x47;
            this.aisinoLBL4.Text = "备\r\n\r\n\r\n注";
            this.txt_bz.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.txt_bz.BorderStyle = BorderStyle.None;
            this.txt_bz.Location = new Point(0x1ca, 0x8a);
            this.txt_bz.Multiline = true;
            this.txt_bz.Name = "txt_bz";
            this.txt_bz.Size = new Size(0x17f, 0x63);
            this.txt_bz.TabIndex = 70;
            this.aisinoLBL2.AutoSize = true;
            this.aisinoLBL2.BackColor = System.Drawing.Color.White;
            this.aisinoLBL2.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.aisinoLBL2.ForeColor = System.Drawing.Color.Black;
            this.aisinoLBL2.Location = new Point(0x4c, 0x66);
            this.aisinoLBL2.Name = "aisinoLBL2";
            this.aisinoLBL2.Size = new Size(0x4a, 0x15);
            this.aisinoLBL2.TabIndex = 0x43;
            this.aisinoLBL2.Text = "单据日期";
            this.txtDJH.BackColor = System.Drawing.Color.White;
            this.txtDJH.ForeColor = System.Drawing.Color.Black;
            this.txtDJH.Location = new Point(0xc0, 0x97);
            this.txtDJH.Name = "txtDJH";
            this.txtDJH.Size = new Size(0xc9, 0x15);
            this.txtDJH.TabIndex = 0x42;
            this.aisinoLBL1.AutoSize = true;
            this.aisinoLBL1.BackColor = System.Drawing.Color.White;
            this.aisinoLBL1.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.aisinoLBL1.ForeColor = System.Drawing.Color.Black;
            this.aisinoLBL1.Location = new Point(0x5c, 0x97);
            this.aisinoLBL1.Name = "aisinoLBL1";
            this.aisinoLBL1.Size = new Size(0x3a, 0x15);
            this.aisinoLBL1.TabIndex = 0x41;
            this.aisinoLBL1.Text = "单据号";
            this.dateTimePicker1.Location = new Point(0x9b, 0x66);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new Size(0xc9, 0x15);
            this.dateTimePicker1.TabIndex = 0x40;
            this.lblJgxx.Font = new Font("宋体", 11.25f);
            this.lblJgxx.ForeColor = System.Drawing.Color.Black;
            this.lblJgxx.Location = new Point(0xe2, 0x259);
            this.lblJgxx.Name = "lblJgxx";
            this.lblJgxx.Size = new Size(0x91, 0x22);
            this.lblJgxx.TabIndex = 0x3f;
            this.lblJgxx.Text = "\x00a5 0.00";
            this.lblJgxx.TextAlign = ContentAlignment.MiddleLeft;
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new Point(0x254, 0x173);
            this.label7.Name = "label7";
            this.label7.Size = new Size(11, 12);
            this.label7.TabIndex = 0x38;
            this.label7.Text = "*";
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new Point(0x2ce, 0x12b);
            this.label6.Name = "label6";
            this.label6.Size = new Size(11, 12);
            this.label6.TabIndex = 0x37;
            this.label6.Text = "*";
            this.txtJsxx.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtJsxx.BackColor = System.Drawing.Color.White;
            this.txtJsxx.ForeColor = System.Drawing.Color.Black;
            this.txtJsxx.Location = new Point(0x2b8, 0x1a3);
            this.txtJsxx.Name = "txtJsxx";
            this.txtJsxx.Size = new Size(0xa1, 0x15);
            this.txtJsxx.TabIndex = 0x1c;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new Point(0xac, 0x12a);
            this.label5.Name = "label5";
            this.label5.Size = new Size(11, 12);
            this.label5.TabIndex = 0x36;
            this.label5.Text = "*";
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new Point(0x2af, 0x198);
            this.label3.Name = "label3";
            this.label3.Size = new Size(11, 12);
            this.label3.TabIndex = 0x34;
            this.label3.Text = "*";
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new Point(0xad, 0xf8);
            this.label2.Name = "label2";
            this.label2.Size = new Size(11, 12);
            this.label2.TabIndex = 0x33;
            this.label2.Text = "*";
            this.lblSwjgdm.AutoSize = true;
            this.lblSwjgdm.ForeColor = System.Drawing.Color.Black;
            this.lblSwjgdm.Location = new Point(0x256, 0x247);
            this.lblSwjgdm.Name = "lblSwjgdm";
            this.lblSwjgdm.Size = new Size(0x1d, 12);
            this.lblSwjgdm.TabIndex = 50;
            this.lblSwjgdm.Text = "Test";
            this.lblSwjg.ForeColor = System.Drawing.Color.Black;
            this.lblSwjg.Location = new Point(0x256, 0x22a);
            this.lblSwjg.Name = "lblSwjg";
            this.lblSwjg.Size = new Size(260, 0x1b);
            this.lblSwjg.TabIndex = 0x31;
            this.lblSwjg.Text = "uiu";
            this.lblSwjg.TextAlign = ContentAlignment.MiddleLeft;
            this.lblSe.Font = new Font("宋体", 11.25f);
            this.lblSe.ForeColor = System.Drawing.Color.Black;
            this.lblSe.Location = new Point(350, 0x22d);
            this.lblSe.Name = "lblSe";
            this.lblSe.Size = new Size(0x7e, 0x22);
            this.lblSe.TabIndex = 0x30;
            this.lblSe.Text = "\x00a5 0.00";
            this.lblSe.TextAlign = ContentAlignment.MiddleLeft;
            this.label21.AutoSize = true;
            this.label21.Font = new Font("Microsoft Sans Serif", 11.25f);
            this.label21.Location = new Point(0xba, 0x1a0);
            this.label21.Name = "label21";
            this.label21.Size = new Size(0x44, 0x12);
            this.label21.TabIndex = 0x2f;
            this.label21.Text = "（大写）";
            this.lblJsdx.ForeColor = System.Drawing.Color.Black;
            this.lblJsdx.Location = new Point(0xfe, 0x19b);
            this.lblJsdx.Name = "lblJsdx";
            this.lblJsdx.Size = new Size(0x18b, 0x1b);
            this.lblJsdx.TabIndex = 0x2e;
            this.lblJsdx.Text = "零";
            this.lblJsdx.TextAlign = ContentAlignment.MiddleLeft;
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Microsoft Sans Serif", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.Location = new Point(0x4d, 0x29c);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x62, 0x11);
            this.label1.TabIndex = 0x2d;
            this.label1.Text = "生产企业名称";
            this.lblKpr.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.lblKpr.BackColor = System.Drawing.Color.White;
            this.lblKpr.Font = new Font("宋体", 11.25f);
            this.lblKpr.ForeColor = System.Drawing.Color.Black;
            this.lblKpr.Location = new Point(0x1cb, 0x287);
            this.lblKpr.Name = "lblKpr";
            this.lblKpr.Size = new Size(0xb2, 15);
            this.lblKpr.TabIndex = 0x2b;
            this.lblKpr.Text = "管理员1";
            this.lblKpr.TextAlign = ContentAlignment.MiddleLeft;
            this.txtXcrs.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtXcrs.BackColor = System.Drawing.Color.White;
            this.txtXcrs.ForeColor = System.Drawing.Color.Black;
            this.txtXcrs.Location = new Point(0x324, 610);
            this.txtXcrs.Name = "txtXcrs";
            this.txtXcrs.Size = new Size(0x36, 0x15);
            this.txtXcrs.TabIndex = 0x29;
            this.txtDw.BackColor = System.Drawing.Color.White;
            this.txtDw.ForeColor = System.Drawing.Color.Black;
            this.txtDw.Location = new Point(0x2b8, 0x263);
            this.txtDw.Name = "txtDw";
            this.txtDw.Size = new Size(0x24, 0x15);
            this.txtDw.TabIndex = 40;
            this.cmbSlv.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.cmbSlv.BackColor = System.Drawing.Color.White;
            this.cmbSlv.ForeColor = System.Drawing.Color.Black;
            this.cmbSlv.FormattingEnabled = true;
            this.cmbSlv.Items.AddRange(new object[] { "0.05" });
            this.cmbSlv.Location = new Point(0xbd, 0x239);
            this.cmbSlv.Name = "cmbSlv";
            this.cmbSlv.Size = new Size(90, 20);
            this.cmbSlv.TabIndex = 0x24;
            this.txtDz.BackColor = System.Drawing.Color.White;
            this.txtDz.ForeColor = System.Drawing.Color.Black;
            this.txtDz.Location = new Point(0xbb, 0x20f);
            this.txtDz.Name = "txtDz";
            this.txtDz.Size = new Size(0x12f, 0x15);
            this.txtDz.TabIndex = 0x22;
            this.txtKhyh.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtKhyh.BackColor = System.Drawing.Color.White;
            this.txtKhyh.ForeColor = System.Drawing.Color.Black;
            this.txtKhyh.Location = new Point(560, 0x20f);
            this.txtKhyh.Name = "txtKhyh";
            this.txtKhyh.Size = new Size(0x12a, 0x15);
            this.txtKhyh.TabIndex = 0x23;
            this.txtZh.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtZh.BackColor = System.Drawing.Color.White;
            this.txtZh.ForeColor = System.Drawing.Color.Black;
            this.txtZh.Location = new Point(0x27f, 0x1eb);
            this.txtZh.Name = "txtZh";
            this.txtZh.Size = new Size(0xda, 0x15);
            this.txtZh.TabIndex = 0x20;
            this.lblNsrsbh.BackColor = System.Drawing.Color.White;
            this.lblNsrsbh.ForeColor = System.Drawing.Color.Black;
            this.lblNsrsbh.Location = new Point(190, 0x1e8);
            this.lblNsrsbh.Name = "lblNsrsbh";
            this.lblNsrsbh.Size = new Size(0x191, 0x18);
            this.lblNsrsbh.TabIndex = 0x21;
            this.lblNsrsbh.Text = "Test";
            this.lblNsrsbh.TextAlign = ContentAlignment.MiddleLeft;
            this.txtDh.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtDh.BackColor = System.Drawing.Color.White;
            this.txtDh.ForeColor = System.Drawing.Color.Black;
            this.txtDh.Location = new Point(0x27f, 0x1c7);
            this.txtDh.Name = "txtDh";
            this.txtDh.Size = new Size(0xda, 0x15);
            this.txtDh.TabIndex = 30;
            this.lblXhdwmc.BackColor = System.Drawing.Color.White;
            this.lblXhdwmc.ForeColor = System.Drawing.Color.Black;
            this.lblXhdwmc.Location = new Point(190, 0x1c1);
            this.lblXhdwmc.Name = "lblXhdwmc";
            this.lblXhdwmc.Size = new Size(0x191, 0x1a);
            this.lblXhdwmc.TabIndex = 0x1f;
            this.lblXhdwmc.Text = "Test";
            this.lblXhdwmc.TextAlign = ContentAlignment.MiddleLeft;
            this.txtCjmc.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtCjmc.BackColor = System.Drawing.Color.White;
            this.txtCjmc.ForeColor = System.Drawing.Color.Black;
            this.txtCjmc.Location = new Point(0xbb, 0x299);
            this.txtCjmc.Name = "txtCjmc";
            this.txtCjmc.Size = new Size(670, 0x15);
            this.txtCjmc.TabIndex = 0x1b;
            this.cmbFdjh.BackColor = System.Drawing.Color.White;
            this.cmbFdjh.ForeColor = System.Drawing.Color.Black;
            this.cmbFdjh.FormattingEnabled = true;
            this.cmbFdjh.Location = new Point(0xbb, 0x17e);
            this.cmbFdjh.Name = "cmbFdjh";
            this.cmbFdjh.Size = new Size(260, 20);
            this.cmbFdjh.TabIndex = 0x19;
            this.txtClsbh.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtClsbh.BackColor = System.Drawing.Color.White;
            this.txtClsbh.ForeColor = System.Drawing.Color.Black;
            this.txtClsbh.Location = new Point(0x265, 0x17e);
            this.txtClsbh.Name = "txtClsbh";
            this.txtClsbh.Size = new Size(0xf4, 0x15);
            this.txtClsbh.TabIndex = 0x1a;
            this.txtSjdh.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtSjdh.BackColor = System.Drawing.Color.White;
            this.txtSjdh.ForeColor = System.Drawing.Color.Black;
            this.txtSjdh.Location = new Point(0x2de, 0x15a);
            this.txtSjdh.Name = "txtSjdh";
            this.txtSjdh.Size = new Size(0x7c, 0x15);
            this.txtSjdh.TabIndex = 0x18;
            this.txtJkzmh.BackColor = System.Drawing.Color.White;
            this.txtJkzmh.ForeColor = System.Drawing.Color.Black;
            this.txtJkzmh.Location = new Point(0x1c6, 0x15a);
            this.txtJkzmh.Name = "txtJkzmh";
            this.txtJkzmh.Size = new Size(0xd1, 0x15);
            this.txtJkzmh.TabIndex = 0x17;
            this.txtHgzh.BackColor = System.Drawing.Color.White;
            this.txtHgzh.ForeColor = System.Drawing.Color.Black;
            this.txtHgzh.Location = new Point(0xbb, 0x15a);
            this.txtHgzh.Name = "txtHgzh";
            this.txtHgzh.Size = new Size(0xa3, 0x15);
            this.txtHgzh.TabIndex = 0x16;
            this.txtCd.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtCd.BackColor = System.Drawing.Color.White;
            this.txtCd.ForeColor = System.Drawing.Color.Black;
            this.txtCd.Location = new Point(0x2de, 310);
            this.txtCd.Name = "txtCd";
            this.txtCd.Size = new Size(0x7c, 0x15);
            this.txtCd.TabIndex = 0x15;
            this.txtCpxh.BackColor = System.Drawing.Color.White;
            this.txtCpxh.ForeColor = System.Drawing.Color.Black;
            this.txtCpxh.Location = new Point(0x1a8, 0x135);
            this.txtCpxh.Name = "txtCpxh";
            this.txtCpxh.Size = new Size(0x107, 0x15);
            this.txtCpxh.TabIndex = 20;
            this.cmbCllx.set_AutoComplate(0);
            this.cmbCllx.set_AutoIndex(1);
            this.cmbCllx.BackColor = System.Drawing.Color.White;
            this.cmbCllx.set_BorderColor(SystemColors.ActiveBorder);
            this.cmbCllx.set_BorderStyle(1);
            this.cmbCllx.set_ButtonAutoHide(true);
            this.cmbCllx.set_buttonStyle(1);
            this.cmbCllx.set_DataSource(null);
            this.cmbCllx.set_DrawHead(true);
            this.cmbCllx.set_Edit(1);
            this.cmbCllx.ForeColor = System.Drawing.Color.Black;
            this.cmbCllx.set_IsSelectAll(false);
            this.cmbCllx.Location = new Point(0xbb, 0x134);
            this.cmbCllx.set_MaxIndex(8);
            this.cmbCllx.set_MaxLength(0x7fff);
            this.cmbCllx.Name = "cmbCllx";
            this.cmbCllx.set_ReadOnly(false);
            this.cmbCllx.set_SelectedIndex(-1);
            this.cmbCllx.set_SelectionStart(0);
            this.cmbCllx.set_ShowText("");
            this.cmbCllx.Size = new Size(0xa3, 0x15);
            this.cmbCllx.TabIndex = 0x13;
            this.cmbCllx.set_UnderLineColor(System.Drawing.Color.Transparent);
            this.cmbCllx.set_UnderLineStyle(0);
            this.cmbGhdwsh.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.cmbGhdwsh.set_AutoComplate(0);
            this.cmbGhdwsh.set_AutoIndex(1);
            this.cmbGhdwsh.BackColor = System.Drawing.Color.White;
            this.cmbGhdwsh.set_BorderColor(SystemColors.ActiveBorder);
            this.cmbGhdwsh.set_BorderStyle(1);
            this.cmbGhdwsh.set_ButtonAutoHide(true);
            this.cmbGhdwsh.set_buttonStyle(1);
            this.cmbGhdwsh.set_DataSource(null);
            this.cmbGhdwsh.set_DrawHead(true);
            this.cmbGhdwsh.set_Edit(1);
            this.cmbGhdwsh.ForeColor = System.Drawing.Color.Black;
            this.cmbGhdwsh.set_IsSelectAll(false);
            this.cmbGhdwsh.Location = new Point(0x277, 0x106);
            this.cmbGhdwsh.set_MaxIndex(8);
            this.cmbGhdwsh.set_MaxLength(0x7fff);
            this.cmbGhdwsh.Name = "cmbGhdwsh";
            this.cmbGhdwsh.set_ReadOnly(false);
            this.cmbGhdwsh.set_SelectedIndex(-1);
            this.cmbGhdwsh.set_SelectionStart(0);
            this.cmbGhdwsh.set_ShowText("");
            this.cmbGhdwsh.Size = new Size(0xe2, 0x15);
            this.cmbGhdwsh.TabIndex = 0x12;
            this.cmbGhdwsh.set_UnderLineColor(System.Drawing.Color.Transparent);
            this.cmbGhdwsh.set_UnderLineStyle(0);
            this.cmbGhdw.set_AutoComplate(0);
            this.cmbGhdw.set_AutoIndex(1);
            this.cmbGhdw.BackColor = System.Drawing.Color.White;
            this.cmbGhdw.set_BorderColor(SystemColors.ActiveBorder);
            this.cmbGhdw.set_BorderStyle(1);
            this.cmbGhdw.set_ButtonAutoHide(true);
            this.cmbGhdw.set_buttonStyle(1);
            this.cmbGhdw.set_DataSource(null);
            this.cmbGhdw.set_DrawHead(true);
            this.cmbGhdw.set_Edit(1);
            this.cmbGhdw.ForeColor = System.Drawing.Color.Black;
            this.cmbGhdw.set_IsSelectAll(false);
            this.cmbGhdw.Location = new Point(0xbb, 0xfb);
            this.cmbGhdw.set_MaxIndex(8);
            this.cmbGhdw.set_MaxLength(0x7fff);
            this.cmbGhdw.Name = "cmbGhdw";
            this.cmbGhdw.set_ReadOnly(false);
            this.cmbGhdw.set_SelectedIndex(-1);
            this.cmbGhdw.set_SelectionStart(0);
            this.cmbGhdw.set_ShowText("");
            this.cmbGhdw.Size = new Size(260, 0x15);
            this.cmbGhdw.TabIndex = 0x10;
            this.cmbGhdw.set_UnderLineColor(System.Drawing.Color.Transparent);
            this.cmbGhdw.set_UnderLineStyle(0);
            this.cmbSfzh.set_AutoComplate(0);
            this.cmbSfzh.set_AutoIndex(1);
            this.cmbSfzh.BackColor = System.Drawing.Color.White;
            this.cmbSfzh.set_BorderColor(SystemColors.ActiveBorder);
            this.cmbSfzh.set_BorderStyle(1);
            this.cmbSfzh.set_ButtonAutoHide(true);
            this.cmbSfzh.set_buttonStyle(1);
            this.cmbSfzh.set_DataSource(null);
            this.cmbSfzh.set_DrawHead(true);
            this.cmbSfzh.set_Edit(1);
            this.cmbSfzh.ForeColor = System.Drawing.Color.Black;
            this.cmbSfzh.set_IsSelectAll(false);
            this.cmbSfzh.Location = new Point(0xbb, 0x112);
            this.cmbSfzh.set_MaxIndex(8);
            this.cmbSfzh.set_MaxLength(0x7fff);
            this.cmbSfzh.Name = "cmbSfzh";
            this.cmbSfzh.set_ReadOnly(false);
            this.cmbSfzh.set_SelectedIndex(-1);
            this.cmbSfzh.set_SelectionStart(0);
            this.cmbSfzh.set_ShowText("");
            this.cmbSfzh.Size = new Size(260, 0x15);
            this.cmbSfzh.TabIndex = 0x11;
            this.cmbSfzh.set_UnderLineColor(System.Drawing.Color.Transparent);
            this.cmbSfzh.set_UnderLineStyle(0);
            this.lblHM.AutoSize = true;
            this.lblHM.BackColor = System.Drawing.Color.White;
            this.lblHM.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lblHM.ForeColor = System.Drawing.Color.Black;
            this.lblHM.Location = new Point(0x28f, 0x5c);
            this.lblHM.Name = "lblHM";
            this.lblHM.Size = new Size(0x52, 0x15);
            this.lblHM.TabIndex = 14;
            this.lblHM.Text = "00000000";
            this.lblHM.Visible = false;
            this.lblDM.AutoSize = true;
            this.lblDM.BackColor = System.Drawing.Color.White;
            this.lblDM.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lblDM.ForeColor = System.Drawing.Color.Black;
            this.lblDM.Location = new Point(0x28f, 0x47);
            this.lblDM.Name = "lblDM";
            this.lblDM.Size = new Size(0x76, 0x15);
            this.lblDM.TabIndex = 13;
            this.lblDM.Text = "000000000000";
            this.lblDM.Visible = false;
            this.lblJqbh.AutoSize = true;
            this.lblJqbh.BackColor = System.Drawing.Color.White;
            this.lblJqbh.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lblJqbh.ForeColor = System.Drawing.Color.Black;
            this.lblJqbh.Location = new Point(0xbd, 0xcc);
            this.lblJqbh.Name = "lblJqbh";
            this.lblJqbh.Size = new Size(100, 0x15);
            this.lblJqbh.TabIndex = 12;
            this.lblJqbh.Text = "0000000000";
            this.lblFphm.AutoSize = true;
            this.lblFphm.BackColor = System.Drawing.Color.White;
            this.lblFphm.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lblFphm.ForeColor = System.Drawing.Color.Black;
            this.lblFphm.Location = new Point(0xbd, 0xb2);
            this.lblFphm.Name = "lblFphm";
            this.lblFphm.Size = new Size(0x52, 0x15);
            this.lblFphm.TabIndex = 11;
            this.lblFphm.Text = "00000000";
            this.lblFpdm.AutoSize = true;
            this.lblFpdm.BackColor = System.Drawing.Color.White;
            this.lblFpdm.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lblFpdm.ForeColor = System.Drawing.Color.Black;
            this.lblFpdm.Location = new Point(0xbd, 0x97);
            this.lblFpdm.Name = "lblFpdm";
            this.lblFpdm.Size = new Size(0x76, 0x15);
            this.lblFpdm.TabIndex = 10;
            this.lblFpdm.Text = "000000000000";
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new Point(0x197, 0x12b);
            this.label4.Name = "label4";
            this.label4.Size = new Size(11, 12);
            this.label4.TabIndex = 0x35;
            this.label4.Text = "*";
            this.aisinoLBL3.BackColor = System.Drawing.Color.White;
            this.aisinoLBL3.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.aisinoLBL3.ForeColor = System.Drawing.Color.Black;
            this.aisinoLBL3.Location = new Point(0x53, 0x84);
            this.aisinoLBL3.Name = "aisinoLBL3";
            this.aisinoLBL3.Size = new Size(0x5f, 110);
            this.aisinoLBL3.TabIndex = 0x44;
            this.aisinoLBL5.BackColor = System.Drawing.Color.White;
            this.aisinoLBL5.Font = new Font("微软雅黑", 20f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.aisinoLBL5.ForeColor = System.Drawing.Color.Black;
            this.aisinoLBL5.Location = new Point(0x12a, 0x17);
            this.aisinoLBL5.Name = "aisinoLBL5";
            this.aisinoLBL5.Size = new Size(0x15f, 90);
            this.aisinoLBL5.TabIndex = 0x48;
            this.aisinoLBL5.Text = "机动车统一销售发票\r\n销售单据";
            this.aisinoLBL5.TextAlign = ContentAlignment.TopCenter;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 10f);
            this.lblTitle.ForeColor = System.Drawing.Color.Crimson;
            this.lblTitle.Location = new Point(0x1bc, 0x39);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(50, 0x12);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "北  京";
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            base.ClientSize = new Size(0x3c4, 0x2dd);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.toolStrip3);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "JDCFPtiankai_new_new";
            this.Text = "JDCFPtiankai_new_new";
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InitializeDefault()
        {
            this.InitializeComponent();
            this.cmbFdjh.Items.Add("无");
            this.cmbSlv.DropDownStyle = ComboBoxStyle.DropDownList;
            if (!this.onlyShow)
            {
                System.Drawing.Color activeBorder = SystemColors.ActiveBorder;
                this.cmbGhdw.set_BorderColor(activeBorder);
                this.cmbGhdwsh.set_BorderColor(activeBorder);
                this.cmbSfzh.set_BorderColor(activeBorder);
                this.cmbCllx.set_BorderColor(activeBorder);
                this.txtDJH.TextChanged += new EventHandler(this.textBoxDJH_TextChanged);
                this.SetGhfCmb(this.cmbGhdw, "MC");
                this.SetGhfCmb(this.cmbGhdwsh, "SH");
                this.SetGhfCmb(this.cmbSfzh, "IDCOC");
                this.SetCllxCmb(this.cmbCllx, "LXMC");
                this.cmbGhdw.OnTextChanged = (EventHandler) Delegate.Combine(this.cmbGhdw.OnTextChanged, new EventHandler(this.cmbGhdw_TextChanged));
                this.cmbGhdwsh.OnTextChanged = (EventHandler) Delegate.Combine(this.cmbGhdwsh.OnTextChanged, new EventHandler(this.cmbGhdwsh_TextChanged));
                this.cmbSfzh.OnTextChanged = (EventHandler) Delegate.Combine(this.cmbSfzh.OnTextChanged, new EventHandler(this.cmbSfzh_TextChanged));
                this.cmbCllx.OnTextChanged = (EventHandler) Delegate.Combine(this.cmbCllx.OnTextChanged, new EventHandler(this.cmbCllx_TextChanged));
                this.cmbCllx.Leave += new EventHandler(this.cmbCllx_Leave);
                this.cmbFdjh.TextChanged += new EventHandler(this.cmbFdjh_TextChanged);
                this.txtCpxh.TextChanged += new EventHandler(this.txtCpxh_TextChanged);
                this.txtCpxh.Leave += new EventHandler(this.txtCpxh_Leave);
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
                this.txtJsxx.LostFocus += new EventHandler(this.txtJsxx_LostFocus);
                base.FormClosing += new FormClosingEventHandler(this.JDCInvoiceForm_FormClosing);
            }
            else
            {
                this.dateTimePicker1.Enabled = false;
            }
            this.cmbSlv.SelectedIndexChanged += new EventHandler(this.cmbSlv_SelectedIndexChanged);
            base.KeyPreview = true;
            base.Resize += new EventHandler(this.JDCInvoiceForm_new_Resize);
        }

        private void JDCInvoiceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.txtJsxx.LostFocus -= new EventHandler(this.txtJsxx_LostFocus);
        }

        private void JDCInvoiceForm_Init(FPLX fplx, string DJBH, string EditFlag)
        {
            if (EditFlag == "xg")
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
            this.CreateJDCInvoice(false, fplx);
            this._fpxx.Jdc_ver_new=true;
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
            if (this.cmbSlv.Items.Count == 0)
            {
                this.initSuccess = false;
                MessageManager.ShowMsgBox("INP-242129", new string[] { "机动车销售统一发票" });
            }
            else
            {
                this.ShowInitInvInfo();
                this.ResetCmbSlv();
                if (this.onlyShow)
                {
                    this.txt_bz.Cursor = this.txtDJH.Cursor = Cursors.No;
                    this.txt_bz.ReadOnly = this.txtDJH.ReadOnly = true;
                    this.toolStripBtnSave.Visible = false;
                    this.SetCmbLabel();
                    this.SetTxtEnabled(false);
                }
                if (EditFlag == "xg")
                {
                    this.Text = "机动车单据修改";
                    this.txtDJH.ReadOnly = true;
                    this.txtDJH.BorderStyle = BorderStyle.FixedSingle;
                    this.ToView(true);
                }
                else
                {
                    this.Text = "机动车单据添加";
                    this.bill.DJRQ = TaxCardValue.taxCard.GetCardClock();
                    this.ToView(false);
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
                byte[] bytes = ToolUtil.GetBytes(MD5_Crypt.GetHashStr("Aisino.Fwkp.Invoice" + this._fpxx.get_Fpdm() + this._fpxx.get_Fphm()));
                byte[] destinationArray = new byte[0x20];
                Array.Copy(bytes, 0, destinationArray, 0, 0x20);
                byte[] buffer3 = new byte[0x10];
                Array.Copy(bytes, 0x20, buffer3, 0, 0x10);
                byte[] inArray = AES_Crypt.Encrypt(ToolUtil.GetBytes(DateTime.Now.ToString("F")), destinationArray, buffer3);
                fp.gfmc = Convert.ToBase64String(AES_Crypt.Encrypt(ToolUtil.GetBytes(Convert.ToBase64String(inArray) + ";" + this._fpxx.get_Gfmc()), destinationArray, buffer3));
                flag = this._fpxx.MakeCardInvoice(fp, false);
            }
            catch (Exception exception)
            {
                this.log.Error(exception);
            }
            if (!flag)
            {
                MessageManager.ShowMsgBox(this._fpxx.GetCode());
                if (this._fpxx.GetCode().StartsWith("TCD_769"))
                {
                    this.CloseJDCForm();
                }
            }
            return flag;
        }

        private void PrintFP(Fpxx fp)
        {
            try
            {
                FPPrint print = new FPPrint(Invoice.FPLX2Str(this._fpxx.Fplx), fp.fpdm, int.Parse(fp.fphm));
                print.Print(true);
                string str = print.IsPrint;
                if ((str != "0000") && (str != "0006"))
                {
                    MessageManager.ShowMsgBox("INP-242116");
                }
                else if (str == "0000")
                {
                    ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FPDYShareMethod", new object[] { fp.fplx, fp.fpdm, fp.fphm });
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
            string str = this._fpxx.get_Kprq();
            string str2 = this._fpxx.get_Fpdm();
            string str3 = this._fpxx.get_Fphm();
            string str4 = this._fpxx.get_Zgswjg_dm();
            string str5 = this._fpxx.get_Zgswjg_mc();
            string str6 = this._fpxx.get_Jqbh();
            bool flag = this._fpxx.get_Jdc_ver_new();
            this.CreateJDCInvoice(isRed, this._fpxx.get_Fplx());
            this._fpxx.set_Fpdm(str2);
            this._fpxx.set_Fphm(str3);
            this._fpxx.set_Jqbh(str6);
            this._fpxx.set_Zgswjg_mc(str5);
            this._fpxx.set_Zgswjg_dm(str4);
            this._fpxx.set_Kprq(str);
            this._fpxx.set_Kpr(UserInfo.get_Yhmc());
            this._fpxx.set_Jdc_ver_new(flag);
            this._fpxx.SetFpSLv(PresentinvMng.GetSLValue(this.cmbSlv.Text));
            this.cmbSlv.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ClearMainInfo();
            this.ShowInitInvInfo();
            this.cmbGhdw.set_Edit(1);
            this.cmbGhdwsh.set_Edit(1);
            this.cmbSfzh.set_Edit(1);
            this.cmbCllx.set_Edit(1);
        }

        private void RegTextChangedEvent()
        {
        }

        private void ResetCmbSlv()
        {
            this.FillComboxSLV();
            if (this.cmbSlv.Items.Count > 0)
            {
                string s = PropertyUtil.GetValue("JDCINVNEW-SLV", "");
                if (s == "")
                {
                    this.cmbSlv.SelectedIndexChanged -= new EventHandler(this.cmbSlv_SelectedIndexChanged);
                    this.cmbSlv.SelectedIndex = 0;
                    this.cmbSlv.SelectedIndexChanged += new EventHandler(this.cmbSlv_SelectedIndexChanged);
                }
                else
                {
                    int num = 0;
                    for (int i = 0; i < this.cmbSlv.Items.Count; i++)
                    {
                        string sLValue = PresentinvMng.GetSLValue(this.cmbSlv.Items[i].ToString());
                        double result = -1.0;
                        double num4 = -1.0;
                        bool flag = double.TryParse(s, out result);
                        bool flag2 = double.TryParse(sLValue, out num4);
                        if (((num4 != -1.0) && !(result == -1.0)) && (Math.Abs((double) (result - num4)) < 1E-06))
                        {
                            num = i;
                            break;
                        }
                    }
                    this.cmbSlv.SelectedIndexChanged -= new EventHandler(this.cmbSlv_SelectedIndexChanged);
                    this.cmbSlv.SelectedIndex = num;
                    this.cmbSlv.SelectedIndexChanged += new EventHandler(this.cmbSlv_SelectedIndexChanged);
                }
            }
        }

        private bool SaveFpData()
        {
            this.SetFpText();
            this._fpxx.set_Xfmc(this.lblXhdwmc.Text.Trim());
            this._fpxx.set_Xfsh(this.lblNsrsbh.Text.Trim());
            this._fpxx.set_Zgswjg_mc(this.lblSwjg.Text.Trim());
            this._fpxx.set_Zgswjg_dm(this.lblSwjgdm.Text.Trim());
            bool flag = this._fpxx.SetFpSLv(PresentinvMng.GetSLValue(this.cmbSlv.Text));
            if (flag)
            {
                this._fpxx.SetJshj(this.txtJsxx.Text.Trim());
            }
            if (!flag)
            {
                MessageManager.ShowMsgBox(this._fpxx.GetCode(), this._fpxx.Params);
            }
            string str = this._fpxx.get_SLv();
            string str2 = "";
            this.ToModel();
            str2 = this.saleBillBL.Save(this.bill);
            if (str2 == "0")
            {
                string str3 = this.saleBillBL.CheckBill(this.bill);
                base.DialogResult = DialogResult.OK;
                return flag;
            }
            MessageManager.ShowMsgBox(str2);
            return flag;
        }

        private void set_saomiao_info(string value, int type)
        {
            if ((value != null) && (value.Length > 0))
            {
                string str = "#%";
                List<string> list = new List<string>();
                string[] strArray = value.Split(new string[] { str }, StringSplitOptions.None);
                foreach (string str2 in strArray)
                {
                    list.Add(str2);
                }
                this.DelTextChangedEvent();
                if (type == 1)
                {
                    this.txtCjmc.Text = list[3];
                    this.txtHgzh.Text = list[1];
                    this.cmbCllx.Text = list[5];
                    this.txtCpxh.Text = list[7];
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
                    this.txtCpxh.Text = list[3];
                    this.txtClsbh.Text = list[4];
                    this.cmbFdjh.Text = list[5];
                    this.txtDw.Text = "";
                    this.txtXcrs.Text = "";
                    this.txtJkzmh.Text = list[8];
                }
                this.RegTextChangedEvent();
            }
        }

        private void SetCllxCmb(AisinoMultiCombox aisinoCmb, string showText)
        {
            aisinoCmb.set_IsSelectAll(true);
            aisinoCmb.set_buttonStyle(0);
            aisinoCmb.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("车辆类型", "MC", 100));
            aisinoCmb.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("厂牌型号", "CPXH", 100));
            aisinoCmb.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("产地", "CD", 80));
            aisinoCmb.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("生产企业", "SCCJMC", 160));
            aisinoCmb.set_ShowText(showText);
            aisinoCmb.set_DrawHead(true);
            aisinoCmb.set_AutoIndex(0);
            aisinoCmb.add_OnButtonClick(new EventHandler(this.cmbcl_OnButtonClick));
            aisinoCmb.add_OnAutoComplate(new EventHandler(this.cmbcl_OnAutoComplate));
            aisinoCmb.add_OnSelectValue(new EventHandler(this.cmbcl_OnSelectValue));
            aisinoCmb.set_AutoComplate(2);
        }

        private void SetClxxValue(object[] bmxx)
        {
            if (bmxx != null)
            {
                if (CommonTool.isSPBMVersion() && (bmxx.Length >= 6))
                {
                    string str = bmxx[5].ToString().Trim();
                    if (str == "")
                    {
                        MessageManager.ShowMsgBox("您选择的车辆类型没有分类编码，请重新选择！");
                        this.cmbCllx.Text = "";
                        return;
                    }
                    object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.CanUseThisSPFLBM", new object[] { str, true });
                    if ((objArray != null) && !bool.Parse(objArray[0].ToString()))
                    {
                        MessageManager.ShowMsgBox("INP-242207", new string[] { "车辆类型", "\r\n可能原因：\r\n1、当前企业没有所选税收分类编码授权。\r\n2、当前版本所选税收分类编码可用状态为不可用。" });
                        this.cmbCllx.Text = "";
                        return;
                    }
                }
                this.DelTextChangedEvent();
                this.cmbCllx.Text = bmxx[0].ToString().Trim();
                this.CL_CLLXName = this.cmbCllx.Text;
                this.txtCpxh.Text = bmxx[1].ToString();
                this.CL_CPXXName = this.txtCpxh.Text.Trim();
                this.txtCd.Text = bmxx[2].ToString();
                this.txtCjmc.Text = bmxx[3].ToString();
                if (CommonTool.isSPBMVersion())
                {
                    List<double> list2;
                    string str3;
                    if (bmxx.Length >= 5)
                    {
                        this.CL_CLBM = bmxx[4].ToString().Trim();
                    }
                    if (bmxx.Length >= 6)
                    {
                        this.CL_FLBM = bmxx[5].ToString().Trim();
                    }
                    if (bmxx.Length >= 7)
                    {
                        string str2 = bmxx[6].ToString().Trim();
                        if (((str2 == "是") || (str2.ToUpper() == "TRUE")) || (str2 == "1"))
                        {
                            this.CL_XSYH = true;
                        }
                        else
                        {
                            this.CL_XSYH = false;
                        }
                    }
                    if (bmxx.Length >= 10)
                    {
                        this.CL_SPFL_ZZSTSGL = bmxx[9].ToString().Trim();
                    }
                    SaleBillDAL ldal = new SaleBillDAL();
                    if (bmxx.Length >= 9)
                    {
                        this.CL_YHZC_SLV = bmxx[8].ToString().Trim();
                    }
                    this.FillComboxSLV();
                    if (!this.CL_XSYH)
                    {
                        if (this.CL_FLBM != "")
                        {
                            list2 = ldal.GET_YHZCSYSLV_BY_FLBM(this.CL_FLBM);
                            foreach (double num2 in list2)
                            {
                                str3 = Convert.ToString((double) (Math.Round(num2, 3) * 100.0)) + "%";
                                if (this.cmbSlv.Items.Contains(str3))
                                {
                                    this.cmbSlv.Text = str3;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        this.cmbSlv.SelectedIndexChanged -= new EventHandler(this.cmbSlv_SelectedIndexChanged);
                        if (this.CL_SPFL_ZZSTSGL.Trim() != "")
                        {
                            List<double> list = ldal.GET_YHZCSLV_BY_YHZCMC(this.CL_SPFL_ZZSTSGL);
                            if (list.Count > 0)
                            {
                                double sLV = list[0];
                                SaleBillCheck check = new SaleBillCheck();
                                if (check.CheckTaxRate(InvType.vehiclesales, false, sLV, this.CL_XSYH, this.CL_CLBM))
                                {
                                    if (sLV == 0.0)
                                    {
                                        this.CL_LSLVBS = PresentinvMng.GetLSLVBSByYHZCMC(this.CL_SPFL_ZZSTSGL);
                                    }
                                    this._fpxx.SetFpSLv(sLV.ToString());
                                    this.ToModel();
                                    this.ToView(true);
                                }
                            }
                            else
                            {
                                list2 = ldal.GET_YHZCSYSLV_BY_FLBM(this.CL_FLBM);
                                foreach (double num2 in list2)
                                {
                                    str3 = Convert.ToString((double) (Math.Round(num2, 3) * 100.0)) + "%";
                                    if (this.cmbSlv.Items.Contains(str3))
                                    {
                                        this.cmbSlv.Text = str3;
                                        break;
                                    }
                                }
                            }
                        }
                        this.cmbSlv.SelectedIndexChanged += new EventHandler(this.cmbSlv_SelectedIndexChanged);
                    }
                }
                this.RegTextChangedEvent();
            }
        }

        private void SetCmbLabel()
        {
            this.cmbGhdw.set_Edit(0);
            this.cmbGhdwsh.set_Edit(0);
            this.cmbSfzh.set_Edit(0);
            this.cmbCllx.set_Edit(0);
        }

        private void SetFpText()
        {
            this._fpxx.set_Gfmc(this.cmbGhdw.Text.Trim());
            this._fpxx.set_Gfsh(this.cmbGhdwsh.Text.Trim());
            this._fpxx.set_Sfzh_zzjgdm(this.cmbSfzh.Text.Trim());
            this._fpxx.set_Cllx(this.cmbCllx.Text.Trim());
            this._fpxx.set_Cpxh(this.txtCpxh.Text.Trim());
            this._fpxx.set_Cd(this.txtCd.Text.Trim());
            this._fpxx.set_Hgzh(this.txtHgzh.Text.Trim());
            this._fpxx.set_Jkzmsh(this.txtJkzmh.Text.Trim());
            this._fpxx.set_Sjdh(this.txtSjdh.Text.Trim());
            this._fpxx.set_Fdjhm(this.cmbFdjh.Text.Trim());
            this._fpxx.set_Clsbdh_cjhm(this.txtClsbh.Text.Trim());
            this._fpxx.set_Sccjmc(this.txtCjmc.Text);
            this._fpxx.set_Dh(this.txtDh.Text.Trim());
            this._fpxx.set_Dz(this.txtDz.Text.Trim());
            this._fpxx.set_Zh(this.txtZh.Text.Trim());
            this._fpxx.set_Khyh(this.txtKhyh.Text);
            this._fpxx.set_Dw(this.txtDw.Text.Trim());
            this._fpxx.set_Xcrs(this.txtXcrs.Text.Trim());
        }

        private void SetGhfCmb(AisinoMultiCombox aisinoCmb, string showText)
        {
            aisinoCmb.set_IsSelectAll(true);
            aisinoCmb.set_buttonStyle(0);
            aisinoCmb.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", 100));
            aisinoCmb.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 100));
            aisinoCmb.get_Columns().Add(new AisinoMultiCombox.AisinoComboxColumn("", "IDCOC", 100));
            aisinoCmb.set_ShowText(showText);
            aisinoCmb.set_DrawHead(false);
            aisinoCmb.set_AutoIndex(0);
            aisinoCmb.add_OnButtonClick(new EventHandler(this.cmbxx_OnButtonClick));
            aisinoCmb.add_OnAutoComplate(new EventHandler(this.cmbxx_OnAutoComplate));
            aisinoCmb.add_OnSelectValue(new EventHandler(this.cmbxx_OnSelectValue));
            aisinoCmb.set_AutoComplate(2);
        }

        private void SetHzxx()
        {
            double result = 0.0;
            double.TryParse(PresentinvMng.GetSLValue(this.cmbSlv.Text), out result);
            this.bill.SLV = result;
            string s = this.bill.JEHJ.ToString();
            this.txtJsxx.Text = s;
            this.lblJsdx.Text = (s == "0.00") ? "零" : ToolUtil.RMBToDaXie(decimal.Parse(s));
            this.lblJgxx.Text = "￥" + Math.Round((double) (this.bill.JEHJ / (1.0 + this.bill.SLV)), 2).ToString("0.00");
            this.lblSe.Text = "￥" + Math.Round((double) ((this.bill.JEHJ * this.bill.SLV) / (1.0 + this.bill.SLV)), 2).ToString("0.00");
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
                this.txtCpxh.BorderStyle = BorderStyle.None;
                this.txtCd.BorderStyle = BorderStyle.None;
                this.txtHgzh.BorderStyle = BorderStyle.None;
                this.txtJkzmh.BorderStyle = BorderStyle.None;
                this.txtSjdh.BorderStyle = BorderStyle.None;
                this.txtClsbh.BorderStyle = BorderStyle.None;
                this.txtCjmc.BorderStyle = BorderStyle.None;
                this.txtDz.BorderStyle = BorderStyle.None;
                this.txtKhyh.BorderStyle = BorderStyle.None;
                this.txtZh.BorderStyle = BorderStyle.None;
                this.txtDh.BorderStyle = BorderStyle.None;
                this.txtJsxx.BorderStyle = BorderStyle.None;
                this.txtXcrs.BorderStyle = BorderStyle.None;
                this.txtDw.BorderStyle = BorderStyle.None;
            }
        }

        private void SetYHZCContent(string comboSlv_Text)
        {
            string str2;
            string str3;
            string str4;
            string sLValue = PresentinvMng.GetSLValue(comboSlv_Text);
            double result = -1.0;
            bool flag = double.TryParse(sLValue, out result);
            if (flag && (result > 1.0))
            {
                result /= 100.0;
            }
            this.CL_LSLVBS = "";
            if (!flag)
            {
                str2 = "出口零税";
                str3 = "免税";
                str4 = "不征税";
                if (comboSlv_Text.Contains(str3))
                {
                    this.CL_XSYH = true;
                    this.CL_LSLVBS = "1";
                }
                else if (comboSlv_Text.Contains(str4))
                {
                    this.CL_XSYH = true;
                    this.CL_LSLVBS = "2";
                }
                else if (this.CL_SPFL_ZZSTSGL.Contains(str2))
                {
                    this.CL_LSLVBS = "0";
                    this.CL_XSYH = true;
                }
                else
                {
                    this.CL_LSLVBS = "3";
                    this.CL_XSYH = false;
                }
            }
            else if (result == 0.0)
            {
                str2 = "出口零税";
                str3 = "免税";
                str4 = "不征税";
                if (comboSlv_Text.Contains(str3))
                {
                    this.CL_XSYH = true;
                    this.CL_LSLVBS = "1";
                }
                else if (comboSlv_Text.Contains(str4))
                {
                    this.CL_XSYH = true;
                    this.CL_LSLVBS = "2";
                }
                else if (this.CL_SPFL_ZZSTSGL.Contains(str2))
                {
                    this.CL_LSLVBS = "0";
                    this.CL_XSYH = true;
                }
                else
                {
                    this.CL_LSLVBS = "3";
                }
            }
            else
            {
                List<double> list = new List<double>();
                string[] strArray = this.CL_SPFL_ZZSTSGL.Split(new char[] { '、' });
                foreach (string str5 in strArray)
                {
                    List<double> list2 = new SaleBillDAL().GET_YHZCSLV_BY_YHZCMC(str5);
                    foreach (double num2 in list2)
                    {
                        bool flag2 = false;
                        foreach (double num3 in list)
                        {
                            if (num3 == num2)
                            {
                                flag2 = true;
                            }
                        }
                        if (!flag2)
                        {
                            list.Add(num2);
                        }
                    }
                }
                if (list.Count > 0)
                {
                    bool flag3 = false;
                    foreach (double num4 in list)
                    {
                        if (num4 == result)
                        {
                            flag3 = true;
                            break;
                        }
                    }
                    this.CL_XSYH = flag3;
                }
            }
        }

        private void ShowCopyMainInfo()
        {
            this.GetQyLabel();
            this.GetFPText();
            if (this.cmbSlv.Items.Count > 0)
            {
            }
            this.SetHzxx();
        }

        private void ShowInfo(Fpxx fp, string zfbz)
        {
            byte[] sourceArray = Invoice.get_TypeByte();
            byte[] destinationArray = new byte[0x20];
            Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
            byte[] buffer3 = new byte[0x10];
            Array.Copy(sourceArray, 0x20, buffer3, 0, 0x10);
            byte[] buffer4 = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes("KP" + DateTime.Now.ToString("F")), destinationArray, buffer3);
            Invoice.set_IsGfSqdFp_Static(false);
            this._fpxx = new Invoice(false, fp, buffer4, null);
            this.GetTaxLabel();
            this.GetQyLabel();
            this.GetFPText();
            DateTime time = new DateTime(0x7dd, 9, 10, 8, 0x22, 30);
            TimeSpan span = (TimeSpan) (DateTime.Now - time);
            byte[] buffer5 = AES_Crypt.Encrypt(ToolUtil.GetBytes(span.TotalSeconds.ToString("F1")), new byte[] { 
                0xff, 0x42, 0xae, 0x95, 11, 0x51, 0xca, 0x15, 0x21, 140, 0x4f, 170, 220, 0x92, 170, 0xed, 
                0xfd, 0xeb, 0x4e, 13, 0xac, 0x80, 0x52, 0xff, 0x45, 0x90, 0x85, 0xca, 0xcb, 0x9f, 0xaf, 0xbd
             }, new byte[] { 0xf2, 0x1f, 0xac, 0x5b, 0x2c, 0xc0, 0xa9, 0xd0, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 });
            fp.Get_Print_Dj(null, 0, buffer5);
            this.cmbSlv.DropDownStyle = ComboBoxStyle.DropDown;
            this.SetHzxx();
            bool flag = zfbz.Equals("1");
        }

        private void ShowInitInvInfo()
        {
            this.DelTextChangedEvent();
            this.GetTaxLabel();
            this.GetQyLabel();
            this.txtDz.Text = this._fpxx.get_Dz();
            this.txtDh.Text = this._fpxx.get_Dh();
            this.txtZh.Text = this._fpxx.get_Zh();
            this.txtKhyh.Text = this._fpxx.get_Khyh();
            this.lblJsdx.Text = "零";
            this.lblSe.Text = "￥0.00";
            this.RegTextChangedEvent();
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
                str3 = str3.TrimEnd(new char[] { ',' });
                MessageManager.ShowMsgBox("INP-242176", new string[] { "新机动车发票", "旧机动车发票", str3 });
            }
        }

        private void ToModel()
        {
            this.bill.BH = this.txtDJH.Text.Trim();
            this.bill.GFMC = this.cmbGhdw.Text.Trim();
            this.bill.GFSH = this.cmbSfzh.Text.Trim();
            this.bill.GFDZDH = this.cmbCllx.Text.Trim();
            this.bill.XFDZ = this.txtCpxh.Text.Trim();
            this.bill.KHYHMC = this.txtCd.Text.Trim();
            this.bill.SCCJMC = this.txtCjmc.Text.Trim();
            this.bill.CM = this.txtHgzh.Text.Trim();
            this.bill.TYDH = this.txtJkzmh.Text.Trim();
            this.bill.QYD = this.txtSjdh.Text.Trim();
            this.bill.ZHD = this.cmbFdjh.Text.Trim();
            this.bill.XHD = this.txtClsbh.Text.Trim();
            this.bill.XFDH = this.txtDh.Text.Trim();
            this.bill.KHYHZH = this.txtZh.Text.Trim();
            this.bill.XFDZDH = this.txtDz.Text.Trim();
            this.bill.XFYHZH = this.txtKhyh.Text.Trim();
            this.bill.DW = this.txtDw.Text.Trim();
            this.bill.MDD = this.txtXcrs.Text.Trim();
            this.bill.DJRQ = this.dateTimePicker1.Value.Date;
            if (this.bill.IsANew)
            {
                this.bill.DJYF = this.bill.DJRQ.Month;
            }
            this.bill.SLV = CommonTool.Todouble(this._fpxx.get_SLv());
            this.bill.JEHJ = CommonTool.Todouble(this._fpxx.GetHjJeHs());
            this.bill.BZ = this.txt_bz.Text.Trim();
            this.bill.GFYHZH = this.cmbGhdwsh.Text.Trim();
            this.bill.DJZL = "j";
            if (CommonTool.isSPBMVersion())
            {
                if (!this.CL_XSYH)
                {
                    this.CL_SPFL_ZZSTSGL = "";
                }
                this.bill.JDC_FLBM = this.CL_FLBM;
                this.bill.JDC_XSYH = this.CL_XSYH;
                this.bill.JDC_XSYHSM = this.CL_SPFL_ZZSTSGL;
                this.bill.JDC_CLBM = this.CL_CLBM;
                this.bill.JDC_LSLVBS = this.CL_LSLVBS;
            }
        }

        private void tool_close_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        private void tool_kehu_Click(object sender, EventArgs e)
        {
            object[] objArray = new object[] { this.cmbGhdw.Text, this.cmbGhdwsh.Text, this.cmbSfzh.Text };
            ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddGHDW", objArray);
        }

        private void tool_print_MouseDown(object sender, MouseEventArgs e)
        {
            this.lblTitle.Focus();
        }

        private void toolStripBtnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void toolStripBtnSave_Click(object sender, EventArgs e)
        {
            this.SaveFpData();
        }

        private void ToView(bool ShowSlv = true)
        {
            try
            {
                this.cmbGhdwsh.OnTextChanged = (EventHandler) Delegate.Remove(this.cmbGhdwsh.OnTextChanged, new EventHandler(this.cmbGhdwsh_TextChanged));
                this.txtClsbh.TextChanged -= new EventHandler(this.txtClsbh_TextChanged);
                this._fpxx.set_Sfzh_zzjgdm(this.cmbSfzh.Text.Trim());
                this._fpxx.set_Cllx(this.cmbCllx.Text.Trim());
                this._fpxx.set_Cpxh(this.txtCpxh.Text.Trim());
                this._fpxx.set_Cd(this.txtCd.Text.Trim());
                this._fpxx.set_Hgzh(this.txtHgzh.Text.Trim());
                this._fpxx.set_Jkzmsh(this.txtJkzmh.Text.Trim());
                this._fpxx.set_Sjdh(this.txtSjdh.Text.Trim());
                this._fpxx.set_Fdjhm(this.cmbFdjh.Text.Trim());
                this._fpxx.set_Clsbdh_cjhm(this.txtClsbh.Text.Trim());
                this._fpxx.set_Sccjmc(this.txtCjmc.Text);
                this._fpxx.set_Dh(this.txtDh.Text.Trim());
                this._fpxx.set_Dz(this.txtDz.Text.Trim());
                this._fpxx.set_Zh(this.txtZh.Text.Trim());
                this._fpxx.set_Khyh(this.txtKhyh.Text);
                this._fpxx.set_Dw(this.txtDw.Text.Trim());
                this._fpxx.set_Xcrs(this.txtXcrs.Text.Trim());
                this.txtDJH.Text = this.bill.BH;
                this.cmbGhdw.Text = this.bill.GFMC;
                this.cmbSfzh.Text = this.bill.GFSH;
                this.cmbCllx.Text = this.bill.GFDZDH;
                this.txtCpxh.Text = this.bill.XFDZ;
                this.txtCd.Text = this.bill.KHYHMC;
                this.txtCjmc.Text = this.bill.SCCJMC;
                this.txtHgzh.Text = this.bill.CM;
                this.txtJkzmh.Text = this.bill.TYDH;
                this.txtSjdh.Text = this.bill.QYD;
                this.cmbFdjh.Text = this.bill.ZHD;
                this.txtClsbh.Text = this.bill.XHD;
                this.txtDh.Text = this.bill.XFDH;
                this.txtZh.Text = this.bill.KHYHZH;
                this.txtDz.Text = this.bill.XFDZDH;
                this.txtKhyh.Text = this.bill.XFYHZH;
                this.txtDw.Text = this.bill.DW;
                this.txtXcrs.Text = this.bill.MDD;
                this.dateTimePicker1.Value = this.bill.DJRQ;
                if (ShowSlv)
                {
                    string slvStr = this.bill.SLV.ToString();
                    string sqSLv = this._fpxx.GetSqSLv();
                    SLV slv = PresentinvMng.GetSlvList(12, slvStr)[0];
                    if (!sqSLv.Contains(slvStr))
                    {
                        List<SLV> slvList = PresentinvMng.GetSlvList(12, slvStr);
                        this.cmbSlv.Items.Add(slvList[0].get_ComboxValue());
                    }
                    this.cmbSlv.SelectedItem = slv.get_ComboxValue();
                }
                this.txt_bz.Text = this.bill.BZ;
                this.cmbGhdwsh.Text = this.bill.GFYHZH;
                string str3 = this._fpxx.get_SLv();
                this.txtJsxx.Text = Finacial.GetRound(this.bill.JEHJ, 2).ToString("0.00");
                bool flag = this._fpxx.SetFpSLv(this.bill.SLV.ToString());
                string code = this._fpxx.GetCode();
                bool flag2 = this._fpxx.SetJshj(this.txtJsxx.Text.Trim());
                code = this._fpxx.GetCode();
                if (true)
                {
                    this.SetHzxx();
                }
                this.lblFphm.Text = ShowString.ShowKPZT(this.bill.KPZT);
                this.lblJqbh.Text = ShowString.ShowDJZT(this.bill.DJZT);
                if (CommonTool.isSPBMVersion() && ShowSlv)
                {
                    this.CL_XSYH = this.bill.JDC_XSYH;
                    this.CL_FLBM = this.bill.JDC_FLBM;
                    this.CL_LSLVBS = this.bill.JDC_LSLVBS;
                    this.CL_CLBM = this.bill.JDC_CLBM;
                    this.CL_SPFL_ZZSTSGL = this.bill.JDC_XSYHSM;
                    this.CL_CPXXName = this.bill.JDC_CPXH;
                    this.CL_CLLXName = this.bill.JDC_LX;
                    this.FillComboxSLV();
                    if (Finacial.Equal(this.bill.SLV, 0.0))
                    {
                        if (this.CL_XSYH)
                        {
                            string str5 = "免税";
                            string str6 = "不征税";
                            if (this.CL_LSLVBS == "0")
                            {
                                this.cmbSlv.Text = "0%";
                            }
                            else if (this.CL_LSLVBS == "1")
                            {
                                this.cmbSlv.Text = str5;
                            }
                            else if (this.CL_LSLVBS == "2")
                            {
                                this.cmbSlv.Text = str6;
                            }
                            else if (this.CL_LSLVBS == "3")
                            {
                                this.cmbSlv.Text = "0%";
                            }
                            else
                            {
                                this.cmbSlv.Text = "0%";
                            }
                        }
                        else
                        {
                            this.cmbSlv.Text = "0%";
                        }
                    }
                    else
                    {
                        string str7 = Convert.ToString((double) (Math.Round(this.bill.SLV, 3) * 100.0)) + "%";
                        if (!this.cmbSlv.Items.Contains(str7))
                        {
                            this.cmbSlv.Items.Add(str7);
                        }
                        this.cmbSlv.Text = str7;
                    }
                }
            }
            catch
            {
            }
            finally
            {
                this.cmbGhdwsh.OnTextChanged = (EventHandler) Delegate.Combine(this.cmbGhdwsh.OnTextChanged, new EventHandler(this.cmbGhdwsh_TextChanged));
                this.txtClsbh.TextChanged += new EventHandler(this.txtClsbh_TextChanged);
            }
        }

        private void txtCd_TextChanged(object sender, EventArgs e)
        {
            string str = this.txtCd.Text.Trim();
            this._fpxx.set_Cd(str);
            if (this._fpxx.get_Cd() != str)
            {
                this.txtCd.Text = this._fpxx.get_Cd();
                this.txtCd.SelectionStart = this.txtCd.Text.Length;
            }
        }

        private void txtCjmc_TextChanged(object sender, EventArgs e)
        {
            string str = this.txtCjmc.Text.Trim();
            this._fpxx.set_Sccjmc(str);
            if (this._fpxx.get_Sccjmc() != str)
            {
                this.txtCjmc.Text = this._fpxx.get_Sccjmc();
                this.txtCjmc.SelectionStart = this.txtCjmc.Text.Length;
            }
        }

        private void txtClsbh_TextChanged(object sender, EventArgs e)
        {
            string text = this.txtClsbh.Text;
            this._fpxx.set_Clsbdh_cjhm(text);
            if (this._fpxx.get_Clsbdh_cjhm() != text)
            {
                this.txtClsbh.Text = this._fpxx.get_Clsbdh_cjhm();
                this.txtClsbh.SelectionStart = this.txtClsbh.Text.Length;
            }
        }

        private void txtCpxh_Leave(object sender, EventArgs e)
        {
        }

        private void txtCpxh_TextChanged(object sender, EventArgs e)
        {
            string str = this.txtCpxh.Text.Trim();
            this._fpxx.set_Cpxh(str);
            if (this._fpxx.get_Cpxh() != str)
            {
                this.txtCpxh.Text = this._fpxx.get_Cpxh();
                this.txtCpxh.SelectionStart = this.txtCpxh.Text.Length;
            }
        }

        private void txtDh_TextChanged(object sender, EventArgs e)
        {
            string str = this.txtDh.Text.Trim();
            this._fpxx.set_Dh(str);
            if (this._fpxx.get_Dh() != str)
            {
                this.txtDh.Text = this._fpxx.get_Dh();
                this.txtDh.SelectionStart = this.txtDh.Text.Length;
            }
        }

        private void txtDw_TextChanged(object sender, EventArgs e)
        {
            string str = this.txtDw.Text.Trim();
            this._fpxx.set_Dw(str);
            if (this._fpxx.get_Dw() != str)
            {
                this.txtDw.Text = this._fpxx.get_Dw();
                this.txtDw.SelectionStart = this.txtDw.Text.Length;
            }
        }

        private void txtDz_TextChanged(object sender, EventArgs e)
        {
            string str = this.txtDz.Text.Trim();
            this._fpxx.set_Dz(str);
            if (this._fpxx.get_Dz() != str)
            {
                this.txtDz.Text = this._fpxx.get_Dz();
                this.txtDz.SelectionStart = this.txtDz.Text.Length;
            }
        }

        private void txtHgzh_TextChanged(object sender, EventArgs e)
        {
            string str = this.txtHgzh.Text.Trim();
            this._fpxx.set_Hgzh(str);
            if (this._fpxx.get_Hgzh() != str)
            {
                this.txtHgzh.Text = this._fpxx.get_Hgzh();
                this.txtHgzh.SelectionStart = this.txtHgzh.Text.Length;
            }
        }

        private void txtJkzmh_TextChanged(object sender, EventArgs e)
        {
            string str = this.txtJkzmh.Text.Trim();
            this._fpxx.set_Jkzmsh(str);
            if (this._fpxx.get_Jkzmsh() != str)
            {
                this.txtJkzmh.Text = this._fpxx.get_Jkzmsh();
                this.txtJkzmh.SelectionStart = this.txtJkzmh.Text.Length;
            }
        }

        private void txtJsxx_LostFocus(object sender, EventArgs e)
        {
            double result = 0.0;
            double.TryParse(PresentinvMng.GetSLValue(this.cmbSlv.Text), out result);
            this._fpxx.SetFpSLv(result.ToString());
            bool flag = this._fpxx.SetJshj(this.txtJsxx.Text.Trim());
            double num2 = 0.0;
            double.TryParse(this.txtJsxx.Text, out num2);
            this.bill.JEHJ = num2;
            if (flag)
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
            string str = this.txtKhyh.Text.Trim();
            this._fpxx.set_Khyh(str);
            if (this._fpxx.get_Khyh() != str)
            {
                this.txtKhyh.Text = this._fpxx.get_Khyh();
                this.txtKhyh.SelectionStart = this.txtKhyh.Text.Length;
            }
        }

        private void txtSjdh_TextChanged(object sender, EventArgs e)
        {
            string str = this.txtSjdh.Text.Trim();
            this._fpxx.set_Sjdh(str);
            if (this._fpxx.get_Sjdh() != str)
            {
                this.txtSjdh.Text = this._fpxx.get_Sjdh();
                this.txtSjdh.SelectionStart = this.txtSjdh.Text.Length;
            }
        }

        private void txtXcrs_TextChanged(object sender, EventArgs e)
        {
            string str = this.txtXcrs.Text.Trim();
            this._fpxx.set_Xcrs(str);
            if (this._fpxx.get_Xcrs() != str)
            {
                this.txtXcrs.Text = this._fpxx.get_Xcrs();
                this.txtXcrs.SelectionStart = this.txtXcrs.Text.Length;
            }
        }

        private void txtZh_TextChanged(object sender, EventArgs e)
        {
            string str = this.txtZh.Text.Trim();
            this._fpxx.set_Zh(str);
            if (this._fpxx.get_Zh() != str)
            {
                this.txtZh.Text = this._fpxx.get_Zh();
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

