namespace Aisino.Fwkp.Hzfp.Form
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
    using Aisino.Fwkp.Hzfp.Common;
    using Aisino.Fwkp.Hzfp.IBLL;
    using Aisino.Fwkp.Hzfp.Model;
    using Aisino.Fwkp.Print;
    using Factory;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    public class SqdTianKai : DockForm
    {
        private bool blSplite;
        private decimal blueje = -1M;
        private AisinoBTN btn_addrow;
        private AisinoBTN btn_delrow;
        private Button btnSplit;
        private AisinoPNL BuyerPan;
        private DataGridViewTextBoxEditingControl CellEdit;
        private List<codeInfo> codeInfoList = new List<codeInfo>();
        private AisinoMultiCombox com_gfmc;
        private AisinoMultiCombox com_gfsbh;
        private AisinoMultiCombox com_xfmc;
        private AisinoMultiCombox com_xfsbh;
        private IContainer components;
        private CustomStyleDataGrid dataGridView1;
        public static bool FLBMqy = (TaxCardFactory.CreateTaxCard().GetExtandParams("FLBMFlag") == "FLBM");
        private string fpdm;
        private string fphm;
        private FPLX fplx;
        private bool hsjbz;
        private bool hysy_flag;
        private bool hzfw = true;
        public bool InitSqdMxType_Edit;
        public bool InitSqdMxType_Read;
        public Invoice inv;
        public static bool isCes = (TaxCardFactory.CreateTaxCard().GetExtandParams("CEBTVisble") == "1");
        public static bool isFLBM = ((TaxCardFactory.CreateTaxCard().GetExtandParams("FLBMFlag") == "FLBM") && lpFLBM);
        private AisinoLBL lab_date;
        private AisinoLBL lab_fpdm;
        private AisinoLBL lab_fphm;
        private AisinoLBL lab_fpzl;
        private AisinoLBL lab_je;
        private AisinoLBL lab_kpy;
        private AisinoLBL lab_No;
        private AisinoLBL lab_se;
        public static bool lp_error = false;
        private int lp_hysy;
        public static int lp_mxchao78 = 0;
        public static string lpbmbbbh = string.Empty;
        private static bool lpFLBM = true;
        public static bool noslv = false;
        public static string oldsh = TaxCardFactory.CreateTaxCard().OldTaxCode.Trim();
        private AisinoPNL pnlGrid;
        private AisinoPNL pnlHjje;
        private AisinoPNL pnlSqly;
        private AisinoRDO rad_hzfw_n;
        private AisinoRDO rad_hzfw_y;
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
        private List<object> SelectInfor = new List<object>();
        private AisinoPNL SellerPan;
        internal AisinoLST slvList;
        private AisinoMultiCombox spmcBt;
        private readonly IHZFP_SQD sqdDal = BLLFactory.CreateInstant<IHZFP_SQD>("HZFP_SQD");
        private readonly IHZFP_SQD_MX sqdMxDal = BLLFactory.CreateInstant<IHZFP_SQD_MX>("HZFP_SQD_MX");
        private InitSqdMxType SqdMxType = InitSqdMxType.Edit;
        public string tempfirstslv = string.Empty;
        private ToolStripButton tool_AddGood;
        private ToolStripButton tool_addRow;
        private ToolStripButton tool_bianji;
        private ToolStripButton tool_chae;
        private ToolStripButton tool_daying;
        private ToolStripButton tool_DeleteRow;
        private ToolStripButton tool_geshi;
        private ToolStripButton tool_hanshuiqiehuan;
        private ToolStripButton tool_insertRow;
        private ToolStripButton tool_tongji;
        private ToolStripButton tool_tuichu;
        private ToolStrip toolStrip2;
        private ToolStripSeparator toolStripSeparator;
        private AisinoTXT txt_lxdh;
        private AisinoTXT txt_sqly;
        public static int xiugai_no = 0;
        private XmlComponentLoader xmlComponentLoader1;
        private const string XT = "稀土";

        public SqdTianKai()
        {
            this.Initialize();
            this.com_gfmc.Edit=(EditStyle)1;
            this.com_gfsbh.Edit=(EditStyle)1;
            this.com_xfmc.Edit=(EditStyle)1;
            this.com_xfsbh.Edit=(EditStyle)1;
            this.spmcBt = new AisinoMultiCombox();
            this.spmcBt.IsSelectAll=(true);
            this.spmcBt.Name = "SPMCBT";
            this.spmcBt.Text = "";
            this.spmcBt.MaxLength=0x5c;
            this.spmcBt.Padding = new Padding(0);
            this.spmcBt.Margin = new Padding(0);
            this.dataGridView1.Controls.Add(this.spmcBt);
            this.spmcBt.Visible = false;
            this.spmcBt.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("货物（劳务 服务）名称", "MC", 270));
            this.spmcBt.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("计量单位", "JLDW"));
            this.spmcBt.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("税率", "SLV"));
            this.spmcBt.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("单价", "DJ"));
            this.spmcBt.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("含税价标志", "HSJBZSTR"));
            this.spmcBt.ShowText="MC";
            this.spmcBt.DrawHead=(true);
            this.spmcBt.AutoIndex=(0);
            this.spmcBt.AutoComplate=(AutoComplateStyle)2;
            this.spmcBt.OnAutoComplate+=(new EventHandler(this.spmcBt_OnAutoComplate));
            this.spmcBt.buttonStyle=(0);
            this.spmcBt.OnButtonClick+=(new EventHandler(this.spmcBt_Click));
            this.spmcBt.DoubleClick += new EventHandler(this.spmcBt_Click);
            this.spmcBt.OnTextChanged = (EventHandler)Delegate.Combine(this.spmcBt.OnTextChanged, new EventHandler(this.spmcBt_TextChanged));
            this.spmcBt.OnSelectValue+=(new EventHandler(this.spmcBt_OnSelectValue));
            this.spmcBt.PreviewKeyDown += new PreviewKeyDownEventHandler(this.spmcBt_PreviewKeyDown);
            this.spmcBt.MouseDoubleClick += new MouseEventHandler(this.spmcBt_MouseDoubleClick);
            this.spmcBt.Leave += new EventHandler(this.spmcBt_leave);
            this.dataGridView1.Columns["MC"].ReadOnly = true;
            this.slvList = new AisinoLST();
            this.slvList.Name = "SLVLIST";
            this.slvList.Items.AddRange(this.GetSqSlv().ToArray());
            this.slvList.ScrollAlwaysVisible = false;
            this.dataGridView1.Controls.Add(this.slvList);
            this.slvList.Visible = false;
            this.slvList.Click += new EventHandler(this.slvList_Click);
            this.slvList.PreviewKeyDown += new PreviewKeyDownEventHandler(this.slvList_PreviewKeyDown);
            if (!isCes)
            {
                this.tool_chae.Visible = false;
            }
            this.dataGridView1.MultiSelect = false;
        }

        protected DataTable _SpmcOnAutoCompleteDataSource(CustomStyleDataGrid dataGrid, string spmc)
        {
            double num;
            if ((this.inv.GetSpxxs().Count > 0) && ((int)(int)this.inv.Zyfplx == 1))
            {
                num = double.Parse(this.inv.SLv);
            }
            else if ((this.inv.SupportMulti || ((this.inv.SLv.Trim() == "") && (this.inv.GetSpxxs().Count > 0))) || ((this.inv.GetSpxxs().Count == 1) && (this.blueje <= -1M)))
            {
                num = -1.0;
            }
            else
            {
                num = double.Parse(this.inv.SLv);
            }
            string str = "";
            if ((int)(int)this.inv.Zyfplx == 1)
            {
                str = "HYSY";
            }
            else if ((int)(int)this.inv.Zyfplx == 10)
            {
                str = "OPF";
            }
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSPMore", new object[] { spmc, 20, "", num, str, "1" });
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
            int num = -1;
            if (this.inv.IsGfSqdFp)
            {
                if (this.rad_hzfw_y.Checked)
                {
                    this.inv.IsXfHzfw=(true);
                }
                if (this.rad_hzfw_n.Checked)
                {
                    this.inv.IsXfHzfw=(false);
                }
            }
            if (!this.inv.CanAddSpxx(1, false))
            {
                MessageManager.ShowMsgBox(this.inv.GetCode(), this.inv.Params);
                return num;
            }
            string str = "0";
            if (this.tool_chae.Checked)
            {
                ChaE_Tax tax = new ChaE_Tax();
                if (tax.ShowDialog() != DialogResult.OK)
                {
                    return num;
                }
                str = tax.kce.ToString("F2");
            }
            num = this.inv.AddSpxx(spxx);
            if (num != -1)
            {
                num = this.dataGridView1.Rows.Add();
                if (this.dataGridView1.Rows.Count > 1)
                {
                    if (spxx.SLv.Trim() == "")
                    {
                        this.dataGridView1.Rows[num].Cells["SLV"].Value = "";
                    }
                    else
                    {
                        double num2 = Convert.ToDouble(spxx.SLv);
                        if (num2 == 0.0)
                        {
                            this.dataGridView1.Rows[num].Cells["SLV"].Value = "免税";
                        }
                        else if ((num2 == 0.05) && ((int)(int)this.inv.Zyfplx == 1))
                        {
                            this.dataGridView1.Rows[num].Cells["SLV"].Value = "中外合作油气田";
                        }
                        else if (num2 == 0.015)
                        {
                            this.dataGridView1.Rows[num].Cells["SLV"].Value = "减按1.5%计算";
                        }
                        else
                        {
                            this.dataGridView1.Rows[num].Cells["SLV"].Value = (Convert.ToDouble(spxx.SLv) * 100.0) + "%";
                        }
                    }
                }
                else
                {
                    if (spxx.SLv.Trim() == "")
                    {
                        this.dataGridView1.Rows[num].Cells["SLV"].Value = "";
                    }
                    else
                    {
                        double num3 = Convert.ToDouble(spxx.SLv);
                        if (num3 == 0.0)
                        {
                            this.dataGridView1.Rows[num].Cells["SLV"].Value = "免税";
                        }
                        else if ((num3 == 0.05) && ((int)(int)this.inv.Zyfplx == 1))
                        {
                            this.dataGridView1.Rows[num].Cells["SLV"].Value = "";
                        }
                        else if (num3 == 0.015)
                        {
                            this.dataGridView1.Rows[num].Cells["SLV"].Value = "减按1.5%计算";
                        }
                        else
                        {
                            this.dataGridView1.Rows[num].Cells["SLV"].Value = (Convert.ToDouble(spxx.SLv) * 100.0) + "%";
                        }
                    }
                    int rowCount = this.dataGridView1.RowCount;
                }
                this.dataGridView1.EndEdit();
                this.lab_je.Text = "￥" + this.inv.GetHjJe();
                this.lab_se.Text = "￥" + this.inv.GetHjSe();
                if (isFLBM)
                {
                    this.codeInfoList.Add(new codeInfo());
                }
                if (this.tool_chae.Checked)
                {
                    this.inv.SetKce(num, str);
                }
                return num;
            }
            MessageManager.ShowMsgBox(this.inv.GetCode(), this.inv.Params);
            return num;
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            if (!this.blSplite)
            {
                this.pnlSqly.Height = this.pnlHjje.Height;
                this.btnSplit.Top = (this.pnlSqly.Top - this.btnSplit.Height) + 3;
                this.pnlGrid.Height = this.btnSplit.Top - this.pnlGrid.Top;
                this.btnSplit.Text = "▲";
                this.blSplite = true;
            }
            else
            {
                this.pnlSqly.Height = 0x111;
                this.btnSplit.Top = (this.pnlSqly.Top - this.btnSplit.Height) + 3;
                this.pnlGrid.Height = this.btnSplit.Top - this.pnlGrid.Top;
                this.btnSplit.Text = "▼";
                this.blSplite = false;
            }
        }

        private bool CheckSLDW(string sl, string dw)
        {
            bool flag = string.IsNullOrEmpty(sl);
            bool flag2 = string.IsNullOrEmpty(dw);
            if ((flag2 && !flag) || (flag && !flag2))
            {
                MessageManager.ShowMsgBox("INP-431326");
                return false;
            }
            if (!(sl == "0") && (!(dw != "公斤") || !(dw != "吨")))
            {
                return true;
            }
            MessageManager.ShowMsgBox("INP-431327");
            return false;
        }

        private bool CheckStringChinese(string text)
        {
            bool flag = true;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] < '\x007f')
                {
                    flag = false;
                }
            }
            return flag;
        }

        private bool CheckXTSp()
        {
            if (this.IsXTQY())
            {
                int num = 0;
                bool flag = false;
                bool flag2 = false;
                string spmc = "";
                string str2 = "";
                List<string> list = new List<string>();
                foreach (Dictionary<SPXX, string> dictionary in this.inv.GetSpxxs())
                {
                    num++;
                    if (string.IsNullOrEmpty(dictionary[(SPXX)7]) || (dictionary[(SPXX)7] == "0"))
                    {
                        MessageManager.ShowMsgBox("INP-431323");
                        return false;
                    }
                    spmc = dictionary[0];
                    List<string> xTSPXXByMC = this.GetXTSPXXByMC(spmc);
                    if ((xTSPXXByMC == null) || (xTSPXXByMC.Count < 2))
                    {
                        flag2 = true;
                    }
                    str2 = dictionary[(SPXX)14];
                    if (string.IsNullOrEmpty(str2))
                    {
                        flag2 = true;
                    }
                    else
                    {
                        flag = true;
                    }
                    if (flag && flag2)
                    {
                        MessageManager.ShowMsgBox("INP-431322");
                        return false;
                    }
                    if ((flag && (dictionary[(SPXX)1].Length > 3)) && !list.Contains(dictionary[(SPXX)1].Substring(0, 3)))
                    {
                        list.Add(dictionary[(SPXX)1].Substring(0, 3));
                    }
                    if (list.Count > 1)
                    {
                        MessageManager.ShowMsgBox("INP-431322");
                        return false;
                    }
                    if (flag)
                    {
                        if (XTSP_Crypt.EncodeXTGoodsName(str2.Substring(0, 1) + dictionary[0]) != str2.Substring(1, str2.Length - 1))
                        {
                            MessageManager.ShowMsgBox("INP-431324");
                            return false;
                        }
                        bool flag3 = (dictionary[(SPXX)6] == null) || (dictionary[(SPXX)6].Trim() == "");
                        bool flag4 = (dictionary[(SPXX)4] == null) || (dictionary[(SPXX)4].Trim() == "");
                        if ((!flag3 && flag4) || (!flag4 && flag3))
                        {
                            MessageManager.ShowMsgBox("INP-431326", new string[] { num.ToString() });
                            return false;
                        }
                        if ((!flag4 && (dictionary[(SPXX)4] != "公斤")) && (dictionary[(SPXX)4] != "吨"))
                        {
                            MessageManager.ShowMsgBox("INP-431327");
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void ClearForm()
        {
            this.lab_fpzl.Text = string.Empty;
            this.lab_fpdm.Text = string.Empty;
            this.lab_fphm.Text = string.Empty;
            this.com_gfmc.Edit=(EditStyle)1;
            this.com_gfsbh.Edit=(EditStyle)1;
            this.com_gfmc.Edit=(EditStyle)1;
            this.com_gfsbh.Edit=(EditStyle)1;
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

        private void com_gf_DropDown(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = (AisinoMultiCombox)sender;
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetKH", new object[] { combox.Text, 0 });
            if ((objArray != null) && (objArray.Length == 4))
            {
                this.com_gfmc.Text = objArray[0].ToString();
                this.com_gfsbh.Text = objArray[1].ToString();
                this.inv.Gfmc=(objArray[0].ToString());
                this.inv.Gfsh=(objArray[1].ToString());
            }
        }

        private void com_gfmc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\b")
            {
                e.Handled = false;
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

        private void com_gfsbh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\b")
            {
                e.Handled = false;
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
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetKHMore", new object[] { text, 20, "MC,SH,DZDH,YHZH" });
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

        private void com_xf_DropDown(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = (AisinoMultiCombox)sender;
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetKH", new object[] { combox.Text, 0, "MC,SH,DZDH,YHZH" });
            if ((objArray != null) && (objArray.Length == 4))
            {
                this.com_xfmc.Text = objArray[0].ToString();
                this.com_xfsbh.Text = objArray[1].ToString();
                this.inv.Xfmc=(objArray[0].ToString());
                this.inv.Xfsh=(objArray[1].ToString());
            }
        }

        private void com_xfmc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\b")
            {
                e.Handled = false;
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

        private void com_xfsbh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\b")
            {
                e.Handled = false;
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
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetKHMore", new object[] { text, 20, "MC,SH,DZDH,YHZH" });
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
            CustomStyleDataGrid grid = (CustomStyleDataGrid)sender;
            this.tool_bianji.Enabled = false;
            Control control1 = grid.Controls["SPMCBT"];
            if ((grid.CurrentCell != null) && !grid.CurrentRow.ReadOnly)
            {
                DataGridViewColumn owningColumn = grid.CurrentCell.OwningColumn;
                if (owningColumn.Name.Equals("SLV"))
                {
                    int index = owningColumn.Index;
                    int rowIndex = grid.CurrentCell.RowIndex;
                    Rectangle rectangle = grid.GetCellDisplayRectangle(index, rowIndex, false);
                    this.slvList.Items.Clear();
                    if (isFLBM)
                    {
                        if (this.codeInfoList[rowIndex].sfxsyhzc.Equals("1"))
                        {
                            this.slvList.Items.AddRange(this.GetSqSyYhSlv(this.codeInfoList[rowIndex].flbm, this.codeInfoList[rowIndex].yhzcmc).ToArray());
                        }
                        else
                        {
                            this.slvList.Items.AddRange(this.GetSqSySlv(this.codeInfoList[rowIndex].flbm).ToArray());
                        }
                    }
                    else
                    {
                        this.slvList.Items.AddRange(this.GetSqSlv().ToArray());
                    }
                    if (this.slvList != null)
                    {
                        if (this.lp_hysy == 1)
                        {
                            this.slvList.Items.Clear();
                            List<string> list = new List<string> { "中外合作油气田" };
                            this.slvList.Items.AddRange(list.ToArray());
                        }
                        else if (this.lp_hysy == 4)
                        {
                            this.slvList.Items.Clear();
                            List<string> list2 = new List<string> { "减按1.5%计算" };
                            this.slvList.Items.AddRange(list2.ToArray());
                        }
                        else if (this.lp_hysy == 2)
                        {
                            this.slvList.Items.Remove("中外合作油气田");
                            this.slvList.Items.Remove("减按1.5%计算");
                        }
                        else if (this.lp_hysy == 3)
                        {
                            this.slvList.Items.Remove("中外合作油气田");
                            this.slvList.Items.Remove("减按1.5%计算");
                        }
                        this.slvList.Left = rectangle.Left;
                        this.slvList.Top = rectangle.Top + rectangle.Height;
                        this.slvList.Width = rectangle.Width;
                        if (this.slvList.Items.Count == 1)
                        {
                            this.slvList.Height = this.slvList.Items.Count * 20;
                        }
                        else
                        {
                            this.slvList.Height = this.slvList.Items.Count * 14;
                        }
                        if (this.slvList.Items.Count > 0)
                        {
                            this.slvList.Visible = true;
                        }
                        this.slvList.ClearSelected();
                        this.slvList.BringToFront();
                    }
                }
                else
                {
                    this.slvList.Visible = false;
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.spmcBt.Visible)
            {
                this.spmcBt.Visible = false;
            }
            if (this.slvList.Visible)
            {
                this.slvList.Visible = false;
            }
            this.tool_bianji.Enabled = true;
            this.tool_daying.Enabled = true;
            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;
            object obj2 = this.dataGridView1.Rows[rowIndex].Cells[columnIndex].Value;
            string s = (obj2 == null) ? "" : obj2.ToString();
            bool flag = false;
            Dictionary<SPXX, string> spxx = this.inv.GetSpxx(rowIndex);
            switch (columnIndex)
            {
                case 0:
                    flag = this.inv.SetSpmc(rowIndex, s);
                    goto Label_08C3;

                case 1:
                    flag = this.inv.SetGgxh(rowIndex, s);
                    goto Label_08C3;

                case 2:
                    flag = this.inv.SetJLdw(rowIndex, s);
                    goto Label_08C3;

                case 3:
                    {
                        double num3;
                        double num4;
                        if ((int)this.inv.Zyfplx != 11)
                        {
                            break;
                        }
                        Dictionary<SPXX, string> dictionary2 = this.inv.GetSpxx(rowIndex);
                        if ((!double.TryParse(s, out num3) || !double.TryParse(dictionary2[(SPXX)5], out num4)) || (Math.Abs((double)(num3 * num4)) >= Math.Abs(double.Parse(dictionary2[(SPXX)0x18]))))
                        {
                            break;
                        }
                        MessageManager.ShowMsgBox("INP-242185", new string[] { "金额的绝对值小于扣除额！" });
                        this.dataGridView1.Rows[rowIndex].Cells[columnIndex].Value = "";
                        this.dataGridView1.Rows[rowIndex].Cells[columnIndex + 2].Value = "";
                        this.inv.SetJe(rowIndex, "");
                        return;
                    }
                case 4:
                    {
                        double num5;
                        double num6;
                        if ((int)this.inv.Zyfplx != 11)
                        {
                            goto Label_02D9;
                        }
                        Dictionary<SPXX, string> dictionary3 = this.inv.GetSpxx(rowIndex);
                        if ((!double.TryParse(s, out num6) || !double.TryParse(dictionary3[(SPXX)6], out num5)) || (Math.Abs((double)(num5 * num6)) >= Math.Abs(double.Parse(dictionary3[(SPXX)0x18]))))
                        {
                            goto Label_02D9;
                        }
                        MessageManager.ShowMsgBox("INP-242185", new string[] { "金额的绝对值小于扣除额！" });
                        this.dataGridView1.Rows[rowIndex].Cells[columnIndex].Value = "";
                        this.dataGridView1.Rows[rowIndex].Cells[columnIndex + 1].Value = "";
                        this.inv.SetJe(rowIndex, "");
                        return;
                    }
                case 5:
                    {
                        double num7;
                        if ((int)this.inv.Zyfplx != 11)
                        {
                            goto Label_0375;
                        }
                        Dictionary<SPXX, string> dictionary4 = this.inv.GetSpxx(rowIndex);
                        if (!double.TryParse(s, out num7) || (Math.Abs(num7) >= Math.Abs(double.Parse(dictionary4[(SPXX)0x18]))))
                        {
                            goto Label_0375;
                        }
                        MessageManager.ShowMsgBox("INP-242185", new string[] { "金额的绝对值小于扣除额！" });
                        this.dataGridView1.Rows[rowIndex].Cells[columnIndex].Value = "";
                        return;
                    }
                case 6:
                    {
                        double num8;
                        bool flag2 = false;
                        if (this.slvList.Items != null)
                        {
                            foreach (object obj3 in this.slvList.Items)
                            {
                                decimal num9;
                                decimal num10;
                                decimal.TryParse(this.SlvListIndexOf(obj3.ToString()), out num9);
                                decimal.TryParse(this.SlvListIndexOf(s), out num10);
                                if (decimal.Compare(num9, num10) == 0)
                                {
                                    flag2 = true;
                                    break;
                                }
                            }
                        }
                        if (this.tool_chae.Checked && (((s.ToString() == "减按1.5%计算") || (s.ToString() == "中外合作油气田")) || (double.TryParse(s, out num8) && (num8 == 0.015))))
                        {
                            Dictionary<SPXX, string> dictionary5 = this.inv.GetSpxx(rowIndex);
                            MessageManager.ShowMsgBox("INP-242185", new string[] { "差额税不支持特殊税率商品填开！" });
                            this.dataGridView1.Rows[rowIndex].Cells[columnIndex].Value = ((double.Parse(dictionary5[(SPXX)8]) * 100.0)).ToString() + "%";
                            return;
                        }
                        string yhzc = spxx[(SPXX)0x16];
                        if (this.SlvListIndexOf(s) == "err")
                        {
                            flag = this.inv.SetSLv(rowIndex, s);
                        }
                        else if (s.Trim() == "中外合作油气田")
                        {
                            if ((this.lp_hysy != 2) && (this.lp_hysy != 3))
                            {
                                if (this.inv.SetZyfpLx((ZYFP_LX)1))
                                {
                                    this.SetHysy(true);
                                    this.hysy_flag = true;
                                    flag = this.inv.SetSLv(rowIndex, this.SlvListIndexOf(s));
                                    if (this.yhzc_contain_slv(yhzc, s.Trim(), true, true))
                                    {
                                        this.inv.SetXsyh(rowIndex, "1");
                                        if (this.codeInfoList.Count != 0)
                                        {
                                            this.codeInfoList[rowIndex].sfxsyhzc = "1";
                                        }
                                    }
                                    else
                                    {
                                        this.inv.SetXsyh(rowIndex, "0");
                                        if (this.codeInfoList.Count != 0)
                                        {
                                            this.codeInfoList[rowIndex].sfxsyhzc = "0";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                flag = true;
                                MessageManager.ShowMsgBox("INP-431355");
                            }
                        }
                        else if ((s.Trim() == "减按1.5%计算") || (double.TryParse(s, out num8) && (num8 == 0.015)))
                        {
                            if ((this.lp_hysy != 2) && (this.lp_hysy != 3))
                            {
                                if (this.inv.SetZyfpLx((ZYFP_LX)10))
                                {
                                    this.SetHysy(false);
                                    this.hysy_flag = true;
                                    flag = this.inv.SetSLv(rowIndex, this.SlvListIndexOf(s));
                                    if (this.yhzc_contain_slv(yhzc, s.Trim(), true, false))
                                    {
                                        this.inv.SetXsyh(rowIndex, "1");
                                        if (this.codeInfoList.Count != 0)
                                        {
                                            this.codeInfoList[rowIndex].sfxsyhzc = "1";
                                        }
                                    }
                                    else
                                    {
                                        this.inv.SetXsyh(rowIndex, "0");
                                        if (this.codeInfoList.Count != 0)
                                        {
                                            this.codeInfoList[rowIndex].sfxsyhzc = "0";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                flag = true;
                                MessageManager.ShowMsgBox("INP-431381");
                            }
                        }
                        else if ((this.SlvListIndexOf(s) == "0") || (this.SlvListIndexOf(s) == "0.00"))
                        {
                            flag = true;
                            MessageManager.ShowMsgBox("INP-431380");
                        }
                        else if ((!this.inv.IsGfSqdFp && !flag2) && (s.Trim() != ""))
                        {
                            flag = true;
                            MessageManager.ShowMsgBox("销方不可填开此税率信息表");
                        }
                        else if ((this.lp_hysy != 1) && (this.lp_hysy != 4))
                        {
                            bool flag5 = false;
                            if (this.tool_chae.Checked)
                            {
                                flag5 = this.inv.SetZyfpLx((ZYFP_LX)11);
                            }
                            else
                            {
                                flag5 = this.inv.SetZyfpLx(0);
                            }
                            if (flag5)
                            {
                                this.SetHysy(false);
                                this.hysy_flag = false;
                                flag = this.inv.SetSLv(rowIndex, this.SlvListIndexOf(s));
                                if (this.yhzc_contain_slv(yhzc, s.Trim(), true, false))
                                {
                                    this.inv.SetXsyh(rowIndex, "1");
                                    if (this.codeInfoList.Count != 0)
                                    {
                                        this.codeInfoList[rowIndex].sfxsyhzc = "1";
                                    }
                                }
                                else
                                {
                                    this.inv.SetXsyh(rowIndex, "0");
                                    if (this.codeInfoList.Count != 0)
                                    {
                                        this.codeInfoList[rowIndex].sfxsyhzc = "0";
                                    }
                                }
                            }
                        }
                        else
                        {
                            flag = true;
                            if (this.lp_hysy == 1)
                            {
                                MessageManager.ShowMsgBox("INP-431356");
                            }
                            else
                            {
                                MessageManager.ShowMsgBox("INP-431382");
                            }
                        }
                        goto Label_08C3;
                    }
                case 7:
                    flag = this.inv.SetSe(rowIndex, s);
                    goto Label_08C3;

                default:
                    goto Label_08C3;
            }
            flag = this.inv.SetSL(rowIndex, s);
            goto Label_08C3;
        Label_02D9:
            flag = this.inv.SetDj(rowIndex, s);
            goto Label_08C3;
        Label_0375:
            flag = this.inv.SetJe(rowIndex, s);
        Label_08C3:
            if (!flag)
            {
                MessageManager.ShowMsgBox(this.inv.GetCode(), this.inv.Params);
            }
            this.ShowDataGrid(this.inv.GetSpxx(rowIndex), rowIndex);
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.FormattedValue.ToString() != string.Empty)
            {
                string name = this.dataGridView1.CurrentCell.OwningColumn.Name;
                switch (name)
                {
                    case "SL":
                    case "DJ":
                    case "JE":
                    case "SLV":
                    case "SE":
                        this.dataGridView1.Rows[e.RowIndex].ErrorText = "";
                        if ((((name == "SL") || (name == "JE")) || (name == "SE")) && (name == "SE"))
                        {
                            decimal num;
                            if (!decimal.TryParse(e.FormattedValue.ToString(), out num))
                            {
                                e.Cancel = true;
                                return;
                            }
                            object obj1 = this.dataGridView1.Rows[e.RowIndex].Cells["SE"].Value;
                        }
                        break;
                }
            }
        }

        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            DataGridViewColumn owningColumn = this.dataGridView1.CurrentCell.OwningColumn;
            if (owningColumn.Name.Equals("MC"))
            {
                int index = owningColumn.Index;
                int rowIndex = this.dataGridView1.CurrentCell.RowIndex;
                Rectangle rectangle = this.dataGridView1.GetCellDisplayRectangle(index, rowIndex, false);
                if (this.spmcBt != null)
                {
                    this.spmcBt.Left = rectangle.Left;
                    this.spmcBt.Top = rectangle.Top;
                    this.spmcBt.Width = rectangle.Width;
                    this.spmcBt.Height = rectangle.Height;
                    object obj2 = this.dataGridView1.CurrentCell.Value;
                    this.spmcBt.Text = (obj2 == null) ? "" : obj2.ToString();
                    DataTable table = this.spmcBt.DataSource;
                    if (table != null)
                    {
                        table.Clear();
                    }
                    this.spmcBt.Visible = true;
                    this.spmcBt.Focus();
                }
            }
            else
            {
                this.spmcBt.Visible = false;
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            CustomStyleDataGrid grid = (CustomStyleDataGrid)sender;
            AisinoMultiCombox combox = grid.Controls["SPMCBT"] as AisinoMultiCombox;
            if ((grid.CurrentCell != null) && !grid.CurrentRow.ReadOnly)
            {
                DataGridViewColumn owningColumn = grid.CurrentCell.OwningColumn;
                if (!owningColumn.Name.Equals("MC"))
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
            if (e.KeyChar.ToString() == "\b")
            {
                e.Handled = false;
            }
            else
            {
                if ((name.Equals("DJ") || name.Equals("SL")) || ((name.Equals("JE") || name.Equals("SE")) || name.Equals("SLV")))
                {
                    if (e.KeyChar.ToString() == ".")
                    {
                        DataGridViewTextBoxEditingControl control = (DataGridViewTextBoxEditingControl)sender;
                        if (control.Text.IndexOf(".") >= 0)
                        {
                            e.Handled = true;
                        }
                        else
                        {
                            e.Handled = false;
                        }
                    }
                    else if (e.KeyChar.ToString() == "-")
                    {
                        DataGridViewTextBoxEditingControl control2 = (DataGridViewTextBoxEditingControl)sender;
                        if (control2.Text.IndexOf("-") >= 0)
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
                if (name.Equals("GGXH"))
                {
                    DataGridViewTextBoxEditingControl control3 = (DataGridViewTextBoxEditingControl)sender;
                    if (control3.Text.Length > 0x27)
                    {
                        e.Handled = true;
                    }
                }
                if (name.Equals("DW"))
                {
                    DataGridViewTextBoxEditingControl control4 = (DataGridViewTextBoxEditingControl)sender;
                    if (control4.Text.Length > 0x15)
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void dataGridView1_EditingControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            string name = this.dataGridView1.CurrentCell.OwningColumn.Name;
            if (name.Equals("SLV") && (e.KeyCode == Keys.Down))
            {
                this.slvList.Focus();
                this.slvList.SelectedIndex = 0;
            }
            name.Equals("GGXH");
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            this.CellEdit = (DataGridViewTextBoxEditingControl)e.Control;
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
                string tempfirstslv = string.Empty;
                if (this.inv.IsGfSqdFp)
                {
                    tempfirstslv = "0.17";
                }
                else
                {
                    if (((this.tempfirstslv == "0.05") || (this.tempfirstslv == "0.050")) && ((int)(int)this.inv.Zyfplx == 1))
                    {
                        this.inv.SetZyfpLx((ZYFP_LX)1);
                        this.SetHysy(true);
                        this.hysy_flag = true;
                    }
                    if (this.tempfirstslv == "0.015")
                    {
                        this.inv.SetZyfpLx((ZYFP_LX)10);
                        this.SetHysy(false);
                        this.hysy_flag = true;
                    }
                    tempfirstslv = this.tempfirstslv;
                }
                Spxx spxx = new Spxx("", "", ((this.inv.SLv == "") || (this.inv.SLv == null)) ? tempfirstslv : this.inv.SLv, "", "", "", false, this.inv.Zyfplx);
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
                else if (index < (count - 1))
                {
                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[index + 1].Cells[0];
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
                HZFP_SQD hzfp_sqd = this.sqdDal.Select(sqd);
                if (hzfp_sqd != null)
                {
                    if (this.InitSqdMxType_Edit && (((((hzfp_sqd.XXBZT == "TZD0000") || (hzfp_sqd.XXBZT == "TZD0061")) || ((hzfp_sqd.XXBZT == "TZD0071") || (hzfp_sqd.XXBZT == "TZD0072"))) || (((hzfp_sqd.XXBZT == "TZD0073") || (hzfp_sqd.XXBZT == "TZD0074")) || ((hzfp_sqd.XXBZT == "TZD0075") || (hzfp_sqd.XXBZT == "TZD0076")))) || ((((hzfp_sqd.XXBZT == "TZD0077") || (hzfp_sqd.XXBZT == "TZD0078")) || ((hzfp_sqd.XXBZT == "TZD0079") || (hzfp_sqd.XXBZT == "TZD0080"))) || (((hzfp_sqd.XXBZT == "TZD0081") || (hzfp_sqd.XXBZT == "TZD0082")) || (hzfp_sqd.XXBZT == "TZD0083")))))
                    {
                        xiugai_no = 1;
                        this.InitSqdMxType_Edit = false;
                    }
                    else
                    {
                        this.SetSelectReason(hzfp_sqd.SQXZ);
                        if (this.Radio_BuyerSQ.Checked)
                        {
                            Invoice.IsGfSqdFp_Static=true;
                        }
                        else
                        {
                            Invoice.IsGfSqdFp_Static=(false);
                        }
                        byte[] buffer = CreateInvoiceTmp();
                        this.inv = new Invoice(true, false, false, (hzfp_sqd.FPZL == "s") ? ((FPLX)0) : ((FPLX)2), buffer, null);
                        if (((Convert.ToString(hzfp_sqd.SL) == "0.05") || (Convert.ToString(hzfp_sqd.SL) == "0.050")) && (hzfp_sqd.YYSBZ.Trim().Substring(8, 1) == "0"))
                        {
                            this.inv.SetZyfpLx((ZYFP_LX)1);
                            this.SetHysy(true);
                        }
                        if (Convert.ToString(hzfp_sqd.SL) == "0.015")
                        {
                            this.inv.SetZyfpLx((ZYFP_LX)10);
                            this.SetHysy(false);
                        }
                        this.tool_chae.Enabled = false;
                        if (hzfp_sqd.YYSBZ.Substring(8, 1) == "2")
                        {
                            this.inv.SetZyfpLx((ZYFP_LX)11);
                        }
                        this.lab_No.Text = sqd;
                        this.lab_kpy.Text = hzfp_sqd.JBR;
                        this.lab_date.Text = hzfp_sqd.TKRQ.ToString("yyyy年MM月dd日");
                        this.lab_je.Text = hzfp_sqd.HJJE.ToString();
                        this.lab_se.Text = hzfp_sqd.HJSE.ToString();
                        this.txt_sqly.Text = hzfp_sqd.SQLY;
                        this.txt_lxdh.Text = hzfp_sqd.SQRDH;
                        this.lab_fpdm.Text = hzfp_sqd.FPDM;
                        string str = hzfp_sqd.FPHM.ToString();
                        if (!(str == "0"))
                        {
                            while (str.Length < 8)
                            {
                                str = "0" + str;
                            }
                            this.lab_fphm.Text = str;
                        }
                        else
                        {
                            this.lab_fphm.Text = "";
                        }
                        this.lab_fpzl.Text = (hzfp_sqd.FPZL == "s") ? "增值税专用发票" : "普通发票";
                        if (this.Radio_BuyerSQ.Checked)
                        {
                            this.inv.IsGfSqdFp=(true);
                        }
                        else
                        {
                            this.inv.IsXfSqdFp=(true);
                            this.slvList.Items.Clear();
                            this.slvList.Items.AddRange(this.GetSqSlv().ToArray());
                        }
                        this.com_xfmc.Text = hzfp_sqd.XFMC;
                        this.com_xfsbh.Text = hzfp_sqd.XFSH;
                        this.inv.Xfmc=(hzfp_sqd.XFMC);
                        this.inv.Xfsh=(hzfp_sqd.XFSH);
                        this.com_gfmc.Text = hzfp_sqd.GFMC;
                        this.com_gfsbh.Text = hzfp_sqd.GFSH;
                        this.inv.Gfmc=(hzfp_sqd.GFMC);
                        this.inv.Gfsh=(hzfp_sqd.GFSH);
                        string sQXZ = hzfp_sqd.SQXZ;
                        if (sQXZ.Substring(sQXZ.Length - 1, 1) == "1")
                        {
                            this.hzfw = true;
                            this.rad_hzfw_y.Checked = true;
                            this.rad_hzfw_n.Checked = false;
                        }
                        else
                        {
                            this.hzfw = false;
                            this.rad_hzfw_y.Checked = false;
                            this.rad_hzfw_n.Checked = true;
                        }
                        this.inv.Fpdm=(hzfp_sqd.FPDM);
                        this.inv.Fphm=(hzfp_sqd.FPHM.ToString());
                        if (!this.inv.IsGfSqdFp)
                        {
                            object[] objArray = new object[] { "s", hzfp_sqd.FPDM, hzfp_sqd.FPHM };
                            Fpxx fpxx = ServiceFactory.InvokePubService("Aisino.Fwkp.QueryFPXX", objArray)[0] as Fpxx;
                            if (fpxx != null)
                            {
                                Invoice invoice = new Invoice(false, fpxx, CreateInvoiceTmp(), null);
                                string hjJe = invoice.GetHjJe();
                                this.blueje = Math.Abs(Convert.ToDecimal(hjJe));
                                if (((invoice.SLv == "0.05") || (invoice.SLv == "0.050")) && ((int)invoice.Zyfplx == 1))
                                {
                                    this.lp_hysy = 1;
                                }
                                else if (invoice.SLv == "0.015")
                                {
                                    this.lp_hysy = 4;
                                }
                                else if ((invoice.SLv == null) || (invoice.SLv == ""))
                                {
                                    this.lp_hysy = 3;
                                }
                                else
                                {
                                    this.lp_hysy = 2;
                                }
                            }
                        }
                        DataTable table = this.sqdMxDal.SelectList(this.sqdh);
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            DataRow row = table.Rows[i];
                            string str4 = GetSafeData.ValidateDoubleValue(row, "SLV").ToString();
                            Spxx spxx = new Spxx(GetSafeData.ValidateValue<string>(row, "SPMC"), GetSafeData.ValidateValue<string>(row, "SPSM"), str4, GetSafeData.ValidateValue<string>(row, "GGXH"), GetSafeData.ValidateValue<string>(row, "JLDW"), (GetSafeData.ValidateValue<string>(row, "DJ") == null) ? "" : GetSafeData.ValidateValue<string>(row, "DJ"), GetSafeData.ValidateValue<bool>(row, "HSJBZ"), ((hzfp_sqd.YYSBZ.Trim().Substring(8, 1) == "0") && ((str4 == "0.05") || (str4 == "0.050"))) ? ((ZYFP_LX)1) : ((str4 == "0.015") ? ((ZYFP_LX)10) : ((hzfp_sqd.YYSBZ.Trim().Substring(8, 1) == "2") ? ((ZYFP_LX)11) : ((ZYFP_LX)0))));
                            spxx.SL=((GetSafeData.ValidateValue<string>(row, "SL") == null) ? "" : GetSafeData.ValidateValue<string>(row, "SL"));
                            spxx.Je=(GetSafeData.ValidateValue<decimal>(row, "JE").ToString());
                            spxx.Se=(GetSafeData.ValidateValue<decimal>(row, "SE").ToString());
                            spxx.Spbh=(GetSafeData.ValidateValue<string>(row, "SPBH"));
                            spxx.XTHash=(GetSafeData.ValidateValue<string>(row, "XTHASH"));
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
                            if (hzfp_sqd.YYSBZ.Substring(8, 1) == "2")
                            {
                                this.tool_chae.Checked = true;
                            }
                            this.ShowDataGrid(this.inv.GetSpxx(i), i);
                        }
                        this.inv.Hsjbz=this.hsjbz;
                    }
                }
            }
        }

        private List<string> GetSqSlv()
        {
            List<string> list = new List<string>();
            TaxCard card = TaxCardFactory.CreateTaxCard();
            if ((card != null) && (card.TaxRateAuthorize) != null)
            {
                if ((card.TaxRateAuthorize).TaxRateNoTax != null && (card.TaxRateAuthorize).TaxRateNoTax.Count != 0)
                {
                    foreach (double num in card.TaxRateAuthorize.TaxRateNoTax)
                    {
                        switch (num.ToString())
                        {
                            case "0.0":
                                break;

                            case "0.015":
                                if (!list.Contains("减按1.5%计算"))
                                {
                                    list.Add("减按1.5%计算");
                                }
                                break;

                            default:
                                {
                                    double num3 = num * 100.0;
                                    if (!list.Contains(num3.ToString() + "%"))
                                    {
                                        list.Add(((num * 100.0)).ToString() + "%");
                                    }
                                    break;
                                }
                        }
                    }
                }
                if ((card.TaxRateAuthorize).TaxRateTax != null && (card.TaxRateAuthorize).TaxRateTax.Count != 0)
                {
                    foreach (double num2 in card.TaxRateAuthorize.TaxRateTax)
                    {
                        switch (num2.ToString())
                        {
                            case "0.05":
                                if (!list.Contains("中外合作油气田"))
                                {
                                    list.Add("中外合作油气田");
                                }
                                break;

                            case "0.015":
                                if (!list.Contains("减按1.5%计算"))
                                {
                                    list.Add("减按1.5%计算");
                                }
                                break;

                            default:
                                {
                                    double num5 = num2 * 100.0;
                                    if (!list.Contains(num5.ToString() + "%"))
                                    {
                                        list.Add(((num2 * 100.0)).ToString() + "%");
                                    }
                                    break;
                                }
                        }
                    }
                }
            }
            list.Add("");
            if (list.Count != 0)
            {
                this.tempfirstslv = this.SlvListIndexOf(list[0]);
            }
            return list;
        }

        private List<string> GetSqSySlv(string flbm)
        {
            List<string> list = new List<string>();
            TaxCard card = TaxCardFactory.CreateTaxCard();
            if ((card != null) && (card.TaxRateAuthorize) != null)
            {
                if ((card.TaxRateAuthorize).TaxRateNoTax != null && (card.TaxRateAuthorize).TaxRateNoTax.Count != 0)
                {
                    foreach (double num in card.TaxRateAuthorize.TaxRateNoTax)
                    {
                        switch (num.ToString())
                        {
                            case "0.0":
                                break;

                            case "0.015":
                                if (!list.Contains("减按1.5%计算"))
                                {
                                    list.Add("减按1.5%计算");
                                }
                                break;

                            default:
                                {
                                    double num4 = num * 100.0;
                                    if (!list.Contains(num4.ToString() + "%"))
                                    {
                                        list.Add(((num * 100.0)).ToString() + "%");
                                    }
                                    break;
                                }
                        }
                    }
                }
                if ((card.TaxRateAuthorize).TaxRateTax != null && (card.TaxRateAuthorize).TaxRateTax.Count != 0)
                {
                    foreach (double num2 in card.TaxRateAuthorize.TaxRateTax)
                    {
                        switch (num2.ToString())
                        {
                            case "0.05":
                                if (!list.Contains("中外合作油气田"))
                                {
                                    list.Add("中外合作油气田");
                                }
                                break;

                            case "0.015":
                                if (!list.Contains("减按1.5%计算"))
                                {
                                    list.Add("减按1.5%计算");
                                }
                                break;

                            default:
                                {
                                    double num6 = num2 * 100.0;
                                    if (!list.Contains(num6.ToString() + "%"))
                                    {
                                        list.Add(((num2 * 100.0)).ToString() + "%");
                                    }
                                    break;
                                }
                        }
                    }
                }
            }
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
            dictionary.Add("HBBM", flbm);
            foreach (Dictionary<string, object> dictionary2 in baseDAOSQLite.querySQL("aisino.Fwkp.Hzfp.SelectSYSLV", dictionary))
            {
                if (dictionary2["SLV"].ToString().Trim() != "")
                {
                    string[] strArray = dictionary2["SLV"].ToString().Split(new string[] { "、", ",", "，" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        if (strArray[i] == "1.5%_5%")
                        {
                            strArray[i] = "1.5%";
                        }
                        if ((!list.Contains(strArray[i]) && (strArray[i] != "0%")) && ((strArray[i] != "0") && (strArray[i] != "0.0")))
                        {
                            list.Add(strArray[i]);
                        }
                    }
                }
            }
            list.Add("");
            if (list.Count != 0)
            {
                this.tempfirstslv = this.SlvListIndexOf(list[0]);
            }
            return list;
        }

        private List<string> GetSqSyYhSlv(string flbm, string yhzcms)
        {
            List<string> list = new List<string>();
            TaxCard card = TaxCardFactory.CreateTaxCard();
            if ((card != null) && (card.TaxRateAuthorize) != null)
            {
                if ((card.TaxRateAuthorize).TaxRateNoTax != null && (card.TaxRateAuthorize).TaxRateNoTax.Count != 0)
                {
                    foreach (double num in card.TaxRateAuthorize.TaxRateNoTax)
                    {
                        switch (num.ToString())
                        {
                            case "0.0":
                                break;

                            case "0.015":
                                if (!list.Contains("减按1.5%计算"))
                                {
                                    list.Add("减按1.5%计算");
                                }
                                break;

                            default:
                                {
                                    double num5 = num * 100.0;
                                    if (!list.Contains(num5.ToString() + "%"))
                                    {
                                        list.Add(((num * 100.0)).ToString() + "%");
                                    }
                                    break;
                                }
                        }
                    }
                }
                if ((card.TaxRateAuthorize).TaxRateTax != null && (card.TaxRateAuthorize).TaxRateTax.Count != 0)
                {
                    foreach (double num2 in card.TaxRateAuthorize.TaxRateTax)
                    {
                        switch (num2.ToString())
                        {
                            case "0.05":
                                if (!list.Contains("中外合作油气田"))
                                {
                                    list.Add("中外合作油气田");
                                }
                                break;

                            case "0.015":
                                if (!list.Contains("减按1.5%计算"))
                                {
                                    list.Add("减按1.5%计算");
                                }
                                break;

                            default:
                                {
                                    double num7 = num2 * 100.0;
                                    if (!list.Contains(num7.ToString() + "%"))
                                    {
                                        list.Add(((num2 * 100.0)).ToString() + "%");
                                    }
                                    break;
                                }
                        }
                    }
                }
            }
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
            dictionary.Add("HBBM", flbm);
            foreach (Dictionary<string, object> dictionary2 in baseDAOSQLite.querySQL("aisino.Fwkp.Hzfp.SelectSYSLV", dictionary))
            {
                if (dictionary2["SLV"].ToString().Trim() != "")
                {
                    string[] strArray = dictionary2["SLV"].ToString().Split(new string[] { "、", ",", "，" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        if (strArray[i] == "1.5%_5%")
                        {
                            strArray[i] = "1.5%";
                        }
                        if ((!list.Contains(strArray[i]) && (strArray[i] != "0%")) && ((strArray[i] != "0") && (strArray[i] != "0.0")))
                        {
                            list.Add(strArray[i]);
                        }
                    }
                }
            }
            Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
            BaseDAOFactory.GetBaseDAOSQLite();
            dictionary3.Add("YHZCMC", yhzcms);
            foreach (Dictionary<string, object> dictionary4 in baseDAOSQLite.querySQL("aisino.Fwkp.Hzfp.SelectYHSLV", dictionary3))
            {
                if (dictionary4["SLV"].ToString().Trim() != "")
                {
                    string[] strArray2 = dictionary4["SLV"].ToString().Split(new string[] { "、", ",", "，" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < strArray2.Length; j++)
                    {
                        if (strArray2[j] == "1.5%_5%")
                        {
                            strArray2[j] = "1.5%";
                        }
                        if ((!list.Contains(strArray2[j]) && (strArray2[j] != "0%")) && ((strArray2[j] != "0") && (strArray2[j] != "0.0")))
                        {
                            list.Add(strArray2[j]);
                        }
                    }
                }
            }
            list.Add("");
            if (list.Count != 0)
            {
                this.tempfirstslv = this.SlvListIndexOf(list[0]);
            }
            return list;
        }

        private int GetStringLength(string text)
        {
            int num = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] < '\x007f')
                {
                    num++;
                }
                else
                {
                    num += 2;
                }
            }
            return num;
        }

        public string[] GetSYSlv(string flbm, bool sfyh)
        {
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
            string[] source = row["SLV"].ToString().Split(new string[] { "、", ",", "，" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == "1.5%_5%")
                {
                    source[i] = "1.5%";
                }
            }
            string[] strArray2 = row["YHZC_SLV"].ToString().Split(new string[] { "、", ",", "，" }, StringSplitOptions.RemoveEmptyEntries);
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
            if (sfyh)
            {
                list.AddRange(strArray2.ToList<string>());
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

        private void GetXfInfo(string fpzl, string fpdm, string fphm)
        {
            object[] objArray = new object[] { "s", fpdm, Convert.ToInt32(fphm) };
            Fpxx fpxx = ServiceFactory.InvokePubService("Aisino.Fwkp.QueryFPXX", objArray)[0] as Fpxx;
            if (fpxx != null)
            {
                this.tool_chae.Enabled = false;
                if (fpxx.yysbz[8] == '2')
                {
                    this.tool_chae.Checked = true;
                }
                byte[] buffer = CreateInvoiceTmp();
                Invoice invoice = new Invoice(false, fpxx, buffer, null);
                invoice.IsXfSqdFp=(true);
                this.inv = invoice.GetRedInvoice(this.hsjbz);
                this.inv.IsXfSqdFp = (true);
                if ((this.inv == null) || (invoice.GetCode() != "0000"))
                {
                    invoice.GetCode();
                    MessageManager.ShowMsgBox(invoice.GetCode(), invoice.Params);
                    lp_error = true;
                }
                else
                {
                    this.inv.IsXfHzfw=this.hzfw;
                    this.blueje = Math.Abs(Convert.ToDecimal(this.inv.GetHjJe()));
                    this.SetHysy((int)(int)this.inv.Zyfplx == 1);
                    TaxCard card = TaxCardFactory.CreateTaxCard();
                    this.com_gfmc.Text = invoice.Gfmc;
                    this.com_gfsbh.Text = this.inv.Gfsh;
                    this.com_xfmc.Text = card.Corporation;
                    if (this.inv.Xfsh == oldsh.Trim())
                    {
                        this.com_xfsbh.Text = card.TaxCode;
                    }
                    else
                    {
                        this.com_xfsbh.Text = this.inv.Xfsh;
                    }
                    this.lab_je.Text = this.inv.GetHjJe();
                    this.lab_se.Text = this.inv.GetHjSe();
                    lpbmbbbh = this.inv.Bmbbbh;
                    List<Dictionary<SPXX, string>> spxxs = this.inv.GetSpxxs();
                    if (spxxs.Count <= 0)
                    {
                        if (((this.tempfirstslv == "0.05") || (this.tempfirstslv == "0.050")) && ((int)(int)this.inv.Zyfplx == 1))
                        {
                            this.inv.SetZyfpLx((ZYFP_LX)1);
                            this.SetHysy(true);
                            this.hysy_flag = true;
                        }
                        if (this.tempfirstslv == "0.015")
                        {
                            this.inv.SetZyfpLx((ZYFP_LX)10);
                            this.SetHysy(false);
                            this.hysy_flag = true;
                        }
                        if (fpxx.yysbz.Trim().Substring(8, 1) == "2")
                        {
                            this.inv.SetZyfpLx((ZYFP_LX)11);
                            this.SetHysy(false);
                            this.hysy_flag = false;
                            this.tool_chae.Checked = true;
                        }
                        Spxx spxx = new Spxx("", "", ((int)(int)this.inv.Zyfplx == 1) ? "0.05" : this.tempfirstslv, "", "", "", false, this.inv.Zyfplx);
                        this.AddRow(spxx);
                    }
                    else
                    {
                        if (((spxxs[0][(SPXX)8] == "0.05") || (spxxs[0][(SPXX)8] == "0.050")) && ((int)(int)this.inv.Zyfplx == 1))
                        {
                            this.lp_hysy = 1;
                        }
                        else if (spxxs[0][(SPXX)8] == "0.015")
                        {
                            this.lp_hysy = 4;
                        }
                        else if ((spxxs[0][(SPXX)8] == null) || (spxxs[0][(SPXX)8] == ""))
                        {
                            this.lp_hysy = 3;
                        }
                        else
                        {
                            this.lp_hysy = 2;
                        }
                        if (((base.TaxCardInstance.StateInfo.CompanyType == 0) || (spxxs.Count <= 7)) && ((base.TaxCardInstance.StateInfo.CompanyType != 0) || (spxxs.Count <= 8)))
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
                        else
                        {
                            if ((base.TaxCardInstance.StateInfo.CompanyType != 0) && (spxxs.Count > 7))
                            {
                                lp_mxchao78 = 7;
                            }
                            if ((base.TaxCardInstance.StateInfo.CompanyType == 0) && (spxxs.Count > 8))
                            {
                                lp_mxchao78 = 8;
                            }
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
                byte[] buffer2 = CreateInvoiceTmp();
                this.inv = new Invoice(true, false, false, this.fplx, buffer2, null);
                this.inv.IsXfSqdFp=(true);
                this.inv.IsXfHzfw=(this.hzfw);
                this.inv.Xfmc=(base.TaxCardInstance.Corporation);
                this.inv.Xfsh=(base.TaxCardInstance.TaxCode);
                if (((this.tempfirstslv == "0.05") || (this.tempfirstslv == "0.050")) && ((int)this.inv.Zyfplx == 1))
                {
                    this.inv.SetZyfpLx((ZYFP_LX)1);
                    this.SetHysy(true);
                    this.hysy_flag = true;
                }
                if (this.tempfirstslv == "0.015")
                {
                    this.inv.SetZyfpLx((ZYFP_LX)10);
                    this.SetHysy(false);
                    this.hysy_flag = true;
                }
                Spxx spxx2 = new Spxx("", "", this.tempfirstslv, "", "", "", false, this.inv.Zyfplx);
                this.AddRow(spxx2);
            }
        }

        private List<string> GetXTSPXXByMC(string spmc)
        {
            List<string> list = new List<string>();
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGetXTSPXX", new object[] { spmc });
            if ((objArray != null) && (objArray.Length > 0))
            {
                string[] collection = objArray[0] as string[];
                list.AddRange(collection);
            }
            return list;
        }

        private string GetYYSBZ(Fpxx fpxx)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("0");
            if (fpxx.hzfw)
            {
                builder.Append("1");
            }
            else
            {
                builder.Append("0");
            }
            if ((int)fpxx.Zyfplx == 7)
            {
                builder.Append("2");
            }
            else if ((int)fpxx.Zyfplx == 6)
            {
                builder.Append("1");
            }
            else if ((((int)fpxx.Zyfplx == 4) || ((int)fpxx.Zyfplx == 5)) || (((int)fpxx.Zyfplx == 2) || ((int)fpxx.Zyfplx == 3)))
            {
                builder.Append("3");
            }
            else
            {
                builder.Append("0");
            }
            builder.Append('0', 5);
            if ((int)fpxx.Zyfplx == 1)
            {
                builder.Append('0');
            }
            else if ((int)fpxx.Zyfplx == 11)
            {
                builder.Append('2');
            }
            else
            {
                builder.Append('1');
            }
            builder.Append('0');
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
            this.toolStrip2 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip2");
            this.tool_daying = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_daying");
            this.tool_tongji = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_tongji");
            this.tool_geshi = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_geshi");
            this.tool_tuichu = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_tuichu");
            this.tool_bianji = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_bianji");
            this.tool_hanshuiqiehuan = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_hanshuiqiehuan");
            this.tool_addRow = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_addRow");
            this.tool_insertRow = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_insertRow");
            this.tool_DeleteRow = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_DeleteRow");
            this.tool_AddGood = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolAddGood");
            this.tool_chae = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_chae");
            this.tool_AddGood.Visible = false;
            this.toolStripSeparator = this.xmlComponentLoader1.GetControlByName<ToolStripSeparator>("toolStripSeparator3");
            this.tool_bianji.Visible = false;
            this.tool_tuichu.Margin = new Padding(20, 1, 0, 2);
            ControlStyleUtil.SetToolStripStyle(this.toolStrip2);
            this.lab_fpzl = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_fpzl");
            this.lab_fphm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_fphm");
            this.lab_fpdm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_fpdm");
            this.lab_je = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_je");
            this.lab_se = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_se");
            this.txt_sqly = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_sqly");
            this.txt_lxdh = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_lxdh");
            this.txt_sqly.KeyPress += new KeyPressEventHandler(this.txt_sqly_KeyPress);
            this.txt_lxdh.KeyPress += new KeyPressEventHandler(this.txt_lxdh_KeyPress);
            this.txt_lxdh.MaxLength = 0x19;
            this.txt_sqly.MaxLength = 200;
            this.txt_lxdh.Enabled = false;
            this.txt_lxdh.Visible = false;
            this.txt_sqly.Enabled = false;
            this.txt_sqly.Visible = false;
            this.dataGridView1 = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGrid1");
            this.rad_hzfw_n = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rad_hzfw_n");
            this.rad_hzfw_y = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rad_hzfw_y");
            this.rad_hzfw_n.Enabled = true;
            this.rad_hzfw_y.Enabled = true;
            this.lab_No = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_No");
            this.lab_kpy = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_kpy");
            this.lab_date = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_date");
            this.com_gfsbh = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("com_gfsbh");
            this.com_gfsbh.IsSelectAll=(true);
            this.com_gfsbh.buttonStyle=(0);
            this.com_gfsbh.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 0x69));
            this.com_gfsbh.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.com_gfsbh.Width - 0x69));
            this.com_gfsbh.ShowText=("SH");
            this.com_gfsbh.DrawHead=(false);
            this.com_gfmc = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("com_gfmc");
            this.com_gfmc.IsSelectAll=(true);
            this.com_gfmc.buttonStyle=(0);
            this.com_gfmc.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 0x69));
            this.com_gfmc.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.com_gfmc.Width - 0x69));
            this.com_gfmc.ShowText=("MC");
            this.com_gfmc.DrawHead=(false);
            this.com_xfsbh = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("com_xfsbh");
            this.com_xfsbh.IsSelectAll=(true);
            this.com_xfsbh.buttonStyle=(0);
            this.com_xfsbh.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 0x69));
            this.com_xfsbh.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.com_xfsbh.Width - 0x69));
            this.com_xfsbh.ShowText=("SH");
            this.com_xfsbh.DrawHead=(false);
            this.com_xfmc = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("com_xfmc");
            this.com_xfmc.IsSelectAll=(true);
            this.com_xfmc.buttonStyle=(0);
            this.com_xfmc.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 0x69));
            this.com_xfmc.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.com_xfmc.Width - 0x69));
            this.com_xfmc.ShowText=("MC");
            this.com_xfmc.DrawHead=(false);
            this.com_xfmc.KeyPress += new KeyPressEventHandler(this.com_xfmc_KeyPress);
            this.com_xfmc.MaxLength=(100);
            this.com_gfmc.KeyPress += new KeyPressEventHandler(this.com_gfmc_KeyPress);
            this.com_gfmc.MaxLength=(100);
            this.com_xfsbh.KeyPress += new KeyPressEventHandler(this.com_xfsbh_KeyPress);
            this.com_xfsbh.MaxLength=(20);
            this.com_gfsbh.KeyPress += new KeyPressEventHandler(this.com_gfsbh_KeyPress);
            this.com_gfsbh.MaxLength=(20);
            this.com_gfsbh.AutoIndex=(1);
            this.com_gfsbh.OnButtonClick+=(new EventHandler(this.com_gf_DropDown));
            this.com_gfsbh.AutoComplate=(AutoComplateStyle)2;
            this.com_gfsbh.OnAutoComplate+=(new EventHandler(this.com_gfsbh_OnAutoComplate));
            this.com_gfsbh.OnSelectValue+=(new EventHandler(this.com_gfsbh_OnSelectValue));
            this.com_gfmc.AutoIndex=(1);
            this.com_gfmc.OnButtonClick+=(new EventHandler(this.com_gf_DropDown));
            this.com_gfmc.AutoComplate=(AutoComplateStyle)2;
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
            this.com_gfmc.OnTextChanged = (EventHandler)Delegate.Combine(this.com_gfmc.OnTextChanged, new EventHandler(this.com_gfmc_TextChanged));
            this.com_gfsbh.OnTextChanged = (EventHandler)Delegate.Combine(this.com_gfsbh.OnTextChanged, new EventHandler(this.com_gfsbh_TextChanged));
            this.com_xfmc.OnTextChanged = (EventHandler)Delegate.Combine(this.com_xfmc.OnTextChanged, new EventHandler(this.com_xfmc_TextChanged));
            this.com_xfsbh.OnTextChanged = (EventHandler)Delegate.Combine(this.com_xfsbh.OnTextChanged, new EventHandler(this.com_xfsbh_TextChanged));
            this.com_gfsbh.KeyPress += new KeyPressEventHandler(this.KeyPressToUpper);
            this.com_xfsbh.KeyPress += new KeyPressEventHandler(this.KeyPressToUpper);
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows=(false);
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
            this.tool_AddGood.Click += new EventHandler(this.tool_AddGood_Click);
            this.tool_chae.ToolTipText = "差额征税";
            this.tool_chae.Click += new EventHandler(this.tool_chae_Click);
            this.tool_bianji.Checked = true;
            this.tool_tongji.Visible = false;
            this.tool_geshi.Visible = false;
            this.BuyerPan = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel10");
            this.SellerPan = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel9");
            this.tool_insertRow.Visible = false;
            this.rad_hzfw_y.CheckedChanged += new EventHandler(this.rad_hzfw_CheckedChanged);
            this.rad_hzfw_n.CheckedChanged += new EventHandler(this.rad_hzfw_CheckedChanged);
            this.btn_addrow = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("bt_addrow");
            this.btn_delrow = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("bt_delrow");
            this.btn_addrow.Visible = false;
            this.btn_delrow.Visible = false;
            this.pnlGrid = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("aisinoPNL2");
            this.pnlSqly = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel3");
            this.pnlHjje = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel4");
            this.btnSplit = this.xmlComponentLoader1.GetControlByName<Button>("button1");
            this.btnSplit.Click += new EventHandler(this.btnSplit_Click);
            base.Shown += new EventHandler(this.SqdTianKai_Shown);
            base.Resize += new EventHandler(this.SqdTianKai_Resize);
            this.dataGridView1.ImeMode = ImeMode.NoControl;
            this.dataGridView1.ColumnWidthChanged += new DataGridViewColumnEventHandler(this.dataGridView1_ColumnWidthChanged);
            this.dataGridView1.GridStyle=((CustomStyle)1);
            ControlStyleUtil.SetToolStripStyle(this.toolStrip2);
            this.btnSplit.Top = (this.pnlSqly.Top - this.btnSplit.Height) + 3;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(SqdTianKai));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x385, 0x232);
            this.xmlComponentLoader1.TabIndex = 1;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "红字发票信息表信息选择";
            this.xmlComponentLoader1.XMLPath=(@"..\Config\Components\Aisino.Fwkp.Hzfp.Form.SqdTianKai\Aisino.Fwkp.Hzfp.Form.SqdTianKai.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.AutoScrollMinSize = new Size(0x367, 0x214);
            this.xmlComponentLoader1.AutoScrollMinSize = new Size(0x367, 0x214);
            base.ClientSize = new Size(0x385, 0x232);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "SqdTianKai";
            this.Text = "红字发票信息表填开";
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "红字发票信息表填开";
            base.ResumeLayout(false);
        }

        public bool InitSqdMx(InitSqdMxType type, List<object> oSelectInfor)
        {
            this.codeInfoList.Clear();
            this.SqdMxType = type;
            this.InitSqdzd();
            PropertyUtil.SetValue("SQD-HSJBZ", "0");
            this.hsjbz = PropertyUtil.GetValue("SQD-HSJBZ", "0") != "0";
            switch (this.SqdMxType)
            {
                case InitSqdMxType.Add:
                    this.SqdAdd(oSelectInfor);
                    break;

                case InitSqdMxType.Edit:
                    this.InitSqdMxType_Edit = true;
                    if (this.SqdEidt())
                    {
                        break;
                    }
                    return false;

                case InitSqdMxType.Read:
                    this.InitSqdMxType_Read = true;
                    this.SqdRead();
                    break;
            }
            if (this.dataGridView1.RowCount > 0)
            {
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0].Cells["SE"];
            }
            return true;
        }

        public void InitSqdzd()
        {
            this.InitSqdMxType_Read = false;
            lp_mxchao78 = 0;
            lp_error = false;
            noslv = false;
            xiugai_no = 0;
            lpFLBM = true;
            lpbmbbbh = "";
            isFLBM = FLBMqy && lpFLBM;
            isCes = TaxCardFactory.CreateTaxCard().GetExtandParams("CEBTVisble") == "1";
            if (!isCes)
            {
                this.tool_chae.Visible = false;
            }
            else
            {
                this.tool_chae.Visible = true;
                this.tool_chae.Checked = false;
            }
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

        private bool isXT(string mc)
        {
            IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("MC", mc);
            return (baseDAOSQLite.querySQL("aisino.Fwkp.Hzfp.selectXThash", dictionary).Count > 0);
        }

        private bool IsXTQY()
        {
            return (((base.TaxCardInstance.CorpAgent!= null) && (base.TaxCardInstance.CorpAgent.Length > 9)) && CheckCodeRoles.IsXT(base.TaxCardInstance.CorpAgent.Substring(0, 10)));
        }

        private void KeyPressToUpper(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'a') && (e.KeyChar <= 'z'))
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper()[0];
            }
        }

        private void rad_hzfw_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rad_hzfw_y.Checked)
            {
                this.hzfw = true;
                if (this.inv != null)
                {
                    this.inv.IsXfHzfw=(true);
                }
            }
            if (this.rad_hzfw_n.Checked)
            {
                this.hzfw = false;
                if (this.inv != null)
                {
                    this.inv.IsXfHzfw=(false);
                }
            }
        }

        private void SelSpxx(CustomStyleDataGrid parent, int type, int showDisableSLv)
        {
            double num;
            object obj2 = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["MC"].Value;
            string str = (obj2 != null) ? obj2.ToString() : "";
            if ((this.inv.GetSpxxs().Count > 0) && ((int)this.inv.Zyfplx == 1))
            {
                num = double.Parse(this.inv.SLv);
            }
            else if (((this.inv.SLv.Length == 0) && (this.inv.GetSpxxs().Count > 0)) || ((this.inv.GetSpxxs().Count == 1) && (this.blueje <= -1M)))
            {
                num = -1.0;
            }
            else
            {
                num = double.Parse(this.inv.SLv);
            }
            object[] objArray = null;
            if ((num == 0.05) && ((int)this.inv.Zyfplx == 1))
            {
                objArray = new object[] { str, num, type, showDisableSLv, "", "HYSY" };
            }
            else if ((int)this.inv.Zyfplx == 10)
            {
                objArray = new object[] { str, num, type, showDisableSLv, "", "OPF" };
            }
            else
            {
                objArray = new object[] { str, -1.0, type, showDisableSLv, "", "" };
            }
            object[] spxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSP", objArray);
            if (spxx != null)
            {
                if (isFLBM)
                {
                    if (spxx[11].ToString().Trim() == "")
                    {
                        objArray = new object[] { spxx[1].ToString(), "", spxx[0].ToString(), true };
                        spxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddSP", objArray);
                    }
                    if (spxx == null)
                    {
                        this.spmcBt.Text = "";
                        return;
                    }
                    bool flag = this.isXT(spxx[1].ToString());
                    object[] objArray3 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.CanUseThisSPFLBM", new object[] { spxx[11].ToString(), true, flag });
                    if ((objArray3 != null) && !bool.Parse(objArray3[0].ToString()))
                    {
                        this.spmcBt.Text = "";
                        MessageManager.ShowMsgBox("INP-242207", new string[] { "商品", "\r\n可能原因：\r\n1、当前企业没有所选税收分类编码授权。\r\n2、当前版本所选税收分类编码可用状态为不可用。" });
                        return;
                    }
                }
                if (((spxx[3].ToString() == "0") || (spxx[3].ToString() == "0.0")) || ((spxx[3].ToString() == "0.00") || (spxx[3].ToString() == "0%")))
                {
                    MessageManager.ShowMsgBox("INP-431380");
                }
                else
                {
                    this.SetSpxx(spxx);
                }
            }
        }

        public void spmcbt(object[] spxx)
        {
            double num;
            string str = "";
            if ((this.inv.GetSpxxs().Count > 0) && ((int)this.inv.Zyfplx == 1))
            {
                num = double.Parse(this.inv.SLv);
            }
            else if (((this.inv.SLv.Length == 0) && (this.inv.GetSpxxs().Count > 0)) || ((this.inv.GetSpxxs().Count == 1) && (this.blueje <= -1M)))
            {
                num = -1.0;
            }
            else
            {
                num = double.Parse(this.inv.SLv);
            }
            object[] objArray = null;
            if ((num == 0.05) && ((int)this.inv.Zyfplx == 1))
            {
                objArray = new object[] { str, num, 0, 0, "", "HYSY" };
            }
            else if ((int)this.inv.Zyfplx == 10)
            {
                objArray = new object[] { str, num, 0, 0, "", "OPF" };
            }
            else
            {
                objArray = new object[] { str, -1.0, 0, 0, "", "" };
            }
            if (isFLBM)
            {
                if (spxx[11].ToString().Trim() == "")
                {
                    objArray = new object[] { spxx[1].ToString(), "", spxx[0].ToString(), true };
                    spxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddSP", objArray);
                }
                if (spxx == null)
                {
                    this.spmcBt.Text = "";
                    return;
                }
                bool flag = this.isXT(spxx[1].ToString());
                object[] objArray3 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.CanUseThisSPFLBM", new object[] { spxx[11].ToString(), true, flag });
                if ((objArray3 != null) && !bool.Parse(objArray3[0].ToString()))
                {
                    this.spmcBt.Text = "";
                    MessageManager.ShowMsgBox("INP-242207", new string[] { "商品", "\r\n可能原因：\r\n1、当前企业没有所选税收分类编码授权。\r\n2、当前版本所选税收分类编码可用状态为不可用。" });
                    return;
                }
            }
            if (((spxx[3].ToString() == "0") || (spxx[3].ToString() == "0.0")) || ((spxx[3].ToString() == "0.00") || (spxx[3].ToString() == "0%")))
            {
                MessageManager.ShowMsgBox("INP-431380");
            }
            else
            {
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

        private void SetHysy(bool flag)
        {
            if (flag)
            {
                this.tool_hanshuiqiehuan.Checked = true;
                this.hsjbz = true;
                this.inv.Hsjbz=(this.hsjbz);
                PropertyUtil.SetValue("SQD-HSJBZ", this.hsjbz ? "1" : "0");
                this.dataGridView1.Columns["DJ"].HeaderText = "单价(含税)";
                this.dataGridView1.Columns["JE"].HeaderText = "金额(不含税)";
                this.tool_hanshuiqiehuan.Enabled = false;
            }
            else
            {
                this.tool_hanshuiqiehuan.Checked = this.hsjbz;
                this.dataGridView1.Columns["DJ"].HeaderText = this.hsjbz ? "单价(含税)" : "单价(不含税)";
                this.dataGridView1.Columns["JE"].HeaderText = this.hsjbz ? "金额(含税)" : "金额(不含税)";
                this.tool_hanshuiqiehuan.Enabled = true;
            }
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
            bool flag9;
            int rowIndex = this.dataGridView1.CurrentCell.RowIndex;
            bool flag = false;
            if (spxx == null)
            {
                return;
            }
            if (!isFLBM)
            {
                decimal num6;
                if (this.tool_chae.Checked)
                {
                    if (this.isXT(spxx[1].ToString()))
                    {
                        this.spmcBt.Text = "";
                        MessageManager.ShowMsgBox("INP-242185", new string[] { "当前发票类型不能填开稀土商品！" });
                        return;
                    }
                    string text2 = ((double.Parse(spxx[3].ToString()) * 100.0)).ToString() + "%";
                    if ((spxx[10].ToString().Trim() == "True") || (spxx[3].ToString().Trim() == "0.015"))
                    {
                        this.spmcBt.Text = "";
                        MessageManager.ShowMsgBox("INP-242185", new string[] { "当前发票类型不能填开特殊税率的商品！" });
                        return;
                    }
                }
                if (((int)this.inv.Zyfplx == 1) && (spxx[10].ToString().Trim() != "True"))
                {
                    this.spmcBt.Text = "";
                    MessageManager.ShowMsgBox("INP-242207", new string[] { "商品", "\r\n原因：中外合作油气田发票不能添加非中外合作油气田税率商品。" });
                    return;
                }
                if (((int)this.inv.Zyfplx == 10) && (spxx[3].ToString().Trim() != "0.015"))
                {
                    this.spmcBt.Text = "";
                    MessageManager.ShowMsgBox("INP-242207", new string[] { "商品", "\r\n原因：0.015发票不能添加非0.015税率商品。" });
                    return;
                }
                if (this.inv.IsGfSqdFp)
                {
                    goto Label_0BF3;
                }
                decimal.TryParse(spxx[3].ToString(), out num6);
                flag9 = false;
                if (this.slvList.Items != null)
                {
                    foreach (object obj3 in this.slvList.Items)
                    {
                        decimal num7;
                        decimal.TryParse(this.SlvListIndexOf(obj3.ToString()), out num7);
                        if (decimal.Compare(num7, num6) == 0)
                        {
                            flag9 = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                decimal num3;
                int index = -1;
                if ((spxx[12].ToString() == "是") && (spxx[15].ToString().Trim() == ""))
                {
                    this.Update_SP(spxx);
                }
                if (this.tool_chae.Checked)
                {
                    if (this.isXT(spxx[1].ToString()))
                    {
                        this.spmcBt.Text = "";
                        MessageManager.ShowMsgBox("INP-242185", new string[] { "当前发票类型不能填开稀土商品！" });
                        return;
                    }
                    string text1 = ((double.Parse(spxx[3].ToString()) * 100.0)).ToString() + "%";
                    if ((spxx[10].ToString().Trim() == "True") || (spxx[3].ToString().Trim() == "0.015"))
                    {
                        this.spmcBt.Text = "";
                        MessageManager.ShowMsgBox("INP-242185", new string[] { "当前发票类型不能填开特殊税率的商品！" });
                        return;
                    }
                }
                if (((int)this.inv.Zyfplx == 1) && (spxx[10].ToString().Trim() != "True"))
                {
                    this.spmcBt.Text = "";
                    MessageManager.ShowMsgBox("INP-242207", new string[] { "商品", "\r\n原因：中外合作油气田发票不能添加非中外合作油气田税率商品。" });
                    return;
                }
                if (((int)this.inv.Zyfplx == 10) && (spxx[3].ToString().Trim() != "0.015"))
                {
                    this.spmcBt.Text = "";
                    MessageManager.ShowMsgBox("INP-242207", new string[] { "商品", "\r\n原因：0.015发票不能添加非0.015税率商品。" });
                    return;
                }
                if (((this.inv.Fplx == 0) && (spxx.Length >= 11)) && (spxx[11].ToString() == "303"))
                {
                    this.spmcBt.Text = "";
                    MessageManager.ShowMsgBox("INP-242207", new string[] { "商品", "\r\n可能原因：\r\n1、当前企业没有所选税收分类编码授权。\r\n2、当前版本所选税收分类编码可用状态为不可用。" });
                    return;
                }
                bool flag3 = this.isXT(spxx[1].ToString());
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.CanUseThisSPFLBM", new object[] { spxx[11].ToString(), true, flag3 });
                if ((objArray != null) && !bool.Parse(objArray[0].ToString()))
                {
                    this.spmcBt.Text = "";
                    MessageManager.ShowMsgBox("INP-242207", new string[] { "商品", "\r\n可能原因：\r\n1、当前企业没有所选税收分类编码授权。\r\n2、当前版本所选税收分类编码可用状态为不可用。" });
                    return;
                }
                if (this.inv.Fplx == 0)
                {
                    if (((spxx[3].ToString() == "0.05") || (spxx[3].ToString() == "0.050")) && (spxx[10].ToString().Trim() == "True"))
                    {
                        if ((this.lp_hysy == 2) || (this.lp_hysy == 3))
                        {
                            MessageManager.ShowMsgBox("INP-431355");
                            return;
                        }
                        if (!this.inv.SetZyfpLx((ZYFP_LX)1))
                        {
                            MessageManager.ShowMsgBox(this.inv.GetCode(), this.inv.Params);
                            return;
                        }
                        this.hysy_flag = true;
                    }
                    if (spxx[3].ToString() == "0.015")
                    {
                        if ((this.lp_hysy == 2) || (this.lp_hysy == 3))
                        {
                            MessageManager.ShowMsgBox("INP-431381");
                            return;
                        }
                        if (!this.inv.SetZyfpLx((ZYFP_LX)10))
                        {
                            MessageManager.ShowMsgBox(this.inv.GetCode(), this.inv.Params);
                            return;
                        }
                        this.hysy_flag = true;
                    }
                }
                decimal.TryParse(spxx[3].ToString(), out num3);
                if (spxx.Length > 13)
                {
                    index = 11;
                }
                else
                {
                    index = 9;
                }
                this.codeInfoList[rowIndex].spbm = spxx[0].ToString();
                string str2 = spxx[index].ToString();
                while (str2.Length < 0x13)
                {
                    str2 = str2 + "0";
                }
                this.codeInfoList[rowIndex].flbm = str2;
                this.codeInfoList[rowIndex].sfxsyhzc = (spxx[index + 1].ToString() == "是") ? "1" : "0";
                this.codeInfoList[rowIndex].yhzcmc = spxx[index + 4].ToString();
                this.codeInfoList[rowIndex].yhzcsl = spxx[index + 3].ToString();
                this.inv.SetSpbh(rowIndex, this.codeInfoList[rowIndex].spbm);
                this.inv.SetFlbm(rowIndex, this.codeInfoList[rowIndex].flbm);
                this.inv.SetYhsm(rowIndex, this.codeInfoList[rowIndex].yhzcmc);
                this.inv.SetXsyh(rowIndex, this.codeInfoList[rowIndex].sfxsyhzc);
                if (this.codeInfoList[rowIndex].sfxsyhzc.Equals("1"))
                {
                    this.slvList.Items.AddRange(this.GetSqSyYhSlv(this.codeInfoList[rowIndex].flbm, this.codeInfoList[rowIndex].yhzcmc).ToArray());
                }
                else
                {
                    this.slvList.Items.AddRange(this.GetSqSySlv(this.codeInfoList[rowIndex].flbm).ToArray());
                }
                bool flag6 = false;
                int num4 = 0;
                if (this.slvList.Items != null)
                {
                    foreach (object obj2 in this.slvList.Items)
                    {
                        decimal num5;
                        decimal.TryParse(this.SlvListIndexOf(obj2.ToString()), out num5);
                        if (decimal.Compare(num5, num3) == 0)
                        {
                            flag6 = true;
                            break;
                        }
                        num4++;
                    }
                }
                if (!flag6)
                {
                    this.spmcBt.Text = "";
                    this.inv.SetFlbm(rowIndex, "");
                    MessageManager.ShowMsgBox("INP-242164");
                    return;
                }
                this.slvList.SelectedIndex = num4;
                string str3 = spxx[1].ToString();
                if ((str3.Trim() == "") || (str3.Trim() == "0"))
                {
                    str3 = "";
                }
                flag = this.inv.SetSpmc(rowIndex, str3);
                if (!this.inv.SetSLv(rowIndex, spxx[3].ToString()))
                {
                    MessageManager.ShowMsgBox(this.inv.GetCode(), this.inv.Params);
                    return;
                }
                this.inv.SetLslvbs(rowIndex, "");
                flag = this.inv.SetSpsm(rowIndex, spxx[4].ToString());
                flag = this.inv.SetGgxh(rowIndex, spxx[5].ToString());
                string str4 = spxx[6].ToString();
                flag = this.inv.SetJLdw(rowIndex, str4);
                bool flag7 = this.inv.Hsjbz;
                this.inv.Hsjbz=(spxx[8].ToString().Equals("True"));
                if ((int)this.inv.Zyfplx == 11)
                {
                    flag = this.inv.SetDj(rowIndex, "");
                }
                else
                {
                    flag = this.inv.SetDj(rowIndex, spxx[7].ToString());
                }
                this.inv.Hsjbz=(flag7);
                if (!this.inv.SetDjHsjbz(rowIndex, spxx[8].ToString().Equals("True")))
                {
                    MessageManager.ShowMsgBox(this.inv.GetCode(), this.inv.Params);
                    return;
                }
                if (spxx.Length > 13)
                {
                    this.inv.SetXtHash(rowIndex, spxx[9].ToString());
                }
                this.inv.SetSL(rowIndex, "");
                this.ShowDataGrid(this.inv.GetSpxx(rowIndex), rowIndex);
                this.SetHysy((int)this.inv.Zyfplx == 1);
                if (Convert.ToDouble(spxx[3].ToString()) == 0.0)
                {
                    if (this.codeInfoList[rowIndex].sfxsyhzc.Equals("0"))
                    {
                        this.codeInfoList[rowIndex].lslbs = "3";
                    }
                    else
                    {
                        foreach (string str5 in this.codeInfoList[rowIndex].yhzcmc.Split(new char[] { ',' }))
                        {
                            if (str5.Equals("出口零税率"))
                            {
                                this.codeInfoList[rowIndex].lslbs = "0";
                                break;
                            }
                            if (str5.Equals("免税"))
                            {
                                this.codeInfoList[rowIndex].lslbs = "1";
                                break;
                            }
                            if (str5.Equals("不征税"))
                            {
                                this.codeInfoList[rowIndex].lslbs = "2";
                                break;
                            }
                        }
                    }
                }
                else
                {
                    this.codeInfoList[rowIndex].lslbs = string.Empty;
                }
                goto Label_0E98;
            }
            if (!flag9)
            {
                MessageManager.ShowMsgBox("INP-431369");
                return;
            }
        Label_0BF3:
            if (this.inv.Fplx == 0)
            {
                if (((spxx[3].ToString() == "0.05") || (spxx[3].ToString() == "0.050")) && (spxx[10].ToString() == "True"))
                {
                    if (!this.inv.SetZyfpLx((ZYFP_LX)1))
                    {
                        MessageManager.ShowMsgBox(this.inv.GetCode(), this.inv.Params);
                        return;
                    }
                    this.hysy_flag = true;
                }
                if (spxx[3].ToString() == "0.015")
                {
                    if (!this.inv.SetZyfpLx((ZYFP_LX)10))
                    {
                        MessageManager.ShowMsgBox(this.inv.GetCode(), this.inv.Params);
                        return;
                    }
                    this.hysy_flag = true;
                }
            }
            string str7 = spxx[1].ToString();
            if ((str7.Trim() == "") || (str7.Trim() == "0"))
            {
                str7 = "";
            }
            flag = this.inv.SetSpmc(rowIndex, str7);
            if (!this.inv.SetSLv(rowIndex, spxx[3].ToString()))
            {
                MessageManager.ShowMsgBox(this.inv.GetCode(), this.inv.Params);
                return;
            }
            this.inv.SetLslvbs(rowIndex, "");
            flag = this.inv.SetSpsm(rowIndex, spxx[4].ToString());
            flag = this.inv.SetGgxh(rowIndex, spxx[5].ToString());
            string str8 = spxx[6].ToString();
            flag = this.inv.SetJLdw(rowIndex, str8);
            bool flag11 = this.inv.Hsjbz;
            this.inv.Hsjbz=(spxx[8].ToString().Equals("True"));
            if ((int)(int)this.inv.Zyfplx == 11)
            {
                flag = this.inv.SetDj(rowIndex, "");
            }
            else
            {
                flag = this.inv.SetDj(rowIndex, spxx[7].ToString());
            }
            this.inv.Hsjbz=(flag11);
            if (!this.inv.SetDjHsjbz(rowIndex, spxx[8].ToString().Equals("True")))
            {
                MessageManager.ShowMsgBox(this.inv.GetCode(), this.inv.Params);
                return;
            }
            if (spxx.Length > 13)
            {
                this.inv.SetXtHash(rowIndex, spxx[9].ToString());
            }
            this.inv.SetSL(rowIndex, "");
            this.ShowDataGrid(this.inv.GetSpxx(rowIndex), rowIndex);
            this.SetHysy((int)this.inv.Zyfplx == 1);
        Label_0E98:
            this.spmcBt.Text = spxx[1].ToString();
        }

        private void ShowDataGrid(Dictionary<SPXX, string> spxx, int row)
        {
            while ((this.dataGridView1.Rows.Count - 1) < row)
            {
                this.dataGridView1.Rows.Add();
            }
            this.dataGridView1.Rows[row].Cells["MC"].Value = spxx[0];
            this.dataGridView1.Rows[row].Cells["GGXH"].Value = spxx[(SPXX)3];
            this.dataGridView1.Rows[row].Cells["DW"].Value = spxx[(SPXX)4];
            this.dataGridView1.Rows[row].Cells["SL"].Value = spxx[(SPXX)6];
            this.dataGridView1.Rows[row].Cells["DJ"].Value = spxx[(SPXX)5];
            if (!string.IsNullOrEmpty(spxx[(SPXX)7]))
            {
                double num = Convert.ToDouble(spxx[(SPXX)7]);
                this.dataGridView1.Rows[row].Cells["JE"].Value = string.Format("{0:0.00}", num);
            }
            else
            {
                this.dataGridView1.Rows[row].Cells["JE"].Value = spxx[(SPXX)7];
            }
            if (this.SqdMxType == InitSqdMxType.Read)
            {
                this.dataGridView1.Rows[row].Cells["SLV"].Value = ((spxx[(SPXX)8] == null) || (spxx[(SPXX)8] == "")) ? "" : (((((spxx[(SPXX)8] == "0.050") || (spxx[(SPXX)8] == "0.05")) && ((int)this.inv.Zyfplx == 1)) || (spxx[(SPXX)8] == "0.015")) ? "" : (((spxx[(SPXX)8] == "0") || (spxx[(SPXX)8] == "0.00")) ? "免税" : ((Convert.ToDouble(spxx[(SPXX)8]) * 100.0) + "%")));
            }
            else
            {
                this.dataGridView1.Rows[row].Cells["SLV"].Value = ((spxx[(SPXX)8] == null) || (spxx[(SPXX)8] == "")) ? "" : ((((spxx[(SPXX)8] == "0.050") || (spxx[(SPXX)8] == "0.05")) && ((int)this.inv.Zyfplx == 1)) ? "中外合作油气田" : (((spxx[(SPXX)8] == "0") || (spxx[(SPXX)8] == "0.00")) ? "免税" : ((spxx[(SPXX)8] == "0.015") ? "减按1.5%计算" : ((Convert.ToDouble(spxx[(SPXX)8]) * 100.0) + "%"))));
            }
            if (!string.IsNullOrEmpty(spxx[(SPXX)9]))
            {
                double num2 = Convert.ToDouble(spxx[(SPXX)9]);
                this.dataGridView1.Rows[row].Cells["SE"].Value = string.Format("{0:0.00}", num2);
            }
            else
            {
                this.dataGridView1.Rows[row].Cells["SE"].Value = spxx[(SPXX)9];
            }
            this.lab_je.Text = "￥" + this.inv.GetHjJe();
            this.lab_se.Text = "￥" + this.inv.GetHjSe();
        }

        private void slvList_Click(object sender, EventArgs e)
        {
            this.dataGridView1.CancelEdit();
            this.dataGridView1.CurrentRow.Cells["SLV"].Value = this.slvList.SelectedItem;
            string str = string.Empty;
            if (this.slvList.SelectedItem == null)
            {
                str = "";
            }
            else
            {
                str = this.slvList.SelectedItem.ToString();
            }
            if (str == "中外合作油气田")
            {
                this.slvList.Visible = false;
                this.dataGridView1.EndEdit();
                this.dataGridView1.Focus();
            }
            else if (str == "减按1.5%计算")
            {
                this.slvList.Visible = false;
                this.dataGridView1.EndEdit();
                this.dataGridView1.Focus();
            }
            else
            {
                if (str.EndsWith("%"))
                {
                    str = (decimal.Parse(str.Remove(str.Length - 1)) / 100M).ToString();
                }
                int rowIndex = this.dataGridView1.CurrentCell.RowIndex;
                if (isFLBM)
                {
                    if ((str != "") && (Convert.ToDouble(str) == 0.0))
                    {
                        foreach (string str2 in this.codeInfoList[rowIndex].yhzcmc.Split(new char[] { ',' }))
                        {
                            if (str2.Equals("出口零税率"))
                            {
                                this.codeInfoList[rowIndex].lslbs = "0";
                                break;
                            }
                            if (str2.Equals("免税"))
                            {
                                this.codeInfoList[rowIndex].lslbs = "1";
                                break;
                            }
                            if (str2.Equals("不征税"))
                            {
                                this.codeInfoList[rowIndex].lslbs = "2";
                                break;
                            }
                        }
                    }
                    else if (str.Equals("免税"))
                    {
                        this.codeInfoList[rowIndex].lslbs = "3";
                    }
                    else
                    {
                        this.codeInfoList[rowIndex].lslbs = string.Empty;
                    }
                }
                this.slvList.Visible = false;
                this.dataGridView1.EndEdit();
                this.dataGridView1.Focus();
            }
        }

        private void slvList_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if ((e.KeyCode == Keys.Down) && (this.slvList.SelectedIndex < (this.slvList.Items.Count - 1)))
            {
                this.slvList.SelectedIndex++;
            }
            if ((e.KeyCode == Keys.Up) && (this.slvList.SelectedIndex > 0))
            {
                this.slvList.SelectedIndex--;
            }
            if (e.KeyCode == Keys.Enter)
            {
                this.dataGridView1.CancelEdit();
                this.dataGridView1.CurrentRow.Cells["SLV"].Value = this.slvList.SelectedItem;
                this.slvList.Visible = false;
                this.dataGridView1.EndEdit();
                this.dataGridView1.Focus();
            }
        }

        private string SlvListIndexOf(string slv)
        {
            decimal num;
            if (slv == "中外合作油气田")
            {
                return "0.05";
            }
            if (slv == "减按1.5%计算")
            {
                return "0.015";
            }
            if (slv == "免税")
            {
                return "0.00";
            }
            if ((slv.Trim() != "1.5%") && (slv.IndexOf(".") >= 0))
            {
                if (!decimal.TryParse(slv, out num))
                {
                    return "err";
                }
                return slv;
            }
            if (slv.IndexOf("%") >= 0)
            {
                if (!decimal.TryParse(slv.Substring(0, slv.Length - 1), out num))
                {
                    return "err";
                }
                return Convert.ToString((double)(Convert.ToDouble(slv.Substring(0, slv.Length - 1)) / 100.0));
            }
            if (slv == "")
            {
                return slv;
            }
            if (!decimal.TryParse(slv, out num))
            {
                return "err";
            }
            if (Convert.ToDouble(slv) >= 1.0)
            {
                return slv;
            }
            if ((slv != null) && !(slv == ""))
            {
                return Convert.ToString((double)(Convert.ToDouble(slv) / 100.0));
            }
            return "";
        }

        private void spmcBt_Click(object sender, EventArgs e)
        {
            CustomStyleDataGrid parent = (CustomStyleDataGrid)this.spmcBt.Parent;
            this.SelSpxx(parent, 0, 0);
        }

        public virtual void spmcBt_leave(object sender, EventArgs e)
        {
            CustomStyleDataGrid parent = (CustomStyleDataGrid)this.spmcBt.Parent;
            if (isFLBM && (parent.CurrentCell != null))
            {
                int rowIndex = parent.CurrentCell.RowIndex;
                Dictionary<SPXX, string> spxx = this.inv.GetSpxx(rowIndex);
                if (((spxx != null) && ((spxx[(SPXX)20] == null) || (spxx[(SPXX)20] == ""))) && (((spxx[(SPXX)0] != null) && (spxx[(SPXX)0] != "")) && ((spxx[(SPXX)0].Trim() != "详见对应正数发票及清单") && !spxx[(SPXX)0].Contains("折扣"))))
                {
                    string text = this.spmcBt.Text;
                    DataTable table = this._SpmcOnAutoCompleteDataSource(parent, text);
                    if ((table == null) || (table.Rows.Count == 0))
                    {
                        if ((spxx != null) && ((spxx[(SPXX)20] == null) || (spxx[(SPXX)20] == "")))
                        {
                            object[] objArray = new object[] { text, "", "", false };
                            object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddSP", objArray);
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
                            if ((table.Rows[i]["SPFL"].ToString() != "") && (text == table.Rows[i]["MC"].ToString()))
                            {
                                this.SetSpxx(new object[] { table.Rows[i]["BM"], table.Rows[i]["MC"], table.Rows[i]["JM"], table.Rows[i]["SLV"], table.Rows[i]["SPSM"], table.Rows[i]["GGXH"], table.Rows[i]["JLDW"], table.Rows[i]["DJ"], table.Rows[i]["HSJBZ"], table.Rows[i]["XTHASH"], table.Rows[i]["HYSY"], table.Rows[i]["SPFL"], table.Rows[i]["YHZC"], table.Rows[i]["SPFL_ZZSTSGL"], table.Rows[i]["YHZC_SLV"], table.Rows[i]["YHZCMC"] });
                                return;
                            }
                        }
                        object[] objArray3 = new object[] { table.Rows[0]["MC"], "", table.Rows[0]["BM"], true };
                        object[] objArray4 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddSP", objArray3);
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

        private void spmcBt_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CustomStyleDataGrid parent = (CustomStyleDataGrid)this.spmcBt.Parent;
            this.SelSpxx(parent, 1, 0);
        }

        private void spmcBt_OnAutoComplate(object sender, EventArgs e)
        {
            string spmc = this.spmcBt.Text.Trim();
            CustomStyleDataGrid parent = (CustomStyleDataGrid)this.spmcBt.Parent;
            DataTable table = this._SpmcOnAutoCompleteDataSource(parent, spmc);
            if (table != null)
            {
                this.spmcBt.DataSource=(table);
            }
        }

        private void spmcBt_OnSelectValue(object sender, EventArgs e)
        {
            Dictionary<string, string> dictionary = this.spmcBt.SelectDict;
            this.SetSpxx(new object[] { dictionary["BM"], dictionary["MC"], dictionary["JM"], dictionary["SLV"], dictionary["SPSM"], dictionary["GGXH"], dictionary["JLDW"], dictionary["DJ"], dictionary["HSJBZ"], dictionary["XTHASH"], dictionary["HYSY"], dictionary["SPFL"], dictionary["YHZC"], dictionary["SPFL_ZZSTSGL"], dictionary["YHZC_SLV"], dictionary["YHZCMC"] });
        }

        private void spmcBt_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CustomStyleDataGrid parent = (CustomStyleDataGrid)this.spmcBt.Parent;
                this.SelSpxx(parent, 1, 0);
            }
        }

        private void spmcBt_TextChanged(object sender, EventArgs e)
        {
            int index = this.dataGridView1.CurrentRow.Index;
            string text = this.spmcBt.Text;
            bool flag = false;
            flag = this.inv.SetSpmc(index, text);
            this.ShowDataGrid(this.inv.GetSpxx(index), index);
            if (!flag)
            {
                MessageManager.ShowMsgBox(this.inv.GetCode(), this.inv.Params);
            }
        }

        private void SqdAdd(List<object> SelectInfor)
        {
            this.fpdm = SelectInfor[0].ToString();
            this.fphm = SelectInfor[1].ToString();
            this.fplx = (SelectInfor[2].ToString() == "s") ? ((FPLX)0) : ((FPLX)2);
            this.SetSelectReason(SelectInfor[3].ToString());
            this.lab_fpdm.Text = SelectInfor[0].ToString();
            if (SelectInfor[1].ToString() == "")
            {
                this.lab_fphm.Text = SelectInfor[1].ToString();
            }
            else
            {
                this.lab_fphm.Text = SelectInfor[1].ToString().PadLeft(8, '0');
            }
            this.lab_fpzl.Text = (SelectInfor[2].ToString() == "s") ? "增值税专用发票" : "废旧物质发票";
            DateTime cardClock = base.TaxCardInstance.GetCardClock();
            this.lab_date.Text = ToolUtil.FormatDateTime(cardClock);
            this.lab_kpy.Text = UserInfo.Yhmc;
            this.sqdh = base.TaxCardInstance.GetInvControlNum().Trim() + cardClock.ToString("yyMMddHHmmss");
            this.lab_No.Text = this.sqdh;
            if (SelectInfor[4].ToString() == "0")
            {
                this.hzfw = false;
                this.rad_hzfw_n.Checked = true;
                this.rad_hzfw_y.Checked = false;
                byte[] buffer = CreateInvoiceTmp();
                Invoice.IsGfSqdFp_Static=true;
                this.inv = new Invoice(true, false, false, this.fplx, buffer, null);
                this.inv.IsGfSqdFp=true;
                Spxx spxx = new Spxx("", "", "0.17", "", "", "", false, this.inv.Zyfplx);
                this.AddRow(spxx);
                this.com_gfmc.Edit=0;
                this.com_gfsbh.Edit=0;
                this.com_gfmc.Text = base.TaxCardInstance.Corporation;
                this.com_gfsbh.Text = base.TaxCardInstance.TaxCode;
                this.inv.Gfmc=base.TaxCardInstance.Corporation;
                this.inv.Gfsh=base.TaxCardInstance.TaxCode;
            }
            else
            {
                Invoice.IsGfSqdFp_Static=false;
                this.slvList.Items.Clear();
                this.slvList.Items.AddRange(this.GetSqSlv().ToArray());
                if (this.slvList.Items.Count == 0)
                {
                    MessageManager.ShowMsgBox("INP-431305");
                    noslv = true;
                }
                else
                {
                    this.hzfw = base.TaxCardInstance.StateInfo.CompanyType != 0;
                    this.rad_hzfw_n.Checked = !this.hzfw;
                    this.rad_hzfw_y.Checked = this.hzfw;
                    this.rad_hzfw_n.Enabled = false;
                    this.rad_hzfw_y.Enabled = false;
                    this.com_xfmc.Edit=0;
                    this.com_xfsbh.Edit=0;
                    this.GetXfInfo(SelectInfor[2].ToString(), this.fpdm, this.fphm);
                }
            }
        }

        private bool SqdEidt()
        {
            this.GetSqdInfo(this.sqdh);
            if ((this.inv != null) && ((int)this.inv.Zyfplx == 11))
            {
                return false;
            }
            if (this.Radio_BuyerSQ.Checked)
            {
                this.com_gfmc.Edit=(0);
                this.com_gfsbh.Edit=(0);
            }
            else
            {
                this.com_xfmc.Edit=(0);
                this.com_xfsbh.Edit=(0);
                this.rad_hzfw_n.Enabled = false;
                this.rad_hzfw_y.Enabled = false;
            }
            return true;
        }

        private void SqdRead()
        {
            this.tool_chae.Visible = false;
            this.tool_bianji.Visible = false;
            this.tool_addRow.Visible = false;
            this.tool_insertRow.Visible = false;
            this.tool_DeleteRow.Visible = false;
            this.toolStripSeparator.Visible = false;
            this.GetSqdInfo(this.sqdh);
            this.com_gfmc.Edit=(0);
            this.com_gfsbh.Edit=(0);
            this.com_xfmc.Edit=(0);
            this.com_xfsbh.Edit=(0);
            this.dataGridView1.ReadOnly = true;
            this.rad_hzfw_n.Enabled = false;
            this.rad_hzfw_y.Enabled = false;
            this.txt_lxdh.ReadOnly = true;
            this.txt_sqly.ReadOnly = true;
        }

        private void SqdTianKai_Resize(object sender, EventArgs e)
        {
            this.btnSplit.Left = (this.pnlSqly.Width - this.btnSplit.Width) / 2;
            this.btnSplit.Top = ((this.pnlSqly.Top - this.btnSplit.Height) + 3) - 4;
        }

        private void SqdTianKai_Shown(object sender, EventArgs e)
        {
            this.btnSplit.Left = (this.pnlSqly.Width - this.btnSplit.Width) / 2;
            this.btnSplit.Top = (this.pnlSqly.Top - this.btnSplit.Height) + 3;
        }

        private void tool_AddGood_Click(object sender, EventArgs e)
        {
            object[] objArray = null;
            ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddSP", objArray);
        }

        private void tool_addRow_Click(object sender, EventArgs e)
        {
            string tempfirstslv = string.Empty;
            if (this.inv.IsGfSqdFp)
            {
                tempfirstslv = "0.17";
            }
            else
            {
                if (((this.tempfirstslv == "0.05") || (this.tempfirstslv == "0.050")) && ((int)this.inv.Zyfplx == 1))
                {
                    this.inv.SetZyfpLx((ZYFP_LX)1);
                    this.SetHysy(true);
                    this.hysy_flag = true;
                }
                if (this.tempfirstslv == "0.015")
                {
                    this.inv.SetZyfpLx((ZYFP_LX)10);
                    this.SetHysy(false);
                    this.hysy_flag = true;
                }
                tempfirstslv = this.tempfirstslv;
            }
            Spxx spxx = new Spxx("", "", ((this.inv.SLv == "") || (this.inv.SLv == null)) ? tempfirstslv : this.inv.SLv, "", "", "", false, this.inv.Zyfplx);
            this.AddRow(spxx);
        }

        private void tool_bianji_CheckedChanged(object sender, EventArgs e)
        {
            this.dataGridView1.ReadOnly = !this.tool_bianji.Checked;
        }

        private void tool_chae_Click(object sender, EventArgs e)
        {
            if (!this.tool_chae.Checked)
            {
                if ((this.inv.GetSpxxs().Count > 1) || this.inv.Qdbz)
                {
                    MessageManager.ShowMsgBox("INP-242187", new string[] { "清单发票或者明细行超过两行不能转化成差额税发票！" });
                }
                else
                {
                    if (this.inv.GetSpxxs().Count == 0)
                    {
                        string str = "0.17";
                        if (this.slvList.Items.Count > 0)
                        {
                            str = this.slvList.Items[0].ToString();
                            str = (double.Parse(str.Remove(str.Length - 1)) / 100.0).ToString();
                        }
                        this.inv.AddSpxx("", str, this.inv.Zyfplx);
                    }
                    List<Dictionary<SPXX, string>> spxxs = this.inv.GetSpxxs();
                    if (((spxxs != null) && (spxxs[0][0].ToString() != "")) && this.isXT(spxxs[0][0].ToString()))
                    {
                        MessageManager.ShowMsgBox("INP-242187", new string[] { "稀土商品不能填开差额税发票！" });
                    }
                    else
                    {
                        string str2 = spxxs[0][(SPXX)8];
                        if (((str2 == "0.05") && ((int)(int)this.inv.Zyfplx == 1)) || (str2 == "0.015"))
                        {
                            MessageManager.ShowMsgBox("INP-242187", new string[] { "特殊税率商品明细不能开具差额税发票！" });
                        }
                        else
                        {
                            ChaE_Tax tax = new ChaE_Tax();
                            if (tax.ShowDialog() == DialogResult.OK)
                            {
                                this.inv.Hsjbz=true;
                                this.hsjbz = true;
                                this.inv.SetZyfpLx((ZYFP_LX)11);
                                this.inv.SetSL(0, "");
                                this.inv.SetDj(0, "");
                                this.inv.SetJe(0, "");
                                string str3 = tax.kce.ToString("F2");
                                if (!str3.StartsWith("-"))
                                {
                                    str3 = "-" + str3;
                                }
                                this.inv.SetKce(0, str3);
                                this.dataGridView1.Columns["DJ"].HeaderText = "单价(含税)";
                                this.dataGridView1.Columns["JE"].HeaderText = "金额(含税)";
                                this.tool_hanshuiqiehuan.Checked = true;
                                this.ShowDataGrid(this.inv.GetSpxx(0), 0);
                                this.tool_chae.Checked = true;
                            }
                        }
                    }
                }
            }
            else if (this.inv.GetSpxxs().Count > 1)
            {
                MessageManager.ShowMsgBox("INP-242187", new string[] { "明细行超过两行的差额税发票不能转化成普通发票！" });
            }
            else
            {
                if (this.inv.GetSpxxs().Count == 1)
                {
                    this.inv.SetSL(0, "");
                    this.inv.SetDj(0, "");
                    this.inv.SetJe(0, "");
                    this.ShowDataGrid(this.inv.GetSpxx(0), 0);
                }
                this.inv.SetZyfpLx(0);
                this.tool_chae.Checked = false;
            }
        }

        private void tool_dayin_MouseDown(object sender, MouseEventArgs e)
        {
            this.CommitEditGrid();
            this.lab_No.Focus();
        }
        
        private void tool_daying_Click(object sender, EventArgs e)
        {
            if (this.Radio_BuyerSQ.Checked)
            {
                if (this.com_xfmc.Text.Trim() == "")
                {
                    MessageManager.ShowMsgBox("INP-431307");
                    return;
                }
                if (this.com_xfsbh.Text.Trim() == "")
                {
                    MessageManager.ShowMsgBox("INP-431308");
                    return;
                }
            }
            if (this.Radio_SellerSQ.Checked)
            {
                if (this.com_gfmc.Text.Trim() == "")
                {
                    MessageManager.ShowMsgBox("INP-431309");
                    return;
                }
                if (this.com_gfsbh.Text.Trim() == "")
                {
                    MessageManager.ShowMsgBox("INP-431310");
                    return;
                }
            }
            if (Convert.ToDouble(this.inv.GetHjJe()) == 0.0)
            {
                MessageManager.ShowMsgBox("INP-431304");
            }
            else if ((this.blueje > -1M) && (this.blueje < Math.Abs(Convert.ToDecimal(this.inv.GetHjJeNotHs()))))
            {
                MessageManager.ShowMsgBox("INP-431315");
            }
            else
            {
                if (!this.inv.IsGfSqdFp)
                {
                    if (!this.hysy_flag)
                    {
                        if (Math.Abs(Convert.ToDouble(this.inv.GetHjJeNotHs())) > base.TaxCardInstance.GetInvLimit(0))
                        {
                            MessageManager.ShowMsgBox("INP-431316");
                            return;
                        }
                    }
                    else if (Math.Abs(Convert.ToDouble(this.inv.GetHjJeNotHs())) > base.TaxCardInstance.GetInvLimit(0))
                    {
                        MessageManager.ShowMsgBox("INP-431316");
                        return;
                    }
                }
                else if (this.hysy_flag)
                {
                    if (Math.Abs(Convert.ToDouble(this.inv.GetHjJeNotHs())) > 99999999.99)
                    {
                        MessageManager.ShowMsgBox("INP-431316");
                        return;
                    }
                }
                else if (Math.Abs(Convert.ToDouble(this.inv.GetHjJeNotHs())) > 99999999.99)
                {
                    MessageManager.ShowMsgBox("INP-431316");
                    return;
                }
                if (this.Radio_BuyerSQ.Checked)
                {
                    string str = this.com_xfsbh.Text.Trim();
                    string str2 = new InvoiceHandler().CheckTaxCode(str, 0);
                    if (!str2.Equals("0000"))
                    {
                        MessageManager.ShowMsgBox(str2, new string[] { "销售方纳税人识别号" });
                        return;
                    }
                }
                this.inv.Gfmc=(this.com_gfmc.Text.Trim());
                this.inv.Gfsh=(this.com_gfsbh.Text.Trim());
                this.inv.Xfmc=(this.com_xfmc.Text.Trim());
                this.inv.Xfsh=(this.com_xfsbh.Text.Trim());
                if (this.SqdMxType != InitSqdMxType.Read)
                {
                    this.inv.Bz=("");
                    Fpxx fpData = this.inv.GetFpData();
                    if (fpData == null)
                    {
                        MessageManager.ShowMsgBox(this.inv.GetCode(), this.inv.Params);
                        return;
                    }
                    if (!this.inv.CheckFpData())
                    {
                        MessageManager.ShowMsgBox(this.inv.GetCode(), this.inv.Params);
                        return;
                    }
                    double num = 0.0;
                    if (!string.IsNullOrEmpty(fpData.sLv))
                    {
                        num = Convert.ToDouble(fpData.sLv);
                    }
                    else
                    {
                        num = -1.0;
                    }
                    HZFP_SQD model = new HZFP_SQD
                    {
                        SQDH = this.sqdh,
                        FPDM = this.lab_fpdm.Text
                    };
                    if (this.lab_fphm.Text.Trim() != "")
                    {
                        model.FPHM = Convert.ToInt32(this.lab_fphm.Text);
                    }
                    if (this.SqdMxType == InitSqdMxType.Add)
                    {
                        model.FPZL = (this.lab_fpzl.Text == "增值税专用发票") ? "s" : "c";
                    }
                    model.KPJH = base.TaxCardInstance.Machine;
                    if (this.SqdMxType == InitSqdMxType.Add)
                    {
                        model.REQNSRSBH = base.TaxCardInstance.TaxCode;
                        string invControlNum = base.TaxCardInstance.GetInvControlNum();
                        model.XXBBH = "";
                        model.XXBZT = "TZD0500";
                        model.XXBMS = "未上传";
                        model.JSPH = invControlNum.Trim();
                    }
                    if (this.SqdMxType == InitSqdMxType.Edit)
                    {
                        model.XXBBH = "";
                        model.XXBZT = "TZD0500";
                        model.XXBMS = "未上传";
                    }
                    DateTime time = Convert.ToDateTime(this.lab_date.Text);
                    model.TKRQ = time;
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
                    model.HJJE = Convert.ToDecimal(fpData.je);
                    model.HJSE = Convert.ToDecimal(fpData.se);
                    model.SL = num;
                    model.JBR = UserInfo.Yhmc;
                    string selectReason = this.GetSelectReason();
                    if (this.rad_hzfw_y.Checked)
                    {
                        selectReason = selectReason + "1";
                    }
                    else
                    {
                        selectReason = selectReason + "0";
                    }
                    model.SQXZ = selectReason;
                    model.SQLY = this.txt_sqly.Text.Trim();
                    model.SQRDH = this.txt_lxdh.Text.Trim();
                    model.BBBZ = "0";
                    model.YYSBZ = this.GetYYSBZ(fpData);
                    if (isFLBM && (this.codeInfoList.Count > 0))
                    {
                        model.FLBMBBBH = new SPFLService().GetMaxBMBBBH();
                    }
                    else if (FLBMqy && (lpbmbbbh != ""))
                    {
                        model.FLBMBBBH = new SPFLService().GetMaxBMBBBH();
                    }
                    else
                    {
                        model.FLBMBBBH = "";
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
                    List<HZFP_SQD_MX> models = new List<HZFP_SQD_MX>();
                    for (int i = 0; i < fpData.Mxxx.Count; i++)
                    {
                        Dictionary<SPXX, string> dictionary = fpData.Mxxx[i];
                        HZFP_SQD_MX item = new HZFP_SQD_MX
                        {
                            SQDH = model.SQDH,
                            MXXH = i,
                            JE = Convert.ToDecimal(dictionary[(SPXX)7])
                        };
                        if (dictionary[(SPXX)8] != "")
                        {
                            item.SLV = Convert.ToDouble(dictionary[(SPXX)8]);
                        }
                        else
                        {
                            item.SLV = -1.0;
                        }
                        if (dictionary[(SPXX)9] != "")
                        {
                            item.SE = Convert.ToDecimal(dictionary[(SPXX)9]);
                        }
                        item.SPMC = dictionary[(SPXX)0];
                        item.SPBH = dictionary[(SPXX)1];
                        item.SPSM = dictionary[(SPXX)2];
                        item.GGXH = dictionary[(SPXX)3];
                        item.JLDW = dictionary[(SPXX)4];
                        if (dictionary[(SPXX)6] != "")
                        {
                            item.SL = Convert.ToDouble(dictionary[(SPXX)6]);
                        }
                        if (dictionary[(SPXX)5] != "")
                        {
                            item.DJ = Convert.ToDecimal(dictionary[(SPXX)5]);
                        }
                        item.HSJBZ = dictionary[(SPXX)11] != "0";
                        item.FPHXZ = Convert.ToInt32(dictionary[(SPXX)10]);
                        item.XTHASH = dictionary[(SPXX)14];
                        if (isFLBM && (this.codeInfoList.Count > 0))
                        {
                            string flbm = this.codeInfoList[i].flbm;
                            if ((flbm != null) && (flbm != ""))
                            {
                                while (flbm.Length < 0x13)
                                {
                                    flbm = flbm + "0";
                                }
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
                this.lp_hysy = 0;
                new HZFPSQDPrint("0" + this.sqdh).Print(true);
                if (!this.InitSqdMxType_Read)
                {
                    base.Close();
                    if ((this.SqdMxType == InitSqdMxType.Add) && (DialogResult.OK == MessageManager.ShowMsgBox("INP-431377")))
                    {
                        new SqdInfoSelect().ShowDialog();
                    }
                }
            }
        }

        public void dayingbt()
        {
            if (!this.inv.IsGfSqdFp)
            {
                //if (!this.hysy_flag)
                //{
                //    if (Math.Abs(Convert.ToDouble(this.inv.GetHjJeNotHs())) > base.TaxCardInstance.GetInvLimit(0))
                //    {
                //        MessageManager.ShowMsgBox("INP-431316");
                //        return;
                //    }
                //}
                //else if (Math.Abs(Convert.ToDouble(this.inv.GetHjJeNotHs())) > base.TaxCardInstance.GetInvLimit(0))
                //{
                //    MessageManager.ShowMsgBox("INP-431316");
                //    return;
                //}
            }
            else if (this.hysy_flag)
            {
                if (Math.Abs(Convert.ToDouble(this.inv.GetHjJeNotHs())) > 99999999.99)
                {
                    MessageManager.ShowMsgBox("INP-431316");
                    return;
                }
            }
            else if (Math.Abs(Convert.ToDouble(this.inv.GetHjJeNotHs())) > 99999999.99)
            {
                MessageManager.ShowMsgBox("INP-431316");
                return;
            }
            if (this.Radio_BuyerSQ.Checked)
            {
                string str = this.inv.Xfsh.Trim();
                string str2 = new InvoiceHandler().CheckTaxCode(str, 0);
                if (!str2.Equals("0000"))
                {
                    MessageManager.ShowMsgBox(str2, new string[] { "销售方纳税人识别号" });
                    return;
                }
            }            
            if (this.SqdMxType != InitSqdMxType.Read)
            {
                this.inv.Bz = ("");
                Fpxx fpData = this.inv.GetFpData();
                if (fpData == null)
                {
                    MessageManager.ShowMsgBox(this.inv.GetCode(), this.inv.Params);
                    return;
                }
                if (!this.inv.CheckFpData())
                {
                    MessageManager.ShowMsgBox(this.inv.GetCode(), this.inv.Params);
                    return;
                }
                double num = 0.0;
                if (!string.IsNullOrEmpty(fpData.sLv))
                {
                    num = Convert.ToDouble(fpData.sLv);
                }
                else
                {
                    num = -1.0;
                }
                HZFP_SQD model = new HZFP_SQD
                {
                    SQDH = this.sqdh,
                    FPDM = this.lab_fpdm.Text
                };
                if (this.lab_fphm.Text.Trim() != "")
                {
                    model.FPHM = Convert.ToInt32(this.lab_fphm.Text);
                }
                if (this.SqdMxType == InitSqdMxType.Add)
                {
                    model.FPZL = (this.lab_fpzl.Text == "增值税专用发票") ? "s" : "c";
                }
                model.KPJH = base.TaxCardInstance.Machine;
                if (this.SqdMxType == InitSqdMxType.Add)
                {
                    model.REQNSRSBH = base.TaxCardInstance.TaxCode;
                    string invControlNum = base.TaxCardInstance.GetInvControlNum();
                    model.XXBBH = "";
                    model.XXBZT = "TZD0500";
                    model.XXBMS = "未上传";
                    model.JSPH = invControlNum.Trim();
                }
                if (this.SqdMxType == InitSqdMxType.Edit)
                {
                    model.XXBBH = "";
                    model.XXBZT = "TZD0500";
                    model.XXBMS = "未上传";
                }
                DateTime time = Convert.ToDateTime(this.lab_date.Text);
                model.TKRQ = time;
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
                model.HJJE = Convert.ToDecimal(fpData.je);
                model.HJSE = Convert.ToDecimal(fpData.se);
                model.SL = num;
                model.JBR = UserInfo.Yhmc;
                string selectReason = this.GetSelectReason();
                if (this.rad_hzfw_y.Checked)
                {
                    selectReason = selectReason + "1";
                }
                else
                {
                    selectReason = selectReason + "0";
                }
                model.SQXZ = selectReason;
                model.SQLY = this.txt_sqly.Text.Trim();
                model.SQRDH = this.txt_lxdh.Text.Trim();
                model.BBBZ = "0";
                model.YYSBZ = this.GetYYSBZ(fpData);
                if (isFLBM && (this.codeInfoList.Count > 0))
                {
                    model.FLBMBBBH = new SPFLService().GetMaxBMBBBH();
                }
                else if (FLBMqy && (lpbmbbbh != ""))
                {
                    model.FLBMBBBH = new SPFLService().GetMaxBMBBBH();
                }
                else
                {
                    model.FLBMBBBH = "";
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
                List<HZFP_SQD_MX> models = new List<HZFP_SQD_MX>();
                for (int i = 0; i < fpData.Mxxx.Count; i++)
                {
                    Dictionary<SPXX, string> dictionary = fpData.Mxxx[i];
                    HZFP_SQD_MX item = new HZFP_SQD_MX
                    {
                        SQDH = model.SQDH,
                        MXXH = i,
                        JE = Convert.ToDecimal(dictionary[(SPXX)7])
                    };
                    if (dictionary[(SPXX)8] != "")
                    {
                        item.SLV = Convert.ToDouble(dictionary[(SPXX)8]);
                    }
                    else
                    {
                        item.SLV = -1.0;
                    }
                    if (dictionary[(SPXX)9] != "")
                    {
                        item.SE = Convert.ToDecimal(dictionary[(SPXX)9]);
                    }
                    item.SPMC = dictionary[(SPXX)0];
                    item.SPBH = dictionary[(SPXX)1];
                    item.SPSM = dictionary[(SPXX)2];
                    item.GGXH = dictionary[(SPXX)3];
                    item.JLDW = dictionary[(SPXX)4];
                    if (dictionary[(SPXX)6] != "")
                    {
                        item.SL = Convert.ToDouble(dictionary[(SPXX)6]);
                    }
                    if (dictionary[(SPXX)5] != "")
                    {
                        item.DJ = Convert.ToDecimal(dictionary[(SPXX)5]);
                    }
                    item.HSJBZ = dictionary[(SPXX)11] != "0";
                    item.FPHXZ = Convert.ToInt32(dictionary[(SPXX)10]);
                    item.XTHASH = dictionary[(SPXX)14];
                    if (isFLBM && (this.codeInfoList.Count > 0))
                    {
                        string flbm = this.codeInfoList[i].flbm;
                        if ((flbm != null) && (flbm != ""))
                        {
                            while (flbm.Length < 0x13)
                            {
                                flbm = flbm + "0";
                            }
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
            this.lp_hysy = 0;
        }

        private void tool_DeleteRow_Click(object sender, EventArgs e)
        {
            if (this.SqdMxType == InitSqdMxType.Add)
            {
                if (this.dataGridView1.SelectedCells.Count > 0)
                {
                    int rowIndex = this.dataGridView1.SelectedCells[0].RowIndex;
                    if (MessageManager.ShowMsgBox("INP-431317") == DialogResult.OK)
                    {
                        this.dataGridView1.Rows.Remove(this.dataGridView1.Rows[rowIndex]);
                        this.inv.DelSpxx(rowIndex);
                        if (isFLBM)
                        {
                            this.codeInfoList.RemoveAt(rowIndex);
                        }
                    }
                }
                if (this.dataGridView1.Rows.Count == 0)
                {
                    if ((int)(int)this.inv.Zyfplx == 1)
                    {
                        Spxx spxx = new Spxx("", "", "0.05", "", "", "", false, this.inv.Zyfplx);
                        this.AddRow(spxx);
                    }
                    else if ((int)(int)this.inv.Zyfplx == 10)
                    {
                        Spxx spxx2 = new Spxx("", "", "0.015", "", "", "", false, this.inv.Zyfplx);
                        this.AddRow(spxx2);
                    }
                    else
                    {
                        if (this.inv.IsGfSqdFp)
                        {
                            Spxx spxx3 = new Spxx("", "", "0.17", "", "", "", false, this.inv.Zyfplx);
                            this.AddRow(spxx3);
                        }
                        else
                        {
                            if (((this.tempfirstslv == "0.05") || (this.tempfirstslv == "0.050")) && ((int)(int)this.inv.Zyfplx == 1))
                            {
                                this.inv.SetZyfpLx((ZYFP_LX)1);
                                this.SetHysy(true);
                                this.hysy_flag = true;
                            }
                            if (this.tempfirstslv == "0.015")
                            {
                                this.inv.SetZyfpLx((ZYFP_LX)10);
                                this.SetHysy(false);
                                this.hysy_flag = true;
                            }
                            Spxx spxx4 = new Spxx("", "", this.tempfirstslv, "", "", "", false, this.inv.Zyfplx);
                            this.AddRow(spxx4);
                        }
                        if (this.hysy_flag)
                        {
                            this.hysy_flag = false;
                            PropertyUtil.SetValue("SQD-HSJBZ", this.hsjbz ? "1" : "0");
                            this.dataGridView1.Columns["DJ"].HeaderText = "单价(含税)";
                            this.dataGridView1.Columns["JE"].HeaderText = "金额(含税)";
                            this.tool_hanshuiqiehuan.Enabled = true;
                            this.tool_hanshuiqiehuan.Checked = true;
                        }
                    }
                }
            }
            else
            {
                if (this.inv.GetSpxxs().Count == 1)
                {
                    MessageManager.ShowMsgBox("INP-431318");
                    return;
                }
                if (this.inv.GetSpxxs().Count > 1)
                {
                    int index = this.dataGridView1.SelectedCells[0].RowIndex;
                    if (MessageManager.ShowMsgBox("INP-431317") == DialogResult.OK)
                    {
                        this.dataGridView1.Rows.Remove(this.dataGridView1.Rows[index]);
                        this.inv.DelSpxx(index);
                        if (isFLBM)
                        {
                            this.codeInfoList.RemoveAt(index);
                        }
                    }
                }
            }
            this.lab_je.Text = "￥" + this.inv.GetHjJe();
            this.lab_se.Text = "￥" + this.inv.GetHjSe();
            if (this.dataGridView1.RowCount > 0)
            {
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0].Cells["DW"];
            }
        }

        private void tool_geshi_Click(object sender, EventArgs e)
        {
            this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGrid1").SetColumnStyles(this.xmlComponentLoader1.XMLPath, this);
        }

        private void tool_hanshuiqiehuan_CheckedChanged(object sender, EventArgs e)
        {
            if ((this.inv.Fplx != 0) || ((int)this.inv.Zyfplx != 1))
            {
                this.hsjbz = this.tool_hanshuiqiehuan.Checked;
                this.inv.Hsjbz = this.hsjbz;
                PropertyUtil.SetValue("SQD-HSJBZ", this.hsjbz ? "1" : "0");
                this.dataGridView1.Columns["DJ"].HeaderText = this.hsjbz ? "单价(含税)" : "单价(不含税)";
                this.dataGridView1.Columns["JE"].HeaderText = this.hsjbz ? "金额(含税)" : "金额(不含税)";
                foreach (DataGridViewRow row in (IEnumerable)this.dataGridView1.Rows)
                {
                    row.Cells["DJ"].Value = this.inv.GetSpxx(row.Index)[(SPXX)5];
                    row.Cells["JE"].Value = this.inv.GetSpxx(row.Index)[(SPXX)7];
                    if (!string.IsNullOrEmpty(this.inv.GetSpxx(row.Index)[(SPXX)7]))
                    {
                        double num = Convert.ToDouble(this.inv.GetSpxx(row.Index)[(SPXX)7]);
                        row.Cells["JE"].Value = string.Format("{0:0.00}", num);
                    }
                    else
                    {
                        row.Cells["JE"].Value = this.inv.GetSpxx(row.Index)[(SPXX)7];
                    }
                }
                this.lab_je.Text = "￥" + this.inv.GetHjJe();
                this.lab_se.Text = "￥" + this.inv.GetHjSe();
            }
        }

        private void tool_tongji_Click(object sender, EventArgs e)
        {
            this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGrid1").Statistics(this.dataGridView1);
        }

        private void tool_tuichu_Click(object sender, EventArgs e)
        {
            this.spmcBt.Leave -= new EventHandler(this.spmcBt_leave);
            this.dataGridView1.CancelEdit();
            base.Close();
            this.spmcBt.Leave += new EventHandler(this.spmcBt_leave);
        }

        private void txt_lxdh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\b")
            {
                e.Handled = false;
            }
            else if (ToolUtil.GetBytes(this.txt_lxdh.Text).Length >= 0x18)
            {
                e.Handled = true;
            }
        }

        private void txt_sqly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\b")
            {
                e.Handled = false;
            }
            else if (ToolUtil.GetBytes(this.txt_sqly.Text).Length >= 200)
            {
                e.Handled = true;
            }
        }

        public void Update_SP(object[] spxx)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
            dictionary.Add("BM", spxx[11].ToString());
            ArrayList list = baseDAOSQLite.querySQL("aisino.Fwkp.Hzfp.SelectYHZCS", dictionary);
            spxx[12] = "否";
            bool ishysy = spxx[10].ToString() == "True";
            foreach (Dictionary<string, object> dictionary2 in list)
            {
                string[] strArray = dictionary2["ZZSTSGL"].ToString().Split(new string[] { "、", ",", "，" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (strArray[i] == "1.5%_5%")
                    {
                        strArray[i] = "1.5%";
                    }
                    if (this.yhzc_contain_slv(strArray[i], spxx[3].ToString(), false, ishysy))
                    {
                        spxx[12] = "是";
                        spxx[15] = strArray[i];
                        break;
                    }
                }
            }
        }

        public bool yhzc_contain_slv(string yhzc, string slv, bool flag, bool ishysy)
        {
            string str = "aisino.Fwkp.Hzfp.SelectYhzcs";
            IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            ArrayList list = baseDAOSQLite.querySQL(str, dictionary);
            if (ishysy)
            {
                foreach (Dictionary<string, object> dictionary2 in list)
                {
                    if ((dictionary2["YHZCMC"].ToString() == yhzc) && (dictionary2["SLV"].ToString() == ""))
                    {
                        return true;
                    }
                }
                return false;
            }
            if ((slv == "免税") || (slv == "不征税"))
            {
                slv = "0%";
            }
            else if (!flag)
            {
                slv = ((double.Parse(slv) * 100.0)).ToString() + "%";
            }
            foreach (Dictionary<string, object> dictionary3 in list)
            {
                string[] source = dictionary3["SLV"].ToString().Split(new string[] { "、", ",", "，" }, StringSplitOptions.RemoveEmptyEntries);
                if ((dictionary3["YHZCMC"].ToString() == yhzc) && (dictionary3["SLV"].ToString() == ""))
                {
                    return true;
                }
                if ((dictionary3["YHZCMC"].ToString() == yhzc) && source.Contains<string>(slv))
                {
                    return true;
                }
            }
            return false;
        }

        public List<string> yhzc2slv(string yhzc)
        {
            List<string> list = new List<string>();
            string str = "aisino.Fwkp.Hzfp.SelectYhzcs";
            IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (Dictionary<string, object> dictionary2 in baseDAOSQLite.querySQL(str, dictionary))
            {
                if (dictionary2["YHZCMC"].ToString() == yhzc)
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
            return list;
        }

        public string sqdh { get; set; }
    }
}

