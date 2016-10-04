namespace Aisino.Fwkp.HomePage.AisinoDock.Docks
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.HomePage.AisinoDock;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class JScardDock : IDock
    {
        private static IDock _dock;
        private IContainer components;
        private AisinoLBL lab_bjkp;
        private AisinoLBL lab_bscgbz;
        private AisinoLBL lab_bszl;
        private AisinoLBL lab_csq;
        private AisinoLBL lab_csqsrq;
        private AisinoLBL lab_dcqdbb;
        private AisinoLBL lab_fjkpsm;
        private AisinoLBL lab_fplyc;
        private AisinoLBL lab_grfpxx;
        private AisinoLBL lab_hzfw;
        private AisinoLBL lab_ickrl;
        private AisinoLBL lab_jskdqrq;
        private AisinoLBL lab_kpjh;
        private AisinoLBL lab_kpxe;
        private AisinoLBL lab_kpxe_dw;
        private AisinoLBL lab_kyfp;
        private AisinoLBL lab_qdbb;
        private AisinoLBL lab_scbsrq;
        private AisinoLBL lab_sqxx;
        private AisinoLBL lab_ssq;
        private AisinoLBL lab_ssrqts;
        private AisinoLBL lab_thfpxx;
        private AisinoLBL label1;
        private AisinoLBL label11;
        private AisinoLBL label13;
        private AisinoLBL label15;
        private AisinoLBL label17;
        private AisinoLBL label18;
        private AisinoLBL label21;
        private AisinoLBL label23;
        private AisinoLBL label24;
        private AisinoLBL label25;
        private AisinoLBL label27;
        private AisinoLBL label29;
        private AisinoLBL label3;
        private AisinoLBL label31;
        private AisinoLBL label33;
        private AisinoLBL label38;
        private AisinoLBL label4;
        private AisinoLBL label40;
        private AisinoLBL label42;
        private AisinoLBL label44;
        private AisinoLBL label46;
        private AisinoLBL label5;
        private AisinoLBL label7;
        private AisinoLBL label8;
        private AisinoLBL label9;
        private AisinoTAB tab;
        private TabPage tab_bsp;
        private TabPage tab_icCard;
        private TabPage tabPage1;
        private TabPage tabPage3;
        private AisinoLBL tb_bszl;
        private AisinoLBL tb_BTCardNo;
        private AisinoLBL tb_TBAuthInfo;
        private AisinoLBL tb_TBBuyInv;
        private AisinoLBL tb_TBCapacity;
        private AisinoLBL tb_TBRegFlag;
        private AisinoLBL tb_TBRepDone;
        private AisinoLBL tb_TBRetInv;

        public JScardDock()
        {
            this.InitializeComponent();
        }

        public JScardDock(PageControl page) : base(page)
        {
            this.InitializeComponent();
        }

        public static IDock CreateDock(PageControl page)
        {
            if ((_dock == null) || _dock.IsDisposed)
            {
                _dock = new JScardDock(page);
            }
            return _dock;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void init()
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            TaxStateInfo info = card.get_StateInfo();
            string str = (info.IsMainMachine == 1) ? "主开票机" : "分开票机";
            int machineNumber = info.MachineNumber;
            DateTime time = card.get_RepDate();
            DateTime time2 = card.get_TaxClock();
            double invLimit = info.InvLimit;
            int isInvEmpty = info.IsInvEmpty;
            int isRepReached = info.IsRepReached;
            int isLockReached = info.IsLockReached;
            int iCCardNo = info.ICCardNo;
            int iCAuthInfo = info.ICAuthInfo;
            int iCCapacity = info.ICCapacity;
            int iCRetInv = info.ICRetInv;
            int iCBuyInv = info.ICBuyInv;
            int iCRepDone = info.ICRepDone;
            int iCRepInfo = info.ICRepInfo;
            int iCInvSegm = info.ICInvSegm;
            string str2 = string.Format("{0}.{1:00}", info.MajorVersion, info.MinorVersion);
            string driverVersion = info.DriverVersion;
            int lockedDays = info.LockedDays;
            int companyType = info.CompanyType;
            DateTime time3 = card.get_LastRepDate();
            int tBCardNo = info.TBCardNo;
            int tBAuthInfo = info.TBAuthInfo;
            int tBBuyInv = info.TBBuyInv;
            int tBCapacity = info.TBCapacity;
            int tBRegFlag = info.TBRegFlag;
            int tBRepDone = info.TBRepDone;
            int tBRepInfo = info.TBRepInfo;
            int tBRetInv = info.TBRetInv;
            int tBType = info.TBType;
            int isTBEnable = info.IsTBEnable;
            SetAttribute method = new SetAttribute(this.OnSetAttribute);
            List<object> list = new List<object> {
                str,
                machineNumber,
                time,
                time2,
                invLimit,
                isInvEmpty,
                isRepReached,
                isLockReached,
                iCCardNo,
                iCAuthInfo,
                iCCapacity,
                iCRetInv,
                iCBuyInv,
                iCRepDone,
                iCRepInfo,
                iCInvSegm,
                str2,
                driverVersion,
                lockedDays,
                companyType,
                time3,
                tBCardNo,
                tBAuthInfo,
                tBBuyInv,
                tBCapacity,
                tBRegFlag,
                tBRepDone,
                tBRepInfo,
                tBRetInv,
                tBType,
                isTBEnable
            };
            base.BeginInvoke(method, new object[] { list });
        }

        private void InitializeComponent()
        {
            this.lab_bjkp = new AisinoLBL();
            this.label4 = new AisinoLBL();
            this.lab_fjkpsm = new AisinoLBL();
            this.label5 = new AisinoLBL();
            this.lab_csqsrq = new AisinoLBL();
            this.label7 = new AisinoLBL();
            this.label9 = new AisinoLBL();
            this.lab_jskdqrq = new AisinoLBL();
            this.lab_kyfp = new AisinoLBL();
            this.lab_csq = new AisinoLBL();
            this.lab_ssq = new AisinoLBL();
            this.lab_kpxe = new AisinoLBL();
            this.lab_kpxe_dw = new AisinoLBL();
            this.lab_thfpxx = new AisinoLBL();
            this.label17 = new AisinoLBL();
            this.lab_ickrl = new AisinoLBL();
            this.label21 = new AisinoLBL();
            this.lab_sqxx = new AisinoLBL();
            this.label23 = new AisinoLBL();
            this.lab_kpjh = new AisinoLBL();
            this.label25 = new AisinoLBL();
            this.lab_grfpxx = new AisinoLBL();
            this.label27 = new AisinoLBL();
            this.lab_bscgbz = new AisinoLBL();
            this.label29 = new AisinoLBL();
            this.lab_fplyc = new AisinoLBL();
            this.label31 = new AisinoLBL();
            this.lab_bszl = new AisinoLBL();
            this.label33 = new AisinoLBL();
            this.lab_scbsrq = new AisinoLBL();
            this.label38 = new AisinoLBL();
            this.lab_hzfw = new AisinoLBL();
            this.label40 = new AisinoLBL();
            this.lab_ssrqts = new AisinoLBL();
            this.label42 = new AisinoLBL();
            this.lab_dcqdbb = new AisinoLBL();
            this.label44 = new AisinoLBL();
            this.lab_qdbb = new AisinoLBL();
            this.label46 = new AisinoLBL();
            this.tab = new AisinoTAB();
            this.tabPage1 = new TabPage();
            this.tab_icCard = new TabPage();
            this.tab_bsp = new TabPage();
            this.label24 = new AisinoLBL();
            this.tb_TBRepDone = new AisinoLBL();
            this.label18 = new AisinoLBL();
            this.tb_TBRetInv = new AisinoLBL();
            this.label15 = new AisinoLBL();
            this.tb_bszl = new AisinoLBL();
            this.label13 = new AisinoLBL();
            this.tb_TBRegFlag = new AisinoLBL();
            this.label11 = new AisinoLBL();
            this.tb_BTCardNo = new AisinoLBL();
            this.label8 = new AisinoLBL();
            this.tb_TBCapacity = new AisinoLBL();
            this.label3 = new AisinoLBL();
            this.tb_TBBuyInv = new AisinoLBL();
            this.label1 = new AisinoLBL();
            this.tb_TBAuthInfo = new AisinoLBL();
            this.tabPage3 = new TabPage();
            this.tab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tab_icCard.SuspendLayout();
            this.tab_bsp.SuspendLayout();
            this.tabPage3.SuspendLayout();
            base.SuspendLayout();
            this.lab_bjkp.AutoSize = true;
            this.lab_bjkp.Font = new Font("宋体", 9f);
            this.lab_bjkp.Location = new Point(0x89, 14);
            this.lab_bjkp.Name = "lab_bjkp";
            this.lab_bjkp.Size = new Size(0, 12);
            this.lab_bjkp.TabIndex = 6;
            this.label4.AutoSize = true;
            this.label4.Font = new Font("宋体", 9f);
            this.label4.Location = new Point(12, 14);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x53, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "本机开票类型:";
            this.lab_fjkpsm.AutoSize = true;
            this.lab_fjkpsm.Font = new Font("宋体", 9f);
            this.lab_fjkpsm.Location = new Point(0x89, 0x22);
            this.lab_fjkpsm.Name = "lab_fjkpsm";
            this.lab_fjkpsm.Size = new Size(0, 12);
            this.lab_fjkpsm.TabIndex = 8;
            this.label5.AutoSize = true;
            this.label5.Font = new Font("宋体", 9f);
            this.label5.Location = new Point(12, 0x22);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x53, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "分开票机数量:";
            this.lab_csqsrq.AutoSize = true;
            this.lab_csqsrq.Font = new Font("宋体", 9f);
            this.lab_csqsrq.Location = new Point(0x89, 0x36);
            this.lab_csqsrq.Name = "lab_csqsrq";
            this.lab_csqsrq.Size = new Size(0, 12);
            this.lab_csqsrq.TabIndex = 10;
            this.label7.AutoSize = true;
            this.label7.Font = new Font("宋体", 9f);
            this.label7.Location = new Point(12, 0x36);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x53, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "抄税起始日期:";
            this.label9.AutoSize = true;
            this.label9.Font = new Font("宋体", 9f);
            this.label9.Location = new Point(12, 0x4a);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x5f, 12);
            this.label9.TabIndex = 11;
            this.label9.Text = "金税卡当前日期:";
            this.lab_jskdqrq.AutoSize = true;
            this.lab_jskdqrq.Font = new Font("宋体", 9f);
            this.lab_jskdqrq.Location = new Point(0x89, 0x4a);
            this.lab_jskdqrq.Name = "lab_jskdqrq";
            this.lab_jskdqrq.Size = new Size(0, 12);
            this.lab_jskdqrq.TabIndex = 13;
            this.lab_kyfp.AutoSize = true;
            this.lab_kyfp.Font = new Font("宋体", 9f);
            this.lab_kyfp.ForeColor = Color.Blue;
            this.lab_kyfp.Location = new Point(0x10, 0x7d);
            this.lab_kyfp.Name = "lab_kyfp";
            this.lab_kyfp.Size = new Size(0x41, 12);
            this.lab_kyfp.TabIndex = 14;
            this.lab_kyfp.Text = "有可用发票";
            this.lab_csq.AutoSize = true;
            this.lab_csq.Font = new Font("宋体", 9f);
            this.lab_csq.ForeColor = Color.Red;
            this.lab_csq.Location = new Point(0x57, 0x7d);
            this.lab_csq.Name = "lab_csq";
            this.lab_csq.Size = new Size(0x41, 12);
            this.lab_csq.TabIndex = 15;
            this.lab_csq.Text = "以到抄税期";
            this.lab_ssq.AutoSize = true;
            this.lab_ssq.Font = new Font("宋体", 9f);
            this.lab_ssq.ForeColor = Color.Blue;
            this.lab_ssq.Location = new Point(0x9e, 0x7d);
            this.lab_ssq.Name = "lab_ssq";
            this.lab_ssq.Size = new Size(0x41, 12);
            this.lab_ssq.TabIndex = 0x10;
            this.lab_ssq.Text = "未到锁死期";
            this.lab_kpxe.AutoSize = true;
            this.lab_kpxe.Font = new Font("宋体", 9f);
            this.lab_kpxe.Location = new Point(0x89, 0x5e);
            this.lab_kpxe.Name = "lab_kpxe";
            this.lab_kpxe.Size = new Size(0, 12);
            this.lab_kpxe.TabIndex = 0x12;
            this.lab_kpxe_dw.AutoSize = true;
            this.lab_kpxe_dw.Font = new Font("宋体", 9f);
            this.lab_kpxe_dw.Location = new Point(12, 0x5e);
            this.lab_kpxe_dw.Name = "lab_kpxe_dw";
            this.lab_kpxe_dw.Size = new Size(0x3b, 12);
            this.lab_kpxe_dw.TabIndex = 0x11;
            this.lab_kpxe_dw.Text = "开票限额:";
            this.lab_thfpxx.AutoSize = true;
            this.lab_thfpxx.Font = new Font("宋体", 9f);
            this.lab_thfpxx.Location = new Point(0x7c, 0x4c);
            this.lab_thfpxx.Name = "lab_thfpxx";
            this.lab_thfpxx.Size = new Size(0x11, 12);
            this.lab_thfpxx.TabIndex = 0x1d;
            this.lab_thfpxx.Text = "无";
            this.label17.AutoSize = true;
            this.label17.Font = new Font("宋体", 9f);
            this.label17.Location = new Point(0x11, 0x4c);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x53, 12);
            this.label17.TabIndex = 0x1c;
            this.label17.Text = "退回发票信息:";
            this.lab_ickrl.AutoSize = true;
            this.lab_ickrl.Font = new Font("宋体", 9f);
            this.lab_ickrl.Location = new Point(0x7c, 0x36);
            this.lab_ickrl.Name = "lab_ickrl";
            this.lab_ickrl.Size = new Size(0, 12);
            this.lab_ickrl.TabIndex = 0x19;
            this.label21.AutoSize = true;
            this.label21.Font = new Font("宋体", 9f);
            this.label21.Location = new Point(0x11, 0x36);
            this.label21.Name = "label21";
            this.label21.Size = new Size(0x3b, 12);
            this.label21.TabIndex = 0x18;
            this.label21.Text = "IC卡容量:";
            this.lab_sqxx.AutoSize = true;
            this.lab_sqxx.Font = new Font("宋体", 9f);
            this.lab_sqxx.Location = new Point(0x7c, 0x20);
            this.lab_sqxx.Name = "lab_sqxx";
            this.lab_sqxx.Size = new Size(0x11, 12);
            this.lab_sqxx.TabIndex = 0x17;
            this.lab_sqxx.Text = "无";
            this.label23.AutoSize = true;
            this.label23.Font = new Font("宋体", 9f);
            this.label23.Location = new Point(0x11, 0x20);
            this.label23.Name = "label23";
            this.label23.Size = new Size(0x53, 12);
            this.label23.TabIndex = 0x16;
            this.label23.Text = "发行授权信息:";
            this.lab_kpjh.AutoSize = true;
            this.lab_kpjh.Font = new Font("宋体", 9f);
            this.lab_kpjh.Location = new Point(0x7c, 10);
            this.lab_kpjh.Name = "lab_kpjh";
            this.lab_kpjh.Size = new Size(0, 12);
            this.lab_kpjh.TabIndex = 0x15;
            this.label25.AutoSize = true;
            this.label25.Font = new Font("宋体", 9f);
            this.label25.Location = new Point(0x11, 10);
            this.label25.Name = "label25";
            this.label25.Size = new Size(0x3b, 12);
            this.label25.TabIndex = 20;
            this.label25.Text = "开票机号:";
            this.lab_grfpxx.AutoSize = true;
            this.lab_grfpxx.Font = new Font("宋体", 9f);
            this.lab_grfpxx.Location = new Point(0x7c, 0x62);
            this.lab_grfpxx.Name = "lab_grfpxx";
            this.lab_grfpxx.Size = new Size(0x11, 12);
            this.lab_grfpxx.TabIndex = 0x1f;
            this.lab_grfpxx.Text = "无";
            this.label27.AutoSize = true;
            this.label27.Font = new Font("宋体", 9f);
            this.label27.Location = new Point(0x11, 0x62);
            this.label27.Name = "label27";
            this.label27.Size = new Size(0x53, 12);
            this.label27.TabIndex = 30;
            this.label27.Text = "购入发票信息:";
            this.lab_bscgbz.AutoSize = true;
            this.lab_bscgbz.Font = new Font("宋体", 9f);
            this.lab_bscgbz.Location = new Point(0x7c, 120);
            this.lab_bscgbz.Name = "lab_bscgbz";
            this.lab_bscgbz.Size = new Size(0x11, 12);
            this.lab_bscgbz.TabIndex = 0x21;
            this.lab_bscgbz.Text = "无";
            this.label29.AutoSize = true;
            this.label29.Font = new Font("宋体", 9f);
            this.label29.Location = new Point(15, 120);
            this.label29.Name = "label29";
            this.label29.Size = new Size(0x53, 12);
            this.label29.TabIndex = 0x20;
            this.label29.Text = "报税成功标志:";
            this.lab_fplyc.AutoSize = true;
            this.lab_fplyc.Font = new Font("宋体", 9f);
            this.lab_fplyc.Location = new Point(0x7c, 0xa4);
            this.lab_fplyc.Name = "lab_fplyc";
            this.lab_fplyc.Size = new Size(0x11, 12);
            this.lab_fplyc.TabIndex = 0x25;
            this.lab_fplyc.Text = "无";
            this.label31.AutoSize = true;
            this.label31.Font = new Font("宋体", 9f);
            this.label31.Location = new Point(0x11, 0xa4);
            this.label31.Name = "label31";
            this.label31.Size = new Size(0x5f, 12);
            this.label31.TabIndex = 0x24;
            this.label31.Text = "发票领用存数据:";
            this.lab_bszl.AutoSize = true;
            this.lab_bszl.Font = new Font("宋体", 9f);
            this.lab_bszl.Location = new Point(0x7c, 0x8e);
            this.lab_bszl.Name = "lab_bszl";
            this.lab_bszl.Size = new Size(0x11, 12);
            this.lab_bszl.TabIndex = 0x23;
            this.lab_bszl.Text = "无";
            this.label33.AutoSize = true;
            this.label33.Font = new Font("宋体", 9f);
            this.label33.Location = new Point(0x11, 0x8e);
            this.label33.Name = "label33";
            this.label33.Size = new Size(0x3b, 12);
            this.label33.TabIndex = 0x22;
            this.label33.Text = "报税资料:";
            this.lab_scbsrq.AutoSize = true;
            this.lab_scbsrq.Font = new Font("宋体", 9f);
            this.lab_scbsrq.Location = new Point(0x79, 0x61);
            this.lab_scbsrq.Name = "lab_scbsrq";
            this.lab_scbsrq.Size = new Size(0, 12);
            this.lab_scbsrq.TabIndex = 0x30;
            this.label38.AutoSize = true;
            this.label38.Font = new Font("宋体", 9f);
            this.label38.Location = new Point(6, 0x61);
            this.label38.Name = "label38";
            this.label38.Size = new Size(0x53, 12);
            this.label38.TabIndex = 0x2f;
            this.label38.Text = "上次报税日期:";
            this.lab_hzfw.AutoSize = true;
            this.lab_hzfw.Font = new Font("宋体", 9f);
            this.lab_hzfw.Location = new Point(0x79, 0x4c);
            this.lab_hzfw.Name = "lab_hzfw";
            this.lab_hzfw.Size = new Size(0x11, 12);
            this.lab_hzfw.TabIndex = 0x2e;
            this.lab_hzfw.Text = "无";
            this.label40.AutoSize = true;
            this.label40.Font = new Font("宋体", 9f);
            this.label40.Location = new Point(8, 0x4c);
            this.label40.Name = "label40";
            this.label40.Size = new Size(0x53, 12);
            this.label40.TabIndex = 0x2d;
            this.label40.Text = "汉字防伪授权:";
            this.lab_ssrqts.AutoSize = true;
            this.lab_ssrqts.Font = new Font("宋体", 9f);
            this.lab_ssrqts.Location = new Point(0x79, 0x37);
            this.lab_ssrqts.Name = "lab_ssrqts";
            this.lab_ssrqts.Size = new Size(0, 12);
            this.lab_ssrqts.TabIndex = 0x2c;
            this.label42.AutoSize = true;
            this.label42.Font = new Font("宋体", 9f);
            this.label42.Location = new Point(10, 0x37);
            this.label42.Name = "label42";
            this.label42.Size = new Size(0x3b, 12);
            this.label42.TabIndex = 0x2b;
            this.label42.Text = "锁死日期:";
            this.lab_dcqdbb.AutoSize = true;
            this.lab_dcqdbb.Font = new Font("宋体", 9f);
            this.lab_dcqdbb.Location = new Point(0x79, 0x22);
            this.lab_dcqdbb.Name = "lab_dcqdbb";
            this.lab_dcqdbb.Size = new Size(0, 12);
            this.lab_dcqdbb.TabIndex = 0x2a;
            this.label44.AutoSize = true;
            this.label44.Font = new Font("宋体", 9f);
            this.label44.Location = new Point(10, 0x22);
            this.label44.Name = "label44";
            this.label44.Size = new Size(0x53, 12);
            this.label44.TabIndex = 0x29;
            this.label44.Text = "底层程序版本:";
            this.lab_qdbb.AutoSize = true;
            this.lab_qdbb.Font = new Font("宋体", 9f);
            this.lab_qdbb.Location = new Point(0x79, 13);
            this.lab_qdbb.Name = "lab_qdbb";
            this.lab_qdbb.Size = new Size(0, 12);
            this.lab_qdbb.TabIndex = 40;
            this.label46.AutoSize = true;
            this.label46.Font = new Font("宋体", 9f);
            this.label46.Location = new Point(10, 13);
            this.label46.Name = "label46";
            this.label46.Size = new Size(0x3b, 12);
            this.label46.TabIndex = 0x27;
            this.label46.Text = "驱动版本:";
            this.tab.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.tab.Controls.Add(this.tabPage1);
            this.tab.Controls.Add(this.tab_icCard);
            this.tab.Controls.Add(this.tab_bsp);
            this.tab.Controls.Add(this.tabPage3);
            this.tab.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.tab.ItemSize = new Size(100, 0x19);
            this.tab.Location = new Point(3, 0x1b);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new Size(0x197, 0xdb);
            this.tab.TabIndex = 0x31;
            this.tabPage1.BackColor = Color.White;
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.lab_ssq);
            this.tabPage1.Controls.Add(this.lab_csq);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.lab_kyfp);
            this.tabPage1.Controls.Add(this.lab_bjkp);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.lab_fjkpsm);
            this.tabPage1.Controls.Add(this.lab_csqsrq);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.lab_jskdqrq);
            this.tabPage1.Controls.Add(this.lab_kpxe_dw);
            this.tabPage1.Controls.Add(this.lab_kpxe);
            this.tabPage1.Location = new Point(4, 0x1d);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(0x18f, 0xba);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "金税卡信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tab_icCard.BackColor = Color.White;
            this.tab_icCard.Controls.Add(this.label29);
            this.tab_icCard.Controls.Add(this.label25);
            this.tab_icCard.Controls.Add(this.lab_kpjh);
            this.tab_icCard.Controls.Add(this.label23);
            this.tab_icCard.Controls.Add(this.lab_sqxx);
            this.tab_icCard.Controls.Add(this.label21);
            this.tab_icCard.Controls.Add(this.lab_ickrl);
            this.tab_icCard.Controls.Add(this.label17);
            this.tab_icCard.Controls.Add(this.lab_thfpxx);
            this.tab_icCard.Controls.Add(this.label27);
            this.tab_icCard.Controls.Add(this.lab_grfpxx);
            this.tab_icCard.Controls.Add(this.lab_bscgbz);
            this.tab_icCard.Controls.Add(this.lab_fplyc);
            this.tab_icCard.Controls.Add(this.label33);
            this.tab_icCard.Controls.Add(this.label31);
            this.tab_icCard.Controls.Add(this.lab_bszl);
            this.tab_icCard.Location = new Point(4, 0x1d);
            this.tab_icCard.Name = "tab_icCard";
            this.tab_icCard.Padding = new Padding(3);
            this.tab_icCard.Size = new Size(0x18f, 0xba);
            this.tab_icCard.TabIndex = 1;
            this.tab_icCard.Text = "IC卡信息";
            this.tab_icCard.UseVisualStyleBackColor = true;
            this.tab_bsp.Controls.Add(this.label24);
            this.tab_bsp.Controls.Add(this.tb_TBRepDone);
            this.tab_bsp.Controls.Add(this.label18);
            this.tab_bsp.Controls.Add(this.tb_TBRetInv);
            this.tab_bsp.Controls.Add(this.label15);
            this.tab_bsp.Controls.Add(this.tb_bszl);
            this.tab_bsp.Controls.Add(this.label13);
            this.tab_bsp.Controls.Add(this.tb_TBRegFlag);
            this.tab_bsp.Controls.Add(this.label11);
            this.tab_bsp.Controls.Add(this.tb_BTCardNo);
            this.tab_bsp.Controls.Add(this.label8);
            this.tab_bsp.Controls.Add(this.tb_TBCapacity);
            this.tab_bsp.Controls.Add(this.label3);
            this.tab_bsp.Controls.Add(this.tb_TBBuyInv);
            this.tab_bsp.Controls.Add(this.label1);
            this.tab_bsp.Controls.Add(this.tb_TBAuthInfo);
            this.tab_bsp.Location = new Point(4, 0x1d);
            this.tab_bsp.Name = "tab_bsp";
            this.tab_bsp.Size = new Size(0x18f, 0xba);
            this.tab_bsp.TabIndex = 3;
            this.tab_bsp.Text = "报税盘信息 ";
            this.tab_bsp.UseVisualStyleBackColor = true;
            this.label24.AutoSize = true;
            this.label24.Font = new Font("宋体", 9f);
            this.label24.Location = new Point(14, 0x6f);
            this.label24.Name = "label24";
            this.label24.Size = new Size(0x53, 12);
            this.label24.TabIndex = 40;
            this.label24.Text = "报税成功标志:";
            this.tb_TBRepDone.AutoSize = true;
            this.tb_TBRepDone.Font = new Font("宋体", 9f);
            this.tb_TBRepDone.Location = new Point(0x69, 0x6f);
            this.tb_TBRepDone.Name = "tb_TBRepDone";
            this.tb_TBRepDone.Size = new Size(0x11, 12);
            this.tb_TBRepDone.TabIndex = 0x29;
            this.tb_TBRepDone.Text = "无";
            this.label18.AutoSize = true;
            this.label18.Font = new Font("宋体", 9f);
            this.label18.Location = new Point(14, 0x45);
            this.label18.Name = "label18";
            this.label18.Size = new Size(0x53, 12);
            this.label18.TabIndex = 0x24;
            this.label18.Text = "退回发票信息:";
            this.tb_TBRetInv.AutoSize = true;
            this.tb_TBRetInv.Font = new Font("宋体", 9f);
            this.tb_TBRetInv.Location = new Point(0x67, 0x45);
            this.tb_TBRetInv.Name = "tb_TBRetInv";
            this.tb_TBRetInv.Size = new Size(0x11, 12);
            this.tb_TBRetInv.TabIndex = 0x25;
            this.tb_TBRetInv.Text = "无";
            this.label15.AutoSize = true;
            this.label15.Font = new Font("宋体", 9f);
            this.label15.Location = new Point(14, 0x84);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x3b, 12);
            this.label15.TabIndex = 0x22;
            this.label15.Text = "报税资料:";
            this.tb_bszl.AutoSize = true;
            this.tb_bszl.Font = new Font("宋体", 9f);
            this.tb_bszl.Location = new Point(0x67, 0x84);
            this.tb_bszl.Name = "tb_bszl";
            this.tb_bszl.Size = new Size(0x11, 12);
            this.tb_bszl.TabIndex = 0x23;
            this.tb_bszl.Text = "无";
            this.label13.AutoSize = true;
            this.label13.Font = new Font("宋体", 9f);
            this.label13.Location = new Point(14, 0x99);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x3b, 12);
            this.label13.TabIndex = 0x20;
            this.label13.Text = "注册状态:";
            this.tb_TBRegFlag.AutoSize = true;
            this.tb_TBRegFlag.Font = new Font("宋体", 9f);
            this.tb_TBRegFlag.Location = new Point(0x67, 0x99);
            this.tb_TBRegFlag.Name = "tb_TBRegFlag";
            this.tb_TBRegFlag.Size = new Size(0x11, 12);
            this.tb_TBRegFlag.TabIndex = 0x21;
            this.tb_TBRegFlag.Text = "无";
            this.label11.AutoSize = true;
            this.label11.Font = new Font("宋体", 9f);
            this.label11.Location = new Point(14, 6);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x3b, 12);
            this.label11.TabIndex = 30;
            this.label11.Text = "开票机号:";
            this.tb_BTCardNo.AutoSize = true;
            this.tb_BTCardNo.Font = new Font("宋体", 9f);
            this.tb_BTCardNo.Location = new Point(0x67, 6);
            this.tb_BTCardNo.Name = "tb_BTCardNo";
            this.tb_BTCardNo.Size = new Size(0x11, 12);
            this.tb_BTCardNo.TabIndex = 0x1f;
            this.tb_BTCardNo.Text = "无";
            this.label8.AutoSize = true;
            this.label8.Font = new Font("宋体", 9f);
            this.label8.Location = new Point(14, 0x30);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x47, 12);
            this.label8.TabIndex = 0x1c;
            this.label8.Text = "报税盘容量:";
            this.tb_TBCapacity.AutoSize = true;
            this.tb_TBCapacity.Font = new Font("宋体", 9f);
            this.tb_TBCapacity.Location = new Point(0x67, 0x30);
            this.tb_TBCapacity.Name = "tb_TBCapacity";
            this.tb_TBCapacity.Size = new Size(0x11, 12);
            this.tb_TBCapacity.TabIndex = 0x1d;
            this.tb_TBCapacity.Text = "无";
            this.label3.AutoSize = true;
            this.label3.Font = new Font("宋体", 9f);
            this.label3.Location = new Point(14, 90);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x53, 12);
            this.label3.TabIndex = 0x1a;
            this.label3.Text = "购入发票信息:";
            this.tb_TBBuyInv.AutoSize = true;
            this.tb_TBBuyInv.Font = new Font("宋体", 9f);
            this.tb_TBBuyInv.Location = new Point(0x67, 90);
            this.tb_TBBuyInv.Name = "tb_TBBuyInv";
            this.tb_TBBuyInv.Size = new Size(0x11, 12);
            this.tb_TBBuyInv.TabIndex = 0x1b;
            this.tb_TBBuyInv.Text = "无";
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 9f);
            this.label1.Location = new Point(14, 0x1b);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x53, 12);
            this.label1.TabIndex = 0x18;
            this.label1.Text = "发行授权信息:";
            this.tb_TBAuthInfo.AutoSize = true;
            this.tb_TBAuthInfo.Font = new Font("宋体", 9f);
            this.tb_TBAuthInfo.Location = new Point(0x67, 0x1b);
            this.tb_TBAuthInfo.Name = "tb_TBAuthInfo";
            this.tb_TBAuthInfo.Size = new Size(0x11, 12);
            this.tb_TBAuthInfo.TabIndex = 0x19;
            this.tb_TBAuthInfo.Text = "无";
            this.tabPage3.BackColor = Color.White;
            this.tabPage3.Controls.Add(this.lab_scbsrq);
            this.tabPage3.Controls.Add(this.label46);
            this.tabPage3.Controls.Add(this.label38);
            this.tabPage3.Controls.Add(this.lab_qdbb);
            this.tabPage3.Controls.Add(this.lab_hzfw);
            this.tabPage3.Controls.Add(this.label44);
            this.tabPage3.Controls.Add(this.label40);
            this.tabPage3.Controls.Add(this.lab_dcqdbb);
            this.tabPage3.Controls.Add(this.lab_ssrqts);
            this.tabPage3.Controls.Add(this.label42);
            this.tabPage3.Location = new Point(4, 0x1d);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new Padding(3);
            this.tabPage3.Size = new Size(0x18f, 0xba);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "其他信息";
            this.tabPage3.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.Controls.Add(this.tab);
            base.Name = "JScardDock";
            base.Size = new Size(0x19d, 0xf8);
            base.Title = "金税卡状态";
            this.tab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tab_icCard.ResumeLayout(false);
            this.tab_icCard.PerformLayout();
            this.tab_bsp.ResumeLayout(false);
            this.tab_bsp.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            base.ResumeLayout(false);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.renew();
        }

        public void OnSetAttribute(List<object> e)
        {
            this.lab_bjkp.Text = e[0].ToString();
            this.lab_fjkpsm.Text = e[1].ToString();
            this.lab_csqsrq.Text = Convert.ToDateTime(e[2]).ToString("yyyy年MM月dd日");
            this.lab_jskdqrq.Text = Convert.ToDateTime(e[3]).ToString("yyyy年MM月dd日");
            decimal num = Convert.ToDecimal(e[4]) / 10000M;
            this.lab_kpxe.Text = string.Format("￥{0:#############.00}万元", num);
            if (Convert.ToInt32(e[5]) == 0)
            {
                this.lab_kyfp.Text = "有可用发票";
                this.lab_kyfp.ForeColor = Color.Blue;
            }
            else
            {
                this.lab_kyfp.Text = "无可用发票";
                this.lab_kyfp.ForeColor = Color.Red;
            }
            if (Convert.ToInt32(e[6]) == 0)
            {
                this.lab_csq.Text = "未到抄税期";
                this.lab_csq.ForeColor = Color.Blue;
            }
            else
            {
                this.lab_csq.Text = "已到抄税期";
                this.lab_csq.ForeColor = Color.Red;
            }
            if (Convert.ToInt32(e[7]) == 0)
            {
                this.lab_ssq.Text = "未到锁死期";
                this.lab_ssq.ForeColor = Color.Blue;
            }
            else
            {
                this.lab_ssq.Text = "已到锁死期";
                this.lab_ssq.ForeColor = Color.Blue;
            }
            this.lab_kpjh.Text = e[8].ToString();
            this.lab_sqxx.Text = (Convert.ToInt32(e[9]) == 0) ? "无" : "有";
            this.lab_ickrl.Text = e[10].ToString() + "K";
            this.lab_thfpxx.Text = (Convert.ToInt32(e[11]) == 0) ? "无" : "有";
            this.lab_grfpxx.Text = (Convert.ToInt32(e[12]) == 0) ? "无" : "有";
            this.lab_bscgbz.Text = (Convert.ToInt32(e[13]) == 0) ? "无" : "有";
            this.lab_bszl.Text = (Convert.ToInt32(e[14]) == 0) ? "无" : "有";
            this.lab_fplyc.Text = (Convert.ToInt32(e[15]) == 0) ? "无" : "有";
            this.lab_qdbb.Text = e[0x10].ToString();
            this.lab_dcqdbb.Text = e[0x11].ToString();
            this.lab_ssrqts.Text = e[0x12].ToString() + "天";
            this.lab_hzfw.Text = (Convert.ToInt32(e[0x13]) == 0) ? "无" : "有";
            this.lab_scbsrq.Text = Convert.ToDateTime(e[20]).ToString("yyyy年MM月dd日");
            this.tb_BTCardNo.Text = e[0x15].ToString();
            this.tb_TBAuthInfo.Text = (Convert.ToInt32(e[0x16]) == 0) ? "无" : "有";
            this.tb_TBBuyInv.Text = (Convert.ToInt32(e[0x17]) == 0) ? "无" : "有";
            this.tb_TBCapacity.Text = e[0x18].ToString() + "M";
            this.tb_TBRegFlag.Text = (Convert.ToInt32(e[0x19]) == 0) ? "未注册" : "已注册";
            this.tb_TBRepDone.Text = (Convert.ToInt32(e[0x1a]) == 0) ? "无" : "有";
            this.tb_TBRetInv.Text = (Convert.ToInt32(e[0x1c]) == 0) ? "无" : "有";
            if (Convert.ToBoolean(e[0x1d]))
            {
                this.tab.TabPages.Remove(this.tab_bsp);
            }
            else
            {
                this.tab.TabPages.Remove(this.tab_icCard);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (this.tab != null)
            {
                this.tab.Size = new Size(base.Width - 4, (base.Height - this.tab.Location.Y) - 2);
            }
        }

        private void renew()
        {
            new Thread(new ThreadStart(this.init)).Start();
        }

        public delegate void SetAttribute(List<object> e);
    }
}

