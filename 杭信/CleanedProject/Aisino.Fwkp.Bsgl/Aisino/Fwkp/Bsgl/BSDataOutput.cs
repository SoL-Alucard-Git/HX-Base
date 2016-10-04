namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.CommonLibrary;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    public class BSDataOutput : DockForm
    {
        private bool afterInit;
        private const string BARCODE_KEY_FLAG = "BARCODEKEY";
        private const string BLANKCHAR = " ";
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private AisinoBTN btnSelect;
        private AisinoCMB cmbMonth;
        private AisinoCMB cmbPiaoZhong;
        private IContainer components;
        private bool FGorNot;
        private AisinoGRP groupBox1;
        private const int HX_FIRM_FLAG = 4;
        private bool isHzfw;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private AisinoLBL lablePiaoZhong;
        private string lastRepDateHY = "";
        private string lastRepDateJDC = "";
        private ILog loger = LogUtil.GetLogger<BSDataOutput>();
        private CommFun m_commFun = new CommFun();
        private List<InvTypeEntity> m_InvTypeEntityList = new List<InvTypeEntity>();
        private BSData mBSData;
        private AisinoPNL panel1;
        private AisinoPNL panel2;
        private const string RPBS_DATAHF_FLAG = "x";
        private const string RPBS_DATAHQ_FLAG = "\x00a4";
        private const string RPBS_DATAHT_FLAG = "y";
        private AisinoTXT txtPath;
        private XmlComponentLoader xmlComponentLoader1;

        public BSDataOutput()
        {
            this.Initialize();
            foreach (InvTypeInfo info2 in base.TaxCardInstance.get_StateInfo().InvTypeInfo)
            {
                if (info2.InvType == 11)
                {
                    this.lastRepDateHY = info2.LastRepDate;
                }
                if (info2.InvType == 12)
                {
                    this.lastRepDateJDC = info2.LastRepDate;
                }
            }
            this.FillCmbPiaoZhong();
            this.FillCmbMonth_();
            string str = PropertyUtil.GetValue("Aisino.Fwkp.Bsgl.BSDataOutput.OutputPath", "");
            this.txtPath.Text = str;
            this.txtPath.ReadOnly = false;
            this.afterInit = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtPath.Text.Trim() == "")
            {
                MessageManager.ShowMsgBox("INP-251206");
            }
            else if (this.IsValidDriver())
            {
                base.TaxCardInstance.GetStateInfo(false);
                this.isHzfw = true;
                this.GetTaxCardInfo();
                if (this.cmbPiaoZhong.SelectedItem.Equals("增值税专普票"))
                {
                    this.FGorNot = true;
                    this.WriteFileText();
                    if (this.FGorNot)
                    {
                        this.MutiCheckAmountTax();
                    }
                }
                if (this.cmbPiaoZhong.SelectedItem.Equals("货物运输业增值税专用发票"))
                {
                    this.FGorNot = true;
                    this.WriteFileTextHWYS();
                    if (this.FGorNot)
                    {
                        this.MutiCheckAmountTax();
                    }
                }
                if (this.cmbPiaoZhong.SelectedItem.Equals("机动车销售统一发票"))
                {
                    this.FGorNot = true;
                    this.WriteFileTextJDC();
                    if (this.FGorNot)
                    {
                        this.MutiCheckAmountTax();
                    }
                }
                if (this.cmbPiaoZhong.SelectedItem.Equals("电子增值税普通发票"))
                {
                    this.FGorNot = true;
                    this.WriteFileText();
                    if (this.FGorNot)
                    {
                        this.MutiCheckAmountTax();
                    }
                }
                PropertyUtil.SetValue("Aisino.Fwkp.Bsgl.BSDataOutput.OutputPath", this.txtPath.Text.Trim());
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtPath.Text = dialog.SelectedPath;
            }
        }

        private void CheckAmountTax()
        {
            int num5;
            int num6;
            decimal d = 0M;
            decimal num2 = 0M;
            if (this.mBSData != null)
            {
                foreach (FPDetail detail in this.mBSData.FPDetailList)
                {
                    if ((detail.FPType == FPType.s) && !detail.ZFBZ)
                    {
                        d += detail.HJJE;
                        num2 += detail.HJSE;
                    }
                }
            }
            decimal num3 = 0M;
            decimal num4 = 0M;
            string str = this.cmbMonth.SelectedValue.ToString();
            int.TryParse(str.Substring(0, 4), out num5);
            int.TryParse(str.Substring(4, 2), out num6);
            try
            {
                string[] strArray = base.TaxCardInstance.AcqMonthTax(num5, num6).Split(new char[] { ',' });
                if (strArray.Length == 2)
                {
                    num3 = Convert.ToDecimal(strArray[0]);
                    num4 = Convert.ToDecimal(strArray[1]);
                }
            }
            catch (Exception exception)
            {
                if (base.TaxCardInstance.get_RetCode() > 0)
                {
                    MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                }
                else
                {
                    MessageManager.ShowMsgBox("INP-111001", "错误", new string[] { "获取金税设备中统计金额税额异常！" });
                }
                this.loger.Error(exception.Message, exception);
                return;
            }
            d = decimal.Round(d, 2);
            num2 = decimal.Round(num2, 2);
            num3 = decimal.Round(num3, 2);
            num4 = decimal.Round(num4, 2);
            if ((d != num3) || (num2 != num4))
            {
                string str2 = "专用发票";
                MessageManager.ShowMsgBox("INP-251209", "提示", new string[] { str2, num3.ToString("C"), d.ToString("C"), num4.ToString("C"), num2.ToString("C") });
                this.loger.Info("errorInfo: INP-251209");
            }
            else
            {
                MessageManager.ShowMsgBox("INP-251210");
                this.loger.Info("errorInfo: INP-251210");
            }
        }

        private void cmbPiaoZhong_SelectedIndexChanged(object sender, EventArgs e)
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

        private void FillCmbMonth()
        {
            List<IdTextPair> list = new List<IdTextPair>();
            DateTime time = base.TaxCardInstance.get_TaxClock();
            int num = 0;
            DateTime time2 = this.getInitDate();
            if (this.cmbPiaoZhong.SelectedItem.Equals("增值税专普票"))
            {
                if (time.Year == time2.Year)
                {
                    num = time.Month - time2.Month;
                    for (int i = num; i >= 0; i--)
                    {
                        DateTime time3 = time.AddMonths(-i);
                        int id = int.Parse(time3.ToString("yyyyMM"));
                        string text = "本年";
                        if (time3.Year < time.Year)
                        {
                            text = "上年";
                        }
                        text = text + time3.ToString("MM月");
                        list.Add(new IdTextPair(id, text));
                    }
                }
                else if (time.Month == 1)
                {
                    if (base.TaxCardInstance.get_LastRepDateMonth() == 1)
                    {
                        int num4 = int.Parse(((time.Year - 1)).ToString() + "12");
                        string str2 = "上年12月";
                        list.Add(new IdTextPair(num4, str2));
                        num4 = int.Parse(time.Year.ToString() + "01");
                        str2 = "本年01月";
                        list.Add(new IdTextPair(num4, str2));
                    }
                    else
                    {
                        if (time2.Year == (time.Year - 1))
                        {
                            num = 12 - time2.Month;
                        }
                        if (time2.Year < (time.Year - 1))
                        {
                            num = 12;
                        }
                        int num5 = 0;
                        string str3 = "";
                        for (int j = num; j >= 0; j--)
                        {
                            DateTime time4 = time.AddMonths(-(j + 1));
                            num5 = int.Parse(time4.ToString("yyyyMM"));
                            str3 = "本年";
                            str3 = str3 + time4.ToString("MM月");
                            list.Add(new IdTextPair(num5, str3));
                        }
                        num5 = int.Parse(time.Year.ToString() + "01");
                        str3 = "下年01月";
                        list.Add(new IdTextPair(num5, str3));
                    }
                }
                else
                {
                    num = time.Month - 1;
                    for (int k = num; k >= 0; k--)
                    {
                        DateTime time5 = time.AddMonths(-k);
                        int num8 = int.Parse(time5.ToString("yyyyMM"));
                        string str4 = "本年";
                        if (time5.Year < time.Year)
                        {
                            str4 = "上年";
                        }
                        str4 = str4 + time5.ToString("MM月");
                        list.Add(new IdTextPair(num8, str4));
                    }
                }
            }
            if (this.cmbPiaoZhong.SelectedItem.Equals("货物运输业增值税专用发票"))
            {
                if (time.Year == time2.Year)
                {
                    num = time.Month - time2.Month;
                    for (int m = num; m >= 0; m--)
                    {
                        DateTime time6 = time.AddMonths(-m);
                        int num10 = int.Parse(time6.ToString("yyyyMM"));
                        string str5 = "本年";
                        if (time6.Year < time.Year)
                        {
                            str5 = "上年";
                        }
                        str5 = str5 + time6.ToString("MM月");
                        list.Add(new IdTextPair(num10, str5));
                    }
                }
                else if (time.Month == 1)
                {
                    string lastRepDateHY = this.lastRepDateHY;
                    int num11 = -1;
                    if ((lastRepDateHY.Length > 0) && lastRepDateHY.Contains("-"))
                    {
                        num11 = int.Parse(lastRepDateHY.Split(new char[] { '-' })[1]);
                    }
                    if (num11 == 1)
                    {
                        int num12 = int.Parse(((time.Year - 1)).ToString() + "12");
                        string str7 = "上年12月";
                        list.Add(new IdTextPair(num12, str7));
                        num12 = int.Parse(time.Year.ToString() + "01");
                        str7 = "本年01月";
                        list.Add(new IdTextPair(num12, str7));
                    }
                    else
                    {
                        if (time2.Year == (time.Year - 1))
                        {
                            num = 12 - time2.Month;
                        }
                        if (time2.Year < (time.Year - 1))
                        {
                            num = 12;
                        }
                        int num13 = 0;
                        string str8 = "";
                        for (int n = num; n >= 0; n--)
                        {
                            DateTime time7 = time.AddMonths(-(n + 1));
                            num13 = int.Parse(time7.ToString("yyyyMM"));
                            str8 = "本年";
                            str8 = str8 + time7.ToString("MM月");
                            list.Add(new IdTextPair(num13, str8));
                        }
                        num13 = int.Parse(time.Year.ToString() + "01");
                        str8 = "下年01月";
                        list.Add(new IdTextPair(num13, str8));
                    }
                }
                else
                {
                    num = time.Month - 1;
                    for (int num15 = num; num15 >= 0; num15--)
                    {
                        DateTime time8 = time.AddMonths(-num15);
                        int num16 = int.Parse(time8.ToString("yyyyMM"));
                        string str9 = "本年";
                        if (time8.Year < time.Year)
                        {
                            str9 = "上年";
                        }
                        str9 = str9 + time8.ToString("MM月");
                        list.Add(new IdTextPair(num16, str9));
                    }
                }
            }
            if (this.cmbPiaoZhong.SelectedItem.Equals("机动车销售统一发票"))
            {
                if (time.Year == time2.Year)
                {
                    num = time.Month - time2.Month;
                    for (int num17 = num; num17 >= 0; num17--)
                    {
                        DateTime time9 = time.AddMonths(-num17);
                        int num18 = int.Parse(time9.ToString("yyyyMM"));
                        string str10 = "本年";
                        if (time9.Year < time.Year)
                        {
                            str10 = "上年";
                        }
                        str10 = str10 + time9.ToString("MM月");
                        list.Add(new IdTextPair(num18, str10));
                    }
                }
                else if (time.Month == 1)
                {
                    string lastRepDateJDC = this.lastRepDateJDC;
                    int num19 = -1;
                    if ((lastRepDateJDC.Length > 0) && lastRepDateJDC.Contains("-"))
                    {
                        num19 = int.Parse(lastRepDateJDC.Split(new char[] { '-' })[1]);
                    }
                    if (num19 == 1)
                    {
                        int num20 = int.Parse(((time.Year - 1)).ToString() + "12");
                        string str12 = "上年12月";
                        list.Add(new IdTextPair(num20, str12));
                        num20 = int.Parse(time.Year.ToString() + "01");
                        str12 = "本年01月";
                        list.Add(new IdTextPair(num20, str12));
                    }
                    else
                    {
                        if (time2.Year == (time.Year - 1))
                        {
                            num = 12 - time2.Month;
                        }
                        if (time2.Year < (time.Year - 1))
                        {
                            num = 12;
                        }
                        int num21 = 0;
                        string str13 = "";
                        for (int num22 = num; num22 >= 0; num22--)
                        {
                            DateTime time10 = time.AddMonths(-(num22 + 1));
                            num21 = int.Parse(time10.ToString("yyyyMM"));
                            str13 = "本年";
                            str13 = str13 + time10.ToString("MM月");
                            list.Add(new IdTextPair(num21, str13));
                        }
                        num21 = int.Parse(time.Year.ToString() + "01");
                        str13 = "下年01月";
                        list.Add(new IdTextPair(num21, str13));
                    }
                }
                else
                {
                    num = time.Month - 1;
                    for (int num23 = num; num23 >= 0; num23--)
                    {
                        DateTime time11 = time.AddMonths(-num23);
                        int num24 = int.Parse(time11.ToString("yyyyMM"));
                        string str14 = "本年";
                        if (time11.Year < time.Year)
                        {
                            str14 = "上年";
                        }
                        str14 = str14 + time11.ToString("MM月");
                        list.Add(new IdTextPair(num24, str14));
                    }
                }
            }
            this.cmbMonth.DataSource = list;
            this.cmbMonth.DisplayMember = "Text";
            this.cmbMonth.ValueMember = "Id";
        }

        private void FillCmbMonth_()
        {
            List<IdTextPair> list = new List<IdTextPair>();
            base.TaxCardInstance.get_TaxClock();
            DateTime time = this.getInitDate();
            try
            {
                for (DateTime time2 = new DateTime(base.TaxCardInstance.get_SysYear(), base.TaxCardInstance.get_SysMonth(), 1); DateTime.Compare(time, time2) <= 0; time2 = time2.AddMonths(-1))
                {
                    int id = int.Parse(time2.ToString("yyyyMM"));
                    string text = time2.ToString("yyyy年MM月");
                    list.Add(new IdTextPair(id, text));
                }
                this.cmbMonth.DataSource = list;
                this.cmbMonth.DisplayMember = "Text";
                this.cmbMonth.ValueMember = "Id";
            }
            catch (Exception exception)
            {
                this.loger.Debug(exception.ToString());
            }
        }

        private void FillCmbPiaoZhong()
        {
            this.m_InvTypeEntityList = this.m_commFun.GetInvTypeCollect();
            foreach (InvTypeEntity entity in this.m_InvTypeEntityList)
            {
                if (((entity.m_invType == INV_TYPE.INV_SPECIAL) || (entity.m_invType == INV_TYPE.INV_COMMON)) && !this.cmbPiaoZhong.Items.Contains("增值税专普票"))
                {
                    this.cmbPiaoZhong.Items.Add("增值税专普票");
                }
                if (entity.m_invType == INV_TYPE.INV_TRANSPORTATION)
                {
                    this.cmbPiaoZhong.Items.Add("货物运输业增值税专用发票");
                }
                if (entity.m_invType == INV_TYPE.INV_VEHICLESALES)
                {
                    this.cmbPiaoZhong.Items.Add("机动车销售统一发票");
                }
                if (entity.m_invType == INV_TYPE.INV_PTDZ)
                {
                    this.cmbPiaoZhong.Items.Add("电子增值税普通发票");
                }
            }
            if (this.cmbPiaoZhong.Items.Count > 0)
            {
                this.cmbPiaoZhong.SelectedIndex = 0;
            }
        }

        private string GetFirstRowText()
        {
            StringBuilder builder = new StringBuilder();
            if (this.mBSData != null)
            {
                builder.Append(this.mBSData.SWJGDM + " ");
                builder.Append(this.mBSData.KPNY + " ");
                builder.Append(this.mBSData.MXJLS.ToString("D6") + " ");
                builder.Append(this.mBSData.KPJH.ToString("D3"));
            }
            return builder.ToString();
        }

        private int GetFpbz(bool isZf, decimal dhjje)
        {
            if (dhjje > 0M)
            {
                if (isZf)
                {
                    return 3;
                }
                return 0;
            }
            if (dhjje < 0M)
            {
                if (isZf)
                {
                    return 4;
                }
                return 1;
            }
            if ((Convert.ToDouble(Math.Abs(dhjje)) < 1E-08) && isZf)
            {
                return 2;
            }
            return 0;
        }

        private List<string> GetFpDetailText()
        {
            List<string> list = new List<string>();
            if (this.mBSData != null)
            {
                Encoding encoding = ToolUtil.GetEncoding();
                foreach (FPDetail detail in this.mBSData.FPDetailList)
                {
                    if (detail != null)
                    {
                        StringBuilder builder = new StringBuilder();
                        if (detail.ZFBZ)
                        {
                            builder.Append("#");
                        }
                        builder.Append(((int) detail.FPType) + " ");
                        builder.Append(detail.FPDM + " ");
                        builder.Append(detail.FPHM.ToString("D8") + " ");
                        builder.Append(detail.KPRQ.ToString("yyyyMMdd").Substring(2) + " ");
                        builder.Append(detail.GFSH + " ");
                        builder.Append(detail.XFSH + " ");
                        builder.Append(string.Format("{0,16:F2}", detail.HJJE) + " ");
                        builder.Append(string.Format("{0,6:F2}", detail.SLV) + " ");
                        builder.Append(string.Format("{0,14:F2}", detail.HJSE));
                        builder.Append(this.MinusFPText(detail));
                        if (this.isHzfw || !string.IsNullOrEmpty(detail.HXM))
                        {
                            FPDetailDAL ldal = new FPDetailDAL();
                            detail.GoodsList.AddRange(ldal.GetGoodsList(detail.FPType.ToString(), detail.FPDM, detail.FPHM));
                            if (detail.QDBZ)
                            {
                                detail.QDList.AddRange(ldal.GetGoodsQDList(detail.FPType.ToString(), detail.FPDM, detail.FPHM));
                            }
                            builder.Append(" 0 ");
                            string fPMainText = GetFPMainText(detail);
                            string str2 = BitConverter.ToString(MD5_Crypt.GetHash(encoding.GetBytes("x" + fPMainText + "y"))).Replace("-", "");
                            builder.Append(str2);
                            builder.Append(" ");
                            string fpGoodsText = this.GetFpGoodsText(detail);
                            builder.Append(fpGoodsText);
                            builder.Append("\x00a4");
                            byte[] hash = MD5_Crypt.GetHash(encoding.GetBytes(fpGoodsText));
                            builder.Append(BitConverter.ToString(hash).Replace("-", ""));
                            builder.Append("\x00a4");
                            builder.Append(encoding.GetBytes(fpGoodsText).Length);
                            builder.Append("\r\n");
                        }
                        else
                        {
                            builder.Append(" 0\r\n");
                        }
                        list.Add(builder.ToString());
                    }
                }
            }
            return list;
        }

        private string GetFpGoodsText(FPDetail fpDetail)
        {
            string str = "";
            if (fpDetail != null)
            {
                str = str + "BARCODEKEY\x00a4";
                string str2 = string.Empty;
                if (fpDetail.YYSBZ.Substring(2, 1) == "1")
                {
                    str2 = "V2";
                }
                else if (fpDetail.YYSBZ.Substring(2, 1) == "2")
                {
                    str2 = "V3";
                }
                else if (fpDetail.YYSBZ.Substring(2, 1) == "3")
                {
                    str2 = "V4";
                }
                else if (fpDetail.YYSBZ.Substring(5, 1) == "1")
                {
                    str2 = "V5";
                }
                else if (fpDetail.YYSBZ.Substring(5, 1) == "2")
                {
                    str2 = "V6";
                }
                else if (fpDetail.YYSBZ.Substring(1, 1) == "1")
                {
                    str2 = "V1";
                }
                str = str + fpDetail.FPDM + str2 + "\x00a4";
                string str3 = fpDetail.XFMC.Replace("\n", "").Replace("\r", "");
                if (string.Empty.Equals(str3))
                {
                    str3 = " ";
                }
                str = str + str3 + "\x00a4";
                string gFMC = fpDetail.GFMC;
                if (gFMC.Trim().Length <= 0)
                {
                    gFMC = " ";
                }
                gFMC = gFMC.Replace("\n", "").Replace("\r", "");
                str = str + gFMC + "\x00a4";
                string mW = fpDetail.MW;
                str = str + mW + "\x00a4";
                string jYM = fpDetail.JYM;
                if (string.IsNullOrEmpty(jYM))
                {
                    jYM = " ";
                }
                str = str + jYM + "\x00a4";
                string jQBH = fpDetail.JQBH;
                if (string.IsNullOrEmpty(jQBH))
                {
                    jQBH = " ";
                }
                str = str + jQBH + "\x00a4";
                string gFDZDH = fpDetail.GFDZDH;
                if (string.IsNullOrEmpty(gFDZDH))
                {
                    gFDZDH = " ";
                }
                str = str + gFDZDH + "\x00a4";
                string gFYHZH = fpDetail.GFYHZH;
                if (string.IsNullOrEmpty(gFYHZH))
                {
                    gFYHZH = " ";
                }
                str = str + gFYHZH + "\x00a4";
                string xFDZDH = fpDetail.XFDZDH;
                if (string.IsNullOrEmpty(xFDZDH))
                {
                    xFDZDH = " ";
                }
                str = str + xFDZDH + "\x00a4";
                string xFYHZH = fpDetail.XFYHZH;
                if (string.IsNullOrEmpty(xFYHZH))
                {
                    xFYHZH = " ";
                }
                str = str + xFYHZH + "\x00a4";
                string sKR = fpDetail.SKR;
                string fHR = fpDetail.FHR;
                string kPR = fpDetail.KPR;
                if (string.IsNullOrEmpty(sKR))
                {
                    sKR = " ";
                }
                if (string.IsNullOrEmpty(fHR))
                {
                    fHR = " ";
                }
                if (string.IsNullOrEmpty(kPR))
                {
                    kPR = " ";
                }
                str = ((str + sKR + "\x00a4") + fHR + "\x00a4") + kPR + "\x00a4";
                string bZ = fpDetail.BZ;
                if (bZ.Contains(Environment.NewLine))
                {
                    bZ = bZ.Replace(Environment.NewLine, "￠\x00a7");
                }
                if (string.IsNullOrEmpty(bZ))
                {
                    bZ = " ";
                }
                str = str + bZ + "\x00a4";
                string sIGN = fpDetail.SIGN;
                str = str + sIGN + "\x00a4";
                DateTime zFRQ = fpDetail.ZFRQ;
                string str17 = null;
                str17 = string.Format("{0:yyyyMMddHHmmss}", zFRQ);
                if (!fpDetail.ZFBZ)
                {
                    str17 = " ";
                }
                str = str + str17 + "\x00a4";
                string str18 = Convert.ToString((decimal) (fpDetail.HJJE + fpDetail.HJSE));
                str = str + str18 + "\x00a4";
                if ((fpDetail.QDBZ && (fpDetail.QDList != null)) && (fpDetail.QDList.Count > 0))
                {
                    object obj2 = str;
                    str = string.Concat(new object[] { obj2, fpDetail.GoodsNum, " ", fpDetail.QDList.Count.ToString(), "\x00a4" });
                }
                else
                {
                    str = str + fpDetail.GoodsNum + "\x00a4";
                }
                string str19 = "";
                foreach (GoodsInfo info in fpDetail.GoodsList)
                {
                    str19 = str19 + this.GetGoodsText(info);
                }
                if (str19.Length >= 1)
                {
                    str = str + str19.Remove(str19.Length - 1);
                }
                if ((fpDetail.QDBZ && (fpDetail.QDList != null)) && (fpDetail.QDList.Count > 0))
                {
                    str19 = "";
                    str19 = str19 + "\x00a4";
                    foreach (GoodsInfo info2 in fpDetail.QDList)
                    {
                        str19 = str19 + this.GetGoodsText(info2);
                    }
                    if (str19.Length >= 1)
                    {
                        str = str + str19.Remove(str19.Length - 1);
                    }
                }
            }
            return str;
        }

        private static string GetFPMainText(FPDetail fpDetail)
        {
            string str = "";
            if (fpDetail == null)
            {
                return str;
            }
            if (fpDetail.ZFBZ)
            {
                str = str + "#";
            }
            return ((((str + ((int) fpDetail.FPType).ToString().Trim() + fpDetail.FPDM.Trim()) + fpDetail.FPHM.ToString("D8").Trim() + fpDetail.KPRQ.ToString("yyyyMMdd").Substring(2).Trim()) + fpDetail.GFSH.Trim() + fpDetail.XFSH.Trim()) + string.Format("{0:F2}", fpDetail.HJJE) + string.Format("{0:F2}", fpDetail.HJSE));
        }

        private string GetGoodsText(GoodsInfo goods)
        {
            StringBuilder builder = new StringBuilder();
            if (goods != null)
            {
                builder.Append(goods.RowNo + "\x00a4");
                builder.Append(goods.Name.Trim() + "\x00a4");
                builder.Append(goods.SpecMark.Trim() + " \x00a4");
                builder.Append(goods.Unit.Trim() + " \x00a4");
                builder.Append(goods.Num.ToString() + " \x00a4");
                builder.Append(goods.Price.ToString() + " \x00a4");
                builder.Append(goods.Amount.ToString() + "\x00a4");
                string str = "";
                if (goods.SLV == -1f)
                {
                    str = " ";
                }
                else
                {
                    str = goods.SLV.ToString("F2");
                }
                builder.Append(str + " \x00a4");
                builder.Append(goods.Tax.ToString("F2") + "\x00a4");
                builder.Append(Convert.ToInt32(goods.TaxSign) + "\x00a4");
            }
            return builder.ToString();
        }

        private DateTime getInitDate()
        {
            DateTime time = base.TaxCardInstance.get_TaxClock().AddMonths(-12);
            string str = base.TaxCardInstance.get_CardEffectDate();
            if (!string.IsNullOrEmpty(str))
            {
                int year = int.Parse(str.Substring(0, 4));
                int month = int.Parse(str.Substring(4, 2));
                time = new DateTime(year, month, 1);
            }
            return time;
        }

        private DateTime GetInitDate()
        {
            DateTime time = base.TaxCardInstance.get_TaxClock().AddMonths(-12);
            string path = @"..\Config\Common\Kzyf.Configure.XML";
            try
            {
                if (File.Exists(path))
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(path);
                    string innerText = document.DocumentElement.SelectSingleNode("kzyf").InnerText;
                    time = Convert.ToDateTime(string.Format("{0}-{1}-01", innerText.Substring(0, 4), innerText.Substring(4, 2)));
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message, exception);
            }
            return time;
        }

        private List<InvTypeEntity> GetInvTypeCollect()
        {
            List<InvTypeEntity> list = new List<InvTypeEntity>();
            InvTypeEntity item = new InvTypeEntity();
            bool iSZYFP = base.TaxCardInstance.get_QYLX().ISZYFP;
            bool iSPTFP = base.TaxCardInstance.get_QYLX().ISPTFP;
            if (iSZYFP)
            {
                item.m_invType = INV_TYPE.INV_SPECIAL;
                item.m_strInvName = "增值税专用发票";
                list.Add(item);
            }
            if (iSPTFP)
            {
                item = new InvTypeEntity {
                    m_invType = INV_TYPE.INV_COMMON,
                    m_strInvName = "增值税普通发票"
                };
                list.Add(item);
            }
            bool iSHY = base.TaxCardInstance.get_QYLX().ISHY;
            if (iSHY)
            {
                item = new InvTypeEntity {
                    m_invType = INV_TYPE.INV_TRANSPORTATION,
                    m_strInvName = "货物运输业增值税专用发票"
                };
                list.Add(item);
            }
            bool iSJDC = base.TaxCardInstance.get_QYLX().ISJDC;
            if (iSJDC)
            {
                item = new InvTypeEntity {
                    m_invType = INV_TYPE.INV_VEHICLESALES,
                    m_strInvName = "机动车销售统一发票"
                };
                list.Add(item);
            }
            if ((!iSZYFP && !iSPTFP) && (!iSHY && !iSJDC))
            {
                item.m_invType = INV_TYPE.INV_SPECIAL;
                item.m_strInvName = "增值税专用发票";
                list.Add(item);
                item = new InvTypeEntity {
                    m_invType = INV_TYPE.INV_COMMON,
                    m_strInvName = "增值税普通发票"
                };
                list.Add(item);
            }
            return list;
        }

        private string GetSecondRowText()
        {
            StringBuilder builder = new StringBuilder();
            if (this.mBSData != null)
            {
                builder.Append("#");
                builder.Append(this.mBSData.NSRName + " ");
                builder.Append(this.mBSData.NSRID + " ");
                builder.Append(this.mBSData.FPLB.ToString() + "\r\n");
            }
            return builder.ToString();
        }

        private void GetTaxCardInfo()
        {
            try
            {
                this.mBSData = new BSData();
                if (base.TaxCardInstance.get_TaxCode() != null)
                {
                    this.mBSData.SWJGDM = base.TaxCardInstance.get_TaxCode().Substring(0, 6);
                }
                this.mBSData.KPNY = this.GetYearMonth();
                this.mBSData.KPJH = base.TaxCardInstance.get_Machine();
                this.mBSData.NSRName = base.TaxCardInstance.get_Corporation();
                this.mBSData.NSRID = base.TaxCardInstance.get_TaxCode();
                this.mBSData.Address = base.TaxCardInstance.get_Address();
                this.mBSData.Phone = base.TaxCardInstance.get_Telephone();
                this.mBSData.FPLB = FPLB.V61;
                FPDetailDAL ldal = new FPDetailDAL();
                if (this.cmbPiaoZhong.SelectedItem.Equals("增值税专普票"))
                {
                    this.mBSData.FPDetailList.AddRange(ldal.GetFPDetailListByFPZL((int) this.cmbMonth.SelectedValue, base.TaxCardInstance.get_TaxCode(), "s"));
                    this.mBSData.FPDetailList.AddRange(ldal.GetFPDetailListByFPZL((int) this.cmbMonth.SelectedValue, base.TaxCardInstance.get_TaxCode(), "c"));
                }
                if (this.cmbPiaoZhong.SelectedItem.Equals("货物运输业增值税专用发票"))
                {
                    this.mBSData.FPDetailList.AddRange(ldal.GetFPDetailListByFPZL((int) this.cmbMonth.SelectedValue, base.TaxCardInstance.get_TaxCode(), "f"));
                    List<FPDetail> list = new List<FPDetail>();
                    foreach (FPDetail detail in this.mBSData.FPDetailList)
                    {
                        if (detail != null)
                        {
                            detail.GoodsList.AddRange(ldal.GetGoodsList(detail.FPType.ToString(), detail.FPDM, detail.FPHM));
                        }
                    }
                }
                if (this.cmbPiaoZhong.SelectedItem.Equals("机动车销售统一发票"))
                {
                    this.mBSData.FPDetailList.AddRange(ldal.GetFPDetailListByFPZL((int) this.cmbMonth.SelectedValue, base.TaxCardInstance.get_TaxCode(), "j"));
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message, exception);
            }
        }

        private string GetYearMonth()
        {
            object selectedValue = this.cmbMonth.SelectedValue;
            if (selectedValue != null)
            {
                string str = selectedValue.ToString();
                if (str.Length == 6)
                {
                    if (this.cmbPiaoZhong.SelectedItem.Equals("增值税专普票"))
                    {
                        return str.Substring(2);
                    }
                    if (this.cmbPiaoZhong.SelectedItem.Equals("货物运输业增值税专用发票") || this.cmbPiaoZhong.SelectedItem.Equals("机动车销售统一发票"))
                    {
                        return str;
                    }
                }
            }
            if (this.cmbPiaoZhong.SelectedItem.Equals("增值税专普票"))
            {
                return DateTime.Today.ToString("yyyyMMdd").Substring(2, 4);
            }
            return DateTime.Today.ToString("yyyyMMdd");
        }

        private void Initialize()
        {
            this.InitializeComponent();
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.panel1 = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel1");
            this.panel2 = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel2");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.groupBox1 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox1");
            this.btnSelect = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnSelect");
            this.txtPath = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtPath");
            this.cmbMonth = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbMonth");
            this.label2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label2");
            this.label1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label1");
            this.cmbPiaoZhong = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbPiaoZhong");
            this.lablePiaoZhong = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lablePiaoZhong");
            this.cmbMonth.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbPiaoZhong.DropDownStyle = ComboBoxStyle.DropDownList;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.btnSelect.Click += new EventHandler(this.btnSelect_Click);
            this.cmbPiaoZhong.SelectedIndexChanged += new EventHandler(this.cmbPiaoZhong_SelectedIndexChanged);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(BSDataOutput));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x1a0, 0x130);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Bsgl.BSDataOutput\Aisino.Fwkp.Bsgl.BSDataOutput.xml");
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1a0, 0x130);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(0, 0);
            base.Name = "BSDataOutput";
            this.Text = "报税资料传出";
            base.ResumeLayout(false);
        }

        private bool IsValidDriver()
        {
            string driveName = "";
            try
            {
                if (!Directory.Exists(this.txtPath.Text))
                {
                    MessageManager.ShowMsgBox("INP-251207");
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            if (this.txtPath.Text.Length < 3)
            {
                MessageManager.ShowMsgBox("INP-251207");
                return false;
            }
            driveName = this.txtPath.Text.Substring(0, 3);
            try
            {
                DriveInfo info = new DriveInfo(driveName);
                if (info.AvailableFreeSpace < 0x989680L)
                {
                    MessageManager.ShowMsgBox("INP-251201");
                    return false;
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-251205");
                this.loger.Error(exception.Message, exception);
                return false;
            }
            return true;
        }

        private string MinusFPText(FPDetail fpDetail)
        {
            string str = "";
            try
            {
                if ((fpDetail == null) || (fpDetail.HJJE >= 0M))
                {
                    return str;
                }
                int num = 0;
                if (fpDetail.FPType == FPType.s)
                {
                    num = 1;
                }
                else if (fpDetail.FPType == FPType.c)
                {
                    num = 0;
                }
                string str2 = NotesUtil.GetInfo(fpDetail.BZ, num, "");
                if (fpDetail.FPType == FPType.s)
                {
                    str2 = fpDetail.HZTZDH.ToString();
                    str = " " + str2 + " TZD";
                }
                if (fpDetail.FPType == FPType.c)
                {
                    string str3 = "";
                    string str4 = "";
                    string str5 = fpDetail.LZDMHM.ToString();
                    if (((str5.Length > 0) && str5.Contains("_")) && (str5.Split(new char[] { '_' }).Length == 2))
                    {
                        str3 = str5.Split(new char[] { '_' })[0];
                        str4 = str5.Split(new char[] { '_' })[1];
                        str = " " + str3 + " " + str4;
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message, exception);
            }
            return str;
        }

        private void MutiCheckAmountTax()
        {
            int num5;
            int num6;
            decimal d = 0M;
            decimal num2 = 0M;
            if (this.mBSData != null)
            {
                foreach (FPDetail detail in this.mBSData.FPDetailList)
                {
                    if (!detail.ZFBZ)
                    {
                        d += detail.HJJE;
                        num2 += detail.HJSE;
                    }
                }
            }
            decimal num3 = 0M;
            decimal num4 = 0M;
            string str = this.cmbMonth.SelectedValue.ToString();
            int.TryParse(str.Substring(0, 4), out num5);
            int.TryParse(str.Substring(4, 2), out num6);
            try
            {
                TaxStatisData data = base.TaxCardInstance.GetMonthStatistics(num5, num6, 0);
                if (this.cmbPiaoZhong.SelectedItem.Equals("增值税专普票"))
                {
                    InvAmountTaxStati stati = data.InvTypeStatData(0);
                    num3 = Convert.ToDecimal(stati.get_Total().SJXSJE);
                    num4 = Convert.ToDecimal(stati.get_Total().SJXXSE);
                    stati = data.InvTypeStatData(2);
                    num3 += Convert.ToDecimal(stati.get_Total().SJXSJE);
                    num4 += Convert.ToDecimal(stati.get_Total().SJXXSE);
                }
                if (this.cmbPiaoZhong.SelectedItem.Equals("货物运输业增值税专用发票"))
                {
                    InvAmountTaxStati stati2 = data.InvTypeStatData(11);
                    num3 = Convert.ToDecimal(stati2.get_Total().SJXSJE);
                    num4 = Convert.ToDecimal(stati2.get_Total().SJXXSE);
                }
                if (this.cmbPiaoZhong.SelectedItem.Equals("机动车销售统一发票"))
                {
                    InvAmountTaxStati stati3 = data.InvTypeStatData(12);
                    num3 = Convert.ToDecimal(stati3.get_Total().SJXSJE);
                    num4 = Convert.ToDecimal(stati3.get_Total().SJXXSE);
                }
            }
            catch (Exception exception)
            {
                if (base.TaxCardInstance.get_RetCode() > 0)
                {
                    MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                }
                else
                {
                    MessageManager.ShowMsgBox("INP-111001", "错误", new string[] { "获取金税设备中统计金额税额异常！" });
                }
                this.loger.Error(exception.Message, exception);
                return;
            }
            d = decimal.Round(d, 2);
            num2 = decimal.Round(num2, 2);
            num3 = decimal.Round(num3, 2);
            num4 = decimal.Round(num4, 2);
            if ((d != num3) || (num2 != num4))
            {
                string str2 = "";
                if (this.cmbPiaoZhong.SelectedItem.Equals("增值税专普票"))
                {
                    str2 = "增值税专普票";
                }
                if (this.cmbPiaoZhong.SelectedItem.Equals("货物运输业增值税专用发票"))
                {
                    str2 = "货物运输业增值税专用发票";
                }
                if (this.cmbPiaoZhong.SelectedItem.Equals("机动车销售统一发票"))
                {
                    str2 = "机动车销售统一发票";
                }
                MessageManager.ShowMsgBox("INP-251209", "提示", new string[] { str2, num3.ToString("C"), d.ToString("C"), num4.ToString("C"), num2.ToString("C") });
                MessageManager.ShowMsgBox("INP-251210");
                this.loger.Info("errorInfo: INP-251209");
            }
            else
            {
                MessageManager.ShowMsgBox("INP-251210");
                this.loger.Info("errorInfo: INP-251210");
            }
        }

        private void WriteDZFPFileText()
        {
            if (this.mBSData != null)
            {
                FileStream stream;
                string str = "fpzl.txt";
                if (this.mBSData.NSRID != null)
                {
                    str = "X" + this.mBSData.NSRID.Substring(this.mBSData.NSRID.Length - 7, 7) + "." + this.mBSData.KPNY.Substring(2, 2);
                }
                string path = Path.Combine(this.txtPath.Text, str);
                try
                {
                    if (File.Exists(path))
                    {
                        if (MessageManager.ShowMsgBox("INP-251208") == DialogResult.Yes)
                        {
                            this.FGorNot = true;
                            File.Delete(path);
                        }
                        else
                        {
                            this.FGorNot = false;
                            return;
                        }
                    }
                    stream = File.Create(path);
                }
                catch (Exception exception)
                {
                    MessageManager.ShowMsgBox("INP-251203", "错误", new string[] { path });
                    this.loger.Error(exception.Message, exception);
                    return;
                }
                string firstRowText = this.GetFirstRowText();
                byte[] bytes = ToolUtil.GetBytes(firstRowText + "\0\r\n");
                string secondRowText = this.GetSecondRowText();
                byte[] buffer2 = ToolUtil.GetBytes(firstRowText);
                byte[] buffer = IDEA_Ctypt.DataToCryp(buffer2, ToolUtil.GetBytes(secondRowText));
                List<string> fpDetailText = this.GetFpDetailText();
                try
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Write(buffer, 0, buffer.Length);
                    foreach (string str6 in fpDetailText)
                    {
                        byte[] buffer4 = IDEA_Ctypt.DataToCryp(buffer2, ToolUtil.GetBytes(str6));
                        stream.Write(buffer4, 0, buffer4.Length);
                    }
                    stream.Flush();
                    stream.Close();
                }
                catch (Exception exception2)
                {
                    MessageManager.ShowMsgBox("INP-251202");
                    this.loger.Error(exception2.Message, exception2);
                }
            }
        }

        private void WriteFileText()
        {
            if (this.mBSData != null)
            {
                FileStream stream;
                string str = "fpzl.txt";
                if (this.mBSData.NSRID != null)
                {
                    str = "X" + this.mBSData.NSRID.Substring(this.mBSData.NSRID.Length - 7, 7) + "." + this.mBSData.KPNY.Substring(2, 2);
                }
                string path = Path.Combine(this.txtPath.Text, str);
                try
                {
                    if (File.Exists(path))
                    {
                        if (MessageManager.ShowMsgBox("INP-251208") == DialogResult.Yes)
                        {
                            this.FGorNot = true;
                            File.Delete(path);
                        }
                        else
                        {
                            this.FGorNot = false;
                            return;
                        }
                    }
                    stream = File.Create(path);
                }
                catch (Exception exception)
                {
                    MessageManager.ShowMsgBox("INP-251203", "错误", new string[] { path });
                    this.loger.Error(exception.Message, exception);
                    return;
                }
                string firstRowText = this.GetFirstRowText();
                byte[] bytes = ToolUtil.GetBytes(firstRowText + "\0\r\n");
                string secondRowText = this.GetSecondRowText();
                byte[] buffer2 = ToolUtil.GetBytes(firstRowText);
                byte[] buffer = IDEA_Ctypt.DataToCryp(buffer2, ToolUtil.GetBytes(secondRowText));
                List<string> fpDetailText = this.GetFpDetailText();
                try
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Write(buffer, 0, buffer.Length);
                    foreach (string str6 in fpDetailText)
                    {
                        byte[] buffer4 = IDEA_Ctypt.DataToCryp(buffer2, ToolUtil.GetBytes(str6));
                        stream.Write(buffer4, 0, buffer4.Length);
                    }
                    stream.Flush();
                    stream.Close();
                }
                catch (Exception exception2)
                {
                    MessageManager.ShowMsgBox("INP-251202");
                    this.loger.Error(exception2.Message, exception2);
                }
            }
        }

        private void WriteFileTextHWYS()
        {
            string str5;
            string str8;
            if (this.mBSData != null)
            {
                decimal num = 0M;
                decimal num2 = 0M;
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
                document.AppendChild(newChild);
                XmlElement element = document.CreateElement("business");
                element.SetAttribute("version", "1.0");
                element.SetAttribute("comment", "货物运输业增值税专用发票");
                element.SetAttribute("id", "HWYSYZZSZYFP");
                for (int i = 0; i < this.mBSData.FPDetailList.Count; i++)
                {
                    FPDetail detail = this.mBSData.FPDetailList[i];
                    XmlElement element2 = document.CreateElement("body");
                    element2.SetAttribute("no", (i + 1).ToString());
                    XmlElement element3 = document.CreateElement("fpdm");
                    element3.InnerText = detail.FPDM;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("fphm");
                    element3.InnerText = string.Format("{0:00000000}", detail.FPHM);
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("kprq");
                    element3.InnerText = detail.KPRQ.ToString("yyyy-MM-dd");
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("kpsj");
                    element3.InnerText = "";
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("jqbh");
                    element3.InnerText = detail.JQBH;
                    element2.AppendChild(element3);
                    decimal hJJE = detail.HJJE;
                    element3 = document.CreateElement("hjje");
                    element3.InnerText = hJJE.ToString("F2");
                    element2.AppendChild(element3);
                    bool zFBZ = detail.ZFBZ;
                    if (!zFBZ)
                    {
                        num += hJJE;
                    }
                    element3 = document.CreateElement("slv");
                    element3.InnerText = (detail.SLV * 100f).ToString();
                    element2.AppendChild(element3);
                    decimal hJSE = detail.HJSE;
                    element3 = document.CreateElement("se");
                    element3.InnerText = hJSE.ToString("F2");
                    element2.AppendChild(element3);
                    if (!zFBZ)
                    {
                        num2 += hJSE;
                    }
                    decimal num6 = hJJE + hJSE;
                    element3 = document.CreateElement("jshj");
                    element3.InnerText = num6.ToString("F2");
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("czch");
                    element3.InnerText = detail.QYD;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("ccdw");
                    element3.InnerText = detail.YYZZH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("wspzhm");
                    element3.InnerText = detail.WSPZHM;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("skm");
                    element3.InnerText = detail.MW;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("cyrmc");
                    element3.InnerText = detail.XFMC;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("cyrsbh");
                    element3.InnerText = detail.XFSH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("spfmc");
                    element3.InnerText = detail.GFMC;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("spfsbh");
                    element3.InnerText = detail.GFSH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("shrmc");
                    element3.InnerText = detail.GFDZDH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("shrsbh");
                    element3.InnerText = detail.CM;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("fhrmc");
                    element3.InnerText = detail.XFDZDH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("fhrsbh");
                    element3.InnerText = detail.TYDH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("qyd");
                    element3.InnerText = detail.XFYHZH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("yshwxx");
                    element3.InnerText = detail.YSHWXX;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("bz");
                    element3.InnerText = detail.BZ;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("swjgmc");
                    element3.InnerText = detail.GFYHZH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("swjgdm");
                    element3.InnerText = detail.HYBM;
                    element2.AppendChild(element3);
                    bool isZf = detail.ZFBZ;
                    int fpbz = this.GetFpbz(isZf, hJJE);
                    element3 = document.CreateElement("fpbz");
                    element3.InnerText = fpbz.ToString();
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("skr");
                    element3.InnerText = detail.SKR;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("fhr");
                    element3.InnerText = detail.FHR;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("kpr");
                    element3.InnerText = detail.KPR;
                    element2.AppendChild(element3);
                    string str = "";
                    if (hJJE < 0M)
                    {
                        string str2 = "";
                        string str3 = "";
                        element3 = document.CreateElement("yfpdm");
                        element3.InnerText = str2;
                        element2.AppendChild(element3);
                        element3 = document.CreateElement("yfphm");
                        element3.InnerText = str3;
                        element2.AppendChild(element3);
                        str = detail.HZTZDH.ToString();
                    }
                    if (isZf)
                    {
                        element3 = document.CreateElement("zfrq");
                        element3.InnerText = detail.KPRQ.ToString("yyyy-MM-dd");
                        element2.AppendChild(element3);
                        element3 = document.CreateElement("zfsj");
                        element3.InnerText = "";
                        element2.AppendChild(element3);
                        element3 = document.CreateElement("zfr");
                        element3.InnerText = detail.KPR;
                        element2.AppendChild(element3);
                    }
                    element3 = document.CreateElement("tzdh");
                    element3.InnerText = str;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("qmz");
                    element3.InnerText = detail.SIGN;
                    element2.AppendChild(element3);
                    if (detail.GoodsNum > 0)
                    {
                        for (int j = 0; j < detail.GoodsList.Count; j++)
                        {
                            element3 = document.CreateElement("zb");
                            XmlElement element4 = document.CreateElement("xh");
                            element4.InnerText = detail.GoodsList[j].FPMXXH;
                            element3.AppendChild(element4);
                            element4 = document.CreateElement("fyxm");
                            element4.InnerText = detail.GoodsList[j].Name;
                            element3.AppendChild(element4);
                            element4 = document.CreateElement("je");
                            element4.InnerText = detail.GoodsList[j].Amount.ToString("F2");
                            element3.AppendChild(element4);
                            element2.AppendChild(element3);
                        }
                    }
                    element.AppendChild(element2);
                }
                document.AppendChild(element);
                string str4 = "XTHTDATA";
                MemoryStream outStream = new MemoryStream();
                document.Save(outStream);
                byte[] bytes = ToolUtil.GetBytes(str4);
                byte[] buffer = outStream.GetBuffer();
                str5 = IDEA_Ctypt.DataToCryp(bytes, buffer, true);
                outStream.Flush();
                outStream.Close();
                string str6 = "fpzl.txt";
                string invControlNum = base.TaxCardInstance.GetInvControlNum();
                if (this.mBSData.NSRID != null)
                {
                    str6 = "CGLPLBL_HH_" + this.mBSData.NSRID + "_" + invControlNum + "_" + this.mBSData.KPNY + "_0001_0001.DAT";
                    string text2 = "HH" + string.Format("{0, -20}", this.mBSData.NSRID) + string.Format("{0, -12}", invControlNum) + this.mBSData.KPNY + "0001";
                }
                str8 = Path.Combine(this.txtPath.Text, str6);
                try
                {
                    if (!File.Exists(str8))
                    {
                        goto Label_0988;
                    }
                    if (MessageManager.ShowMsgBox("INP-251208") == DialogResult.Yes)
                    {
                        this.FGorNot = true;
                        File.Delete(str8);
                        goto Label_0988;
                    }
                    this.FGorNot = false;
                }
                catch (Exception exception)
                {
                    MessageManager.ShowMsgBox("INP-251203", "错误", new string[] { str8 });
                    this.loger.Error(exception.Message, exception);
                }
            }
            return;
        Label_0988:
            try
            {
                using (StreamWriter writer = new StreamWriter(str8, false, ToolUtil.GetEncoding()))
                {
                    writer.Write(str5);
                    writer.Flush();
                    writer.Close();
                }
            }
            catch (Exception exception2)
            {
                MessageManager.ShowMsgBox("INP-251202");
                this.loger.Error(exception2.Message, exception2);
            }
        }

        private void WriteFileTextJDC()
        {
            string str5;
            string str8;
            if (this.mBSData != null)
            {
                decimal num = 0M;
                decimal num2 = 0M;
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
                document.AppendChild(newChild);
                XmlElement element = document.CreateElement("business");
                element.SetAttribute("version", "1.0");
                element.SetAttribute("comment", "机动车销售统一发票");
                element.SetAttribute("id", "JDCFP");
                for (int i = 0; i < this.mBSData.FPDetailList.Count; i++)
                {
                    FPDetail detail = this.mBSData.FPDetailList[i];
                    XmlElement element2 = document.CreateElement("body");
                    element2.SetAttribute("no", (i + 1).ToString());
                    XmlElement element3 = document.CreateElement("fpdm");
                    element3.InnerText = detail.FPDM;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("fphm");
                    element3.InnerText = string.Format("{0:00000000}", detail.FPHM);
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("jqbh");
                    element3.InnerText = detail.JQBH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("kprq");
                    element3.InnerText = detail.KPRQ.ToString("yyyy-MM-dd");
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("kpsj");
                    element3.InnerText = "";
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("skm");
                    element3.InnerText = detail.MW;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("ghdw");
                    element3.InnerText = detail.GFMC;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("scqymc");
                    element3.InnerText = detail.SCCJMC;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("sfzhm");
                    element3.InnerText = detail.XSBM;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("gfsbh");
                    element3.InnerText = detail.GFSH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("xhdwmc");
                    element3.InnerText = detail.XFMC;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("nsrsbh");
                    element3.InnerText = detail.XFSH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("dz");
                    element3.InnerText = detail.XFDZDH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("dh");
                    element3.InnerText = detail.XFDH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("khyh");
                    element3.InnerText = detail.XFYHZH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("zh");
                    element3.InnerText = detail.KHYHZH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("cpxh");
                    element3.InnerText = detail.XFDZ;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("cllx");
                    element3.InnerText = detail.GFDZDH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("hgzs");
                    element3.InnerText = detail.CM;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("jkzmsh");
                    element3.InnerText = detail.TYDH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("cd");
                    element3.InnerText = detail.KHYHMC;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("sjdh");
                    element3.InnerText = detail.QYD;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("fdjhm");
                    element3.InnerText = detail.ZHD;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("cjhm");
                    element3.InnerText = detail.XHD;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("cjfy");
                    element3.InnerText = "";
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("zzssl");
                    element3.InnerText = (detail.SLV * 100f).ToString();
                    element2.AppendChild(element3);
                    decimal hJSE = detail.HJSE;
                    element3 = document.CreateElement("zzsse");
                    element3.InnerText = hJSE.ToString("F2");
                    element2.AppendChild(element3);
                    decimal hJJE = detail.HJJE;
                    bool zFBZ = detail.ZFBZ;
                    if (!zFBZ)
                    {
                        num += hJJE;
                    }
                    if (!zFBZ)
                    {
                        num2 += hJSE;
                    }
                    decimal num6 = hJJE + hJSE;
                    element3 = document.CreateElement("jshj");
                    element3.InnerText = num6.ToString("F2");
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("kpr");
                    element3.InnerText = detail.KPR;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("dw");
                    element3.InnerText = detail.YYZZH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("xcrs");
                    element3.InnerText = detail.MDD;
                    element2.AppendChild(element3);
                    bool isZf = detail.ZFBZ;
                    int fpbz = this.GetFpbz(isZf, hJJE);
                    element3 = document.CreateElement("fpztbz");
                    element3.InnerText = fpbz.ToString();
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("swjg_dm");
                    element3.InnerText = detail.HYBM;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("swjg_mc");
                    element3.InnerText = detail.GFYHZH;
                    element2.AppendChild(element3);
                    if (hJJE < 0M)
                    {
                        string str = "";
                        string str2 = "";
                        string str3 = detail.LZDMHM.ToString();
                        if (((str3.Length > 0) && str3.Contains("_")) && (str3.Split(new char[] { '_' }).Length == 2))
                        {
                            str = str3.Split(new char[] { '_' })[0];
                            str2 = str3.Split(new char[] { '_' })[1];
                        }
                        element3 = document.CreateElement("yfpdm");
                        element3.InnerText = str;
                        element2.AppendChild(element3);
                        element3 = document.CreateElement("yfphm");
                        element3.InnerText = str2;
                        element2.AppendChild(element3);
                    }
                    if (isZf)
                    {
                        element3 = document.CreateElement("zfrq");
                        element3.InnerText = detail.KPRQ.ToString("yyyy-MM-dd");
                        element2.AppendChild(element3);
                        element3 = document.CreateElement("zfsj");
                        element3.InnerText = "";
                        element2.AppendChild(element3);
                        element3 = document.CreateElement("zfr");
                        element3.InnerText = detail.KPR;
                        element2.AppendChild(element3);
                    }
                    element3 = document.CreateElement("wspzhm");
                    element3.InnerText = detail.WSPZHM;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("qmz");
                    element3.InnerText = detail.SIGN;
                    element2.AppendChild(element3);
                    element.AppendChild(element2);
                }
                document.AppendChild(element);
                string str4 = "XTHTDATA";
                MemoryStream outStream = new MemoryStream();
                document.Save(outStream);
                byte[] bytes = ToolUtil.GetBytes(str4);
                byte[] buffer = outStream.GetBuffer();
                str5 = IDEA_Ctypt.DataToCryp(bytes, buffer, true);
                outStream.Flush();
                outStream.Close();
                string str6 = "fpzl.txt";
                string invControlNum = base.TaxCardInstance.GetInvControlNum();
                if (this.mBSData.NSRID != null)
                {
                    str6 = "CGLPLBL_HH_" + this.mBSData.NSRID + "_" + invControlNum + "_" + this.mBSData.KPNY + "_0001_0002.DAT";
                    string text2 = "HH" + string.Format("{0, -20}", this.mBSData.NSRID) + string.Format("{0, -12}", invControlNum) + this.mBSData.KPNY + "0001";
                }
                str8 = Path.Combine(this.txtPath.Text, str6);
                try
                {
                    if (!File.Exists(str8))
                    {
                        goto Label_099A;
                    }
                    if (MessageManager.ShowMsgBox("INP-251208") == DialogResult.Yes)
                    {
                        this.FGorNot = true;
                        File.Delete(str8);
                        goto Label_099A;
                    }
                    this.FGorNot = false;
                }
                catch (Exception exception)
                {
                    MessageManager.ShowMsgBox("INP-251203", "错误", new string[] { str8 });
                    this.loger.Error(exception.Message, exception);
                }
            }
            return;
        Label_099A:
            try
            {
                using (StreamWriter writer = new StreamWriter(str8, false, ToolUtil.GetEncoding()))
                {
                    writer.Write(str5);
                }
            }
            catch (Exception exception2)
            {
                MessageManager.ShowMsgBox("INP-251202");
                this.loger.Error(exception2.Message, exception2);
            }
        }
    }
}

