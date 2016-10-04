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
    using System.Globalization;
    using System.IO;
    using System.Windows.Forms;

    public class JSPStateQuery : DockForm
    {
        private AisinoLBL aisinoLBL1;
        private AisinoLBL aisinoLBL10;
        private AisinoLBL aisinoLBL12;
        private AisinoLBL aisinoLBL13;
        private AisinoLBL aisinoLBL14;
        private AisinoLBL aisinoLBL15;
        private AisinoLBL aisinoLBL16;
        private AisinoLBL aisinoLBL17;
        private AisinoLBL aisinoLBL18;
        private AisinoLBL aisinoLBL19;
        private AisinoLBL aisinoLBL2;
        private AisinoLBL aisinoLBL20;
        private AisinoLBL aisinoLBL21;
        private AisinoLBL aisinoLBL22;
        private AisinoLBL aisinoLBL23;
        private AisinoLBL aisinoLBL24;
        private AisinoLBL aisinoLBL25;
        private AisinoLBL aisinoLBL26;
        private AisinoLBL aisinoLBL27;
        private AisinoLBL aisinoLBL28;
        private AisinoLBL aisinoLBL3;
        private AisinoLBL aisinoLBL37;
        private AisinoLBL aisinoLBL38;
        private AisinoLBL aisinoLBL39;
        private AisinoLBL aisinoLBL4;
        private AisinoLBL aisinoLBL40;
        private AisinoLBL aisinoLBL5;
        private AisinoLBL aisinoLBL6;
        private AisinoLBL aisinoLBL7;
        private AisinoLBL aisinoLBL8;
        private AisinoLBL aisinoLBL9;
        private AisinoLBL aisinoLBLLXSSQ;
        private AisinoLBL aisinoLBLLXSX;
        private IContainer components;
        private AisinoGRP gbBSP;
        private AisinoGRP gbJSK;
        private bool hasBSP;
        private AisinoLBL label0;
        private AisinoLBL label1;
        private AisinoLBL label10;
        private AisinoLBL label12;
        private AisinoLBL label13;
        private AisinoLBL label14;
        private AisinoLBL label15;
        private AisinoLBL label16;
        private AisinoLBL label17;
        private AisinoLBL label18;
        private AisinoLBL label19;
        private AisinoLBL label2;
        private AisinoLBL label20;
        private AisinoLBL label21;
        private AisinoLBL label22;
        private AisinoLBL label3;
        private AisinoLBL label33;
        private AisinoLBL label34;
        private AisinoLBL label35;
        private AisinoLBL label36;
        private AisinoLBL label37;
        private AisinoLBL label38;
        private AisinoLBL label4;
        private AisinoLBL label5;
        private AisinoLBL label6;
        private AisinoLBL label7;
        private AisinoLBL label8;
        private AisinoLBL label9;
        private AisinoLBL lbl_1;
        private AisinoLBL lbl_11;
        private AisinoLBL lbl_111;
        private AisinoLBL lbl_2;
        private AisinoLBL lbl_22;
        private AisinoLBL lbl_222;
        private AisinoLBL lbl_3;
        private AisinoLBL lbl_33;
        private AisinoLBL lbl_333;
        private AisinoLBL lbl_4;
        private AisinoLBL lbl_44;
        private AisinoLBL lbl_444;
        private AisinoLBL lbl_5;
        private AisinoLBL lbl_55;
        private AisinoLBL lbl_555;
        private AisinoLBL lbl_6;
        private AisinoLBL lbl_66;
        private AisinoLBL lbl_666;
        private AisinoLBL lbl_7;
        private AisinoLBL lbl_77;
        private AisinoLBL lbl_777;
        private AisinoLBL lbl_8;
        private AisinoLBL lbl_88;
        private AisinoLBL lbl_888;
        private AisinoLBL lbl_9;
        private AisinoLBL lbl_99;
        private AisinoLBL lbl_999;
        private AisinoLBL lbl100;
        private AisinoLBL lbl101;
        private AisinoLBL lbl102;
        private AisinoLBL lbl103;
        private AisinoLBL lbl104;
        private AisinoLBL lbl105;
        private AisinoLBL lbl106;
        private AisinoLBL lbl31;
        private AisinoLBL lbl32;
        private AisinoLBL lbl35;
        private AisinoLBL lbl41;
        private AisinoLBL lbl43;
        private AisinoLBL lbl44;
        private AisinoLBL lbl45;
        private AisinoLBL lbl46;
        private AisinoLBL lbl47;
        private AisinoLBL lbl48;
        private AisinoLBL lbl49;
        private AisinoLBL lbl50;
        private AisinoLBL lbl51;
        private AisinoLBL lbl52;
        private AisinoLBL lbl53;
        private AisinoLBL lbl54;
        private AisinoLBL lbl55;
        private AisinoLBL lbl56;
        private AisinoLBL lbl57;
        private AisinoLBL lbl58;
        private AisinoLBL lbl59;
        private AisinoLBL lbl60;
        private AisinoLBL lbl61;
        private AisinoLBL lbl62;
        private AisinoLBL lbl63;
        private AisinoLBL lbl64;
        private AisinoLBL lbl65;
        private AisinoLBL lbl66;
        private AisinoLBL lbl67;
        private AisinoLBL lbl68;
        private AisinoLBL lbl69;
        private AisinoLBL lbl70;
        private AisinoLBL lbl71;
        private AisinoLBL lbl72;
        private AisinoLBL lbl73;
        private AisinoLBL lbl74;
        private AisinoLBL lbl75;
        private AisinoLBL lbl76;
        private AisinoLBL lbl77;
        private AisinoLBL lbl78;
        private AisinoLBL lbl79;
        private AisinoLBL lbl80;
        private AisinoLBL lbl81;
        private AisinoLBL lbl82;
        private AisinoLBL lbl83;
        private AisinoLBL lbl84;
        private AisinoLBL lbl85;
        private AisinoLBL lbl86;
        private AisinoLBL lbl87;
        private AisinoLBL lbl88;
        private AisinoLBL lbl89;
        private AisinoLBL lbl90;
        private AisinoLBL lbl91;
        private AisinoLBL lbl92;
        private AisinoLBL lbl93;
        private AisinoLBL lbl94;
        private AisinoLBL lbl95;
        private AisinoLBL lbl96;
        private AisinoLBL lbl97;
        private AisinoLBL lbl98;
        private AisinoLBL lbl99;
        private AisinoLBL lblBackFP;
        private AisinoLBL lblBottomNo;
        private AisinoLBL lblBSData;
        private AisinoLBL lblBSFlag;
        private AisinoLBL lblBSPBackFP;
        private AisinoLBL lblBSPBottomNo;
        private AisinoLBL lblBSPBsData;
        private AisinoLBL lblBSPBsFlag;
        private AisinoLBL lblBSPBuyFP;
        private AisinoLBL lblBSPCapacity;
        private AisinoLBL lblBSPKpjNo;
        private AisinoLBL lblBSPNo;
        private AisinoLBL lblBuyFP;
        private AisinoLBL lblDriverNo;
        private AisinoLBL lblDue;
        private AisinoLBL lblFPAvailable;
        private AisinoLBL lblFPLimitSum;
        private AisinoLBL lblFPLimitSumPT;
        private AisinoLBL lblFPStock;
        private AisinoLBL lblHYBackFP;
        private AisinoLBL lblHYBsData;
        private AisinoLBL lblHYBsFlag;
        private AisinoLBL lblHYBuyFP;
        private AisinoLBL lblHYDue;
        private AisinoLBL lblHYFPLimitSum;
        private AisinoLBL lblHYLastBSDate;
        private AisinoLBL lblHYLock;
        private AisinoLBL lblHYLockday;
        private AisinoLBL lblhylxxe;
        private AisinoLBL lblhylxxe1;
        private AisinoLBL lblHYStartDate;
        private AisinoLBL lblHZFW;
        private AisinoLBL lblJDCBackFP;
        private AisinoLBL lblJDCBsData;
        private AisinoLBL lblJDCBsFlag;
        private AisinoLBL lblJDCBuyFP;
        private AisinoLBL lblJDCDue;
        private AisinoLBL lblJDCFPLimitSum;
        private AisinoLBL lblJDCKPTotal;
        private AisinoLBL lblJDCKPTotalLimit;
        private AisinoLBL lblJDCLastBSDate;
        private AisinoLBL lblJDCLock;
        private AisinoLBL lblJDCLockday;
        private AisinoLBL lbljdclxxe;
        private AisinoLBL lbljdclxxe1;
        private AisinoLBL lblJDCStartDate;
        private AisinoLBL lblJDCTPTotal;
        private AisinoLBL lblJDCTPTotalLimit;
        private AisinoLBL lblJQNo;
        private AisinoLBL lblJSPNo;
        private AisinoLBL lblKPJNo;
        private AisinoLBL lblLastBSDate;
        private AisinoLBL lblLock;
        private AisinoLBL lblLockday;
        private AisinoLBL lbllxsyjehy;
        private AisinoLBL lbllxsyjejdc;
        private AisinoLBL lbllxsyjept;
        private AisinoLBL lbllxsyjezy;
        private AisinoLBL lblMainKPJ;
        private AisinoLBL lblNowDate;
        private AisinoLBL lblNSDJH;
        private AisinoLBL lblptfplxxe;
        private AisinoLBL lblptfplxxe1;
        private AisinoLBL lblSQ;
        private AisinoLBL lblStartDate;
        private AisinoLBL lblSubKPJNum;
        private AisinoLBL lblXTSQ;
        private AisinoLBL lblzyfplxxe;
        private AisinoLBL lblzyfplxxe1;
        private ILog loger = LogUtil.GetLogger<JSPStateQuery>();
        private Panel panel1;
        private Panel panel10;
        private Panel panel11;
        private Panel panel12;
        private Panel panel13;
        private Panel panel14;
        private Panel panel15;
        private Panel panel16;
        private Panel panel2;
        private Panel panel25;
        private Panel panel26;
        private Panel panel27;
        private Panel panel28;
        private Panel panel29;
        private Panel panel3;
        private Panel panel30;
        private Panel panel31;
        private Panel panel32;
        private Panel panel33;
        private Panel panel34;
        private Panel panel35;
        private Panel panel36;
        private Panel panel37;
        private Panel panel38;
        private Panel panel39;
        private Panel panel4;
        private Panel panel40;
        private Panel panel9;
        private TabControlPwSkin tabControl1;
        private TabPageEx tabPage1;
        private TabPageEx tabPage2;
        private TabPageEx tabPage3;
        private TabPageEx tabPage4;
        private TabPageEx tabPage5;
        private TabPageEx tabPage6;
        private TaxStateInfo taxStateInfo;
        private TaxStateWrapper taxStateWrapper;
        private XmlComponentLoader xmlComponentLoader1;

        public JSPStateQuery()
        {
            this.Initialize();
            this.tabControl1.Alignment = TabAlignment.Left;
            this.tabControl1.SizeMode = TabSizeMode.Fixed;
            this.tabControl1.ItemSize = new Size(80, 0x9b);
            this.tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.tabPage1.BackColor = Color.White;
            this.tabPage2.BackColor = Color.White;
            this.tabPage3.BackColor = Color.White;
            this.tabPage4.BackColor = Color.White;
            this.tabPage4.BackColor = Color.White;
            this.tabPage5.BackColor = Color.White;
            this.tabPage6.BackColor = Color.White;
            this.tabControl1.Dock = DockStyle.Fill;
            this.taxStateInfo = base.TaxCardInstance.get_StateInfo();
            this.taxStateWrapper = new TaxStateWrapper(this.taxStateInfo, base.TaxCardInstance.get_Machine());
            if (!this.taxStateWrapper.IsTBEnable || (this.taxStateInfo.TBRegFlag == 0))
            {
                this.hasBSP = false;
            }
            else
            {
                this.hasBSP = true;
            }
            base.Load += new EventHandler(this.JSPStateQuery_Load);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitBSPInfo()
        {
            this.lblBSPKpjNo.Text = this.taxStateWrapper.BSPKPJH.ToString();
        }

        private void InitDZInvInfo()
        {
            try
            {
                this.lbl59.Text = this.taxStateWrapper.IsPTDZCSQ;
                this.lbl74.Text = this.taxStateWrapper.IsPTDZSSQ;
                List<PZSQType> pZSQType = base.TaxCardInstance.get_SQInfo().PZSQType;
                NumberFormatInfo numberFormat = new CultureInfo("zh-CN", false).NumberFormat;
                foreach (PZSQType type in pZSQType)
                {
                    if (type.invType == 0x33)
                    {
                        this.lbl50.Text = type.InvAmountLimit.ToString("C", numberFormat) + "元";
                    }
                }
                OfflineInvAmount offlineInvAmout = base.TaxCardInstance.GetOfflineInvAmout(0x33);
                this.lbl31.Text = ((offlineInvAmout.InvTotalAmount - offlineInvAmout.InvAmount)).ToString("C", numberFormat) + " 元";
                List<string> cSDate = base.TaxCardInstance.GetCSDate(0x33);
                this.lbl69.Text = Convert.ToDateTime(cSDate[1]).ToString("yyyy年MM月dd日");
                this.lbl71.Text = Convert.ToDateTime(cSDate[0]).ToString("yyyy年MM月dd日HH时mm分");
                this.lbl75.Text = Convert.ToDateTime(cSDate[2]).ToString("yyyy年MM月dd日");
                if (base.TaxCardInstance.get_QYLX().ISTDQY)
                {
                    this.lbl86.Text = "-";
                    this.lbl32.Text = "-";
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox(exception.ToString());
                this.loger.Error(exception.Message, exception);
            }
        }

        private void InitHYInvInfo()
        {
            this.lblHYDue.Text = this.taxStateWrapper.IsHYCSQ;
            this.lblHYLock.Text = this.taxStateWrapper.IsHYSSQ;
            List<PZSQType> pZSQType = base.TaxCardInstance.get_SQInfo().PZSQType;
            NumberFormatInfo numberFormat = new CultureInfo("zh-CN", false).NumberFormat;
            foreach (PZSQType type in pZSQType)
            {
                if (type.invType == 11)
                {
                    this.lblHYFPLimitSum.Text = type.InvAmountLimit.ToString("C", numberFormat) + "元";
                }
            }
            OfflineInvAmount offlineInvAmout = base.TaxCardInstance.GetOfflineInvAmout(11);
            this.lbllxsyjehy.Text = ((offlineInvAmout.InvTotalAmount - offlineInvAmout.InvAmount)).ToString("C", numberFormat) + " 元";
            string[] strArray = this.taxStateWrapper.HYLockedDate.Split(new char[] { '-' });
            this.lblHYLockday.Text = strArray[0] + "年" + strArray[1] + "月" + strArray[2] + "日";
            strArray = this.taxStateWrapper.HYLastRepDate.Split(new char[] { '-', ' ', ':' });
            this.lblHYLastBSDate.Text = strArray[0] + "年" + strArray[1] + "月" + strArray[2] + "日" + strArray[3] + "时" + strArray[4] + "分";
            strArray = this.taxStateWrapper.HYNextRepDate.Split(new char[] { '-' });
            this.lblHYStartDate.Text = strArray[0] + "年" + strArray[1] + "月" + strArray[2] + "日";
            if (base.TaxCardInstance.get_QYLX().ISTDQY)
            {
                this.lblhylxxe1.Text = "-";
                this.lbllxsyjehy.Text = "-";
            }
        }

        private void Initialize()
        {
            this.InitializeComponent();
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.tabControl1 = this.xmlComponentLoader1.GetControlByName<TabControlPwSkin>("tabControl1");
            this.tabPage1 = this.xmlComponentLoader1.GetControlByName<TabPageEx>("tabPage1");
            this.tabPage2 = this.xmlComponentLoader1.GetControlByName<TabPageEx>("tabPage2");
            this.tabPage3 = this.xmlComponentLoader1.GetControlByName<TabPageEx>("tabPage3");
            this.tabPage4 = this.xmlComponentLoader1.GetControlByName<TabPageEx>("tabPage4");
            this.tabPage5 = this.xmlComponentLoader1.GetControlByName<TabPageEx>("tabPage5");
            this.tabPage6 = this.xmlComponentLoader1.GetControlByName<TabPageEx>("tabPage6");
            this.lblNSDJH = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblNSDJH");
            this.lblMainKPJ = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblMainKPJ");
            this.lblSubKPJNum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblSubKPJNum");
            this.lblKPJNo = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblKPJNo");
            this.lblNowDate = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblNowDate");
            this.lblFPAvailable = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblFPAvailable");
            this.lblDriverNo = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblDriverNo");
            this.lblBottomNo = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBottomNo");
            this.lblJQNo = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJQNo");
            this.lblJSPNo = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJSPNo");
            this.lblBSPNo = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBSPNo");
            this.lblBSPBottomNo = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBSPBottomNo");
            this.lblHZFW = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblHZFW");
            this.lblSQ = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblSQ");
            this.lblBSPCapacity = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBSPCapacity");
            this.label2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label2");
            this.label4 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label4");
            this.label5 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label5");
            this.label9 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label9");
            this.lblDue = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblDue");
            this.lblLock = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblLock");
            this.lblFPLimitSum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblFPLimitSum");
            this.lblLockday = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblLockday");
            this.lblLastBSDate = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblLastBSDate");
            this.lblStartDate = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblStartDate");
            this.label33 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label33");
            this.label34 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label34");
            this.label35 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label35");
            this.label36 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label36");
            this.lblBSPBsData = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBSPBsData");
            this.lblBSPBsFlag = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBSPBsFlag");
            this.lblBSPBuyFP = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBSPBuyFP");
            this.lblBSPBackFP = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBSPBackFP");
            this.label6 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label6");
            this.label7 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label7");
            this.label8 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label8");
            this.label18 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label18");
            this.label21 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label21");
            this.label3 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label3");
            this.lblHYDue = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblHYDue");
            this.lblHYLock = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblHYLock");
            this.lblHYFPLimitSum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblHYFPLimitSum");
            this.lblHYLockday = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblHYLockday");
            this.lblHYLastBSDate = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblHYLastBSDate");
            this.lblHYStartDate = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblHYStartDate");
            this.aisinoLBL25 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL25");
            this.aisinoLBL26 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL26");
            this.aisinoLBL27 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL27");
            this.aisinoLBL28 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL28");
            this.lblHYBackFP = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblHYBackFP");
            this.lblHYBuyFP = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblHYBuyFP");
            this.lblHYBsFlag = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblHYBsFlag");
            this.lblHYBsData = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblHYBsData");
            this.lblHYDue = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblHYDue");
            this.lblHYLock = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblHYLock");
            this.lblHYFPLimitSum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblHYFPLimitSum");
            this.lblHYLockday = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblHYLockday");
            this.lblHYLastBSDate = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblHYLastBSDate");
            this.lblHYLastBSDate = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblHYLastBSDate");
            this.lblJDCDue = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJDCDue");
            this.lblJDCLock = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJDCLock");
            this.lblJDCFPLimitSum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJDCFPLimitSum");
            this.lblJDCKPTotalLimit = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJDCKPTotalLimit");
            this.lblJDCTPTotalLimit = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJDCTPTotalLimit");
            this.lblJDCKPTotal = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJDCKPTotal");
            this.lblJDCTPTotal = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJDCTPTotal");
            this.lblJDCLockday = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJDCLockday");
            this.lblJDCLastBSDate = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJDCLastBSDate");
            this.lblJDCStartDate = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJDCStartDate");
            this.aisinoLBL37 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL37");
            this.aisinoLBL38 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL38");
            this.aisinoLBL39 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL39");
            this.aisinoLBL40 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL40");
            this.lblJDCBackFP = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJDCBackFP");
            this.lblJDCBuyFP = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJDCBuyFP");
            this.lblJDCBsFlag = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJDCBsFlag");
            this.lblJDCBsData = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJDCBsData");
            this.lblJDCDue = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJDCDue");
            this.lblJDCLock = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJDCLock");
            this.lblJDCFPLimitSum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJDCFPLimitSum");
            this.lblJDCKPTotalLimit = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJDCKPTotalLimit");
            this.lblJDCKPTotal = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJDCKPTotal");
            this.lblJDCTPTotal = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJDCTPTotal");
            this.lblJDCLockday = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJDCLockday");
            this.lblJDCLastBSDate = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJDCLastBSDate");
            this.lblJDCStartDate = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJDCStartDate");
            this.aisinoLBL4 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL4");
            this.aisinoLBL17 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL17");
            this.aisinoLBLLXSX = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBLLXSX");
            this.aisinoLBLLXSSQ = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBLLXSSQ");
            this.lblzyfplxxe1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblzyfplxxe1");
            this.lblptfplxxe1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblptfplxxe1");
            this.lblptfplxxe = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblptfplxxe");
            this.lblzyfplxxe = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblzyfplxxe");
            this.lblhylxxe1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblhylxxe1");
            this.lblhylxxe = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblhylxxe");
            this.lbljdclxxe1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbljdclxxe1");
            this.lbljdclxxe = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbljdclxxe");
            this.lblFPLimitSumPT = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblFPLimitSumPT");
            this.aisinoLBL23 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL23");
            this.aisinoLBL12 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL12");
            this.lblFPStock = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblFPStock");
            this.lbl_1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_1");
            this.lbl_2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_2");
            this.lbl_3 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_3");
            this.lbl_4 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_4");
            this.lbl_5 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_5");
            this.lbl_6 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_6");
            this.lbl_7 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_7");
            this.lbl_8 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_8");
            this.lbl_9 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_9");
            this.panel1 = this.xmlComponentLoader1.GetControlByName<Panel>("panel1");
            this.panel2 = this.xmlComponentLoader1.GetControlByName<Panel>("panel2");
            this.panel3 = this.xmlComponentLoader1.GetControlByName<Panel>("panel3");
            this.panel4 = this.xmlComponentLoader1.GetControlByName<Panel>("panel4");
            this.lbl_11 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_11");
            this.lbl_22 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_22");
            this.lbl_33 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_33");
            this.lbl_44 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_44");
            this.lbl_55 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_55");
            this.lbl_66 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_66");
            this.lbl_77 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_77");
            this.lbl_88 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_88");
            this.lbl_99 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_99");
            this.panel9 = this.xmlComponentLoader1.GetControlByName<Panel>("panel9");
            this.panel10 = this.xmlComponentLoader1.GetControlByName<Panel>("panel10");
            this.panel11 = this.xmlComponentLoader1.GetControlByName<Panel>("panel11");
            this.panel12 = this.xmlComponentLoader1.GetControlByName<Panel>("panel12");
            this.lbl_111 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_111");
            this.lbl_222 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_222");
            this.lbl_333 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_333");
            this.lbl_444 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_444");
            this.lbl_555 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_555");
            this.lbl_666 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_666");
            this.lbl_777 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_777");
            this.lbl_888 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_888");
            this.lbl_999 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_999");
            this.panel13 = this.xmlComponentLoader1.GetControlByName<Panel>("panel13");
            this.panel14 = this.xmlComponentLoader1.GetControlByName<Panel>("panel14");
            this.panel15 = this.xmlComponentLoader1.GetControlByName<Panel>("panel15");
            this.panel16 = this.xmlComponentLoader1.GetControlByName<Panel>("panel16");
            this.lbllxsyjezy = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbllxsyjezy");
            this.lbllxsyjept = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbllxsyjept");
            this.lbllxsyjehy = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbllxsyjehy");
            this.lbllxsyjejdc = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbllxsyjejdc");
            this.label22 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label22");
            this.lblXTSQ = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblXTSQ");
            this.gbBSP = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("gbBSP");
            this.lblBSPBackFP = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBSPBackFP");
            this.lblBSPBuyFP = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBSPBuyFP");
            this.lblBSPBsFlag = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBSPBsFlag");
            this.lblBSPBsData = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBSPBsData");
            this.lblBSPKpjNo = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBSPKpjNo");
            this.panel25 = this.xmlComponentLoader1.GetControlByName<Panel>("panel25");
            this.panel26 = this.xmlComponentLoader1.GetControlByName<Panel>("panel26");
            this.panel27 = this.xmlComponentLoader1.GetControlByName<Panel>("panel27");
            this.panel28 = this.xmlComponentLoader1.GetControlByName<Panel>("panel28");
            this.panel29 = this.xmlComponentLoader1.GetControlByName<Panel>("panel29");
            this.panel30 = this.xmlComponentLoader1.GetControlByName<Panel>("panel30");
            this.panel31 = this.xmlComponentLoader1.GetControlByName<Panel>("panel31");
            this.panel32 = this.xmlComponentLoader1.GetControlByName<Panel>("panel32");
            this.lbl43 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL43");
            this.lbl60 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL60");
            this.lbl59 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL59");
            this.lbl77 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL77");
            this.lbl74 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL74");
            this.lbl58 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL58");
            this.lbl54 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL54");
            this.lbl41 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL41");
            this.lbl31 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL31");
            this.lbl52 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL52");
            this.lbl50 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL50");
            this.lbl70 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL70");
            this.lbl69 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL69");
            this.lbl72 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL72");
            this.lbl71 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL71");
            this.lbl78 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL78");
            this.lbl75 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL75");
            this.lbl68 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL69");
            this.lbl64 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL64");
            this.lbl67 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL67");
            this.lbl63 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL63");
            this.lbl66 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL66");
            this.lbl62 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL62");
            this.lbl65 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL65");
            this.lbl61 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL61");
            this.lbl43 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL43");
            this.lbl47 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL47");
            this.lbl44 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL44");
            this.lbl45 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL45");
            this.lbl46 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL46");
            this.lbl48 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL48");
            this.lbl51 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL51");
            this.lbl55 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL55");
            this.lbl49 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL49");
            this.lbl53 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL53");
            this.panel34 = this.xmlComponentLoader1.GetControlByName<Panel>("panel34");
            this.panel36 = this.xmlComponentLoader1.GetControlByName<Panel>("panel36");
            this.panel35 = this.xmlComponentLoader1.GetControlByName<Panel>("panel35");
            this.panel33 = this.xmlComponentLoader1.GetControlByName<Panel>("panel33");
            this.panel40 = this.xmlComponentLoader1.GetControlByName<Panel>("panel40");
            this.panel39 = this.xmlComponentLoader1.GetControlByName<Panel>("panel39");
            this.panel38 = this.xmlComponentLoader1.GetControlByName<Panel>("panel38");
            this.panel37 = this.xmlComponentLoader1.GetControlByName<Panel>("panel37");
            this.lbl56 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL56");
            this.lbl90 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL90");
            this.lbl89 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL89");
            this.lbl105 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL105");
            this.lbl103 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL103");
            this.lbl88 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL88");
            this.lbl86 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL86");
            this.lbl35 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL35");
            this.lbl32 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL32");
            this.lbl84 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL84");
            this.lbl82 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL82");
            this.lbl100 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL100");
            this.lbl99 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL99");
            this.lbl102 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL102");
            this.lbl101 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL101");
            this.lbl106 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL106");
            this.lbl104 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL104");
            this.lbl98 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL98");
            this.lbl94 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL94");
            this.lbl97 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL97");
            this.lbl93 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL93");
            this.lbl96 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL96");
            this.lbl92 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL92");
            this.lbl95 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL95");
            this.lbl91 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL91");
            this.lbl79 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL79");
            this.lbl57 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL57");
            this.lbl73 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL73");
            this.lbl76 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL76");
            this.lbl80 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL80");
            this.lbl83 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL83");
            this.lbl87 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL87");
            this.lbl81 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL81");
            this.lbl85 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL85");
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(JSPStateQuery));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(740, 0x223);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Bsgl.JSPStateQuery\Aisino.Fwkp.Bsgl.JSPStateQuery.xml");
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(740, 0x223);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "JSPStateQuery";
            base.set_TabText("金税设备状态查询");
            this.Text = "金税设备状态查询";
            base.ResumeLayout(false);
        }

        private void InitJDCInvInfo()
        {
            this.lblJDCDue.Text = this.taxStateWrapper.IsJDCCSQ;
            this.lblJDCLock.Text = this.taxStateWrapper.IsJDCSSQ;
            NumberFormatInfo numberFormat = new CultureInfo("zh-CN", false).NumberFormat;
            if (base.TaxCardInstance.GetInvMonthAmount().Count == 0)
            {
                this.lblJDCKPTotal.Text = double.Parse("0").ToString("C", numberFormat) + "元";
                this.lblJDCTPTotal.Text = double.Parse("0").ToString("C", numberFormat) + "元";
            }
            foreach (InvoiceAmountType type in base.TaxCardInstance.GetInvMonthAmount())
            {
                if (type.InvType == "12")
                {
                    this.lblJDCKPTotal.Text = double.Parse(type.KPMoney).ToString("C", numberFormat) + "元";
                    this.lblJDCTPTotal.Text = double.Parse(type.TPMoney).ToString("C", numberFormat) + "元";
                }
            }
            OfflineInvAmount offlineInvAmout = base.TaxCardInstance.GetOfflineInvAmout(12);
            this.lbllxsyjejdc.Text = ((offlineInvAmout.InvTotalAmount - offlineInvAmout.InvAmount)).ToString("C", numberFormat) + " 元";
            string[] strArray = this.taxStateWrapper.JDCLockedDate.Split(new char[] { '-' });
            this.lblJDCLockday.Text = strArray[0] + "年" + strArray[1] + "月" + strArray[2] + "日";
            strArray = this.taxStateWrapper.JDCLastRepDate.Split(new char[] { '-', ' ', ':' });
            this.lblJDCLastBSDate.Text = strArray[0] + "年" + strArray[1] + "月" + strArray[2] + "日" + strArray[3] + "时" + strArray[4] + "分";
            strArray = this.taxStateWrapper.JDCNextRepDate.Split(new char[] { '-' });
            this.lblJDCStartDate.Text = strArray[0] + "年" + strArray[1] + "月" + strArray[2] + "日";
            if (base.TaxCardInstance.get_QYLX().ISTDQY)
            {
                this.lbljdclxxe1.Text = "-";
                this.lbllxsyjejdc.Text = "-";
            }
        }

        private void InitJSFPInvInfo()
        {
            try
            {
                this.lbl89.Text = this.taxStateWrapper.IsJSFPCSQ;
                this.lbl103.Text = this.taxStateWrapper.IsJSFPSSQ;
                List<PZSQType> pZSQType = base.TaxCardInstance.get_SQInfo().PZSQType;
                NumberFormatInfo numberFormat = new CultureInfo("zh-CN", false).NumberFormat;
                foreach (PZSQType type in pZSQType)
                {
                    if (type.invType == 0x29)
                    {
                        this.lbl82.Text = type.InvAmountLimit.ToString("C", numberFormat) + "元";
                    }
                }
                OfflineInvAmount offlineInvAmout = base.TaxCardInstance.GetOfflineInvAmout(0x29);
                this.lbl32.Text = ((offlineInvAmout.InvTotalAmount - offlineInvAmout.InvAmount)).ToString("C", numberFormat) + " 元";
                List<string> cSDate = base.TaxCardInstance.GetCSDate(0x29);
                this.lbl99.Text = Convert.ToDateTime(cSDate[1]).ToString("yyyy年MM月dd日");
                this.lbl101.Text = Convert.ToDateTime(cSDate[0]).ToString("yyyy年MM月dd日HH时mm分");
                this.lbl104.Text = Convert.ToDateTime(cSDate[2]).ToString("yyyy年MM月dd日");
                if (base.TaxCardInstance.get_QYLX().ISTDQY)
                {
                    this.lbl54.Text = "-";
                    this.lbl31.Text = "-";
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("状态查询-初始化增值税普通发票(卷票)异常：" + exception.ToString());
            }
        }

        private void InitNorSpeInvInfo()
        {
            try
            {
                this.lblDue.Text = this.taxStateWrapper.IsCSQ;
                this.lblLock.Text = this.taxStateWrapper.IsSSQ;
                List<string> cSDate = base.TaxCardInstance.GetCSDate(0);
                if ((cSDate != null) && (cSDate.Count == 3))
                {
                    this.lblLockday.Text = Convert.ToDateTime(cSDate[1]).ToString("yyyy年MM月dd日");
                }
                this.lblLastBSDate.Text = base.TaxCardInstance.get_LastRepDate().ToString("yyyy年MM月dd日HH时mm分");
                this.lblStartDate.Text = base.TaxCardInstance.get_RepDate().ToString("yyyy年MM月dd日");
                NumberFormatInfo numberFormat = new CultureInfo("zh-CN", false).NumberFormat;
                if (base.TaxCardInstance.get_QYLX().ISZYFP)
                {
                    OfflineInvAmount offlineInvAmout = base.TaxCardInstance.GetOfflineInvAmout(0);
                    this.lbllxsyjezy.Text = ((offlineInvAmout.InvTotalAmount - offlineInvAmout.InvAmount)).ToString("C", numberFormat) + " 元";
                }
                if (base.TaxCardInstance.get_QYLX().ISPTFP)
                {
                    OfflineInvAmount amount2 = base.TaxCardInstance.GetOfflineInvAmout(2);
                    this.lbllxsyjept.Text = ((amount2.InvTotalAmount - amount2.InvAmount)).ToString("C", numberFormat) + " 元";
                }
                this.lblBSPBsData.Text = (this.taxStateInfo.ICRepInfo == 0) ? "无" : "有";
                this.lblBSPBsFlag.Text = (this.taxStateInfo.ICRepDone == 0) ? "无" : "有";
                this.lblBSPBuyFP.Text = (this.taxStateInfo.ICBuyInv == 0) ? "无" : "有";
                this.lblBSPBackFP.Text = (this.taxStateInfo.ICRetInv == 0) ? "无" : "有";
                if (this.hasBSP)
                {
                    this.lbl_5.Text = (this.taxStateInfo.TBRepInfo == 0) ? "无" : "有";
                    this.lbl_6.Text = (this.taxStateInfo.TBRepDone == 0) ? "无" : "有";
                    this.lbl_7.Text = (this.taxStateInfo.TBBuyInv == 0) ? "无" : "有";
                    this.lbl_8.Text = (this.taxStateInfo.TBRetInv == 0) ? "无" : "有";
                }
                else
                {
                    this.lbl_1.Visible = false;
                    this.lbl_2.Visible = false;
                    this.lbl_3.Visible = false;
                    this.lbl_4.Visible = false;
                    this.lbl_5.Visible = false;
                    this.lbl_6.Visible = false;
                    this.lbl_7.Visible = false;
                    this.lbl_8.Visible = false;
                    this.lbl_9.Visible = false;
                    this.panel1.Visible = false;
                    this.panel2.Visible = false;
                    this.panel3.Visible = false;
                    this.panel4.Visible = false;
                }
                if (base.TaxCardInstance.get_QYLX().ISTDQY)
                {
                    string str = "-";
                    this.lblzyfplxxe1.Text = str;
                    this.lblptfplxxe1.Text = str;
                    this.lbllxsyjezy.Text = str;
                    this.lbllxsyjept.Text = str;
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox(exception.ToString());
                this.loger.Error(exception.Message, exception);
            }
        }

        private void InitTaxCardInfo()
        {
            this.lblNSDJH.Text = this.taxStateWrapper.NSDJH;
            this.lblMainKPJ.Text = this.taxStateWrapper.KPJLX;
            if (this.taxStateWrapper.KPJLX.Equals("主开票机"))
            {
                this.lblSubKPJNum.Text = this.taxStateWrapper.FKPJSM.ToString();
            }
            else
            {
                this.lblSubKPJNum.Text = "0";
                this.lblSubKPJNum.Visible = false;
                this.label2.Visible = false;
                int height = -28;
                this.MoveAisinoLable(this.lblKPJNo, height);
                this.MoveAisinoLable(this.lblNowDate, height);
                this.MoveAisinoLable(this.lblFPAvailable, height);
                this.MoveAisinoLable(this.aisinoLBLLXSSQ, height);
                this.MoveAisinoLable(this.aisinoLBLLXSX, height);
                this.MoveAisinoLable(this.lblXTSQ, height);
                this.MoveAisinoLable(this.label9, height);
                this.MoveAisinoLable(this.label4, height);
                this.MoveAisinoLable(this.label5, height);
                this.MoveAisinoLable(this.aisinoLBL4, height);
                this.MoveAisinoLable(this.aisinoLBL17, height);
                this.MoveAisinoLable(this.label22, height);
            }
            if (base.TaxCardInstance.get_SQInfo().DHYBZ.Equals("Y") || base.TaxCardInstance.get_SQInfo().DHYBZ.Equals("Z"))
            {
                this.lblSubKPJNum.Text = "-";
            }
            this.lblKPJNo.Text = this.taxStateWrapper.ICKPJH.ToString();
            this.lblNowDate.Text = base.TaxCardInstance.GetCardClock().ToString("yyyy年MM月dd日");
            this.lblFPAvailable.Text = this.taxStateWrapper.IsFPYW;
            this.lblDriverNo.Text = this.taxStateWrapper.DriverNo;
            this.lblBottomNo.Text = this.taxStateWrapper.BottomNo;
            this.aisinoLBLLXSSQ.Text = "每月" + base.TaxCardInstance.get_SQInfo().LXSSQ.ToString() + "号";
            this.aisinoLBLLXSX.Text = base.TaxCardInstance.get_SQInfo().LXKPTIME.ToString() + " 小时";
            List<PZSQType> pZSQType = base.TaxCardInstance.get_SQInfo().PZSQType;
            NumberFormatInfo numberFormat = new CultureInfo("zh-CN", false).NumberFormat;
            if (pZSQType != null)
            {
                foreach (PZSQType type in pZSQType)
                {
                    if (type.invType == null)
                    {
                        this.lblzyfplxxe1.Text = type.OffLineAmoutLimit.ToString("C", numberFormat) + " 元";
                        if (this.taxStateInfo.InvLimit > 0L)
                        {
                            this.lblFPLimitSum.Text = ((this.taxStateInfo.InvLimit - 0.01)).ToString("C", numberFormat) + " 元";
                        }
                        else
                        {
                            this.lblFPLimitSum.Text = ((double) this.taxStateInfo.InvLimit).ToString("C", numberFormat) + " 元";
                        }
                    }
                    if (type.invType == 2)
                    {
                        this.lblptfplxxe1.Text = type.OffLineAmoutLimit.ToString("C", numberFormat) + " 元";
                        this.lblFPLimitSumPT.Text = type.InvAmountLimit.ToString("C", numberFormat) + " 元";
                    }
                    if (type.invType == 11)
                    {
                        this.lblhylxxe1.Text = type.OffLineAmoutLimit.ToString("C", numberFormat) + " 元";
                    }
                    if (type.invType == 12)
                    {
                        this.lbljdclxxe1.Text = type.OffLineAmoutLimit.ToString("C", numberFormat) + " 元";
                        this.lblJDCFPLimitSum.Text = type.InvAmountLimit.ToString("C", numberFormat) + " 元";
                        this.lblJDCKPTotalLimit.Text = type.MonthAmountLimit.ToString("C", numberFormat) + " 元";
                        this.lblJDCTPTotalLimit.Text = type.ReturnAmountLimit.ToString("C", numberFormat) + " 元";
                    }
                    if (type.invType == 0x33)
                    {
                        this.lbl54.Text = type.OffLineAmoutLimit.ToString("C", numberFormat) + " 元";
                    }
                    if (type.invType == 0x29)
                    {
                        this.lbl86.Text = type.OffLineAmoutLimit.ToString("C", numberFormat) + " 元";
                    }
                }
            }
            List<InvTypeInfo> invTypeInfo = this.taxStateInfo.InvTypeInfo;
            if (invTypeInfo != null)
            {
                foreach (InvTypeInfo info2 in invTypeInfo)
                {
                    if (info2.InvType == 11)
                    {
                        this.lblHYBackFP.Text = (info2.JSPRetInv == 0) ? "无" : "有";
                        this.lblHYBuyFP.Text = (info2.JSPBuyInv == 0) ? "无" : "有";
                        this.lblHYBsFlag.Text = (info2.JSPRepDone == 0) ? "无" : "有";
                        this.lblHYBsData.Text = (info2.JSPRepInfo == 0) ? "无" : "有";
                        if (this.hasBSP)
                        {
                            this.lbl_55.Text = (info2.ICRepInfo == 0) ? "无" : "有";
                            this.lbl_66.Text = (info2.ICRepDone == 0) ? "无" : "有";
                            this.lbl_77.Text = (info2.ICBuyInv == 0) ? "无" : "有";
                            this.lbl_88.Text = (info2.ICRetInv == 0) ? "无" : "有";
                        }
                        else
                        {
                            this.lbl_11.Visible = false;
                            this.lbl_22.Visible = false;
                            this.lbl_33.Visible = false;
                            this.lbl_44.Visible = false;
                            this.lbl_55.Visible = false;
                            this.lbl_66.Visible = false;
                            this.lbl_77.Visible = false;
                            this.lbl_88.Visible = false;
                            this.lbl_99.Visible = false;
                            this.panel9.Visible = false;
                            this.panel10.Visible = false;
                            this.panel11.Visible = false;
                            this.panel12.Visible = false;
                        }
                    }
                    if (info2.InvType == 12)
                    {
                        this.lblJDCBackFP.Text = (info2.JSPRetInv == 0) ? "无" : "有";
                        this.lblJDCBuyFP.Text = (info2.JSPBuyInv == 0) ? "无" : "有";
                        this.lblJDCBsFlag.Text = (info2.JSPRepDone == 0) ? "无" : "有";
                        this.lblJDCBsData.Text = (info2.JSPRepInfo == 0) ? "无" : "有";
                        if (this.hasBSP)
                        {
                            this.lbl_555.Text = (info2.ICRepInfo == 0) ? "无" : "有";
                            this.lbl_666.Text = (info2.ICRepDone == 0) ? "无" : "有";
                            this.lbl_777.Text = (info2.ICBuyInv == 0) ? "无" : "有";
                            this.lbl_888.Text = (info2.ICRetInv == 0) ? "无" : "有";
                        }
                        else
                        {
                            this.lbl_111.Visible = false;
                            this.lbl_222.Visible = false;
                            this.lbl_333.Visible = false;
                            this.lbl_444.Visible = false;
                            this.lbl_555.Visible = false;
                            this.lbl_666.Visible = false;
                            this.lbl_777.Visible = false;
                            this.lbl_888.Visible = false;
                            this.lbl_999.Visible = false;
                            this.panel13.Visible = false;
                            this.panel14.Visible = false;
                            this.panel15.Visible = false;
                            this.panel16.Visible = false;
                        }
                    }
                    if (info2.InvType == 0x33)
                    {
                        this.lbl61.Text = (info2.JSPRetInv == 0) ? "无" : "有";
                        this.lbl62.Text = (info2.JSPBuyInv == 0) ? "无" : "有";
                        this.lbl63.Text = (info2.JSPRepDone == 0) ? "无" : "有";
                        this.lbl64.Text = (info2.JSPRepInfo == 0) ? "无" : "有";
                        if (this.hasBSP)
                        {
                            this.lbl45.Text = (info2.ICRepInfo == 0) ? "无" : "有";
                            this.lbl48.Text = (info2.ICRepDone == 0) ? "无" : "有";
                            this.lbl55.Text = (info2.ICBuyInv == 0) ? "无" : "有";
                            this.lbl53.Text = (info2.ICRetInv == 0) ? "无" : "有";
                        }
                        else
                        {
                            this.lbl45.Visible = false;
                            this.lbl48.Visible = false;
                            this.lbl55.Visible = false;
                            this.lbl53.Visible = false;
                            this.lbl47.Visible = false;
                            this.lbl44.Visible = false;
                            this.lbl46.Visible = false;
                            this.lbl51.Visible = false;
                            this.lbl47.Visible = false;
                            this.panel32.Visible = false;
                            this.panel31.Visible = false;
                            this.panel30.Visible = false;
                            this.panel29.Visible = false;
                            this.lbl49.Visible = false;
                        }
                    }
                    if (info2.InvType == 0x29)
                    {
                        this.lbl91.Text = (info2.JSPRetInv == 0) ? "无" : "有";
                        this.lbl92.Text = (info2.JSPBuyInv == 0) ? "无" : "有";
                        this.lbl93.Text = (info2.JSPRepDone == 0) ? "无" : "有";
                        this.lbl94.Text = (info2.JSPRepInfo == 0) ? "无" : "有";
                        if (this.hasBSP)
                        {
                            this.lbl73.Text = (info2.ICRepInfo == 0) ? "无" : "有";
                            this.lbl80.Text = (info2.ICRepDone == 0) ? "无" : "有";
                            this.lbl87.Text = (info2.ICBuyInv == 0) ? "无" : "有";
                            this.lbl85.Text = (info2.ICRetInv == 0) ? "无" : "有";
                        }
                        else
                        {
                            this.lbl79.Visible = false;
                            this.lbl57.Visible = false;
                            this.lbl73.Visible = false;
                            this.lbl76.Visible = false;
                            this.lbl80.Visible = false;
                            this.lbl83.Visible = false;
                            this.lbl87.Visible = false;
                            this.lbl81.Visible = false;
                            this.lbl85.Visible = false;
                            this.panel40.Visible = false;
                            this.panel38.Visible = false;
                            this.panel39.Visible = false;
                            this.panel37.Visible = false;
                        }
                    }
                }
            }
            string invControlNum = base.TaxCardInstance.GetInvControlNum();
            if (base.TaxCardInstance.get_RetCode() == 0)
            {
                this.lblJQNo.Text = invControlNum;
                if (invControlNum.Length == 12)
                {
                    this.lblJSPNo.Text = invControlNum;
                }
                else if (invControlNum.Substring(0, 2).Equals("87"))
                {
                    this.lblJSPNo.Text = "W" + invControlNum.Substring(2, invControlNum.Length - 2);
                }
                else
                {
                    this.lblJSPNo.Text = invControlNum;
                }
            }
            base.TaxCardInstance.GetInvControlBSNum();
            if (base.TaxCardInstance.get_RetCode() == 0)
            {
                string[] strArray = base.TaxCardInstance.GetInvControlBSNum().Split(new char[] { ',' });
                if (strArray.Length == 2)
                {
                    this.lblBSPNo.Text = strArray[1];
                    this.lblBSPBottomNo.Text = strArray[0];
                }
            }
            this.lblHZFW.Text = this.taxStateWrapper.HZFWAuth;
            this.lblSQ.Text = this.taxStateWrapper.ICFXSQXX;
            this.lblBSPCapacity.Text = this.taxStateWrapper.BSPRL.ToString() + " M";
            string xTAuth = this.taxStateWrapper.XTAuth;
            string str3 = "";
            if (!string.IsNullOrEmpty(xTAuth))
            {
                if (CheckCodeRoles.IsSNY(xTAuth))
                {
                    str3 = str3 + "石脑油、燃料油企业" + Environment.NewLine + Environment.NewLine;
                }
                if (CheckCodeRoles.IsXT(xTAuth))
                {
                    string str5 = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Config\Common\xtqybm.xml");
                    string str6 = CodeRoles.ChangeXTCodeToName(xTAuth);
                    if (!string.IsNullOrEmpty(str6))
                    {
                        string str7 = "XTQY/ZYJKSQ/" + str6;
                        str3 = str3 + XmlManager.GetValue(str5, str7) + Environment.NewLine + Environment.NewLine;
                    }
                }
            }
            if (base.TaxCardInstance.get_QYLX().ISTLQY)
            {
                str3 = str3 + "铁路运输企业" + Environment.NewLine + Environment.NewLine;
            }
            if (base.TaxCardInstance.get_QYLX().ISTDQY)
            {
                str3 = str3 + "特定企业" + Environment.NewLine + Environment.NewLine;
            }
            if (base.TaxCardInstance.get_QYLX().ISNCPSG)
            {
                str3 = str3 + "收购企业" + Environment.NewLine + Environment.NewLine;
            }
            if (base.TaxCardInstance.get_QYLX().ISNCPXS)
            {
                str3 = str3 + "销售企业" + Environment.NewLine + Environment.NewLine;
            }
            if (base.TaxCardInstance.get_QYLX().ISDXQY)
            {
                str3 = str3 + "电信特殊纳税人" + Environment.NewLine + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(str3))
            {
                this.label22.Visible = false;
                this.lblXTSQ.Visible = false;
            }
            else
            {
                this.label22.Text = "授权类型";
                this.lblXTSQ.Text = str3;
            }
            if (base.TaxCardInstance.get_QYLX().ISTDQY)
            {
                this.aisinoLBLLXSSQ.Text = "-";
                this.aisinoLBLLXSX.Text = "-";
            }
        }

        private void JSPStateQuery_Load(object sender, EventArgs e)
        {
            this.InitTaxCardInfo();
            if (string.IsNullOrEmpty(base.TaxCardInstance.get_SQInfo().DHYBZ))
            {
                this.InitNorSpeInvInfo();
                this.tabControl1.TabPages.Remove(this.tabPage3);
                this.tabControl1.TabPages.Remove(this.tabPage4);
                this.tabControl1.TabPages.Remove(this.tabPage5);
            }
            else
            {
                if (base.TaxCardInstance.get_QYLX().ISZYFP || base.TaxCardInstance.get_QYLX().ISPTFP)
                {
                    this.InitNorSpeInvInfo();
                }
                else
                {
                    this.tabControl1.TabPages.Remove(this.tabPage2);
                }
                if (base.TaxCardInstance.get_QYLX().ISHY)
                {
                    this.InitHYInvInfo();
                }
                else
                {
                    this.tabControl1.TabPages.Remove(this.tabPage3);
                }
                if (base.TaxCardInstance.get_QYLX().ISJDC)
                {
                    this.InitJDCInvInfo();
                }
                else
                {
                    this.tabControl1.TabPages.Remove(this.tabPage4);
                }
                if (base.TaxCardInstance.get_QYLX().ISPTFPDZ)
                {
                    this.InitDZInvInfo();
                }
                else
                {
                    this.tabControl1.TabPages.Remove(this.tabPage5);
                }
                if (base.TaxCardInstance.get_QYLX().ISPTFPJSP)
                {
                    this.InitJSFPInvInfo();
                }
                else
                {
                    this.tabControl1.TabPages.Remove(this.tabPage6);
                }
            }
        }

        private void MoveAisinoLable(AisinoLBL lable, int height)
        {
            lable.SetBounds(lable.Location.X, lable.Location.Y + height, lable.Width, lable.Height);
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Brush brush;
            Graphics graphics = e.Graphics;
            TabPage page = this.tabControl1.TabPages[e.Index];
            Rectangle tabRect = this.tabControl1.GetTabRect(e.Index);
            if (e.State == DrawItemState.Selected)
            {
                brush = new SolidBrush(e.ForeColor);
                Brush brush2 = new SolidBrush(Color.FromArgb(0xae, 0xc4, 0xd4));
                graphics.FillRectangle(brush2, e.Bounds);
            }
            else
            {
                brush = new SolidBrush(e.ForeColor);
                Brush brush3 = new SolidBrush(this.xmlComponentLoader1.BackColor);
                graphics.FillRectangle(brush3, e.Bounds);
            }
            StringFormat format = new StringFormat {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            graphics.DrawString(page.Text, this.Font, brush, tabRect, new StringFormat(format));
        }
    }
}

