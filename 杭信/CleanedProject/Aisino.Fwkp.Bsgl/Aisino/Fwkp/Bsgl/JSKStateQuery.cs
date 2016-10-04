namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Windows.Forms;

    public class JSKStateQuery : DockForm
    {
        private AisinoBTN btnOK;
        private IContainer components;
        private AisinoGRP gbBSP;
        private AisinoGRP gbICCard;
        private AisinoGRP gbJSK;
        private AisinoGRP gbOther;
        private AisinoLBL label22;
        private AisinoLBL lblBackFP;
        private AisinoLBL lblBottomNo;
        private AisinoLBL lblBSData;
        private AisinoLBL lblBSFlag;
        private AisinoLBL lblBSPBackFP;
        private AisinoLBL lblBSPBsData;
        private AisinoLBL lblBSPBsFlag;
        private AisinoLBL lblBSPBuyFP;
        private AisinoLBL lblBSPCapacity;
        private AisinoLBL lblBSPKpjNo;
        private AisinoLBL lblBuyFP;
        private AisinoLBL lblDriverNo;
        private AisinoLBL lblDue;
        private AisinoLBL lblFPAvailable;
        private AisinoLBL lblFPLimitSum;
        private AisinoLBL lblFPStock;
        private AisinoLBL lblHZFW;
        private AisinoLBL lblICCapacity;
        private AisinoLBL lblKPJNo;
        private AisinoLBL lblLastBSDate;
        private AisinoLBL lblLock;
        private AisinoLBL lblLockday;
        private AisinoLBL lblMainKPJ;
        private AisinoLBL lblNowDate;
        private AisinoLBL lblSQ;
        private AisinoLBL lblStartDate;
        private AisinoLBL lblSubKPJNum;
        private AisinoLBL lblXTSQ;
        private ILog loger = LogUtil.GetLogger<JSKStateQuery>();
        private TaxStateWrapper taxStateWrapper;
        private const int WIDTH = 200;
        private XmlComponentLoader xmlComponentLoader1;

        public JSKStateQuery()
        {
            this.Initialize();
            this.taxStateWrapper = new TaxStateWrapper(base.TaxCardInstance.GetStateInfo(false), base.TaxCardInstance.get_Machine());
            if (!this.taxStateWrapper.IsTBEnable || (base.TaxCardInstance.get_StateInfo().TBRegFlag == 0))
            {
                this.gbBSP.Visible = false;
                base.Width -= 200;
                this.gbOther.Width -= 200;
                this.btnOK.Left = this.btnOK.Location.X - 200;
            }
            base.Load += new EventHandler(this.JSKStateQuery_Load);
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
            this.lblBSPCapacity.Text = this.taxStateWrapper.BSPRL.ToString() + " M";
            this.lblBSPBsData.Text = this.taxStateWrapper.BSPBSZL;
            this.lblBSPBsFlag.Text = this.taxStateWrapper.BSPBSCGBZ;
            this.lblBSPBuyFP.Text = this.taxStateWrapper.BSPGRFP;
            this.lblBSPBackFP.Text = this.taxStateWrapper.BSPTHFP;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.gbJSK = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("gbJSK");
            this.lblFPLimitSum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblFPLimitSum");
            this.lblLock = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblLock");
            this.lblDue = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblDue");
            this.lblFPAvailable = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblFPAvailable");
            this.lblNowDate = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblNowDate");
            this.lblStartDate = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblStartDate");
            this.lblSubKPJNum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblSubKPJNum");
            this.lblMainKPJ = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblMainKPJ");
            this.gbICCard = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("gbICCard");
            this.lblFPStock = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblFPStock");
            this.lblBSData = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBSData");
            this.lblBSFlag = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBSFlag");
            this.lblBuyFP = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBuyFP");
            this.lblBackFP = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBackFP");
            this.lblICCapacity = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblICCapacity");
            this.lblSQ = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblSQ");
            this.lblKPJNo = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblKPJNo");
            this.gbOther = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("gbOther");
            this.lblLastBSDate = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblLastBSDate");
            this.lblHZFW = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblHZFW");
            this.label22 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label22");
            this.lblXTSQ = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblXTSQ");
            this.lblBottomNo = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBottomNo");
            this.lblLockday = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblLockday");
            this.lblDriverNo = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblDriverNo");
            this.gbBSP = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("gbBSP");
            this.lblBSPBackFP = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBSPBackFP");
            this.lblBSPBuyFP = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBSPBuyFP");
            this.lblBSPBsFlag = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBSPBsFlag");
            this.lblBSPBsData = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBSPBsData");
            this.lblBSPCapacity = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBSPCapacity");
            this.lblBSPKpjNo = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBSPKpjNo");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x2b1, 0x1bd);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Bsgl.JSKStateQuery\Aisino.Fwkp.Bsgl.JSKStateQuery.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2b1, 0x1bd);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "JSKStateQuery";
            this.Text = "金税设备状态查询";
            base.ResumeLayout(false);
        }

        private void InitICCardInfo()
        {
            this.lblKPJNo.Text = this.taxStateWrapper.ICKPJH.ToString();
            this.lblSQ.Text = this.taxStateWrapper.ICFXSQXX;
            this.lblICCapacity.Text = this.taxStateWrapper.ICRL.ToString() + " K";
            this.lblBackFP.Text = this.taxStateWrapper.ICTHFP;
            this.lblBuyFP.Text = this.taxStateWrapper.ICGRFP;
            this.lblBSFlag.Text = this.taxStateWrapper.ICBSCGBZ;
            this.lblBSData.Text = this.taxStateWrapper.ICBSZL;
            this.lblFPStock.Text = this.taxStateWrapper.ICFPLYC;
        }

        private void InitOtherInfo()
        {
            this.lblDriverNo.Text = this.taxStateWrapper.DriverNo;
            this.lblBottomNo.Text = this.taxStateWrapper.BottomNo;
            this.lblLastBSDate.Text = base.TaxCardInstance.get_LastRepDate().ToString("yyyy年MM月dd日HH时mm分");
            this.lblLockday.Text = this.taxStateWrapper.LockedDays.ToString() + " 天";
            this.lblHZFW.Text = this.taxStateWrapper.HZFWAuth;
            string xTAuth = this.taxStateWrapper.XTAuth;
            if (string.IsNullOrEmpty(xTAuth))
            {
                this.label22.Visible = false;
                this.lblXTSQ.Visible = false;
            }
            else
            {
                if (CheckCodeRoles.IsSNY(xTAuth))
                {
                    this.label22.Text = "石脑油/燃料油";
                    this.lblXTSQ.Text = "已授权";
                }
                if (CheckCodeRoles.IsXT(xTAuth))
                {
                    this.label22.Text = "稀土授权";
                    string str3 = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Config\Common\xtqybm.xml");
                    string str4 = CodeRoles.ChangeXTCodeToName(xTAuth);
                    if (!string.IsNullOrEmpty(str4))
                    {
                        string str5 = "XTQY/ZYJKSQ/" + str4;
                        this.lblXTSQ.Text = XmlManager.GetValue(str3, str5);
                    }
                }
            }
        }

        private void InitTaxCardInfo()
        {
            this.lblMainKPJ.Text = this.taxStateWrapper.KPJLX;
            this.lblSubKPJNum.Text = this.taxStateWrapper.FKPJSM.ToString();
            this.lblStartDate.Text = base.TaxCardInstance.get_RepDate().ToString("yyyy年MM月dd日");
            this.lblNowDate.Text = base.TaxCardInstance.GetCardClock().ToString("yyyy年MM月dd日");
            this.lblFPAvailable.Text = this.taxStateWrapper.IsFPYW;
            this.lblDue.Text = this.taxStateWrapper.IsCSQ;
            this.lblLock.Text = this.taxStateWrapper.IsSSQ;
            double invLimit = base.TaxCardInstance.GetInvLimit(0);
            NumberFormatInfo numberFormat = new CultureInfo("zh-CN", false).NumberFormat;
            numberFormat.CurrencyDecimalDigits = 2;
            this.lblFPLimitSum.Text = ((invLimit / 10000.0)).ToString("C", numberFormat) + " 万元";
        }

        private void JSKStateQuery_Load(object sender, EventArgs e)
        {
            this.InitTaxCardInfo();
            this.InitICCardInfo();
            this.InitBSPInfo();
            this.InitOtherInfo();
        }
    }
}

