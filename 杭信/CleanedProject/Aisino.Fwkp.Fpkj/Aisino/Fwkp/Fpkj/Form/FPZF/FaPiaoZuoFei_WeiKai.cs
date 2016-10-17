namespace Aisino.Fwkp.Fpkj.Form.FPZF
{
    using Aisino.Framework.MainForm;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fpkj.Common;
    using Aisino.Fwkp.Fpkj.DAL;
    using Aisino.Fwkp.Fpkj.Form.FPXF;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    public class FaPiaoZuoFei_WeiKai : DockForm
    {
        public string _strFphm = string.Empty;
        private AisinoBTN but_queding;
        private AisinoBTN but_quxiao;
        private IContainer components;
        public FPLX FaPiaoType;
        private List<Fpxx> FpList = new List<Fpxx>();
        private AisinoLBL lab_fpdm;
        private AisinoLBL lab_fpHasNum;
        private AisinoLBL lab_fpStartNum;
        private AisinoLBL lab_fpzl;
        private AisinoLBL label1;
        private ILog loger = LogUtil.GetLogger<FaPiaoZuoFei_WeiKai>();
        private FPProgressBar progressBar;
        private int step = 0x7d0;
        private AisinoTXT txt_zuofeiNum;
        private XmlComponentLoader xmlComponentLoader1;
        public XXFP xxfpChaXunBll = new XXFP(false);

        public FaPiaoZuoFei_WeiKai()
        {
            try
            {
                this.Initialize();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
            }
        }

         public Fpxx BlankWasteTaxCardZuoFei(object[] param)
        {
            string str = param[0].ToString();
            string str2 = param[1].ToString();
            string str3 = param[2].ToString();
            string str4 = param[3].ToString();
            string str5 = param[4].ToString();
            string str6 = param[5].ToString();
            string code = "0000";
            byte[] sourceArray = Invoice.TypeByte;
            byte[] destinationArray = new byte[0x20];
            Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
            byte[] buffer3 = new byte[0x10];
            Array.Copy(sourceArray, 0x20, buffer3, 0, 0x10);
            byte[] buffer4 = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes("KP" + DateTime.Now.ToString("F")), destinationArray, buffer3);
            Invoice.IsGfSqdFp_Static = false;
            Invoice invoice = new Invoice(Invoice.ParseFPLX(str), str2, str3, str4, str5, str6, buffer4, "NEW76mmX177mm");
            invoice.Hjje = "0.00";
            invoice.Hjse = "0.00";
            Fpxx fpData = invoice.GetFpData();
            code = invoice.GetCode();
            if (fpData == null)
            {
                MessageManager.ShowMsgBox(invoice.GetCode(), invoice.Params);
                return fpData;
            }
            fpData.hzfw = (TaxCardFactory.CreateTaxCard().StateInfo.CompanyType != 0) || (fpData.hxm.Length > 0);
            fpData.sLv = "0.17";
            string str8 = "Aisino.Fwkp.Invoice" + invoice.Fpdm + invoice.Fphm;
            byte[] bytes = Encoding.Unicode.GetBytes(MD5_Crypt.GetHashStr(str8));
            destinationArray = new byte[0x20];
            Array.Copy(bytes, 0, destinationArray, 0, 0x20);
            buffer3 = new byte[0x10];
            Array.Copy(bytes, 0x20, buffer3, 0, 0x10);
            byte[] inArray = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes(DateTime.Now.ToString("F")), destinationArray, buffer3);
            fpData.gfmc = Convert.ToBase64String(AES_Crypt.Encrypt(Encoding.Unicode.GetBytes(Convert.ToBase64String(inArray) + ";" + invoice.Gfmc), destinationArray, buffer3));
            fpData.zfbz = true;
            fpData.bszt = 0;
            if ((int)fpData.fplx == 12)
            {
                fpData.clsbdh = "0";
                fpData.zyspsm = "#%";
                fpData.zyspmc = "";
            }
            fpData.dy_mb = "NEW76mmX177mm";
            if (!invoice.MakeCardInvoice(fpData, false))
            {
                code = invoice.GetCode();
                if (code.StartsWith("TCD_768") || code.StartsWith("TCD_769"))
                {
                    FormMain.CallUpload();
                }
                MessageManager.ShowMsgBox(invoice.GetCode(), invoice.Params);
                return fpData;
            }
            code = "0000";
            return fpData;
        }

        private void but_queding_Click(object sender, EventArgs e)
        {
            try
            {
                int num = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(this.lab_fpHasNum.Text.Trim());
                int zuoFeiNum = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(this.txt_zuofeiNum.Text.Trim());
                if (zuoFeiNum <= 0)
                {
                    MessageManager.ShowMsgBox("FPZF-000013");
                }
                else if (zuoFeiNum > num)
                {
                    MessageManager.ShowMsgBox("FPZF-000011");
                }
                else if ((zuoFeiNum <= 1) || (DialogResult.Cancel != MessageManager.ShowMsgBox("FPZF-000009", new string[] { this.lab_fpdm.Text, this.lab_fpStartNum.Text, zuoFeiNum.ToString() })))
                {
                    base.Visible = false;
                    this.Refresh();
                    this.ZuoFeiMainFunction(zuoFeiNum);
                    base.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
            }
        }

        private void but_quxiao_Click(object sender, EventArgs e)
        {
            try
            {
                base.Close();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
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

        private void FaPiaoZuoFei_WeiKai_FormClosing(object sender, EventArgs e)
        {
            this.xxfpChaXunBll = null;
            this.progressBar = null;
            this.FpList = null;
            base.Dispose();
        }

        public _InvoiceType GetInvoiceType(FPLX type)
        {
            _InvoiceType type2;
            FPLX fplx = type;
            if ((int)fplx <= 12)
            {
                switch ((int)fplx)
                {
                    case 0:
                        type2.dbfpzl = "s";
                        type2.displayfpzl = "专用发票";
                        type2.TaxCardfpzl = (InvoiceType)0;
                        return type2;

                    case 2:
                        type2.dbfpzl = "c";
                        type2.displayfpzl = "普通发票";
                        type2.TaxCardfpzl = (InvoiceType)2;
                        return type2;

                    case 11:
                        type2.dbfpzl = "f";
                        type2.displayfpzl = "货物运输业增值税专用发票";
                        type2.TaxCardfpzl = (InvoiceType)11;
                        return type2;

                    case 12:
                        type2.dbfpzl = "j";
                        type2.displayfpzl = "机动车销售统一发票";
                        type2.TaxCardfpzl = (InvoiceType)12;
                        return type2;
                }
            }
            else if ((int)fplx != 0x29)
            {
                if ((int)fplx == 0x33)
                {
                    type2.dbfpzl = "p";
                    type2.displayfpzl = "电子增值税普通发票";
                    type2.TaxCardfpzl = (InvoiceType)0x33;
                    return type2;
                }
            }
            else
            {
                type2.dbfpzl = "q";
                type2.displayfpzl = "增值税普通发票(卷票)";
                type2.TaxCardfpzl = (InvoiceType)0x29;
                return type2;
            }
            type2.dbfpzl = "";
            type2.displayfpzl = "";
            type2.TaxCardfpzl = (InvoiceType)1;
            return type2;
        }

        public string GetTaxCardCurrentFpNum(ref InvCodeNum invCodeNum)
        {
            _InvoiceType invoiceType = this.GetInvoiceType(this.FaPiaoType);
            //逻辑修改 本地测试使用假数据
            if(InternetWare.Config.Constants.IsTest)
                invCodeNum = new InvCodeNum() { InvTypeCode = "3100153320", InvNum = "88888888", EndNum = "99999999" };
            else invCodeNum = base.TaxCardInstance.GetCurrentInvCode(invoiceType.TaxCardfpzl);
            if ((string.IsNullOrEmpty(invCodeNum.InvNum) || string.IsNullOrEmpty(invCodeNum.InvTypeCode)) || (base.TaxCardInstance.RetCode != 0))
            {
                FormMain.CallUpload();
                MessageManager.ShowMsgBox(base.TaxCardInstance.ErrCode);
                return "0001";
            }
            if (!invCodeNum.InvNum.Equals(new string('0', 8)) && !invCodeNum.InvTypeCode.Equals(new string('0', 10)))
            {
                return "0000";
            }
            MessageManager.ShowMsgBox("FPZF-000005");
            return "0001";
        }

        public int GetTaxCardFPNum(string TypeCode, int InvType, int startNum)
        {
            //逻辑修改
            if(InternetWare.Config.Constants.IsTest) return 100;

            List<InvVolumeApp> invStock = base.TaxCardInstance.GetInvStock();
            if (invStock != null)
            {
                foreach (InvVolumeApp app in invStock)
                {
                    if (((app.TypeCode == TypeCode) && (app.InvType == InvType)) && (app.HeadCode == startNum))
                    {
                        return app.Number;
                    }
                }
            }
            return 0;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.label1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label1");
            this.lab_fpzl = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_fpzl");
            this.lab_fpdm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_fpdm");
            this.lab_fpHasNum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_fpHasNum");
            this.lab_fpStartNum = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_fpStartNum");
            this.txt_zuofeiNum = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_zuofeiNum");
            this.but_quxiao = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_quxiao");
            this.but_queding = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_queding");
            this.but_queding.TabIndex = 0;
            this.but_quxiao.Click += new EventHandler(this.but_quxiao_Click);
            this.but_queding.Click += new EventHandler(this.but_queding_Click);
            this.txt_zuofeiNum.KeyPress += new KeyPressEventHandler(this.txt_zuofeiNum_KeyPress);
            this.txt_zuofeiNum.MaxLength = 8;
            this.txt_zuofeiNum.ImeMode = ImeMode.Disable;
            this.txt_zuofeiNum.ShortcutsEnabled = false;
            base.FormClosing += new FormClosingEventHandler(this.FaPiaoZuoFei_WeiKai_FormClosing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FaPiaoZuoFei_WeiKai));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(420, 0x11d);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "未开发票号作废。发票号码确认";
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Fpkj.Form.FPZF.FaPiaoZuoFei_WeiKai\Aisino.Fwkp.Fpkj.Form.FPZF.FaPiaoZuoFei_WeiKai.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(420, 0x11d);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(0, 0);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FaPiaoZuoFei_WeiKai";
            base.StartPosition = FormStartPosition.CenterParent;
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "未开发票号作废.发票号码确认";
            base.ResumeLayout(false);
        }

        public string IsEmpty_DengYu(string strValue)
        {
            try
            {
                if (strValue == null)
                {
                    return string.Empty;
                }
                if (strValue.Trim() == string.Empty)
                {
                    return string.Empty;
                }
                return strValue.Trim();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return string.Empty;
            }
        }

        private void PerformStep(int step)
        {
            for (int i = 0; i < step; i++)
            {
                if ((this.progressBar.fpxf_progressBar.Value + 1) > this.progressBar.fpxf_progressBar.Maximum)
                {
                    this.progressBar.fpxf_progressBar.Value = this.progressBar.fpxf_progressBar.Maximum;
                }
                else
                {
                    this.progressBar.fpxf_progressBar.Value++;
                }
                this.progressBar.fpxf_progressBar.Refresh();
            }
        }

        private void ProccessBarShow(object obj)
        {
            try
            {
                int step = (int) obj;
                this.PerformStep(step);
            }
            catch (Exception exception)
            {
                this.loger.Error("[ThreadFun]" + exception.ToString());
            }
        }

        public void ProcessStartThread(int value)
        {
            this.PerformStep(value);
        }

        public bool SetValue()
        {
            try
            {
                InvCodeNum invCodeNum = new InvCodeNum();
                if ("0000" != this.GetTaxCardCurrentFpNum(ref invCodeNum))
                {
                    return false;
                }
                _InvoiceType invoiceType = this.GetInvoiceType(this.FaPiaoType);
                this.lab_fpzl.Text = invoiceType.displayfpzl;
                this.lab_fpdm.Text = invCodeNum.InvTypeCode.Trim();
                this.lab_fpStartNum.Text = invCodeNum.InvNum.Trim();
                int num2 = this.GetTaxCardFPNum(invCodeNum.InvTypeCode, (int)invoiceType.TaxCardfpzl, Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(invCodeNum.InvNum));
                if (num2 == 0)
                {
                    return false;
                }
                this.lab_fpHasNum.Text = num2.ToString();
                this.txt_zuofeiNum.Text = "1";
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return false;
            }
            return true;
        }

        private void txt_zuofeiNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsDigit(e.KeyChar) && (e.KeyChar != '\r')) && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
        }

        public void ZuoFeiMainFunction(int ZuoFeiNum)
        {
            try
            {
                UpLoadCheckState.SetFpxfState(true);
                int num = 0;
                int num2 = 0;
                int num3 = 0x1770;
                this.FpList.Clear();
                InvCodeNum invCodeNum = new InvCodeNum();
                if (this.progressBar == null)
                {
                    this.progressBar = new FPProgressBar();
                }
                this.progressBar.SetTip("正在作废未开发票", "请等待任务完成", "未开发票作废过程");
                this.progressBar.Visible = true;
                this.progressBar.Refresh();
                this.progressBar.Show();
                this.progressBar.Refresh();
                this.ProcessStartThread(this.step);
                this.progressBar.Refresh();
                for (int i = 0; i < ZuoFeiNum; i++)
                {
                    if (this.GetTaxCardCurrentFpNum(ref invCodeNum) != "0000")
                    {
                        MessageManager.ShowMsgBox("FPZF-000012");
                        this.progressBar.Visible = false;
                        this.progressBar.Refresh();
                        return;
                    }
                    string dbfpzl = this.GetInvoiceType(this.FaPiaoType).dbfpzl;
                    string str3 = this.lab_fpdm.Text.Trim();
                    string str4 = ShareMethods.FPHMTo8Wei(invCodeNum.InvNum);
                    string str5 = this.IsEmpty_DengYu(base.TaxCardInstance.Address) + " " + base.TaxCardInstance.Telephone;
                    string str6 = this.IsEmpty_DengYu(base.TaxCardInstance.BankAccount);
                    string title = "正在作废发票代码：" + str3 + "发票号码：" + str4;
                    this.progressBar.SetTip(title, "请等待任务完成", "未开发票作废过程");
                    this.ProcessStartThread(num3 / ZuoFeiNum);
                    this.progressBar.Refresh();
                    object[] param = new object[] { dbfpzl, str3, str4, DingYiZhiFuChuan1._UserMsg.MC, str5, str6 };
                    Fpxx item = this.BlankWasteTaxCardZuoFei(param);
                    if ((item == null) || !(item.retCode == "0000"))
                    {
                        break;
                    }
                    this.FpList.Add(item);
                    num++;
                }
                this.xxfpChaXunBll.SaveXxfp(this.FpList);
                this.progressBar.SetTip("正在将作废发票写入数据库", "请等待任务完成", "未开发票作废过程");
                this.ProcessStartThread(this.step);
                this.progressBar.Refresh();
                this.progressBar.Visible = false;
                num2 = ZuoFeiNum - num;
                MessageManager.ShowMsgBox("FPZF-000010", new string[] { ZuoFeiNum.ToString(), num.ToString(), num2.ToString() });
            }
            catch (Exception exception)
            {
                this.loger.Error("[ZuoFeiMainFunction函数异常]" + exception.ToString());
            }
            finally
            {
                if (this.progressBar != null)
                {
                    this.progressBar.Visible = false;
                    this.progressBar.Close();
                    this.progressBar.Dispose();
                    this.progressBar = null;
                    GC.Collect();
                }
                UpLoadCheckState.SetFpxfState(false);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct _InvoiceType
        {
            public string dbfpzl;
            public InvoiceType TaxCardfpzl;
            public string displayfpzl;
        }

        private delegate void PerformStepHandle(int step);
    }
}

