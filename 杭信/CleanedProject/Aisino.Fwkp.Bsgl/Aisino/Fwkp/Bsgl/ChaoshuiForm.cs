namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ChaoshuiForm : DockForm
    {
        private ushort BSPRepInfo_HY;
        private ushort BSPRepInfo_JDC;
        private ushort BSPRepInfo_JSFP;
        private ushort BSPRepInfo_PTDZ;
        private ushort BSPRepInfo_ZP;
        private AisinoBTN btnBSPCSQuery;
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private AisinoCHK checkBox1;
        private AisinoCHK checkBox2;
        private AisinoCHK checkBox3;
        private AisinoCMB cmbShuiqi;
        private IContainer components;
        private FPDetailDAL fpDAL = new FPDetailDAL();
        private AisinoGRP groupBox1;
        private bool hasRepHY;
        private bool hasRepJDC;
        private bool hasRepJSFP;
        private bool hasRepPTDZ;
        private bool hySuccess;
        private bool jdcSuccess;
        private ILog loger = LogUtil.GetLogger<ChaoshuiForm>();
        private CommFun m_commFun = new CommFun();
        private List<InvTypeEntity> m_InvTypeEntityList = new List<InvTypeEntity>();
        private int nMonth;
        private int nYear;
        private TaxcardEntityBLL taxcardEntityBLL = new TaxcardEntityBLL();
        private InvTypeInfo typeHY = new InvTypeInfo();
        private InvTypeInfo typeJDC = new InvTypeInfo();
        private InvTypeInfo typeJSFP = new InvTypeInfo();
        private InvTypeInfo typePTDZ = new InvTypeInfo();
        private XmlComponentLoader xmlComponentLoader1;
        private bool zpSuccess;

        public ChaoshuiForm()
        {
            this.Initialize();
            this.nYear = this.taxcardEntityBLL.GetTaxDate().Year;
            this.nMonth = this.taxcardEntityBLL.GetTaxDate().Month;
        }

        private void baoshuipanRep()
        {
            int num = 0;
            int num2 = 2;
            TaxStateInfo info = base.TaxCardInstance.get_StateInfo();
            ushort tBRepInfo = info.TBRepInfo;
            foreach (InvTypeInfo info2 in info.InvTypeInfo)
            {
                ushort invType = info2.InvType;
                ushort num12 = info2.InvType;
                ushort num13 = info2.InvType;
                ushort num14 = info2.InvType;
            }
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            try
            {
                int iCRepInfo;
                TaxReportResult result;
                string str;
                int num4;
                string str2;
                int num6;
                string str3;
                int num10;
                bool flag4;
                if (this.checkBox1.Checked)
                {
                    iCRepInfo = base.TaxCardInstance.get_StateInfo().ICRepInfo;
                    ushort num15 = base.TaxCardInstance.get_StateInfo().TBRepInfo;
                    result = new TaxReportResult();
                    RepResult result2 = base.TaxCardInstance.TaxReport(num, 0, num2, ref result);
                    iCRepInfo = base.TaxCardInstance.get_StateInfo().ICRepInfo;
                    ushort num16 = base.TaxCardInstance.get_StateInfo().TBRepInfo;
                    str = "";
                    switch (result2)
                    {
                        case 0:
                            goto Label_01C7;

                        case 1:
                            this.ShowNextCard();
                            break;

                        case 2:
                            num4 = base.TaxCardInstance.get_RetCode();
                            if ((num4 != 0x2c7) && (num4 != 0x3e8))
                            {
                                goto Label_0144;
                            }
                            MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                            return;
                    }
                }
                goto Label_022B;
            Label_0144:
                switch (num4)
                {
                    case 0xca:
                    case 0xd1:
                        num = 1;
                        str = base.TaxCardInstance.get_ErrCode();
                        this.loger.Error(string.Concat(new object[] { "大厅报税盘抄专普税失败-RetCode:", num4, "   ErrorCode：", str }));
                        goto Label_022B;

                    default:
                        if (num4 > 0)
                        {
                            MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                        }
                        goto Label_022B;
                }
            Label_01C7:
                if ((iCRepInfo != 0) && result.NewOldFlag.Equals("0"))
                {
                    this.fpDAL.UpDateBSBZ(true, false, base.TaxCardInstance.get_TaxClock(), "s", result.Period);
                    this.fpDAL.UpDateBSBZ(true, false, base.TaxCardInstance.get_TaxClock(), "c", result.Period);
                }
                flag = true;
            Label_022B:
                if (this.checkBox2.Checked && !this.hasRepHY)
                {
                    num = 0;
                    int jSPRepInfo = this.typeHY.JSPRepInfo;
                    TaxReportResult result3 = new TaxReportResult();
                    RepResult result4 = base.TaxCardInstance.TaxReport(num, 11, num2, ref result3);
                    foreach (InvTypeInfo info4 in base.TaxCardInstance.get_StateInfo().InvTypeInfo)
                    {
                        if (info4.InvType == 11)
                        {
                            jSPRepInfo = info4.JSPRepInfo;
                        }
                    }
                    str2 = "";
                    switch (result4)
                    {
                        case 0:
                            if ((jSPRepInfo != 0) && result3.NewOldFlag.Equals("0"))
                            {
                                this.fpDAL.UpDateBSBZ(true, false, base.TaxCardInstance.get_TaxClock(), "f", result3.Period);
                            }
                            flag2 = true;
                            break;

                        case 1:
                            this.ShowNextCard();
                            break;

                        case 2:
                            num6 = base.TaxCardInstance.get_RetCode();
                            if ((num6 != 0x2c7) && (num6 != 0x3e8))
                            {
                                goto Label_031F;
                            }
                            MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                            return;
                    }
                }
                goto Label_03D5;
            Label_031F:
                if (num6 == 0xca)
                {
                    num = 1;
                    str2 = base.TaxCardInstance.get_ErrCode();
                    this.loger.Error(string.Concat(new object[] { "大厅报税盘抄货运税失败-RetCode:", num6, "   ErrorCode：", str2 }));
                }
                else if (num6 > 0)
                {
                    MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                }
            Label_03D5:
                if ((this.checkBox3.Checked && !this.hasRepJDC) && (!this.hasRepPTDZ && !this.hasRepJSFP))
                {
                    if (this.CheckJDCDZFXSJ())
                    {
                        this.loger.Error(this.checkBox3.Text + "发行当天不允许抄税！");
                    }
                    else if (this.IsLingShuiDanDZ())
                    {
                        this.loger.Error(this.checkBox3.Text + "零税单电子不允许抄税！");
                    }
                    else
                    {
                        num = 0;
                        int num7 = this.typeJDC.JSPRepInfo;
                        int num8 = this.typePTDZ.JSPRepInfo;
                        int num9 = this.typeJSFP.JSPRepInfo;
                        TaxReportResult result5 = new TaxReportResult();
                        RepResult result6 = base.TaxCardInstance.TaxReport(num, 12, num2, ref result5);
                        foreach (InvTypeInfo info6 in base.TaxCardInstance.get_StateInfo().InvTypeInfo)
                        {
                            if (info6.InvType == 12)
                            {
                                num7 = info6.JSPRepInfo;
                            }
                            if (info6.InvType == 0x33)
                            {
                                num8 = info6.JSPRepInfo;
                            }
                            if (info6.InvType == 0x29)
                            {
                                num9 = info6.JSPRepInfo;
                            }
                        }
                        str3 = "";
                        switch (result6)
                        {
                            case 0:
                                if ((num7 != 0) && result5.NewOldFlag.Equals("0"))
                                {
                                    this.fpDAL.UpDateBSBZ(true, false, base.TaxCardInstance.get_TaxClock(), "j", result5.Period);
                                }
                                if (((num8 != 0) && (base.TaxCardInstance.get_InvEleKindCode() == 0x33)) && result5.NewOldFlag.Equals("0"))
                                {
                                    this.fpDAL.UpDateBSBZ(true, false, base.TaxCardInstance.get_TaxClock(), "p", result5.Period);
                                }
                                if ((num9 != 0) && result5.NewOldFlag.Equals("0"))
                                {
                                    this.fpDAL.UpDateBSBZ(true, false, base.TaxCardInstance.get_TaxClock(), "q", result5.Period);
                                }
                                flag3 = true;
                                break;

                            case 1:
                                this.ShowNextCard();
                                break;

                            case 2:
                                num10 = base.TaxCardInstance.get_RetCode();
                                if ((num10 != 0x2c7) && (num10 != 0x3e8))
                                {
                                    goto Label_057B;
                                }
                                MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                                return;
                        }
                    }
                }
                goto Label_06C4;
            Label_057B:
                if (num10 == 0xca)
                {
                    num = 1;
                    str3 = base.TaxCardInstance.get_ErrCode();
                    this.loger.Error(string.Concat(new object[] { "大厅报税盘抄机动车电子税失败-RetCode:", num10, "   ErrorCode：", str3 }));
                }
                else if (num10 > 0)
                {
                    MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                }
            Label_06C4:
                flag4 = info.TBRepInfo != 0;
                bool flag5 = false;
                bool flag6 = false;
                bool flag7 = false;
                foreach (InvTypeInfo info7 in info.InvTypeInfo)
                {
                    if (info7.InvType == 11)
                    {
                        flag5 = info7.ICRepInfo != 0;
                    }
                    if (info7.InvType == 12)
                    {
                        flag6 = info7.ICRepInfo != 0;
                    }
                    if (info7.InvType == 0x33)
                    {
                        flag7 = info7.ICRepInfo != 0;
                    }
                }
                string str4 = string.Empty;
                if (flag4)
                {
                    str4 = str4 + "“" + this.checkBox1.Text + "”";
                }
                if (flag5)
                {
                    str4 = str4 + "“" + this.checkBox2.Text + "”";
                }
                if (flag6 || flag7)
                {
                    str4 = str4 + "“" + this.checkBox3.Text + "”";
                }
                if ((flag || flag2) || flag3)
                {
                    string str5 = "";
                    string str6 = "";
                    if (!string.IsNullOrEmpty(str4))
                    {
                        str5 = str5 + "报税盘包含：" + str4 + "的报税资料。";
                    }
                    str6 = Environment.NewLine + str5;
                    MessageManager.ShowMsgBox("TCD_9116_", new List<KeyValuePair<string, string>>(), new string[] { str6 }, new string[] { "" });
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-251105", "提示", new string[] { "抄税处理异常！" });
                this.loger.Error("抄税处理异常：", exception);
            }
        }

        private void btnBSPCSQuery_Click(object sender, EventArgs e)
        {
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.ShowTips2();
            if (base.TaxCardInstance.get_StateInfo().TBRegFlag == 1)
            {
                ChaoshuiMedium medium = new ChaoshuiMedium {
                    StartPosition = FormStartPosition.CenterScreen,
                    ShowInTaskbar = true
                };
                if (medium.ShowDialog() == DialogResult.OK)
                {
                    this.judgeHyJdc();
                    if (medium.jinshuipan)
                    {
                        this.jinshuipanRep();
                    }
                    else if (base.TaxCardInstance.get_StateInfo().IsTBEnable == 0)
                    {
                        if (MessageManager.ShowMsgBox("INP-251108") == DialogResult.OK)
                        {
                            this.baoshuipanRep();
                        }
                        else
                        {
                            base.Close();
                        }
                    }
                    else
                    {
                        this.baoshuipanRep();
                    }
                }
            }
            else
            {
                this.judgeHyJdc();
                this.jinshuipanRep();
            }
            base.Close();
        }

        private void ChaoshuiForm_Load(object sender, EventArgs e)
        {
            this.LoadInvType();
        }

        private bool CheckJDCDZFXSJ()
        {
            bool flag = false;
            try
            {
                if (!this.checkBox3.Checked)
                {
                    return flag;
                }
                string str = string.Empty;
                int num = Convert.ToInt32(base.TaxCardInstance.GetCardClock().ToString("yyyyMMdd"));
                foreach (InvTypeInfo info in base.TaxCardInstance.get_StateInfo().InvTypeInfo)
                {
                    if (((info.InvType == 12) && !string.IsNullOrEmpty(base.TaxCardInstance.get_FXSJJDC())) && (Convert.ToInt32(base.TaxCardInstance.get_FXSJJDC()) == num))
                    {
                        str = str + "\"机动车销售统一发票\"";
                        flag = true;
                    }
                    if (((info.InvType == 0x33) && !string.IsNullOrEmpty(base.TaxCardInstance.get_FXSJDZ())) && (Convert.ToInt32(base.TaxCardInstance.get_FXSJDZ()) == num))
                    {
                        str = str + "\"电子增值税普通发票\"";
                        flag = true;
                    }
                    if (((info.InvType == 0x29) && !string.IsNullOrEmpty(base.TaxCardInstance.get_FXSJJT())) && (Convert.ToInt32(base.TaxCardInstance.get_FXSJJT()) == num))
                    {
                        str = str + "\"增值税普通发票(卷票)\"";
                        flag = true;
                    }
                }
                if (flag && !string.IsNullOrEmpty(str))
                {
                    MessageManager.ShowMsgBox("INP-251105", new string[] { str + "发行当天不允许抄税！" });
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("判断机动车电子发票异常：" + exception.ToString());
                flag = true;
            }
            return flag;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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

        private void Initialize()
        {
            this.InitializeComponent();
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Load += new EventHandler(this.ChaoshuiForm_Load);
            this.cmbShuiqi = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbShuiqi");
            this.groupBox1 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox1");
            this.btnBSPCSQuery = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnBSPCSQuery");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.checkBox1 = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("checkBox1");
            this.checkBox2 = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("checkBox2");
            this.checkBox3 = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("checkBox3");
            this.checkBox1.Checked = false;
            this.checkBox2.Checked = false;
            this.checkBox3.Checked = false;
            this.checkBox1.Visible = true;
            this.checkBox2.Visible = true;
            this.checkBox3.Visible = true;
            this.checkBox1.CheckedChanged += new EventHandler(this.pzCheckChanged);
            this.checkBox2.CheckedChanged += new EventHandler(this.pzCheckChanged);
            this.checkBox3.CheckedChanged += new EventHandler(this.pzCheckChanged);
            this.btnOK.Enabled = false;
            this.cmbShuiqi.DropDownStyle = ComboBoxStyle.DropDownList;
            this.groupBox1.Location = new Point(0, this.btnOK.Location.Y - 15);
            this.btnBSPCSQuery.Visible = false;
            this.btnBSPCSQuery.Click += new EventHandler(this.btnBSPCSQuery_Click);
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ChaoshuiForm));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x1f9, 0x135);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Bsgl.ChaoshuiForm\Aisino.Fwkp.Bsgl.ChaoshuiForm.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1f9, 0x135);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "ChaoshuiForm";
            base.set_TabText("请选择要抄税的票种");
            this.Text = "请选择要抄税的票种";
            base.ResumeLayout(false);
        }

        private void InitTaxPeriod()
        {
            this.cmbShuiqi.Items.Clear();
            IdTextPair pair = new IdTextPair(0, "本期资料");
            IdTextPair pair2 = new IdTextPair(1, "上期资料");
            IdTextPair pair3 = new IdTextPair(2, "测试资料");
            this.cmbShuiqi.Items.AddRange(new IdTextPair[] { pair, pair2, pair3 });
            this.cmbShuiqi.SelectedIndex = 0;
        }

        private bool IsLingShuiDanDZ()
        {
            bool flag = false;
            try
            {
                DateTime time2;
                if ((!base.TaxCardInstance.get_QYLX().ISPTFPDZ || base.TaxCardInstance.get_QYLX().ISJDC) || base.TaxCardInstance.get_QYLX().ISPTFPJSP)
                {
                    return flag;
                }
                List<string> cSDate = base.TaxCardInstance.GetCSDate(0x33);
                if ((cSDate == null) || (cSDate.Count < 1))
                {
                    return flag;
                }
                DateTime cardClock = base.TaxCardInstance.GetCardClock();
                this.loger.Debug("判断是否是单电子0税,金水盘当前日期：" + cardClock.ToString("yyyyMMddHHmmss"));
                DateTime.TryParse(cSDate[1], out time2);
                this.loger.Debug("判断是否是单电子0税,电子票锁死日期：" + time2.ToString("yyyyMMddHHmmss"));
                int num = Convert.ToInt32(cardClock.ToString("yyyyMM"));
                int num2 = Convert.ToInt32(time2.ToString("yyyyMM"));
                DataSumDAL mdal = new DataSumDAL();
                if ((num < num2) && (mdal.GetFPNum("p", num.ToString()) < 1))
                {
                    MessageManager.ShowMsgBox("INP-251111", new string[] { "未开电子增值税普通发票，不允许抄税！" });
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("判断是否是单电子0税异常：" + exception.ToString());
            }
            return flag;
        }

        private void jinshuipanRep()
        {
            int num = 0;
            int num2 = 1;
            try
            {
                int iCRepInfo;
                TaxReportResult result;
                string str;
                int num4;
                int num6;
                int num10;
                if (this.checkBox1.Checked)
                {
                    iCRepInfo = base.TaxCardInstance.get_StateInfo().ICRepInfo;
                    ushort tBRepInfo = base.TaxCardInstance.get_StateInfo().TBRepInfo;
                    result = new TaxReportResult();
                    this.loger.Debug("办税厅抄报专普开始");
                    RepResult result2 = base.TaxCardInstance.TaxReport(num, 0, num2, ref result);
                    this.loger.Debug("办税厅抄报专普结束");
                    iCRepInfo = base.TaxCardInstance.get_StateInfo().ICRepInfo;
                    ushort num11 = base.TaxCardInstance.get_StateInfo().TBRepInfo;
                    str = "";
                    switch (result2)
                    {
                        case 0:
                            goto Label_0148;

                        case 1:
                            this.ShowNextCard();
                            break;

                        case 2:
                            goto Label_00B5;
                    }
                }
                goto Label_01AF;
            Label_00B5:
                num4 = base.TaxCardInstance.get_RetCode();
                switch (num4)
                {
                    case 0xca:
                    case 0xd1:
                        num = 1;
                        str = base.TaxCardInstance.get_ErrCode();
                        this.loger.Error(string.Concat(new object[] { "大厅金税盘抄专普税失败-RetCode:", num4, "   ErrorCode：", str }));
                        goto Label_01AF;

                    default:
                        if (num4 > 0)
                        {
                            MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                        }
                        goto Label_01AF;
                }
            Label_0148:
                if ((iCRepInfo != 0) && result.NewOldFlag.Equals("0"))
                {
                    this.fpDAL.UpDateBSBZ(true, false, base.TaxCardInstance.get_TaxClock(), "s", result.Period);
                    this.fpDAL.UpDateBSBZ(true, false, base.TaxCardInstance.get_TaxClock(), "c", result.Period);
                }
                this.zpSuccess = true;
            Label_01AF:
                if (this.checkBox2.Checked && !this.hasRepHY)
                {
                    num = 0;
                    int jSPRepInfo = this.typeHY.JSPRepInfo;
                    TaxReportResult result3 = new TaxReportResult();
                    this.loger.Debug("办税厅抄报货运开始");
                    RepResult result4 = base.TaxCardInstance.TaxReport(num, 11, num2, ref result3);
                    this.loger.Debug("办税厅抄报货运结束");
                    foreach (InvTypeInfo info2 in base.TaxCardInstance.get_StateInfo().InvTypeInfo)
                    {
                        if (info2.InvType == 11)
                        {
                            jSPRepInfo = info2.JSPRepInfo;
                        }
                    }
                    string str2 = "";
                    switch (result4)
                    {
                        case 0:
                            if ((jSPRepInfo != 0) && result3.NewOldFlag.Equals("0"))
                            {
                                this.fpDAL.UpDateBSBZ(true, false, base.TaxCardInstance.get_TaxClock(), "f", result3.Period);
                            }
                            this.hySuccess = true;
                            break;

                        case 1:
                            this.ShowNextCard();
                            break;

                        case 2:
                            num6 = base.TaxCardInstance.get_RetCode();
                            if (num6 != 0xca)
                            {
                                goto Label_02F2;
                            }
                            num = 1;
                            str2 = base.TaxCardInstance.get_ErrCode();
                            this.loger.Error(string.Concat(new object[] { "大厅金税盘抄货运税失败-RetCode:", num6, "   ErrorCode：", str2 }));
                            break;
                    }
                }
                goto Label_0355;
            Label_02F2:
                if (num6 > 0)
                {
                    MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                }
            Label_0355:
                if ((this.checkBox3.Checked && !this.hasRepJDC) && (!this.hasRepPTDZ && !this.hasRepJSFP))
                {
                    if (this.CheckJDCDZFXSJ())
                    {
                        this.loger.Error(this.checkBox3.Text + "发行当天不允许抄税！");
                    }
                    else if (this.IsLingShuiDanDZ())
                    {
                        this.loger.Error(this.checkBox3.Text + "零税单电子不允许抄税！");
                    }
                    else
                    {
                        num = 0;
                        int num7 = this.typeJDC.JSPRepInfo;
                        int num8 = this.typePTDZ.JSPRepInfo;
                        int num9 = this.typeJSFP.JSPRepInfo;
                        TaxReportResult result5 = new TaxReportResult();
                        this.loger.Debug("办税厅抄报机动车开始");
                        RepResult result6 = base.TaxCardInstance.TaxReport(num, 12, num2, ref result5);
                        this.loger.Debug("办税厅抄报机动车结束");
                        foreach (InvTypeInfo info4 in base.TaxCardInstance.get_StateInfo().InvTypeInfo)
                        {
                            if (info4.InvType == 12)
                            {
                                num7 = info4.JSPRepInfo;
                            }
                            if (info4.InvType == 0x33)
                            {
                                num8 = info4.JSPRepInfo;
                            }
                            if (info4.InvType == 0x29)
                            {
                                num9 = info4.JSPRepInfo;
                            }
                        }
                        string str3 = "";
                        switch (result6)
                        {
                            case 0:
                                if ((num7 != 0) && result5.NewOldFlag.Equals("0"))
                                {
                                    this.fpDAL.UpDateBSBZ(true, false, base.TaxCardInstance.get_TaxClock(), "j", result5.Period);
                                }
                                if (((num8 != 0) && (base.TaxCardInstance.get_InvEleKindCode() == 0x33)) && result5.NewOldFlag.Equals("0"))
                                {
                                    this.fpDAL.UpDateBSBZ(true, false, base.TaxCardInstance.get_TaxClock(), "p", result5.Period);
                                }
                                if ((num9 != 0) && result5.NewOldFlag.Equals("0"))
                                {
                                    this.fpDAL.UpDateBSBZ(true, false, base.TaxCardInstance.get_TaxClock(), "q", result5.Period);
                                }
                                this.jdcSuccess = true;
                                break;

                            case 1:
                                this.ShowNextCard();
                                break;

                            case 2:
                                num10 = base.TaxCardInstance.get_RetCode();
                                if (num10 != 0xca)
                                {
                                    goto Label_054D;
                                }
                                num = 1;
                                str3 = base.TaxCardInstance.get_ErrCode();
                                this.loger.Error(string.Concat(new object[] { "大厅金税盘抄机动车电子卷票税失败-RetCode:", num10, "   ErrorCode：", str3 }));
                                break;
                        }
                    }
                }
                goto Label_0640;
            Label_054D:
                if (num10 > 0)
                {
                    MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                }
            Label_0640:
                if ((this.zpSuccess || this.hySuccess) || this.jdcSuccess)
                {
                    MessageManager.ShowMsgBox("TCD_9116_", new List<KeyValuePair<string, string>>(), new string[] { "" }, new string[] { "" });
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-251105", "提示", new string[] { "抄税处理异常！" });
                this.loger.Error("抄税处理异常：", exception);
            }
        }

        private void judgeHyJdc()
        {
            TaxStateInfo info = base.TaxCardInstance.get_StateInfo();
            List<InvTypeInfo> invTypeInfo = info.InvTypeInfo;
            this.BSPRepInfo_ZP = info.TBRepInfo;
            foreach (InvTypeInfo info2 in invTypeInfo)
            {
                if (info2.InvType == 11)
                {
                    this.typeHY = info2;
                    this.BSPRepInfo_HY = info2.ICRepInfo;
                }
                if ((info2.InvType == 11) && this.checkBox2.Checked)
                {
                    this.typeHY = info2;
                    string str = info2.LastRepDate.Split(new char[] { ' ' })[0];
                    DateTime time = base.TaxCardInstance.get_TaxClock();
                    int year = -1;
                    int month = -1;
                    int day = -1;
                    if ((str.Length > 0) && str.Contains("-"))
                    {
                        year = int.Parse(str.Split(new char[] { '-' })[0]);
                        month = int.Parse(str.Split(new char[] { '-' })[1]);
                        day = int.Parse(str.Split(new char[] { '-' })[2]);
                        DateTime time2 = new DateTime(year, month, day);
                        DateTime time3 = new DateTime(time.Year, time.Month, time.Day);
                        if ((DateTime.Compare(time2, time3) == 0) && (info2.JSPRepInfo == 0))
                        {
                            MessageManager.ShowMsgBox("TCD_9210_", new List<KeyValuePair<string, string>>(), new string[] { "货物运输业增值税专用发票" + Environment.NewLine }, new string[] { "" });
                            this.hasRepHY = true;
                        }
                    }
                }
                if (info2.InvType == 12)
                {
                    this.typeJDC = info2;
                    this.BSPRepInfo_JDC = info2.ICRepInfo;
                }
                if ((info2.InvType == 12) && this.checkBox3.Checked)
                {
                    this.typeJDC = info2;
                    string str2 = info2.LastRepDate.Split(new char[] { ' ' })[0];
                    DateTime time4 = base.TaxCardInstance.get_TaxClock();
                    int num4 = -1;
                    int num5 = -1;
                    int num6 = -1;
                    if ((str2.Length > 0) && str2.Contains("-"))
                    {
                        num4 = int.Parse(str2.Split(new char[] { '-' })[0]);
                        num5 = int.Parse(str2.Split(new char[] { '-' })[1]);
                        num6 = int.Parse(str2.Split(new char[] { '-' })[2]);
                        DateTime time5 = new DateTime(num4, num5, num6);
                        DateTime time6 = new DateTime(time4.Year, time4.Month, time4.Day);
                        if ((DateTime.Compare(time5, time6) == 0) && (info2.JSPRepInfo == 0))
                        {
                            MessageManager.ShowMsgBox("TCD_9210_", new List<KeyValuePair<string, string>>(), new string[] { "机动车销售统一发票" + Environment.NewLine }, new string[] { "" });
                            this.hasRepJDC = true;
                        }
                    }
                }
                if ((info2.InvType == 0x33) && (base.TaxCardInstance.get_InvEleKindCode() == 0x33))
                {
                    this.typePTDZ = info2;
                    this.BSPRepInfo_PTDZ = info2.ICRepInfo;
                }
                if (((info2.InvType == 0x33) && (base.TaxCardInstance.get_InvEleKindCode() == 0x33)) && this.checkBox3.Checked)
                {
                    this.typePTDZ = info2;
                    List<string> cSDate = base.TaxCardInstance.GetCSDate(0x33);
                    DateTime time7 = base.TaxCardInstance.get_TaxClock();
                    string str3 = cSDate[0];
                    int num7 = -1;
                    int num8 = -1;
                    int num9 = -1;
                    if ((str3.Length > 0) && str3.Contains("-"))
                    {
                        string[] strArray = str3.Split(new char[] { '-' });
                        num7 = int.Parse(strArray[0]);
                        num8 = int.Parse(strArray[1]);
                        num9 = int.Parse(strArray[2].Substring(0, 2));
                    }
                    DateTime time8 = new DateTime(num7, num8, num9);
                    DateTime time9 = new DateTime(time7.Year, time7.Month, time7.Day);
                    if ((DateTime.Compare(time8, time9) == 0) && (info2.JSPRepInfo == 0))
                    {
                        MessageManager.ShowMsgBox("TCD_9210_", new List<KeyValuePair<string, string>>(), new string[] { "电子增值税普通发票" + Environment.NewLine }, new string[] { "" });
                        this.hasRepPTDZ = true;
                    }
                }
                if (info2.InvType == 0x29)
                {
                    this.typeJSFP = info2;
                    this.BSPRepInfo_JSFP = info2.ICRepInfo;
                }
                if ((info2.InvType == 0x29) && this.checkBox3.Checked)
                {
                    this.typeJSFP = info2;
                    List<string> list3 = base.TaxCardInstance.GetCSDate(0x29);
                    DateTime time10 = base.TaxCardInstance.get_TaxClock();
                    string str4 = list3[0];
                    int num10 = -1;
                    int num11 = -1;
                    int num12 = -1;
                    if ((str4.Length > 0) && str4.Contains("-"))
                    {
                        string[] strArray2 = str4.Split(new char[] { '-' });
                        num10 = int.Parse(strArray2[0]);
                        num11 = int.Parse(strArray2[1]);
                        num12 = int.Parse(strArray2[2].Substring(0, 2));
                    }
                    DateTime time11 = new DateTime(num10, num11, num12);
                    DateTime time12 = new DateTime(time10.Year, time10.Month, time10.Day);
                    if ((DateTime.Compare(time11, time12) == 0) && (info2.JSPRepInfo == 0))
                    {
                        MessageManager.ShowMsgBox("TCD_9210_", new List<KeyValuePair<string, string>>(), new string[] { "增值税普通发票(卷票)" + Environment.NewLine }, new string[] { "" });
                        this.hasRepJSFP = true;
                    }
                }
            }
        }

        private void LoadInvType()
        {
            try
            {
                this.m_InvTypeEntityList = this.m_commFun.GetInvTypeCollect();
                if (this.m_InvTypeEntityList == null)
                {
                    MessageManager.ShowMsgBox("INP-251303", new string[] { "获取发票种类失败" });
                }
                this.checkBox1.Visible = false;
                this.checkBox2.Visible = false;
                this.checkBox3.Visible = false;
                for (int i = 0; i < this.m_InvTypeEntityList.Count; i++)
                {
                    if ((this.m_InvTypeEntityList[i].m_invType == INV_TYPE.INV_SPECIAL) || (this.m_InvTypeEntityList[i].m_invType == INV_TYPE.INV_COMMON))
                    {
                        this.checkBox1.Visible = true;
                        this.checkBox1.Checked = true;
                    }
                    if (this.m_InvTypeEntityList[i].m_invType == INV_TYPE.INV_TRANSPORTATION)
                    {
                        this.checkBox2.Visible = true;
                        this.checkBox2.Checked = true;
                    }
                    if (this.m_InvTypeEntityList[i].m_invType == INV_TYPE.INV_VEHICLESALES)
                    {
                        this.checkBox3.Visible = true;
                        this.checkBox3.Checked = true;
                    }
                    if (this.m_InvTypeEntityList[i].m_invType == INV_TYPE.INV_PTDZ)
                    {
                        this.checkBox3.Visible = true;
                        this.checkBox3.Checked = true;
                        if (base.TaxCardInstance.get_QYLX().ISJDC && !base.TaxCardInstance.get_QYLX().ISPTFPJSP)
                        {
                            this.checkBox3.Text = "机动车销售统一发票及电子增值税普通发票";
                        }
                        else if (!base.TaxCardInstance.get_QYLX().ISJDC && base.TaxCardInstance.get_QYLX().ISPTFPJSP)
                        {
                            this.checkBox3.Text = "电子增值税普通发票及增值税普通发票(卷票)";
                        }
                        else if (base.TaxCardInstance.get_QYLX().ISJDC && base.TaxCardInstance.get_QYLX().ISPTFPJSP)
                        {
                            this.checkBox3.Text = "机动车销售统一发票、电子增值税普通发票";
                            this.checkBox3.Text = this.checkBox3.Text + Environment.NewLine;
                            this.checkBox3.Text = this.checkBox3.Text + "及增值税普通发票(卷票)";
                        }
                        else
                        {
                            this.checkBox3.Text = "电子增值税普通发票";
                        }
                        this.checkBox3.AutoSize = true;
                    }
                    if (this.m_InvTypeEntityList[i].m_invType == INV_TYPE.INV_JSFP)
                    {
                        this.checkBox3.Visible = true;
                        this.checkBox3.Checked = true;
                        if (base.TaxCardInstance.get_QYLX().ISJDC && !base.TaxCardInstance.get_QYLX().ISPTFPDZ)
                        {
                            this.checkBox3.Text = "机动车销售统一发票及增值税普通发票(卷票)";
                        }
                        else if (!base.TaxCardInstance.get_QYLX().ISJDC && base.TaxCardInstance.get_QYLX().ISPTFPDZ)
                        {
                            this.checkBox3.Text = "电子增值税普通发票及增值税普通发票(卷票)";
                        }
                        else if (base.TaxCardInstance.get_QYLX().ISJDC && base.TaxCardInstance.get_QYLX().ISPTFPDZ)
                        {
                            this.checkBox3.Text = "机动车销售统一发票、电子增值税普通发票";
                            this.checkBox3.Text = this.checkBox3.Text + Environment.NewLine;
                            this.checkBox3.Text = this.checkBox3.Text + "及增值税普通发票(卷票)";
                        }
                        else
                        {
                            this.checkBox3.Text = "增值税普通发票(卷票)";
                        }
                        this.checkBox3.AutoSize = true;
                    }
                }
                if ((this.checkBox1.Visible && !this.checkBox2.Visible) && !this.checkBox3.Visible)
                {
                    this.checkBox1.SetBounds(this.checkBox2.Location.X, this.checkBox2.Location.Y, this.checkBox1.Size.Width, this.checkBox1.Size.Height);
                }
                if ((!this.checkBox1.Visible && this.checkBox2.Visible) && this.checkBox3.Visible)
                {
                    int num2 = (this.checkBox1.Location.Y + this.checkBox2.Location.Y) / 2;
                    int num3 = (this.checkBox3.Location.Y + this.checkBox2.Location.Y) / 2;
                    this.checkBox2.SetBounds(this.checkBox2.Location.X, num2 - 5, this.checkBox2.Width, this.checkBox2.Height);
                    this.checkBox3.SetBounds(this.checkBox3.Location.X, num3 + 5, this.checkBox3.Width, this.checkBox3.Height);
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }

        private void pzCheckChanged(object sender, EventArgs e)
        {
            if ((!this.checkBox1.Checked && !this.checkBox2.Checked) && !this.checkBox3.Checked)
            {
                this.btnOK.Enabled = false;
            }
            else
            {
                this.btnOK.Enabled = true;
            }
        }

        private void ShowNextCard()
        {
            string str = string.Empty;
            if (base.TaxCardInstance.get_ECardType() == 3)
            {
                str = "请正确插入本机下一张金税盘，按确认键继续抄税!";
            }
            else
            {
                str = "请正确插入本机下一张IC卡，按确认键继续抄税!";
            }
            MessageManager.ShowMsgBox("INP-251106", new string[] { str });
        }

        public bool ShowTips()
        {
            if (base.TaxCardInstance.get_QYLX().ISTDQY)
            {
                return true;
            }
            bool flag = true;
            string str = "办税厅抄税提醒：";
            str = str + "\r\n" + "  1、您上次抄税日期为：";
            string str2 = string.Empty;
            DateTime cardClock = base.TaxCardInstance.GetCardClock();
            string str3 = cardClock.Month.ToString("00");
            DateTime time2 = new DateTime(0x76c, 1, 1);
            try
            {
                if (base.TaxCardInstance.get_QYLX().ISZYFP || base.TaxCardInstance.get_QYLX().ISPTFP)
                {
                    DateTime time3 = base.TaxCardInstance.get_LastRepDate();
                    if (time2 < time3)
                    {
                        time2 = time3;
                    }
                }
                foreach (InvTypeInfo info in base.TaxCardInstance.get_StateInfo().InvTypeInfo)
                {
                    DateTime time4 = Convert.ToDateTime(base.TaxCardInstance.GetCSDate(info.InvType)[0]);
                    if (time2 < time4)
                    {
                        time2 = time4;
                    }
                }
                if (Convert.ToInt32(time2.ToString("yyyyMM")) >= Convert.ToInt32(cardClock.ToString("yyyyMM")))
                {
                    str2 = "非";
                }
                string str4 = str + time2.ToString("yyyy年MM月dd日HH时mm分；") + "\r\n";
                str = ((str4 + "  2、您要进行的是" + str3 + "月份的" + str2 + "征期抄税；") + "\r\n" + "  3、您进行抄税操作后，必须到税局办税大厅进行报税处理。") + "\r\n" + "您确认要进行该操作吗？";
                if (MessageManager.ShowMsgBox("INP-251109", new string[] { str }) == DialogResult.Cancel)
                {
                    flag = false;
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("大厅报税-提示信息异常：" + exception.ToString());
            }
            return flag;
        }

        private void ShowTips2()
        {
            if ((base.TaxCardInstance.get_QYLX().ISPTFP || base.TaxCardInstance.get_QYLX().ISZYFP) && ((this.checkBox1.Checked && base.TaxCardInstance.get_QYLX().ISPTFPDZ) && this.checkBox3.Checked))
            {
                string str = string.Empty + "您抄的税中同时包含增值税专普票和电子增值税普通发票，抄完税后请到税局防伪税控系统和货运系统分别报税清卡！";
                MessageManager.ShowMsgBox("INP-251110", new string[] { str });
            }
        }
    }
}

