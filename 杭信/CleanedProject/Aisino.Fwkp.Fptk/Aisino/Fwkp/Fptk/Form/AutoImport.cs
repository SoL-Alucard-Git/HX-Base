namespace Aisino.Fwkp.Fptk.Form
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fptk;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;
    using log4net;

    public class AutoImport : BaseForm
    {
        private BackgroundWorker backgroundWorker1;
        private string bfPath;
        public static Fpxx blueFpxx = null;
        private AisinoBTN btnExit;
        private AisinoBTN btnStart;
        internal static XmlElement bus;
        internal static string bz = "";
        private IContainer components;
        private List<string> djFiles = new List<string>();
        private FPDJHelper djHelper;
        internal static XmlDocument doc;
        private string drPath;
        private InvoiceForm_DZ dzinvoiceForm;
        internal static bool ErrorExist;
        internal static string ewm = "";
        internal static string fp_dm = "";
        internal static string fp_hm = "";
        internal static string fp_mw = "";
        private FPLX fplx;
        private IFpManager fpm;
        private string hxFileNamexml = "";
        private string hxPath;
        private HYInvoiceForm hyInvoiceForm;
        private bool interrupted;
        private int interval;
        private InvoiceForm_ZZS invoiceForm;
        private JDCInvoiceForm_new jdcInvoiceForm;
        internal static string jqbh = "";
        internal static string jym = "";
        internal static string KpResult = "";
        internal static string kprq = "";
        private AisinoLBL lblDjh;
        private AisinoLBL lblFile;
        private AisinoLBL lblFpdm;
        private AisinoLBL lblFphm;
        private AisinoLBL lblFpzl;
        private AisinoLBL lblNotFind;
        private AisinoLBL lblStartTip;
        private ILog log = LogUtil.GetLogger<AutoImport>();
        private string mDjh;
        private string mFileName;
        private string mFpdm;
        private string mFphm;
        internal ZYFP_LX mZyfplx;
        private AisinoPNL panel1;
        private AisinoPNL panel2;
        internal static bool PathIsNull = false;
        private AisinoPRG pbImport;
        internal static string returncode = "0000";
        internal static string returnmsg = "";
        private string sqslv;
        private bool stopped = true;
        public static bool success = true;
        private TaxCard taxCard = TaxCardFactory.CreateTaxCard();
        private System.Windows.Forms.Timer timer1;
        public static StreamWriter writer;
        private XmlComponentLoader xmlComponentLoader1;
        private ZYFP_LX zyfplx;

        public AutoImport(FPLX fplx, string sqslv, ZYFP_LX zyfplx)
        {
            this.zyfplx = zyfplx;
            this.fplx = fplx;
            this.Initialize();
            this.sqslv = sqslv;
            this.lblNotFind.Visible = false;
            this.GetPaths(fplx);
            if ((string.IsNullOrEmpty(this.drPath) || string.IsNullOrEmpty(this.hxPath)) || (string.IsNullOrEmpty(this.bfPath) || (this.interval == 0)))
            {
                MessageManager.ShowMsgBox("INP-242169");
                this.btnStart.Enabled = false;
                PathIsNull = true;
            }
            else if ((!Directory.Exists(this.drPath) || !Directory.Exists(this.hxPath)) || !Directory.Exists(this.bfPath))
            {
                MessageManager.ShowMsgBox("INP-242201");
                this.btnStart.Enabled = false;
                PathIsNull = true;
            }
            else
            {
                this.timer1.Interval = this.interval * 0x3e8;
                this.fpm = new FpManager();
                this.djHelper = new FPDJHelper();
                PathIsNull = false;
            }
        }

        private void AutoImport_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.stopped)
            {
                this.backgroundWorker1.CancelAsync();
                e.Cancel = true;
            }
            else
            {
                this.timer1.Stop();
                this.CloseDialog();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (string str in this.djFiles)
            {
                this.mFileName = str.Substring(str.LastIndexOf('\\') + 1);
                string bfFile = Path.Combine(this.bfPath, this.mFileName);
                int length = this.mFileName.Length;
                string str3 = this.mFileName.Substring(0, length - 4) + "_开票结果.TXT";
                this.hxFileNamexml = this.mFileName.Substring(0, length - 4) + "_开票结果.xml";
                List<string> list = this.djHelper.QueryYkdj(this.mFileName);
                if ((int)(int)this.fplx == 0x33)
                {
                    doc = new XmlDocument();
                    XmlDeclaration newChild = doc.CreateXmlDeclaration("1.0", "GBK", null);
                    doc.AppendChild(newChild);
                    bus = doc.CreateElement("business");
                    doc.AppendChild(bus);
                }
                else
                {
                    writer = new StreamWriter(new FileStream(Path.Combine(this.hxPath, str3), FileMode.Append, FileAccess.Write, FileShare.ReadWrite), ToolUtil.GetEncoding());
                }
                List<Djfp> list2 = this.ParseDjFile(str);
                success = true;
                for (int i = 0; (i < list2.Count) && success; i++)
                {
                    jqbh = "";
                    fp_dm = "";
                    fp_hm = "";
                    kprq = "";
                    fp_mw = "";
                    jym = "";
                    ewm = "";
                    bz = "";
                    returncode = "0000";
                    returnmsg = "";
                    Djfp dj = list2[i];
                    this.mDjh = dj.Djh;
                    this.backgroundWorker1.ReportProgress(0, null);
                    bool flag = false;
                    if (list.Contains(this.mDjh))
                    {
                        flag = false;
                        object[] args = new object[] { base.TaxCardInstance.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), this.mDjh, 0, "该单据已成功填开过发票，不可重复开具！" };
                        KpResult = string.Format("[{0}] 单据号:{1},开具结果:{2},开具失败原因:{3}", args);
                        returncode = "0001";
                        returnmsg = "该单据已成功填开过发票，不可重复开具！";
                        this.backgroundWorker1.ReportProgress(15, null);
                    }
                    else if (((int)(int)this.fplx == 0x33) && dj.Fpxx.isRed)
                    {
                        string str4 = this.CheckDZRed(dj);
                        if (str4 != "")
                        {
                            flag = false;
                            object[] objArray2 = new object[] { base.TaxCardInstance.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), this.mDjh, 0, str4 };
                            KpResult = string.Format("[{0}] 单据号:{1},开具结果:{2},开具失败原因:{3}", objArray2);
                            returncode = "0001";
                            returnmsg = str4;
                            this.backgroundWorker1.ReportProgress(15, null);
                        }
                        else
                        {
                            this.TKfpxx(dj);
                            if (this.SuccessKp())
                            {
                                list.Add(this.mDjh);
                            }
                            flag = true;
                        }
                    }
                    else
                    {
                        this.TKfpxx(dj);
                        if (this.SuccessKp())
                        {
                            list.Add(this.mDjh);
                        }
                        flag = true;
                    }
                    if ((int)this.fplx == 0x33)
                    {
                        XmlNode node = doc.CreateElement("RESPONSE_COMMON_FPKJ");
                        bus.AppendChild(node);
                        XmlElement element = doc.CreateElement("FPQQLSH");
                        if ((this.mDjh != null) && (this.mDjh != ""))
                        {
                            element.InnerText = this.mDjh;
                        }
                        node.AppendChild(element);
                        XmlElement element2 = doc.CreateElement("JQBH");
                        if ((jqbh != null) && (jqbh != ""))
                        {
                            element2.InnerText = jqbh;
                        }
                        node.AppendChild(element2);
                        XmlElement element3 = doc.CreateElement("FP_DM");
                        if ((fp_dm != null) && (fp_dm != ""))
                        {
                            element3.InnerText = fp_dm;
                        }
                        node.AppendChild(element3);
                        XmlElement element4 = doc.CreateElement("FP_HM");
                        if ((fp_hm != null) && (fp_hm != ""))
                        {
                            element4.InnerText = fp_hm;
                        }
                        node.AppendChild(element4);
                        XmlElement element5 = doc.CreateElement("KPRQ");
                        if ((kprq != "") && (kprq != null))
                        {
                            DateTime time2 = Convert.ToDateTime(kprq);
                            kprq = string.Format("{0:yyyyMMddHHmmss}", time2);
                            element5.InnerText = kprq;
                        }
                        node.AppendChild(element5);
                        XmlElement element6 = doc.CreateElement("FP_MW");
                        if ((fp_mw != null) && (fp_mw != ""))
                        {
                            element6.InnerText = fp_mw;
                        }
                        node.AppendChild(element6);
                        XmlElement element7 = doc.CreateElement("JYM");
                        if ((jym != null) && (jym != ""))
                        {
                            element7.InnerText = jym;
                        }
                        node.AppendChild(element7);
                        XmlElement element8 = doc.CreateElement("EWM");
                        if ((ewm != null) && (ewm != ""))
                        {
                            element8.InnerText = ewm;
                        }
                        node.AppendChild(element8);
                        XmlElement element9 = doc.CreateElement("BZ");
                        if ((bz != null) && (bz != ""))
                        {
                            element9.InnerText = bz;
                        }
                        node.AppendChild(element9);
                        XmlElement element10 = doc.CreateElement("RETURNCODE");
                        element10.InnerText = returncode;
                        node.AppendChild(element10);
                        XmlElement element11 = doc.CreateElement("RETURNMSG");
                        element11.InnerText = KpResult;
                        node.AppendChild(element11);
                    }
                    else
                    {
                        writer.WriteLine(KpResult);
                    }
                    Thread.Sleep(500);
                    if (ErrorExist)
                    {
                        this.CloseFile(str, bfFile);
                        break;
                    }
                    if (this.backgroundWorker1.CancellationPending)
                    {
                        this.CloseFile(str, bfFile);
                        this.interrupted = true;
                        e.Cancel = true;
                        break;
                    }
                    if (flag && (i != (list2.Count - 1)))
                    {
                        Thread.Sleep(0x2710);
                    }
                    Thread.Sleep(500);
                }
                if ((int)this.fplx == 0x33)
                {
                    doc.Save(this.hxPath + @"\" + this.hxFileNamexml);
                }
                else
                {
                    writer.Close();
                    writer.Dispose();
                }
                File.Copy(str, bfFile, true);
                File.Delete(str);
                Thread.Sleep(200);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progressPercentage = e.ProgressPercentage;
            if (progressPercentage == 0)
            {
                this.SetProgressTip();
                this.ResetProgressValue();
            }
            else if (progressPercentage < 5)
            {
                this.SetProgress(progressPercentage);
                Djfp userState = e.UserState as Djfp;
                if (userState != null)
                {
                    if (this.invoiceForm != null)
                    {
                        this.invoiceForm.ShowImprotFp(userState);
                    }
                    else if (this.dzinvoiceForm != null)
                    {
                        this.dzinvoiceForm.ShowImprotFp(userState);
                    }
                    else if (this.hyInvoiceForm != null)
                    {
                        this.hyInvoiceForm.ShowImprotFp(userState);
                    }
                    else if (this.jdcInvoiceForm != null)
                    {
                        this.jdcInvoiceForm.ShowImprotFp(userState);
                    }
                }
            }
            else
            {
                this.SetProgress(progressPercentage - 5);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.interrupted)
            {
                this.timer1.Enabled = false;
                MessageBoxHelper.Show("退出批量开具。\r\n 当前文件为：" + this.mFileName, "确认对话框", MessageBoxButtons.OK);
                if (((int)this.fplx != 0x33) && (writer != null))
                {
                    writer.Close();
                    writer.Dispose();
                }
                if (((int)this.fplx == 0x33) && (doc != null))
                {
                    doc.Save(this.hxPath + @"\" + this.hxFileNamexml);
                }
                this.CloseDialog();
            }
            else
            {
                this.ShowResultTip();
                this.timer1.Enabled = true;
            }
            this.stopped = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (!this.stopped)
            {
                this.backgroundWorker1.CancelAsync();
            }
            else
            {
                this.timer1.Dispose();
                this.backgroundWorker1.Dispose();
                base.DialogResult = DialogResult.Cancel;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.btnStart.Enabled = false;
            this.btnExit.Enabled = true;
            this.ExcuteFpImp();
        }

        private string CanEmptyRedFp(string dm, string hm, string djje)
        {
            string str = "";
            if ((((int)this.fplx != 2) && ((int)this.fplx != 0x33)) || this.taxCard.QYLX.ISTDQY)
            {
                return str;
            }
            string xml = "";
            int num = int.Parse(hm);
            FpManager manager = new FpManager();
            string xfsh = manager.GetXfsh();
            string str4 = new StringBuilder().Append("<?xml version=\"1.0\" encoding=\"GBK\"?>").Append("<FPXT><INPUT>").Append("<NSRSBH>").Append(xfsh).Append("</NSRSBH>").Append("<KPJH>").Append(this.taxCard.Machine).Append("</KPJH>").Append("<SBBH>").Append(manager.GetMachineNum()).Append("</SBBH>").Append("<LZFPDM>").Append(dm).Append("</LZFPDM>").Append("<LZFPHM>").Append(string.Format("{0:00000000}", num)).Append("</LZFPHM>").Append("<FPZL>").Append(((int)this.fplx == 2) ? "c" : "p").Append("</FPZL>").Append("</INPUT></FPXT>").ToString();
            if (HttpsSender.SendMsg("0007", str4, out xml) == 0)
            {
                XmlDocument document1 = new XmlDocument();
                document1.LoadXml(xml);
                XmlNode node1 = document1.SelectSingleNode("/FPXT/OUTPUT");
                string innerText = node1.SelectSingleNode("HZJE").InnerText;
                if (node1.SelectSingleNode("CODE").InnerText.Trim().Equals("0000"))
                {
                    if (!innerText.Trim().Equals("-0"))
                    {
                        decimal num2;
                        decimal num3;
                        decimal.TryParse(innerText, out num2);
                        decimal.TryParse(djje, out num3);
                        if (num2 <= decimal.Zero)
                        {
                            return "数据库中未找到对应的蓝票，且受理平台无可开红字发票金额，不能开红字发票";
                        }
                        if (num2.CompareTo(Math.Abs(num3)) < 0)
                        {
                            str = string.Format("销项红字发票填开金额超过对应蓝字发票金额！本张发票填开限额：￥{0}所开发票已填金额：￥{1}", decimal.Negate(num2).ToString("F2"), num3.ToString("F2"));
                        }
                    }
                    return str;
                }
                return "数据库中未找到对应的蓝票，且与受理平台网络不通，不能开红字发票";
            }
            return "数据库中未找到对应的蓝票，且与受理平台网络不通，不能开红字发票";
        }

        private string CanRedFp(string djje)
        {
            decimal num;
            decimal num3;
            string str = "";
            if (decimal.Parse(blueFpxx.je).CompareTo(decimal.Parse("0.00")) < 0)
            {
                str = "发票为红字发票";
            }
            else if (blueFpxx.zfbz)
            {
                str = "发票已作废";
            }
            else if (decimal.Parse(blueFpxx.je).CompareTo(Math.Abs(decimal.Parse(djje))) < 0)
            {
                str = string.Format("销项红字发票填开金额超过对应蓝字发票金额！本张发票填开限额：￥{0}所开发票已填金额：￥{1}", decimal.Negate(decimal.Parse(blueFpxx.je)).ToString("F2"), decimal.Parse(djje).ToString("F2"));
            }
            else if (((blueFpxx.fplx == (FPLX)2) || (blueFpxx.fplx == (FPLX)0x33)) && (((((int)this.mZyfplx == 0) && (blueFpxx.Zyfplx == (ZYFP_LX)9)) || ((this.mZyfplx == (ZYFP_LX)8) && (blueFpxx.Zyfplx != (ZYFP_LX)8))) || ((this.mZyfplx == (ZYFP_LX)9) && (blueFpxx.Zyfplx != (ZYFP_LX)9))))
            {
                str = "发票类型不一致";
            }
            decimal.TryParse(djje, out num);
            decimal num2 = Math.Abs(num);
            if ((!(blueFpxx.je != "") || (decimal.Parse(blueFpxx.je).CompareTo(decimal.Parse("0.00")) < 0)) || !decimal.TryParse(blueFpxx.je, out num3))
            {
                return str;
            }
            decimal totalRedJe = this.fpm.GetTotalRedJe(blueFpxx.fpdm, blueFpxx.fphm);
            decimal num6 = decimal.Add(num3, totalRedJe);
            if ((num6 > decimal.Zero) && (num2.CompareTo(Math.Abs(num6)) <= 0))
            {
                return str;
            }
            return string.Format("销项红字发票填开金额超过对应蓝字发票金额！本张发票填开限额：￥{0}所开发票已填金额：￥{1}", decimal.Negate(num6).ToString("F2"), num.ToString());
        }

        public string CheckDZRed(Djfp dj)
        {
            Fpxx fpxx = dj.Fpxx;
            string blueFpdm = fpxx.blueFpdm;
            string blueFphm = fpxx.blueFphm;
            string str3 = "";
            int result = 0;
            if (int.TryParse(blueFphm, out result))
            {
                blueFpxx = new FpManager().GetXxfp(this.fplx, blueFpdm, result);
            }
            else
            {
                str3 = string.Format("本张发票不能开红字发票！原因：YFP_HM有误", new object[0]);
            }
            if (blueFpxx != null)
            {
                string str4 = this.CanRedFp(fpxx.je);
                if (str4 != "")
                {
                    str3 = string.Format("本张发票不能开红字发票！原因：{0}", str4);
                }
                return str3;
            }
            string str5 = this.CanEmptyRedFp(blueFpdm, blueFphm, fpxx.je);
            if (str5 != "")
            {
                str3 = string.Format("本张发票不能开红字发票！原因：{0}", str5);
            }
            return str3;
        }

        private void CloseDialog()
        {
            this.timer1.Tick -= new EventHandler(this.timer1_Tick);
            this.timer1.Dispose();
            this.backgroundWorker1.Dispose();
            base.DialogResult = DialogResult.Cancel;
        }

        private void CloseFile(string djFile, string bfFile)
        {
            if ((int)this.fplx != 0x33)
            {
                writer.WriteLine("退出批量开具");
                writer.Close();
                writer.Dispose();
            }
            else
            {
                this.Empty_dz_hx();
            }
            File.Copy(djFile, bfFile, true);
            File.Delete(djFile);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Empty_dz_hx()
        {
            XmlNode newChild = doc.CreateElement("RESPONSE_COMMON_FPKJ");
            bus.AppendChild(newChild);
            XmlElement element = doc.CreateElement("FPQQLSH");
            newChild.AppendChild(element);
            XmlElement element2 = doc.CreateElement("JQBH");
            newChild.AppendChild(element2);
            XmlElement element3 = doc.CreateElement("FP_DM");
            newChild.AppendChild(element3);
            XmlElement element4 = doc.CreateElement("FP_HM");
            newChild.AppendChild(element4);
            XmlElement element5 = doc.CreateElement("KPRQ");
            newChild.AppendChild(element5);
            XmlElement element6 = doc.CreateElement("FP_MW");
            newChild.AppendChild(element6);
            XmlElement element7 = doc.CreateElement("JYM");
            newChild.AppendChild(element7);
            XmlElement element8 = doc.CreateElement("EWM");
            newChild.AppendChild(element8);
            XmlElement element9 = doc.CreateElement("BZ");
            newChild.AppendChild(element9);
            XmlElement element10 = doc.CreateElement("RETURNCODE");
            element10.InnerText = "0001";
            newChild.AppendChild(element10);
            XmlElement element11 = doc.CreateElement("RETURNMSG");
            element11.InnerText = "退出批量开具";
            newChild.AppendChild(element11);
            doc.Save(this.hxPath + @"\" + this.hxFileNamexml);
        }

        private void ExcuteFpImp()
        {
            try
            {
                this.timer1.Stop();
                this.interrupted = false;
                this.GetPaths(this.fplx);
                this.djFiles = FileUtil.SearchDirectory(this.drPath, ".xml");
                if (this.djFiles.Count == 0)
                {
                    this.lblNotFind.Visible = true;
                    this.timer1.Enabled = true;
                }
                else
                {
                    this.panel1.Visible = false;
                    this.panel2.Visible = true;
                    this.lblNotFind.Visible = false;
                    this.pbImport.Minimum = 1;
                    this.pbImport.Maximum = 10;
                    this.pbImport.Value = 1;
                    this.stopped = false;
                    ErrorExist = false;
                    this.backgroundWorker1.RunWorkerAsync();
                }
            }
            catch (Exception exception)
            {
                this.log.Error(exception);
            }
        }

        private void GetPaths(FPLX type)
        {
            if ((type == 0) || ((int)type == 2))
            {
                this.drPath = PropertyUtil.GetValue("FPDJ_DRPATH");
                this.bfPath = PropertyUtil.GetValue("FPDJ_BFPATH");
                this.hxPath = PropertyUtil.GetValue("FPDJ_HXPATH");
                int.TryParse(PropertyUtil.GetValue("FPDJ_INTERVAL"), out this.interval);
            }
            else if ((int)type == 0x33)
            {
                this.drPath = PropertyUtil.GetValue("DZFPDJ_DRPATH");
                this.bfPath = PropertyUtil.GetValue("DZFPDJ_BFPATH");
                this.hxPath = PropertyUtil.GetValue("DZFPDJ_HXPATH");
                int.TryParse(PropertyUtil.GetValue("DZFPDJ_INTERVAL"), out this.interval);
            }
            else if ((int)type == 11)
            {
                this.drPath = PropertyUtil.GetValue("HYFPDJ_DRPATH");
                this.bfPath = PropertyUtil.GetValue("HYFPDJ_BFPATH");
                this.hxPath = PropertyUtil.GetValue("HYFPDJ_HXPATH");
                int.TryParse(PropertyUtil.GetValue("HYFPDJ_INTERVAL"), out this.interval);
            }
            else if ((int)type == 12)
            {
                this.drPath = PropertyUtil.GetValue("JDCFPDJ_DRPATH");
                this.bfPath = PropertyUtil.GetValue("JDCFPDJ_BFPATH");
                this.hxPath = PropertyUtil.GetValue("JDCFPDJ_HXPATH");
                int.TryParse(PropertyUtil.GetValue("JDCFPDJ_INTERVAL"), out this.interval);
            }
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.panel1 = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel1");
            this.panel2 = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel2");
            this.panel1.Visible = true;
            this.panel2.Visible = false;
            this.lblStartTip = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblStartTip");
            this.lblNotFind = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblNotFind");
            this.lblFpzl = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblFpzl");
            this.lblFpdm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblFpdm");
            this.lblFphm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblFphm");
            this.lblFile = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblFile");
            this.lblDjh = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblDjh");
            this.btnStart = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnStart");
            this.btnExit = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnExit");
            this.pbImport = this.xmlComponentLoader1.GetControlByName<AisinoPRG>("pbImport");
            this.btnStart.Click += new EventHandler(this.btnStart_Click);
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            this.lblFile.AutoEllipsis = true;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(AutoImport));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            this.backgroundWorker1 = new BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x1a7, 0x101);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath=@"..\Config\Components\Aisino.Fwkp.Fpkj.Form.FPDR.AutoImport\Aisino.Fwkp.Fpkj.Form.FPDR.AutoImport.xml";
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1a7, 0x101);
            base.Controls.Add(this.xmlComponentLoader1);
            this.Cursor = Cursors.Default;
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "AutoImport";
            this.Text = "自动导入";
            base.FormClosing += new FormClosingEventHandler(this.AutoImport_FormClosing);
            base.ResumeLayout(false);
        }

        private List<Djfp> ParseDjFile(string djFile)
        {
            string errorTip = "";
            if (((int)this.fplx == 0) || ((int)this.fplx == 2))
            {
                return this.djHelper.ParseDjFile(this.fplx, djFile, out errorTip, this.sqslv, this.zyfplx);
            }
            if ((int)this.fplx == 0x33)
            {
                return this.djHelper.ParseDZDjFile(this.fplx, djFile, out errorTip, this.sqslv);
            }
            if ((int)this.fplx == 12)
            {
                return this.djHelper.ParseJDCDjFile(djFile, out errorTip, this.sqslv);
            }
            return this.djHelper.ParseHYDjFile(djFile, out errorTip, this.sqslv);
        }

        private void ResetProgressValue()
        {
            if (base.InvokeRequired)
            {
                base.Invoke(new MethodInvoker(this.ResetProgressValue));
            }
            else
            {
                this.pbImport.Value = 1;
                this.pbImport.Maximum = 10;
            }
        }

        private void SetProgress(int value)
        {
            if (base.InvokeRequired)
            {
                object[] args = new object[] { value };
                base.BeginInvoke(new Action<int>(this.SetProgress), args);
            }
            else
            {
                this.pbImport.Increment(value);
            }
        }

        private void SetProgressTip()
        {
            if (base.InvokeRequired)
            {
                base.Invoke(new MethodInvoker(this.SetProgressTip));
            }
            else
            {
                this.lblDjh.Text = this.mDjh;
                if ((int)this.fplx == 0)
                {
                    this.lblFpzl.Text = "专用发票";
                }
                else if ((int)this.fplx == 2)
                {
                    this.lblFpzl.Text = "普通发票";
                }
                else if ((int)this.fplx == 0x33)
                {
                    this.lblFpzl.Text = "电子增值税普通发票";
                }
                else if ((int)this.fplx == 11)
                {
                    this.lblFpzl.Text = "货物运输业增值税专用发票";
                }
                else if ((int)this.fplx == 12)
                {
                    this.lblFpzl.Text = "机动车销售统一发票";
                }
                this.lblFpdm.Text = this.mFpdm;
                this.lblFphm.Text = this.mFphm;
                this.lblFile.Text = this.mFileName;
                this.Refresh();
                Thread.Sleep(100);
            }
        }

        private void ShowResultTip()
        {
            if (base.InvokeRequired)
            {
                base.Invoke(new MethodInvoker(this.ShowResultTip));
            }
            else
            {
                this.panel2.Visible = false;
                this.panel1.Visible = true;
                this.lblNotFind.Visible = true;
            }
        }

        private bool SuccessKp()
        {
            if ((int)this.fplx == 0x33)
            {
                return (returncode == "0000");
            }
            int index = KpResult.IndexOf("开具结果:");
            return (((index > -1) && ((index + 5) < KpResult.Length)) && (KpResult.Substring(index + 5, 1) == "1"));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.ExcuteFpImp();
        }

        private void TKfpxx(Djfp dj)
        {
            if ((((int)this.fplx == 0) || ((int)this.fplx == 2)) && (this.invoiceForm != null))
            {
                this.invoiceForm.FillDjxx(dj);
                this.backgroundWorker1.ReportProgress(2, dj);
                this.invoiceForm.AutoImportZpfp(this, dj);
                for (int i = 0; i < 5; i++)
                {
                    this.backgroundWorker1.ReportProgress(7, null);
                }
            }
            else if (((int)this.fplx == 0x33) && (this.dzinvoiceForm != null))
            {
                if (dj.Fpxx.isRed)
                {
                    this.dzinvoiceForm.FillDjxx(dj, true);
                }
                else
                {
                    this.dzinvoiceForm.FillDjxx(dj, false);
                }
                this.backgroundWorker1.ReportProgress(2, dj);
                this.dzinvoiceForm.AutoImportDzfp(this, dj);
                for (int j = 0; j < 5; j++)
                {
                    this.backgroundWorker1.ReportProgress(7, null);
                }
            }
            else if (((int)this.fplx == 11) && (this.hyInvoiceForm != null))
            {
                this.hyInvoiceForm.FillDjxx(dj);
                this.backgroundWorker1.ReportProgress(2, dj);
                this.hyInvoiceForm.AutoImporthyfp(this, dj);
                for (int k = 0; k < 5; k++)
                {
                    this.backgroundWorker1.ReportProgress(7, null);
                }
            }
            else if (((int)this.fplx == 12) && (this.jdcInvoiceForm != null))
            {
                this.jdcInvoiceForm.FillDjxx(dj);
                this.backgroundWorker1.ReportProgress(2, dj);
                this.jdcInvoiceForm.AutoImportjdcfp(this, dj);
                for (int m = 0; m < 5; m++)
                {
                    this.backgroundWorker1.ReportProgress(7, null);
                }
            }
            Thread.Sleep(0x3e8);
        }

        public string CurFpdm
        {
            get
            {
                return this.mFpdm;
            }
            set
            {
                this.mFpdm = value;
            }
        }

        public string CurFphm
        {
            get
            {
                return this.mFphm;
            }
            set
            {
                this.mFphm = value;
            }
        }

        public BaseForm FPTKForm
        {
            get
            {
                if (((int)this.fplx == 0) || ((int)this.fplx == 2))
                {
                    return this.invoiceForm;
                }
                if ((int)this.fplx == 0x33)
                {
                    return this.dzinvoiceForm;
                }
                if ((int)this.fplx == 11)
                {
                    return this.hyInvoiceForm;
                }
                return this.jdcInvoiceForm;
            }
            set
            {
                if (((int)this.fplx == 0) || ((int)this.fplx == 2))
                {
                    this.invoiceForm = value as InvoiceForm_ZZS;
                }
                else if ((int)this.fplx == 0x33)
                {
                    this.dzinvoiceForm = value as InvoiceForm_DZ;
                }
                else if ((int)this.fplx == 11)
                {
                    this.hyInvoiceForm = value as HYInvoiceForm;
                }
                else
                {
                    this.jdcInvoiceForm = value as JDCInvoiceForm_new;
                }
            }
        }
    }
}

