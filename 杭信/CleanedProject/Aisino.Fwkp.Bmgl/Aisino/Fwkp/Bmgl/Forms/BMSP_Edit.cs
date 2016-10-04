namespace Aisino.Fwkp.Bmgl.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Bmgl.BLLSys;
    using Aisino.Fwkp.Bmgl.Common;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class BMSP_Edit : BMEditBase
    {
        private AisinoLBL aisinoLBL1;
        private AisinoLBL aisinoLBL2;
        private AisinoLBL aisinoLBL3;
        private AisinoLBL aisinoLBL4;
        private AisinoCMB chbXTHide;
        private AisinoCMB comboBoxHSJBZ;
        private AisinoCMB comboBoxKJM;
        private AisinoCMB comboBoxSLV;
        private AisinoCMB comboBoxYHZC;
        private AisinoCMB comboBoxYHZCMC;
        private AisinoMultiCombox comBoxSPFL;
        private AisinoMultiCombox comBoxSPSM;
        private IContainer components;
        private bool IsXT;
        private AisinoLBL label11;
        private AisinoLBL label13;
        private AisinoLBL label14;
        private AisinoLBL label3;
        private AisinoLBL label4;
        private AisinoLBL label5;
        private AisinoLBL label6;
        private AisinoLBL label7;
        private AisinoLBL label9;
        private AisinoLBL labelKJM;
        private AisinoLBL lblXTHide;
        public string retCode;
        private AisinoTXT SPFLMC;
        private static BLL.BMSPManager splogical = new BLL.BMSPManager();
        private BLL.BMSPManager spManager;
        private BMSPModel spModel;
        private bool SucDialog;
        private string SuggestMC;
        private List<string> taxRate;
        private AisinoTXT textBoxDJ;
        private AisinoTXT textBoxGGXH;
        private AisinoTXT textBoxJLDW;
        private AisinoTXT textBoxXSSRKM;
        private AisinoTXT textBoxXSTHKM;
        private AisinoTXT textBoxYJZZSKM;
        private string xtHash;
        private List<string> YHZCMC;

        public BMSP_Edit()
        {
            this.spModel = new BMSPModel();
            this.xtHash = string.Empty;
            this.retCode = string.Empty;
            this.SuggestMC = string.Empty;
            this.SucDialog = true;
            this.YHZCMC = new List<string>();
            this.taxRate = new List<string>();
            this.spManager = new BLL.BMSPManager();
            this.Initialize();
        }

        public BMSP_Edit(string BM, bool Isupdate) : base(BM, Isupdate)
        {
            this.spModel = new BMSPModel();
            this.xtHash = string.Empty;
            this.retCode = string.Empty;
            this.SuggestMC = string.Empty;
            this.SucDialog = true;
            this.YHZCMC = new List<string>();
            this.taxRate = new List<string>();
            this.spManager = new BLL.BMSPManager();
            this.Initialize();
            this.InitData();
        }

        public BMSP_Edit(string SJBM, object Father) : base(SJBM, Father)
        {
            this.spModel = new BMSPModel();
            this.xtHash = string.Empty;
            this.retCode = string.Empty;
            this.SuggestMC = string.Empty;
            this.SucDialog = true;
            this.YHZCMC = new List<string>();
            this.taxRate = new List<string>();
            this.spManager = new BLL.BMSPManager();
            this.Initialize();
            this.InitData();
        }

        public BMSP_Edit(string BM, string MC, bool Isupdate, bool sucdialog) : this(BM, Isupdate)
        {
            this.SuggestMC = MC;
            this.SucDialog = sucdialog;
        }

        private void Add()
        {
            this.InModel();
            string str = splogical.AddMerchandise(this.spModel);
            switch (str)
            {
                case "0":
                    this.retCode = this.spModel.BM;
                    if (this.SucDialog)
                    {
                        MessageManager.ShowMsgBox("INP-235301");
                    }
                    base.DialogResult = DialogResult.OK;
                    base.Close();
                    return;

                case "e1":
                    MessageManager.ShowMsgBox("INP-235108");
                    return;

                case "e2":
                    MessageManager.ShowMsgBox("INP-235110");
                    return;

                case "e3":
                    MessageManager.ShowMsgBox("INP-235133");
                    return;

                case "e4":
                    MessageManager.ShowMsgBox("INP-235134");
                    break;
            }
            if (str == "e4")
            {
                MessageManager.ShowMsgBox("INP-235134");
            }
            else
            {
                MessageManager.ShowMsgBox("INP-235302");
            }
        }

        private void addevent(bool flag)
        {
            if (flag)
            {
                this.comboBoxYHZCMC.SelectedIndexChanged += new EventHandler(this.comboBoxYHZCMC_SelectedIndexChanged);
                this.comboBoxSLV.SelectedIndexChanged += new EventHandler(this.comboBoxSLV_SelectedIndexChanged);
            }
            else
            {
                this.comboBoxYHZCMC.SelectedIndexChanged -= new EventHandler(this.comboBoxYHZCMC_SelectedIndexChanged);
                this.comboBoxSLV.SelectedIndexChanged -= new EventHandler(this.comboBoxSLV_SelectedIndexChanged);
            }
        }

        private void AdjustComboBoxYHZC()
        {
            if (Flbm.IsYM())
            {
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.GetSLV_BY_BM", new object[] { this.comBoxSPFL.Text.Trim() });
                if (((objArray != null) && ((objArray[0] as DataTable).Rows.Count > 0)) && ((objArray[0] as DataTable).Rows[0]["ZZSTSGL"].ToString() != ""))
                {
                    this.comboBoxYHZC.Enabled = true;
                }
                else
                {
                    this.comboBoxYHZC.SelectedIndex = 1;
                    this.comboBoxYHZC.Enabled = false;
                }
            }
            else
            {
                this.comboBoxYHZC.Text = "否";
                this.comboBoxYHZCMC.Items.Add("");
                this.comboBoxYHZCMC.Text = "";
                this.comboBoxYHZCMC.Enabled = false;
            }
        }

        private void AdjustComboBoxYHZCMC()
        {
            if (this.comboBoxYHZC.Enabled && (this.comboBoxYHZC.Text.Trim() == "是"))
            {
                this.comboBoxYHZCMC.Enabled = true;
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.GetSLV_BY_BM", new object[] { this.comBoxSPFL.Text.Trim() });
                if ((objArray != null) && (objArray.Length > 0))
                {
                    string[] strArray = (objArray[0] as DataTable).Rows[0]["ZZSTSGL"].ToString().Split(new char[] { '，', '、', '；', ',', ';' });
                    if (strArray.Length > 0)
                    {
                        string str = this.comboBoxYHZCMC.Text;
                        this.comboBoxYHZCMC.Items.Clear();
                        foreach (string str2 in strArray)
                        {
                            this.comboBoxYHZCMC.Items.Add(str2);
                            if (str == str2)
                            {
                                this.comboBoxYHZCMC.Text = str;
                            }
                        }
                    }
                }
                string text = this.comboBoxSLV.Text;
                if (this.comboBoxYHZCMC.Text == "")
                {
                    if (!this.comboBoxSLV.Items.Contains(""))
                    {
                        this.comboBoxSLV.Items.Add("");
                        this.comboBoxSLV.Text = "";
                    }
                    else
                    {
                        this.comboBoxSLV.Text = "";
                    }
                }
                else
                {
                    this.comboBoxSLV.Text = text;
                }
            }
            else
            {
                this.comboBoxYHZCMC.Text = "";
                this.comboBoxYHZCMC.Enabled = false;
            }
        }

        private void AdjustSYSL()
        {
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.GetSLV_BY_BM", new object[] { this.comBoxSPFL.Text.Trim() });
            if (((objArray == null) || ((objArray[0] as DataTable).Rows.Count <= 0)) || ((objArray[0] as DataTable).Rows[0]["SLV"].ToString() == ""))
            {
                string text = this.comboBoxSLV.Text;
                this.comboBoxSLV.Items.Clear();
                foreach (string str2 in this.taxRate)
                {
                    this.comboBoxSLV.Items.Add(this.ChangeTaxRate(str2));
                }
                if (!this.comboBoxSLV.Items.Contains(text))
                {
                    this.comboBoxSLV.Items.Add(this.ChangeTaxRate(text));
                    this.comboBoxSLV.Text = text;
                }
                else
                {
                    this.comboBoxSLV.Text = text;
                }
            }
        }

        protected override void BMEditBase_Load(object sender, EventArgs e)
        {
            try
            {
                this.aisinoLBL1.Visible = Flbm.IsYM();
                this.aisinoLBL2.Visible = Flbm.IsYM();
                this.comBoxSPFL.Visible = Flbm.IsYM();
                this.comboBoxYHZC.Visible = Flbm.IsYM();
                this.aisinoLBL3.Visible = Flbm.IsYM();
                this.SPFLMC.Visible = Flbm.IsYM();
                this.aisinoLBL4.Visible = Flbm.IsYM();
                this.comboBoxYHZCMC.Visible = Flbm.IsYM();
                List<double> zPSQSlv = this.GetZPSQSlv();
                if (zPSQSlv != null)
                {
                    foreach (double num in zPSQSlv)
                    {
                        decimal d = Math.Round(decimal.Parse(num.ToString()), 3);
                        if (d == 0.015M)
                        {
                            this.taxRate.Add("减按1.5%计算");
                            this.taxRate.Remove("0.015");
                        }
                        else if (d == 1.05M)
                        {
                            this.taxRate.Add("中外合作油气田");
                            this.taxRate.Remove("1.05");
                        }
                        else
                        {
                            if ((d == 0.0M) && !Flbm.IsYM())
                            {
                                this.taxRate.Add("免税");
                                this.taxRate.Remove("0.00");
                            }
                            else
                            {
                                this.taxRate.Add(num.ToString("f2"));
                            }
                        }
                        //switch (num)
                        //{
                        //    case 1.05:
                        //        this.taxRate.Add("中外合作油气田");
                        //        this.taxRate.Remove("1.05");
                        //        break;

                        //    case 0.015:
                        //        this.taxRate.Add("减按1.5%计算");
                        //        this.taxRate.Remove("0.015");
                        //        break;

                        //    default:
                        //        if ((num == 0.0) && !Flbm.IsYM())
                        //        {
                        //            this.taxRate.Add("免税");
                        //            this.taxRate.Remove("0.00");
                        //        }
                        //        else
                        //        {
                        //            this.taxRate.Add(num.ToString("f2"));
                        //        }
                        //        break;
                        //}
                    }
                }
                this.taxRate.Sort();
                foreach (string str in this.taxRate)
                {
                    this.comboBoxSLV.Items.Add(this.ChangeTaxRate(str));
                }
                this.comboBoxKJM.DropDownStyle = ComboBoxStyle.Simple;
                this.chbXTHide.Items.AddRange(new string[] { "是", "否" });
                this.chbXTHide.SelectedIndex = 1;
                if (base.isUpdate)
                {
                    this.LoadData(base._bm);
                    base._sjbm = this.spModel.SJBM;
                    base.textBoxSJBM.SelectBM = this.spModel.SJBM;
                    base.toolStripBtnContinue.Visible = false;
                    if (!string.IsNullOrEmpty(this.spModel.XTHASH))
                    {
                        this.IsXT = true;
                        this.xtHash = this.spModel.XTHASH;
                        base.textBoxSJBM.Enabled = false;
                        base.textBoxBM.Enabled = false;
                        base.textBoxWaitMC.Enabled = false;
                    }
                }
                else
                {
                    this.comboBoxHSJBZ.SelectedIndex = 1;
                    this.comboBoxHSJBZ.FormatString = "{0:C3}";
                    this.comboBoxYHZC.SelectedIndex = 1;
                    base.textBoxSJBM.SelectBM = base._sjbm;
                    base.SuggestBM = splogical.TuiJianBM(base._sjbm);
                    if (base.SuggestBM == "NoNode")
                    {
                        MessageManager.ShowMsgBox("INP-235121");
                        base.Close();
                        return;
                    }
                    if (base.SuggestBM.Length > 0x10)
                    {
                        MessageManager.ShowMsgBox("INP-235111");
                        base.Close();
                        return;
                    }
                    base.textBoxBM.Text = base.SuggestBM;
                    base.textBoxWaitMC.Text = this.SuggestMC;
                }
                if (this.IsXT && this.comboBoxSLV.Items.Contains("中外合作油气田"))
                {
                    this.comboBoxSLV.Items.Remove("中外合作油气田");
                }
                base.textBoxBM.Select(base.textBoxBM.Text.Length, 0);
                base.textBoxSJBM.TreeLoad();
                if (Flbm.IsYM())
                {
                    this.AdjustComboBoxYHZC();
                    this.AdjustComboBoxYHZCMC();
                    this.AdjustSYSL();
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }

        private string ChangeTaxRate(string rate)
        {
            string str = string.Empty;
            try
            {
                double result = 0.0;
                if (double.TryParse(rate, out result))
                {
                    double num2 = result * 100.0;
                    return (num2.ToString() + "%");
                }
                return rate;
            }
            catch (Exception)
            {
                str = string.Empty;
            }
            return str;
        }

        private void comboBoxSLV_Leave(object sender, EventArgs e)
        {
            if ((this.comboBoxSLV.Text != "System.Data.DataRowView") && (this.comboBoxSLV.Text != ""))
            {
                this.comboBoxSLV_Validat();
            }
        }

        private void comboBoxSLV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Flbm.IsYM())
            {
                this.comboBoxYHZCMC.SelectedIndexChanged -= new EventHandler(this.comboBoxYHZCMC_SelectedIndexChanged);
                if ("是" == this.comboBoxYHZC.SelectedItem.ToString())
                {
                    DAL.BMSPFLManager manager = new DAL.BMSPFLManager();
                    string str = manager.GetSPFLSLVBYMC(this.comboBoxYHZCMC.Text.Trim()).Replace("1.5%_5%", "1.5%");
                    string[] source = str.Split(new char[] { '，', '、', '；', ',', ';' });
                    if (str != "")
                    {
                        string text = this.comboBoxSLV.Text;
                        string str3 = this.comboBoxSLV.Text;
                        if ((this.comboBoxSLV.Text.Trim() == "免税") || (this.comboBoxSLV.Text.Trim() == "不征税"))
                        {
                            str3 = "0%";
                        }
                        if (this.comboBoxSLV.Text.Trim() == "减按1.5%计算")
                        {
                            str3 = "1.5%";
                        }
                        bool str3Found = false;
                        foreach (string strTmp in source)
                            if (strTmp == str3) str3Found = true;
                        if (!str3Found)
                        {
                            this.comboBoxYHZC.Text = "否";
                            this.comboBoxYHZCMC.Items.Add("");
                            this.comboBoxYHZCMC.Text = "";
                        }
                        //if (!source.Contains<string>(str3))
                        //{
                        //    this.comboBoxYHZC.Text = "否";
                        //    this.comboBoxYHZCMC.Items.Add("");
                        //    this.comboBoxYHZCMC.Text = "";
                        //}
                        if ((this.comboBoxSLV.Text == "0%") && ((this.comboBoxYHZCMC.Text == "免税") || (this.comboBoxYHZCMC.Text == "不征税")))
                        {
                            this.comboBoxYHZC.Text = "否";
                            this.comboBoxYHZCMC.Items.Add("");
                            this.comboBoxYHZCMC.Text = "";
                        }
                        this.comboBoxSLV.Text = text;
                    }
                }
                this.comboBoxYHZCMC.SelectedIndexChanged += new EventHandler(this.comboBoxYHZCMC_SelectedIndexChanged);
            }
        }

        private bool comboBoxSLV_Validat()
        {
            decimal num;
            bool flag = true;
            if (this.comboBoxSLV.Text.Trim() == "中外合作油气田")
            {
                if (this.IsXT)
                {
                    if (this.comboBoxSLV.Items.Count > 0)
                    {
                        this.comboBoxSLV.SelectedIndex = 0;
                    }
                    else
                    {
                        this.comboBoxSLV.Text = "";
                    }
                    MessageBoxHelper.Show("稀土商品税率不能为中外合作油气田税率！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.comboBoxSLV.Focus();
                    return false;
                }
                this.comboBoxHSJBZ.SelectedItem = "是";
                this.comboBoxHSJBZ.Enabled = false;
            }
            else
            {
                this.comboBoxHSJBZ.Enabled = true;
            }
            if (((this.comboBoxSLV.Text.Trim() == "中外合作油气田") || (this.comboBoxSLV.Text.Trim() == "免税")) || ((this.comboBoxSLV.Text.Trim() == "不征税") || (this.comboBoxSLV.Text.Trim() == "减按1.5%计算")))
            {
                return true;
            }
            if (!this.comboBoxSLV.Text.Contains("%"))
            {
                if (this.comboBoxSLV.Text == "")
                {
                    MessageBoxHelper.Show("税率不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.comboBoxSLV.Focus();
                    return false;
                }
                string pattern = "^[0-9]*.?[0-9]+$";
                Regex regex = new Regex(pattern);
                if (!regex.IsMatch(this.comboBoxSLV.Text.Trim()))
                {
                    if (this.comboBoxSLV.Items.Count <= 0)
                    {
                        this.comboBoxSLV.Text = "";
                    }
                    MessageBoxHelper.Show("税率应为小数或百分数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.comboBoxSLV.Focus();
                    return false;
                }
                double num2 = double.Parse(this.comboBoxSLV.Text.Trim());
                if ((num2 < 0.0) || (num2 > 100.0))
                {
                    if (this.comboBoxSLV.Items.Count > 0)
                    {
                        this.comboBoxSLV.SelectedIndex = 0;
                    }
                    else
                    {
                        this.comboBoxSLV.Text = "";
                    }
                    MessageBoxHelper.Show("税率不合法！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.comboBoxSLV.Focus();
                    return false;
                }
                if ((num2 > 0.0) && (num2 <= 1.0))
                {
                    if ((num2 > 0.0) && (num2 < 0.01))
                    {
                        this.comboBoxSLV.Text = "0%";
                        return true;
                    }
                    num2 *= 100.0;
                    this.comboBoxSLV.Text = num2.ToString("F0") + "%";
                    return true;
                }
                this.comboBoxSLV.Text = num2.ToString("F0") + "%";
                return flag;
            }
            if (!decimal.TryParse(this.comboBoxSLV.Text.Trim(new char[] { '%' }), out num))
            {
                if (this.comboBoxSLV.Items.Count > 0)
                {
                    this.comboBoxSLV.SelectedIndex = 0;
                }
                else
                {
                    this.comboBoxSLV.Text = "";
                }
                MessageBoxHelper.Show("税率应为小数或百分数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.comboBoxSLV.Focus();
                return false;
            }
            if ((num < 0M) || (num > 100M))
            {
                if (this.comboBoxSLV.Items.Count > 0)
                {
                    this.comboBoxSLV.SelectedIndex = 0;
                }
                else
                {
                    this.comboBoxSLV.Text = "";
                }
                MessageBoxHelper.Show("税率不合法！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.comboBoxSLV.Focus();
                return false;
            }
            if ((num > 0M) && (num < 1M))
            {
                this.comboBoxSLV.Text = "0%";
                return flag;
            }
            this.comboBoxSLV.Text = num.ToString("F0") + "%";
            return flag;
        }

        private void comboBoxYHZC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ("是" == this.comboBoxYHZC.SelectedItem.ToString())
            {
                this.addevent(false);
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.GetSLV_BY_BM", new object[] { this.comBoxSPFL.Text.Trim() });
                string[] strArray = (objArray[0] as DataTable).Rows[0]["SLV"].ToString().Split(new char[] { '，', '、', '；', ',', ';' });
                string sPFLSLVBYMC = new DAL.BMSPFLManager().GetSPFLSLVBYMC(this.comboBoxYHZCMC.Text.Trim());
                string str2 = sPFLSLVBYMC.Split(new char[] { '，', '、', '；', ',', ';' })[0];
                foreach (string str3 in strArray)
                {
                    if (!string.IsNullOrEmpty(str3) && !sPFLSLVBYMC.Contains(str3))
                    {
                        sPFLSLVBYMC = sPFLSLVBYMC + "，" + str3;
                    }
                }
                string[] strArray2 = sPFLSLVBYMC.Split(new char[] { '，', '、', '；', ',', ';' });
                if (strArray2.Length > 0)
                {
                    this.comboBoxSLV.Items.Clear();
                    foreach (string str4 in this.taxRate)
                    {
                        this.comboBoxSLV.Items.Add(this.ChangeTaxRate(str4));
                    }
                }
                foreach (string str5 in strArray2)
                {
                    if (str5.ToString().EndsWith("%"))
                    {
                        double num = double.Parse(str5.ToString().TrimEnd(new char[] { '%' })) / 100.0;
                        if (!this.taxRate.Contains(num.ToString("F2")))
                        {
                            this.comboBoxSLV.Items.Add(str5.ToString());
                        }
                    }
                }
                if (str2 != "")
                {
                    this.comboBoxSLV.Text = str2;
                }
                else
                {
                    this.comboBoxSLV.Text = strArray[0];
                }
                string[] strArray3 = (objArray[0] as DataTable).Rows[0]["ZZSTSGL"].ToString().Split(new char[] { '，', '、', '；', ',', ';' });
                if (strArray3.Length > 0)
                {
                    this.comboBoxYHZCMC.Items.Clear();
                    foreach (string str7 in strArray3)
                    {
                        this.comboBoxYHZCMC.Items.Add(str7);
                    }
                }
                this.comboBoxYHZCMC.Enabled = true;
                this.addevent(true);
            }
            else
            {
                this.comboBoxSLV.Items.Clear();
                foreach (string str8 in this.taxRate)
                {
                    this.comboBoxSLV.Items.Add(this.ChangeTaxRate(str8));
                }
                this.AdjustSYSL();
                this.comboBoxYHZC.Text = "否";
                this.comboBoxYHZCMC.Items.Add("");
                this.comboBoxYHZCMC.Text = "";
                this.comboBoxYHZCMC.Enabled = false;
            }
        }

        private void comboBoxYHZCMC_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBoxSLV.SelectedIndexChanged -= new EventHandler(this.comboBoxSLV_SelectedIndexChanged);
            if ("免税" == this.comboBoxYHZCMC.Text)
            {
                this.comboBoxSLV.Items.Clear();
                foreach (string str in this.taxRate)
                {
                    this.comboBoxSLV.Items.Add(this.ChangeTaxRate(str));
                }
                if (!this.taxRate.Contains("免税"))
                {
                    this.comboBoxSLV.Items.Add("免税");
                }
                this.comboBoxSLV.Text = "免税";
            }
            if ("不征税" == this.comboBoxYHZCMC.Text)
            {
                this.comboBoxSLV.Items.Clear();
                foreach (string str2 in this.taxRate)
                {
                    this.comboBoxSLV.Items.Add(this.ChangeTaxRate(str2));
                }
                if (!this.taxRate.Contains("不征税"))
                {
                    this.comboBoxSLV.Items.Add("不征税");
                }
                this.comboBoxSLV.Text = "不征税";
            }
            if ("按5%简易征收减按1.5%计征" == this.comboBoxYHZCMC.Text)
            {
                this.comboBoxSLV.Items.Clear();
                foreach (string str3 in this.taxRate)
                {
                    this.comboBoxSLV.Items.Add(this.ChangeTaxRate(str3));
                }
                if (!this.taxRate.Contains("减按1.5%计算"))
                {
                    this.comboBoxSLV.Items.Add("减按1.5%计算");
                }
                this.comboBoxSLV.Text = "减按1.5%计算";
            }
            if ((("免税" != this.comboBoxYHZCMC.Text) && ("不征税" != this.comboBoxYHZCMC.Text)) && (("按5%简易征收减按1.5%计征" != this.comboBoxYHZCMC.Text) && ("" != this.comboBoxYHZCMC.Text)))
            {
                DAL.BMSPFLManager manager = new DAL.BMSPFLManager();
                string text = this.comboBoxSLV.Text;
                bool flag = false;
                string str5 = manager.GetSPFLSLVBYMC(this.comboBoxYHZCMC.Text.Trim()).Replace("1.5%_5%", "1.5%");
                if (str5.Contains(text) || (str5 == ""))
                {
                    flag = true;
                }
                string str6 = str5.Split(new char[] { '，', '、', '；', ',', ';' })[0];
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.GetSLV_BY_BM", new object[] { this.comBoxSPFL.Text.Trim() });
                if ((objArray != null) && (objArray.Length > 0))
                {
                    string[] strArray = (objArray[0] as DataTable).Rows[0]["SLV"].ToString().Split(new char[] { '，', '、', '；', ',', ';' });
                    foreach (string str7 in strArray)
                    {
                        if (!string.IsNullOrEmpty(str7) && !str5.Contains(str7))
                        {
                            str5 = str5 + "，" + str7;
                        }
                    }
                    string[] strArray2 = str5.Split(new char[] { '，', '、', '；', ',', ';' });
                    if (strArray2.Length > 0)
                    {
                        this.comboBoxSLV.Items.Clear();
                        foreach (string str8 in this.taxRate)
                        {
                            this.comboBoxSLV.Items.Add(this.ChangeTaxRate(str8));
                        }
                    }
                    foreach (string str9 in strArray2)
                    {
                        string str10 = str9.ToString();
                        if (str10.EndsWith("%"))
                        {
                            double num = double.Parse(str10.TrimEnd(new char[] { '%' })) / 100.0;
                            if (!this.taxRate.Contains(num.ToString("F2")))
                            {
                                this.comboBoxSLV.Items.Add(str9.ToString());
                            }
                        }
                    }
                    if (flag)
                    {
                        this.comboBoxSLV.Text = text;
                    }
                    else if (str6 != "")
                    {
                        this.comboBoxSLV.Text = str6;
                    }
                    else
                    {
                        this.comboBoxSLV.Text = strArray[0];
                    }
                }
            }
            this.AdjustSYSL();
            this.comboBoxSLV.SelectedIndexChanged += new EventHandler(this.comboBoxSLV_SelectedIndexChanged);
        }

        private void ComBoxSPFL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                this.AdjustComboBoxYHZC();
                this.AdjustSYSL();
            }
        }

        private void comBoxSPFL_Leave(object sender, EventArgs e)
        {
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGetSPFLMore", new object[] { this.comBoxSPFL.Text.Trim(), 10, true, "BM,MC,SLV" });
            if ((objArray != null) && (objArray.Length > 0))
            {
                DataTable table = objArray[0] as DataTable;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["BM"].ToString() == this.comBoxSPFL.Text.Trim())
                    {
                        this.comBoxSPFL.Text = table.Rows[i]["BM"].ToString();
                    }
                }
            }
        }

        private void ComBoxSPFL_OnAutoComplate(object sender, EventArgs e)
        {
            string str = "";
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if ((combox != null) && combox.Name.Equals("comBoxSPFL"))
            {
                str = this.comBoxSPFL.Text.Trim();
            }
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGetSPFLMore", new object[] { str, 10, true, "BM,MC,SLV" });
            if ((objArray != null) && (objArray.Length > 0))
            {
                DataTable table = objArray[0] as DataTable;
                if ((combox != null) && (table != null))
                {
                    combox.DataSource = table;
                }
            }
        }

        private void comBoxSPFL_OnButtonClick(object sender, EventArgs e)
        {
            bool isxtsp = false;
            if (this.IsXTSP(base.textBoxBM.Text.Trim()))
            {
                isxtsp = true;
            }
            BMSPFLSelect select = new BMSPFLSelect(this.comBoxSPFL.Text.Trim(), isxtsp, this);
            if (select.ShowDialog() == DialogResult.OK)
            {
                this.comBoxSPFL.Text = select.SelectBM;
                this.SPFLMC.Text = select.SelectBMMC;
                this.addevent(false);
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.GetSLV_BY_BM", new object[] { this.comBoxSPFL.Text.Trim() });
                if (((objArray != null) && ((objArray[0] as DataTable).Rows.Count > 0)) && ((objArray[0] as DataTable).Rows[0]["SLV"].ToString() != ""))
                {
                    string[] strArray = (objArray[0] as DataTable).Rows[0]["SLV"].ToString().Split(new char[] { '，', '、', '；', ',', ';' });
                    if (strArray.Length > 0)
                    {
                        this.comboBoxSLV.Items.Clear();
                        foreach (string str in this.taxRate)
                        {
                            this.comboBoxSLV.Items.Add(this.ChangeTaxRate(str));
                        }
                    }
                    foreach (string str2 in strArray)
                    {
                        if (str2.ToString().EndsWith("%"))
                        {
                            double num = double.Parse(str2.ToString().TrimEnd(new char[] { '%' })) / 100.0;
                            if (!this.taxRate.Contains(num.ToString("F2")))
                            {
                                this.comboBoxSLV.Items.Add(str2.ToString());
                            }
                        }
                    }
                    this.comboBoxSLV.Text = strArray[0];
                }
                this.addevent(true);
            }
            this.AdjustComboBoxYHZC();
            this.AdjustComboBoxYHZCMC();
            this.AdjustSYSL();
        }

        private void ComBoxSPFL_TextChanged(object sender, EventArgs e)
        {
            if (this.comBoxSPFL.Text.Trim() == "")
            {
                this.AdjustComboBoxYHZC();
                this.addevent(false);
                if (!this.comboBoxSLV.Items.Contains(""))
                {
                    this.comboBoxSLV.Items.Add("");
                    this.comboBoxSLV.Text = "";
                }
                else
                {
                    this.comboBoxSLV.Text = "";
                }
                this.addevent(true);
            }
            else
            {
                int selectionStart = this.comBoxSPFL.SelectionStart;
                this.comBoxSPFL.Text = StringUtils.GetSubString(this.comBoxSPFL.Text, 0x13).Trim();
                this.comBoxSPFL.SelectionStart = selectionStart;
                this.addevent(false);
                if (!this.comboBoxSLV.Items.Contains(""))
                {
                    this.comboBoxSLV.Items.Add("");
                    this.comboBoxSLV.Text = "";
                }
                else
                {
                    this.comboBoxSLV.Text = "";
                }
                this.comboBoxYHZC.SelectedIndex = 1;
                this.addevent(true);
                this.addevent(false);
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.GetSLV_BY_BM", new object[] { this.comBoxSPFL.Text.Trim() });
                if (((objArray != null) && ((objArray[0] as DataTable).Rows.Count > 0)) && ((objArray[0] as DataTable).Rows[0]["SLV"].ToString() != ""))
                {
                    DAL.BMSPFLManager manager = new DAL.BMSPFLManager();
                    if (this.IsXTSP(base.textBoxBM.Text.Trim()))
                    {
                        if (!manager.CanUseThisSPFLBM(this.comBoxSPFL.Text.Trim(), true, true))
                        {
                            MessageBox.Show("稀土商品只能使用稀土分类编码或此分类编码不可用！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            this.comBoxSPFL.Focus();
                            if (!this.comboBoxSLV.Items.Contains(""))
                            {
                                this.comboBoxSLV.Items.Add("");
                                this.comboBoxSLV.Text = "";
                            }
                            else
                            {
                                this.comboBoxSLV.Text = "";
                            }
                            this.comboBoxYHZC.SelectedIndex = 1;
                            this.comboBoxYHZC.Enabled = false;
                            return;
                        }
                    }
                    else if (!manager.CanUseThisSPFLBM(this.comBoxSPFL.Text.Trim(), true, false))
                    {
                        MessageBox.Show("查无此分类编码或此分类编码不可用，请重新输入！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        this.comBoxSPFL.Focus();
                        if (!this.comboBoxSLV.Items.Contains(""))
                        {
                            this.comboBoxSLV.Items.Add("");
                            this.comboBoxSLV.Text = "";
                        }
                        else
                        {
                            this.comboBoxSLV.Text = "";
                        }
                        this.comboBoxYHZC.SelectedIndex = 1;
                        this.comboBoxYHZC.Enabled = false;
                        return;
                    }
                    string[] strArray = (objArray[0] as DataTable).Rows[0]["SLV"].ToString().Split(new char[] { '，', '、', '；', ',', ';' });
                    if (strArray.Length > 0)
                    {
                        this.comboBoxSLV.Items.Clear();
                        foreach (string str in this.taxRate)
                        {
                            this.comboBoxSLV.Items.Add(this.ChangeTaxRate(str));
                        }
                    }
                    foreach (string str2 in strArray)
                    {
                        if (str2.ToString().EndsWith("%"))
                        {
                            double num2 = double.Parse(str2.ToString().TrimEnd(new char[] { '%' })) / 100.0;
                            if (!this.taxRate.Contains(num2.ToString("F2")))
                            {
                                this.comboBoxSLV.Items.Add(str2.ToString());
                            }
                        }
                    }
                    this.comboBoxSLV.Text = strArray[0];
                }
                this.addevent(true);
                this.AdjustComboBoxYHZC();
                this.AdjustComboBoxYHZCMC();
                this.AdjustSYSL();
            }
        }

        private void ComBoxSPSM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ComBoxSPSM_OnAutoComplate(object sender, EventArgs e)
        {
            string str = "";
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if ((combox != null) && combox.Name.Equals("comBoxSPSM"))
            {
                str = this.comBoxSPSM.Text.Trim();
            }
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGetSPSMMore", new object[] { str, 5, "BM,MC,SLV" });
            if ((objArray != null) && (objArray.Length > 0))
            {
                DataTable table = objArray[0] as DataTable;
                if ((combox != null) && (table != null))
                {
                    combox.DataSource = table;
                }
            }
        }

        private void ComBoxSPSM_TextChanged(object sender, EventArgs e)
        {
            if (this.comBoxSPSM.Text.Trim() != "")
            {
                decimal num;
                if (!decimal.TryParse(this.comBoxSPSM.Text.Trim(), out num))
                {
                    MessageBoxHelper.Show("税目代码必须为数字!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.comBoxSPSM.Text = "";
                    this.comBoxSPSM.Focus();
                }
                else
                {
                    int selectionStart = this.comBoxSPSM.SelectionStart;
                    this.comBoxSPSM.Text = StringUtils.GetSubString(this.comBoxSPSM.Text, 4).Trim();
                    this.comBoxSPSM.SelectionStart = selectionStart;
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

        private List<double> GetZPSQSlv()
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            List<double> list = new List<double>();
            foreach (InvoiceType type in Enum.GetValues(typeof(InvoiceType)))
            {
                switch (type)
                {
                    case InvoiceType.special:
                    case InvoiceType.common:
                    case InvoiceType.Electronic:
                    case InvoiceType.volticket:
                    {
                        PZSQType type2 = card.SQInfo[type];
                        if (type2 != null)
                        {
                            foreach (TaxRateType type3 in type2.TaxRate)
                            {
                                if (!list.Contains(type3.Rate))
                                {
                                    list.Add(type3.Rate);
                                }
                            }
                            foreach (TaxRateType type4 in type2.TaxRate2)
                            {
                                if (type4.Rate == 0.05)
                                {
                                    list.Add(1.05);
                                }
                                if (!list.Contains(type4.Rate) && (type4.Rate != 0.05))
                                {
                                    list.Add(type4.Rate);
                                }
                            }
                            break;
                        }
                        break;
                    }
                }
            }
            list.Sort();
            return list;
        }

        protected override void InitData()
        {
            if (base.isUpdate)
            {
                this.Text = "商品编码编辑";
            }
            else
            {
                this.Text = "商品编码添加";
            }
            base.lblMC.Text = "*商品名称";
            base.baseLogical = splogical;
            base.log = LogUtil.GetLogger<BMSP_Edit>();
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.comBoxSPSM.KeyPress += new KeyPressEventHandler(this.ComBoxSPSM_KeyPress);
            this.comBoxSPSM.OnTextChanged = (EventHandler) Delegate.Combine(this.comBoxSPSM.OnTextChanged, new EventHandler(this.ComBoxSPSM_TextChanged));
            this.comBoxSPSM.OnAutoComplate += new EventHandler(this.ComBoxSPSM_OnAutoComplate);
            this.comBoxSPSM.OnSelectValue += new EventHandler(this.textBoxSPSM_OnSelectValue);
            this.comBoxSPSM.AutoComplate = AutoComplateStyle.HeadWork;
            this.comBoxSPSM.AutoIndex = 1;
            this.comBoxSPSM.ButtonAutoHide = true;
            this.comBoxSPSM.buttonStyle = ButtonStyle.Select;
            this.comBoxSPSM.Edit = EditStyle.TextBox;
            this.comBoxSPSM.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("编码", "BM", 50));
            this.comBoxSPSM.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("税目名称", "MC", this.comBoxSPSM.Width - 50));
            this.comBoxSPFL.KeyPress += new KeyPressEventHandler(this.ComBoxSPFL_KeyPress);
            this.comBoxSPFL.OnTextChanged = (EventHandler) Delegate.Combine(this.comBoxSPFL.OnTextChanged, new EventHandler(this.ComBoxSPFL_TextChanged));
            this.comBoxSPFL.OnAutoComplate += new EventHandler(this.ComBoxSPFL_OnAutoComplate);
            this.comBoxSPFL.OnSelectValue += new EventHandler(this.textBoxSPFL_OnSelectValue);
            this.comBoxSPFL.OnButtonClick += new EventHandler(this.comBoxSPFL_OnButtonClick);
            this.comBoxSPFL.AutoComplate = AutoComplateStyle.HeadWork;
            this.comBoxSPFL.AutoIndex = 1;
            this.comBoxSPFL.Edit = EditStyle.TextBox;
            this.comBoxSPFL.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("编码", "BM", 80));
            this.comBoxSPFL.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("分类名称", "MC", 160));
            this.textBoxJLDW.Leave += new EventHandler(this.textBoxJLDW_Leave);
            this.textBoxJLDW.TextChanged += new EventHandler(this.textBoxJLDW_TextChanged);
            this.textBoxDJ.KeyPress += new KeyPressEventHandler(this.textBoxDJ_KeyPress);
            this.textBoxDJ.TextChanged += new EventHandler(this.textBoxDJ_TextChanged);
            this.textBoxGGXH.TextChanged += new EventHandler(this.textBoxGGXH_TextChanged);
            this.comboBoxHSJBZ.DropDownStyle = ComboBoxStyle.DropDownList;
            this.chbXTHide.DropDownStyle = ComboBoxStyle.DropDownList;
            base.textBoxWaitMC.WaterMarkString = "商品名称";
            base.textBoxWaitMC.TextChangedWaitGetText += new GetTextEventHandler(this.textBoxWaitMC_TextChangedWaitGetText);
            this.comboBoxSLV.Leave += new EventHandler(this.comboBoxSLV_Leave);
            this.comboBoxSLV.SelectedValueChanged += new EventHandler(this.comboBoxSLV_Leave);
            this.textBoxDJ.Validating += new CancelEventHandler(this.textBoxDJ_Validating);
            base.textBoxSJBM.RootNodeString = "商品编码";
            base.textBoxSJBM.Text = "编码";
            base.toolStripBtnContinue.Visible = false;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(BMSP_Edit));
            this.comBoxSPSM = new AisinoMultiCombox();
            this.chbXTHide = new AisinoCMB();
            this.lblXTHide = new AisinoLBL();
            this.label5 = new AisinoLBL();
            this.label6 = new AisinoLBL();
            this.textBoxXSSRKM = new AisinoTXT();
            this.textBoxYJZZSKM = new AisinoTXT();
            this.label14 = new AisinoLBL();
            this.label7 = new AisinoLBL();
            this.textBoxXSTHKM = new AisinoTXT();
            this.label13 = new AisinoLBL();
            this.textBoxJLDW = new AisinoTXT();
            this.textBoxDJ = new AisinoTXT();
            this.label4 = new AisinoLBL();
            this.textBoxGGXH = new AisinoTXT();
            this.label3 = new AisinoLBL();
            this.comboBoxHSJBZ = new AisinoCMB();
            this.label11 = new AisinoLBL();
            this.labelKJM = new AisinoLBL();
            this.comboBoxKJM = new AisinoCMB();
            this.label9 = new AisinoLBL();
            this.comboBoxSLV = new AisinoCMB();
            this.aisinoLBL1 = new AisinoLBL();
            this.aisinoLBL2 = new AisinoLBL();
            this.comBoxSPFL = new AisinoMultiCombox();
            this.comboBoxYHZC = new AisinoCMB();
            this.aisinoLBL3 = new AisinoLBL();
            this.SPFLMC = new AisinoTXT();
            this.aisinoLBL4 = new AisinoLBL();
            this.comboBoxYHZCMC = new AisinoCMB();
            base.SuspendLayout();
            base.textBoxBM.Location = new Point(0x5f, 0x48);
            base.textBoxJM.BackColor = SystemColors.Window;
            base.textBoxJM.Location = new Point(0x5f, 0x66);
            base.label10.Location = new Point(0x3b, 0x6a);
            base.label8.Location = new Point(0x23, 0x2e);
            base.label1.Location = new Point(0x35, 0x4c);
            base.textBoxSJBM.Location = new Point(0x5f, 0x2a);
            base.textBoxSJBM.SelectBM = "";
            base.textBoxSJBM.Text = "";
            base.textBoxWaitMC.Location = new Point(0x16e, 0x2a);
            base.lblMC.Location = new Point(0x12a, 0x2e);
            base.lblMC.Size = new Size(0x3b, 12);
            base.lblMC.Text = "*商品名称";
            this.comBoxSPSM.AutoComplate = AutoComplateStyle.None;
            this.comBoxSPSM.AutoIndex = 1;
            this.comBoxSPSM.BorderColor = SystemColors.WindowFrame;
            this.comBoxSPSM.BorderStyle = AisinoComboxBorderStyle.System;
            this.comBoxSPSM.ButtonAutoHide = true;
            this.comBoxSPSM.buttonStyle = ButtonStyle.Select;
            this.comBoxSPSM.DataSource = null;
            this.comBoxSPSM.DrawHead = false;
            this.comBoxSPSM.Edit = EditStyle.TextBox;
            this.comBoxSPSM.ForeColor = Color.Maroon;
            this.comBoxSPSM.IsSelectAll = true;
            this.comBoxSPSM.Location = new Point(0x16d, 0x48);
            this.comBoxSPSM.MaxIndex = 8;
            this.comBoxSPSM.MaxLength = 0x7fff;
            this.comBoxSPSM.Name = "comBoxSPSM";
            this.comBoxSPSM.ReadOnly = false;
            this.comBoxSPSM.SelectedIndex = -1;
            this.comBoxSPSM.SelectionStart = 0;
            this.comBoxSPSM.ShowText = "";
            this.comBoxSPSM.Size = new Size(0xa9, 0x15);
            this.comBoxSPSM.TabIndex = 190;
            this.comBoxSPSM.UnderLineColor = Color.White;
            this.comBoxSPSM.UnderLineStyle = AisinoComboxBorderStyle.None;
            this.chbXTHide.DropDownStyle = ComboBoxStyle.DropDownList;
            this.chbXTHide.FormattingEnabled = true;
            this.chbXTHide.Location = new Point(0x1e4, 160);
            this.chbXTHide.Name = "chbXTHide";
            this.chbXTHide.Size = new Size(50, 20);
            this.chbXTHide.TabIndex = 0xb2;
            this.lblXTHide.AutoSize = true;
            this.lblXTHide.Location = new Point(0x1ac, 0xa4);
            this.lblXTHide.Name = "lblXTHide";
            this.lblXTHide.Size = new Size(0x35, 12);
            this.lblXTHide.TabIndex = 0xbd;
            this.lblXTHide.Text = "隐藏标志";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x11c, 0x11a);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x4d, 12);
            this.label5.TabIndex = 0xb5;
            this.label5.Text = "销售收入科目";
            this.label5.Visible = false;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(2, 0x137);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x59, 12);
            this.label6.TabIndex = 0xb6;
            this.label6.Text = "应缴增值税科目";
            this.label6.Visible = false;
            this.textBoxXSSRKM.Location = new Point(0x16f, 0x117);
            this.textBoxXSSRKM.Name = "textBoxXSSRKM";
            this.textBoxXSSRKM.Size = new Size(0xa9, 0x15);
            this.textBoxXSSRKM.TabIndex = 0xaf;
            this.textBoxXSSRKM.Visible = false;
            this.textBoxYJZZSKM.Location = new Point(0x61, 0x133);
            this.textBoxYJZZSKM.Name = "textBoxYJZZSKM";
            this.textBoxYJZZSKM.Size = new Size(0xa9, 0x15);
            this.textBoxYJZZSKM.TabIndex = 0xb0;
            this.textBoxYJZZSKM.Visible = false;
            this.label14.AutoSize = true;
            this.label14.Location = new Point(0x130, 0x4c);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x35, 12);
            this.label14.TabIndex = 0xbb;
            this.label14.Text = "商品税目";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x11c, 0x137);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x4d, 12);
            this.label7.TabIndex = 0xb7;
            this.label7.Text = "销售退回科目";
            this.label7.Visible = false;
            this.textBoxXSTHKM.Location = new Point(0x16f, 0x133);
            this.textBoxXSTHKM.Name = "textBoxXSTHKM";
            this.textBoxXSTHKM.Size = new Size(0xa9, 0x15);
            this.textBoxXSTHKM.TabIndex = 0xb1;
            this.textBoxXSTHKM.Visible = false;
            this.label13.AutoSize = true;
            this.label13.Location = new Point(0x23, 0xa4);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x35, 12);
            this.label13.TabIndex = 0xba;
            this.label13.Text = "计量单位";
            this.textBoxJLDW.BorderStyle = BorderStyle.FixedSingle;
            this.textBoxJLDW.Location = new Point(0x5f, 160);
            this.textBoxJLDW.Name = "textBoxJLDW";
            this.textBoxJLDW.Size = new Size(0xaf, 0x15);
            this.textBoxJLDW.TabIndex = 0xab;
            this.textBoxDJ.BackColor = SystemColors.Window;
            this.textBoxDJ.BorderStyle = BorderStyle.FixedSingle;
            this.textBoxDJ.Location = new Point(0x16d, 0x83);
            this.textBoxDJ.Name = "textBoxDJ";
            this.textBoxDJ.Size = new Size(0xa9, 0x15);
            this.textBoxDJ.TabIndex = 170;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x148, 0x87);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 12);
            this.label4.TabIndex = 180;
            this.label4.Text = "单价";
            this.textBoxGGXH.BorderStyle = BorderStyle.FixedSingle;
            this.textBoxGGXH.Location = new Point(0x5f, 0x83);
            this.textBoxGGXH.Name = "textBoxGGXH";
            this.textBoxGGXH.Size = new Size(0xaf, 0x15);
            this.textBoxGGXH.TabIndex = 0xad;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x23, 0x87);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 12);
            this.label3.TabIndex = 0xb3;
            this.label3.Text = "规格型号";
            this.comboBoxHSJBZ.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxHSJBZ.FormattingEnabled = true;
            this.comboBoxHSJBZ.Items.AddRange(new object[] { "是", "否" });
            this.comboBoxHSJBZ.Location = new Point(0x16d, 160);
            this.comboBoxHSJBZ.Name = "comboBoxHSJBZ";
            this.comboBoxHSJBZ.Size = new Size(50, 20);
            this.comboBoxHSJBZ.TabIndex = 0xac;
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0x11e, 0xa4);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x47, 12);
            this.label11.TabIndex = 0xb9;
            this.label11.Text = "*含税价标志";
            this.labelKJM.AutoSize = true;
            this.labelKJM.Location = new Point(50, 0x11a);
            this.labelKJM.Name = "labelKJM";
            this.labelKJM.Size = new Size(0x29, 12);
            this.labelKJM.TabIndex = 0xbc;
            this.labelKJM.Text = "快捷码";
            this.labelKJM.Visible = false;
            this.comboBoxKJM.DropDownStyle = ComboBoxStyle.Simple;
            this.comboBoxKJM.FormattingEnabled = true;
            this.comboBoxKJM.Location = new Point(0x61, 0x116);
            this.comboBoxKJM.Name = "comboBoxKJM";
            this.comboBoxKJM.Size = new Size(0xa9, 20);
            this.comboBoxKJM.TabIndex = 0xae;
            this.comboBoxKJM.Visible = false;
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x142, 0x6a);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x23, 12);
            this.label9.TabIndex = 0xb8;
            this.label9.Text = "*税率";
            this.comboBoxSLV.BackColor = SystemColors.Window;
            this.comboBoxSLV.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxSLV.FlatStyle = FlatStyle.System;
            this.comboBoxSLV.FormattingEnabled = true;
            this.comboBoxSLV.Location = new Point(0x16d, 0x66);
            this.comboBoxSLV.Name = "comboBoxSLV";
            this.comboBoxSLV.Size = new Size(0xa9, 20);
            this.comboBoxSLV.TabIndex = 0xa9;
            this.comboBoxSLV.SelectedIndexChanged += new EventHandler(this.comboBoxSLV_SelectedIndexChanged);
            this.aisinoLBL1.AutoSize = true;
            this.aisinoLBL1.Location = new Point(5, 0xc3);
            this.aisinoLBL1.Name = "aisinoLBL1";
            this.aisinoLBL1.Size = new Size(0x53, 12);
            this.aisinoLBL1.TabIndex = 0xbc;
            this.aisinoLBL1.Text = "*税收分类编码";
            this.aisinoLBL2.AutoSize = true;
            this.aisinoLBL2.Location = new Point(12, 230);
            this.aisinoLBL2.Name = "aisinoLBL2";
            this.aisinoLBL2.Size = new Size(0x4d, 12);
            this.aisinoLBL2.TabIndex = 0xb5;
            this.aisinoLBL2.Text = "享受优惠政策";
            this.comBoxSPFL.AutoComplate = AutoComplateStyle.None;
            this.comBoxSPFL.AutoIndex = 1;
            this.comBoxSPFL.BorderColor = SystemColors.WindowFrame;
            this.comBoxSPFL.BorderStyle = AisinoComboxBorderStyle.System;
            this.comBoxSPFL.ButtonAutoHide = false;
            this.comBoxSPFL.buttonStyle = ButtonStyle.Button;
            this.comBoxSPFL.DataSource = null;
            this.comBoxSPFL.DrawHead = false;
            this.comBoxSPFL.Edit = EditStyle.TextBox;
            this.comBoxSPFL.ForeColor = Color.Maroon;
            this.comBoxSPFL.IsSelectAll = true;
            this.comBoxSPFL.Location = new Point(0x5f, 0xbf);
            this.comBoxSPFL.MaxIndex = 8;
            this.comBoxSPFL.MaxLength = 0x7fff;
            this.comBoxSPFL.Name = "comBoxSPFL";
            this.comBoxSPFL.ReadOnly = false;
            this.comBoxSPFL.SelectedIndex = -1;
            this.comBoxSPFL.SelectionStart = 0;
            this.comBoxSPFL.ShowText = "";
            this.comBoxSPFL.Size = new Size(0xaf, 0x15);
            this.comBoxSPFL.TabIndex = 190;
            this.comBoxSPFL.UnderLineColor = Color.White;
            this.comBoxSPFL.UnderLineStyle = AisinoComboxBorderStyle.None;
            this.comBoxSPFL.Leave += new EventHandler(this.comBoxSPFL_Leave);
            this.comboBoxYHZC.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxYHZC.FormattingEnabled = true;
            this.comboBoxYHZC.Items.AddRange(new object[] { "是", "否" });
            this.comboBoxYHZC.Location = new Point(0x60, 0xe0);
            this.comboBoxYHZC.Name = "comboBoxYHZC";
            this.comboBoxYHZC.Size = new Size(50, 20);
            this.comboBoxYHZC.TabIndex = 0xac;
            this.comboBoxYHZC.SelectedIndexChanged += new EventHandler(this.comboBoxYHZC_SelectedIndexChanged);
            this.aisinoLBL3.AutoSize = true;
            this.aisinoLBL3.Location = new Point(0x123, 0xc5);
            this.aisinoLBL3.Name = "aisinoLBL3";
            this.aisinoLBL3.Size = new Size(0x4d, 12);
            this.aisinoLBL3.TabIndex = 0xbf;
            this.aisinoLBL3.Text = "税收分类名称";
            this.SPFLMC.BackColor = SystemColors.Window;
            this.SPFLMC.BorderStyle = BorderStyle.FixedSingle;
            this.SPFLMC.Enabled = false;
            this.SPFLMC.Location = new Point(0x176, 0xbf);
            this.SPFLMC.Name = "SPFLMC";
            this.SPFLMC.Size = new Size(160, 0x15);
            this.SPFLMC.TabIndex = 0xc0;
            this.aisinoLBL4.AutoSize = true;
            this.aisinoLBL4.Location = new Point(0x117, 0xe7);
            this.aisinoLBL4.Name = "aisinoLBL4";
            this.aisinoLBL4.Size = new Size(0x4d, 12);
            this.aisinoLBL4.TabIndex = 0xc1;
            this.aisinoLBL4.Text = "优惠政策类型";
            this.comboBoxYHZCMC.BackColor = SystemColors.Window;
            this.comboBoxYHZCMC.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxYHZCMC.FlatStyle = FlatStyle.System;
            this.comboBoxYHZCMC.FormattingEnabled = true;
            this.comboBoxYHZCMC.Location = new Point(0x16e, 0xe2);
            this.comboBoxYHZCMC.Name = "comboBoxYHZCMC";
            this.comboBoxYHZCMC.Size = new Size(0xa8, 20);
            this.comboBoxYHZCMC.TabIndex = 0xc2;
            this.comboBoxYHZCMC.SelectedIndexChanged += new EventHandler(this.comboBoxYHZCMC_SelectedIndexChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x233, 0x103);
            base.Controls.Add(this.comboBoxYHZCMC);
            base.Controls.Add(this.aisinoLBL4);
            base.Controls.Add(this.SPFLMC);
            base.Controls.Add(this.aisinoLBL3);
            base.Controls.Add(this.comBoxSPFL);
            base.Controls.Add(this.comBoxSPSM);
            base.Controls.Add(this.chbXTHide);
            base.Controls.Add(this.lblXTHide);
            base.Controls.Add(this.aisinoLBL2);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.textBoxXSSRKM);
            base.Controls.Add(this.textBoxYJZZSKM);
            base.Controls.Add(this.label14);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.textBoxXSTHKM);
            base.Controls.Add(this.label13);
            base.Controls.Add(this.textBoxJLDW);
            base.Controls.Add(this.textBoxDJ);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.textBoxGGXH);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.comboBoxYHZC);
            base.Controls.Add(this.comboBoxHSJBZ);
            base.Controls.Add(this.label11);
            base.Controls.Add(this.aisinoLBL1);
            base.Controls.Add(this.labelKJM);
            base.Controls.Add(this.comboBoxKJM);
            base.Controls.Add(this.label9);
            base.Controls.Add(this.comboBoxSLV);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            this.MaximumSize = new Size(700, 400);
            base.Name = "BMSP_Edit";
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "XSDJ_Edit";
            base.Controls.SetChildIndex(this.comboBoxSLV, 0);
            base.Controls.SetChildIndex(this.label9, 0);
            base.Controls.SetChildIndex(this.comboBoxKJM, 0);
            base.Controls.SetChildIndex(this.labelKJM, 0);
            base.Controls.SetChildIndex(this.aisinoLBL1, 0);
            base.Controls.SetChildIndex(this.label11, 0);
            base.Controls.SetChildIndex(this.comboBoxHSJBZ, 0);
            base.Controls.SetChildIndex(this.comboBoxYHZC, 0);
            base.Controls.SetChildIndex(this.label3, 0);
            base.Controls.SetChildIndex(this.textBoxGGXH, 0);
            base.Controls.SetChildIndex(this.label4, 0);
            base.Controls.SetChildIndex(this.textBoxDJ, 0);
            base.Controls.SetChildIndex(this.textBoxJLDW, 0);
            base.Controls.SetChildIndex(this.label13, 0);
            base.Controls.SetChildIndex(this.textBoxXSTHKM, 0);
            base.Controls.SetChildIndex(this.label7, 0);
            base.Controls.SetChildIndex(this.label14, 0);
            base.Controls.SetChildIndex(this.textBoxYJZZSKM, 0);
            base.Controls.SetChildIndex(this.textBoxXSSRKM, 0);
            base.Controls.SetChildIndex(this.label6, 0);
            base.Controls.SetChildIndex(this.label5, 0);
            base.Controls.SetChildIndex(this.aisinoLBL2, 0);
            base.Controls.SetChildIndex(this.lblXTHide, 0);
            base.Controls.SetChildIndex(this.chbXTHide, 0);
            base.Controls.SetChildIndex(this.comBoxSPSM, 0);
            base.Controls.SetChildIndex(this.comBoxSPFL, 0);
            base.Controls.SetChildIndex(this.aisinoLBL3, 0);
            base.Controls.SetChildIndex(base.textBoxBM, 0);
            base.Controls.SetChildIndex(base.textBoxJM, 0);
            base.Controls.SetChildIndex(base.label10, 0);
            base.Controls.SetChildIndex(base.label8, 0);
            base.Controls.SetChildIndex(base.label1, 0);
            base.Controls.SetChildIndex(base.textBoxSJBM, 0);
            base.Controls.SetChildIndex(base.textBoxWaitMC, 0);
            base.Controls.SetChildIndex(base.lblMC, 0);
            base.Controls.SetChildIndex(this.SPFLMC, 0);
            base.Controls.SetChildIndex(this.aisinoLBL4, 0);
            base.Controls.SetChildIndex(this.comboBoxYHZCMC, 0);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InModel()
        {
            this.spModel.BM = base.textBoxBM.Text.Trim();
            this.spModel.MC = base.textBoxWaitMC.Text.Trim();
            this.spModel.JM = base.textBoxJM.Text.Trim();
            this.spModel.SJBM = base.textBoxSJBM.Text.Trim();
            if (this.comboBoxSLV.Text.Trim() == "中外合作油气田")
            {
                this.spModel.SLV = 0.05;
                this.spModel.HYSY = true;
            }
            else if (((this.comboBoxSLV.Text.Trim() == "0%") && (this.comboBoxYHZCMC.Text.Trim() == "出口零税")) && Flbm.IsYM())
            {
                this.spModel.SLV = 0.0;
                this.spModel.LSLVBS = "0";
                this.spModel.HYSY = false;
            }
            else if ((this.comboBoxSLV.Text.Trim() == "免税") && Flbm.IsYM())
            {
                this.spModel.SLV = 0.0;
                this.spModel.LSLVBS = "1";
                this.spModel.HYSY = false;
            }
            else if ((this.comboBoxSLV.Text.Trim() == "免税") && !Flbm.IsYM())
            {
                this.spModel.SLV = 0.0;
                this.spModel.HYSY = false;
            }
            else if ((this.comboBoxSLV.Text.Trim() == "不征税") && Flbm.IsYM())
            {
                this.spModel.SLV = 0.0;
                this.spModel.LSLVBS = "2";
                this.spModel.HYSY = false;
            }
            else if ((this.comboBoxSLV.Text.Trim() == "0%") && Flbm.IsYM())
            {
                this.spModel.SLV = 0.0;
                this.spModel.LSLVBS = "3";
                this.spModel.HYSY = false;
            }
            else if (this.comboBoxSLV.Text.Trim() == "减按1.5%计算")
            {
                this.spModel.SLV = 0.015;
            }
            else
            {
                this.spModel.SLV = Convert.ToDouble(this.comboBoxSLV.Text.Trim(new char[] { '%' })) / 100.0;
                this.spModel.HYSY = false;
                this.spModel.LSLVBS = "";
                this.spModel.HYSY = false;
            }
            this.spModel.SPSM = this.comBoxSPSM.Text.Trim();
            this.spModel.GGXH = this.textBoxGGXH.Text.Trim();
            this.spModel.JLDW = this.textBoxJLDW.Text.Trim();
            string str = this.textBoxDJ.Text.Trim();
            this.spModel.DJ = string.IsNullOrEmpty(str) ? 0.0 : double.Parse(str);
            this.spModel.DJ = Math.Round(this.spModel.DJ, 5);
            this.spModel.HSJBZ = this.comboBoxHSJBZ.SelectedIndex == 0;
            this.spModel.XSSRKM = this.textBoxXSSRKM.Text.Trim();
            this.spModel.YJZZSKM = this.textBoxYJZZSKM.Text.Trim();
            this.spModel.XSTHKM = this.textBoxXSTHKM.Text.Trim();
            this.spModel.KJM = CommonFunc.GenerateKJM(base.textBoxWaitMC.Text.Trim());
            this.spModel.WJ = 1;
            this.spModel.ISHIDE = (this.chbXTHide.SelectedIndex == 0) ? "1000000000" : "0000000000";
            if (!this.IsXT)
            {
                this.spModel.XTHASH = null;
                this.spModel.XTCODE = "0000000000";
            }
            if (Flbm.IsYM())
            {
                this.spModel.SPFL = this.comBoxSPFL.Text.Trim();
                this.spModel.SPFLMC = this.SPFLMC.Text.Trim();
                this.spModel.YHZC = (this.comboBoxYHZC.SelectedIndex == 0) ? "是" : "否";
                this.spModel.YHZCMC = this.comboBoxYHZCMC.Text.Trim();
            }
        }

        private bool IsXTSP(string bm)
        {
            if ((bm == null) || string.Empty.Equals(bm))
            {
                return false;
            }
            BMSPModel model = (BMSPModel) this.spManager.GetModel(bm);
            return ((model.XTHASH != null) && (model.XTHASH != ""));
        }

        protected override void LoadData(string BM)
        {
            base.LoadData(BM);
            this.spModel = (BMSPModel) base.baseModel;
            this.comboBoxKJM.Text = this.spModel.KJM;
            this.comBoxSPSM.Text = this.spModel.SPSM;
            this.textBoxGGXH.Text = this.spModel.GGXH;
            this.textBoxJLDW.Text = this.spModel.JLDW;
            this.textBoxDJ.Text = this.spModel.DJ.ToString();
            this.comboBoxHSJBZ.SelectedIndex = this.spModel.HSJBZ ? 0 : 1;
            this.textBoxXSSRKM.Text = this.spModel.XSSRKM;
            this.textBoxYJZZSKM.Text = this.spModel.YJZZSKM;
            this.textBoxXSTHKM.Text = this.spModel.XSTHKM;
            this.chbXTHide.SelectedIndex = (this.spModel.ISHIDE == "0000000000") ? 1 : 0;
            if (Flbm.IsYM())
            {
                this.comBoxSPFL.Text = this.spModel.SPFL;
                this.SPFLMC.Text = this.spModel.SPFLMC;
                this.comboBoxYHZC.SelectedIndex = ("是" == this.spModel.YHZC) ? 0 : 1;
            }
            if ((this.spModel.SLV == 0.05) && this.spModel.HYSY)
            {
                this.comboBoxSLV.Text = "中外合作油气田";
                this.comboBoxHSJBZ.SelectedItem = "是";
                this.comboBoxHSJBZ.Enabled = false;
            }
            else if ((("0.00" == this.spModel.SLV.ToString("f2")) && (this.spModel.LSLVBS == "1")) && Flbm.IsYM())
            {
                if (!this.comboBoxSLV.Items.Contains("免税"))
                {
                    this.comboBoxSLV.Items.Add("免税");
                    this.comboBoxSLV.Text = "免税";
                }
                else
                {
                    this.comboBoxSLV.Text = "免税";
                }
            }
            else if ((("0.00" == this.spModel.SLV.ToString("f2")) && (this.spModel.LSLVBS == "2")) && Flbm.IsYM())
            {
                if (!this.comboBoxSLV.Items.Contains("不征税"))
                {
                    this.comboBoxSLV.Items.Add("不征税");
                    this.comboBoxSLV.Text = "不征税";
                }
                else
                {
                    this.comboBoxSLV.Text = "不征税";
                }
            }
            else if ((("0.00" == this.spModel.SLV.ToString("f2")) && ((this.spModel.LSLVBS == "0") || (this.spModel.LSLVBS == "3"))) && Flbm.IsYM())
            {
                this.comboBoxSLV.Text = "0%";
            }
            else if ("0.015" == this.spModel.SLV.ToString("f3"))
            {
                if (!this.comboBoxSLV.Items.Contains("减按1.5%计算"))
                {
                    this.comboBoxSLV.Items.Add("减按1.5%计算");
                    this.comboBoxSLV.Text = "减按1.5%计算";
                }
                else
                {
                    this.comboBoxSLV.Text = "减按1.5%计算";
                }
            }
            else if ((("0.00" == this.spModel.SLV.ToString("f2")) && ((this.spModel.LSLVBS == null) || (this.spModel.LSLVBS == ""))) && Flbm.IsYM())
            {
                this.comboBoxSLV.Text = "0%";
            }
            else if (("0.00" == this.spModel.SLV.ToString("f2")) && !Flbm.IsYM())
            {
                if (!this.comboBoxSLV.Items.Contains("免税"))
                {
                    this.comboBoxSLV.Items.Add("免税");
                    this.comboBoxSLV.Text = "免税";
                }
                else
                {
                    this.comboBoxSLV.Text = "免税";
                }
            }
            else
            {
                this.comboBoxSLV.Text = this.spModel.SLV.ToString("#%");
            }
            if (Flbm.IsYM() && this.comboBoxYHZCMC.Enabled)
            {
                try
                {
                    this.comboBoxYHZCMC.Text = this.spModel.YHZCMC.ToString();
                }
                catch
                {
                    if (!this.comboBoxYHZCMC.Items.Contains(""))
                    {
                        this.comboBoxYHZCMC.Items.Add("");
                        this.comboBoxYHZCMC.Text = "";
                    }
                }
            }
        }

        private bool SimpleValidated()
        {
            bool flag = true;
            if (base.textBoxWaitMC.Text.Trim().Length == 0)
            {
                base.textBoxWaitMC.Focus();
                flag = false;
            }
            if (!this.comboBoxSLV_Validat())
            {
                this.comboBoxSLV.Focus();
                return false;
            }
            if (!this.textBoxDJ_Validating())
            {
                this.textBoxDJ.Focus();
                return false;
            }
            if (this.comboBoxHSJBZ.Text.Trim().Length == 0)
            {
                this.comboBoxHSJBZ.Focus();
                flag = false;
            }
            if (!flag)
            {
                MessageManager.ShowMsgBox("INP-235309");
                return flag;
            }
            if (this.IsXT)
            {
                if (this.xtHash.Length != 1)
                {
                    string str = this.xtHash.Substring(0, 1);
                    string str2 = this.xtHash.Substring(1, this.xtHash.Length - 1);
                    if (!XTSP_Crypt.EncodeXTGoodsName(str + this.spModel.MC).Equals(str2))
                    {
                        MessageManager.ShowMsgBox("INP-235126");
                        flag = false;
                    }
                    if (this.spModel.SJBM != base.textBoxSJBM.Text.Trim())
                    {
                        MessageManager.ShowMsgBox("235128");
                        flag = false;
                    }
                }
                string str4 = this.textBoxJLDW.Text.Trim();
                if (((str4 != "") && (str4 != "公斤")) && (str4 != "吨"))
                {
                    MessageBoxHelper.Show("稀土商品的计量单位必须为空、“吨”或“公斤”", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.textBoxJLDW.Text = "";
                    this.textBoxJLDW.Focus();
                    return false;
                }
                if (this.comboBoxSLV.Text.Trim() == "1.5%")
                {
                    MessageBox.Show("稀土商品税率不能为1.5%税率！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    if (!this.comboBoxSLV.Items.Contains(""))
                    {
                        this.comboBoxSLV.Items.Add("");
                        this.comboBoxSLV.Text = "";
                    }
                    return false;
                }
                if (Flbm.IsYM())
                {
                    DAL.BMSPFLManager manager = new DAL.BMSPFLManager();
                    if (!manager.CanUseThisSPFLBM(this.comBoxSPFL.Text.Trim(), true, true))
                    {
                        MessageBox.Show("稀土商品只能使用稀土分类编码或此分类编码不可用！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        this.comBoxSPFL.Focus();
                        this.SPFLMC.Text = "";
                        return false;
                    }
                    this.SPFLMC.Text = manager.GetSPFLMCBYBM(this.comBoxSPFL.Text.Trim());
                    if (("是" == this.comboBoxYHZC.SelectedItem.ToString()) && (this.comboBoxYHZCMC.Text == ""))
                    {
                        MessageBox.Show("优惠类型不能为空，请重新选择！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        this.comboBoxYHZCMC.Focus();
                        flag = false;
                    }
                    return flag;
                }
                return true;
            }
            if (Flbm.IsYM())
            {
                DAL.BMSPFLManager manager2 = new DAL.BMSPFLManager();
                if (!manager2.CanUseThisSPFLBM(this.comBoxSPFL.Text.Trim(), true, false))
                {
                    MessageBox.Show("查无此分类编码或此分类编码不可用，请重新输入！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.comBoxSPFL.Focus();
                    this.SPFLMC.Text = "";
                    return false;
                }
                this.SPFLMC.Text = manager2.GetSPFLMCBYBM(this.comBoxSPFL.Text.Trim());
                if (("是" == this.comboBoxYHZC.SelectedItem.ToString()) && (this.comboBoxYHZCMC.Text == ""))
                {
                    MessageBox.Show("优惠政策类型不能为空，请重新选择！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.comboBoxYHZCMC.Focus();
                    flag = false;
                }
                return flag;
            }
            return true;
        }

        private void textBoxDJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar) && ((e.KeyChar != '.') || this.textBoxDJ.Text.Contains("."))))
            {
                e.Handled = true;
            }
        }

        private void textBoxDJ_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = this.textBoxDJ.SelectionStart;
            this.textBoxDJ.Text = StringUtils.GetSubString(this.textBoxDJ.Text, 15).Trim();
            this.textBoxDJ.SelectionStart = selectionStart;
        }

        private bool textBoxDJ_Validating()
        {
            decimal num;
            bool flag = true;
            if (this.textBoxDJ.Text.Trim().Length == 0)
            {
                return true;
            }
            if (!decimal.TryParse(this.textBoxDJ.Text.Trim(), out num))
            {
                MessageBoxHelper.Show("单价只能录入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.textBoxDJ.Focus();
                flag = false;
            }
            if (((double) num) < 0.0)
            {
                MessageBoxHelper.Show("单价不能为负数!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.textBoxDJ.Focus();
                flag = false;
            }
            return flag;
        }

        private void textBoxDJ_Validating(object sender, CancelEventArgs e)
        {
            if (!this.textBoxDJ_Validating())
            {
                e.Cancel = true;
            }
        }

        private void textBoxGGXH_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = this.textBoxGGXH.SelectionStart;
            this.textBoxGGXH.Text = StringUtils.GetSubString(this.textBoxGGXH.Text, 40);
            this.textBoxGGXH.SelectionStart = selectionStart;
        }

        private void textBoxJLDW_Leave(object sender, EventArgs e)
        {
            if (this.IsXT)
            {
                string str = this.textBoxJLDW.Text.Trim();
                if (((str != "") && (str != "公斤")) && (str != "吨"))
                {
                    MessageBoxHelper.Show("稀土商品的计量单位必须为空、“吨”或“公斤”", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.textBoxJLDW.Text = "";
                    this.textBoxJLDW.Focus();
                }
            }
        }

        private void textBoxJLDW_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = this.textBoxJLDW.SelectionStart;
            this.textBoxJLDW.Text = StringUtils.GetSubString(this.textBoxJLDW.Text, 0x16);
            this.textBoxJLDW.SelectionStart = selectionStart;
        }

        private void textBoxSPFL_OnSelectValue(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if (combox != null)
            {
                Dictionary<string, string> selectDict = combox.SelectDict;
                this.comBoxSPFL.Text = selectDict["BM"].ToString();
                this.SPFLMC.Text = selectDict["MC"].ToString();
                this.ComBoxSPFL_OnAutoComplate(this.comBoxSPFL, e);
            }
        }

        private void textBoxSPSM_OnSelectValue(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if (combox != null)
            {
                Dictionary<string, string> selectDict = combox.SelectDict;
                this.comBoxSPSM.Text = selectDict["BM"].ToString();
                this.comboBoxSLV.Text = this.ChangeTaxRate(selectDict["SLV"].ToString());
                this.ComBoxSPSM_OnAutoComplate(this.comBoxSPSM, e);
            }
        }

        protected override void textBoxWaitMC_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = base.textBoxWaitMC.SelectionStart;
            base.textBoxWaitMC.Text = StringUtils.GetSubString(base.textBoxWaitMC.Text, 0x5c);
            base.textBoxWaitMC.SelectionStart = selectionStart;
        }

        private void textBoxWaitMC_TextChangedWaitGetText(object sender, GetTextEventArgs e)
        {
            string[] spellCode = StringUtils.GetSpellCode(e.StartText.Trim());
            if (spellCode.Length > 1)
            {
                this.comboBoxKJM.DropDownStyle = ComboBoxStyle.DropDown;
                this.comboBoxKJM.Items.Clear();
                this.comboBoxKJM.Items.AddRange(spellCode);
                this.comboBoxKJM.SelectedIndex = 0;
            }
            else
            {
                this.comboBoxKJM.DropDownStyle = ComboBoxStyle.Simple;
                if (spellCode.Length > 0)
                {
                    this.comboBoxKJM.Text = spellCode[0];
                }
            }
        }

        protected override void toolStripBtnContinue_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.SimpleValidated())
                {
                    this.InModel();
                    if (splogical.AddMerchandise(this.spModel) == "0")
                    {
                        MessageManager.ShowMsgBox("INP-235301");
                        ((BMBase<BMSP_Edit, BMSPFenLei, BMSPSelect>) base.father).RefreshGrid();
                        base.textBoxBM.Text = splogical.TuiJianBM(base.textBoxSJBM.Text);
                        base.textBoxBM.Select(base.textBoxBM.Text.Length, 0);
                        base.textBoxWaitMC.Text = "";
                        base.textBoxJM.Text = "";
                        this.comboBoxKJM.Items.Clear();
                        this.comboBoxKJM.Text = "";
                        this.comboBoxKJM.DropDownStyle = ComboBoxStyle.Simple;
                        if (this.comboBoxSLV.Items.Count > 0)
                        {
                            this.comboBoxSLV.SelectedIndex = 0;
                        }
                        else
                        {
                            this.comboBoxSLV.Text = "";
                        }
                        this.comBoxSPSM.Text = "";
                        this.textBoxGGXH.Text = "";
                        this.textBoxJLDW.Text = "";
                        this.textBoxDJ.Text = "";
                        this.textBoxXSSRKM.Text = "";
                        this.textBoxYJZZSKM.Text = "";
                        this.textBoxXSTHKM.Text = "";
                    }
                    else
                    {
                        MessageManager.ShowMsgBox("INP-235302");
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }

        protected override void toolStripBtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (base.textBoxBM_Validating() && this.SimpleValidated())
                {
                    if (!base.isUpdate)
                    {
                        this.Add();
                    }
                    else
                    {
                        this.UpdateSP();
                    }
                }
            }
            catch (Exception exception)
            {
                base.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
        }

        private void UpdateSP()
        {
            this.InModel();
            string str = splogical.ModifyData(this.spModel, base._bm);
            switch (str)
            {
                case "0":
                    this.retCode = this.spModel.BM;
                    if (this.SucDialog)
                    {
                        MessageManager.ShowMsgBox("INP-235303");
                    }
                    base.DialogResult = DialogResult.OK;
                    base.Close();
                    return;

                case "e1":
                    MessageManager.ShowMsgBox("INP-235108");
                    return;

                case "e2":
                    MessageManager.ShowMsgBox("INP-235110");
                    return;

                case "e3":
                    MessageManager.ShowMsgBox("INP-235133");
                    return;

                case "e4":
                    MessageManager.ShowMsgBox("INP-235134");
                    break;
            }
            if (str == "e4")
            {
                MessageManager.ShowMsgBox("INP-235134");
            }
            else
            {
                MessageManager.ShowMsgBox("INP-235304");
            }
        }
    }
}

