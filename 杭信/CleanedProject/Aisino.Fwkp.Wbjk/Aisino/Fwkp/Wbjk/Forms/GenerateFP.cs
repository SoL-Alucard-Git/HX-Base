namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.MainForm;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.DAL;
    using Aisino.Fwkp.Wbjk.Model;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class GenerateFP : BaseForm
    {
        private SaleBillCtrl billBL;
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private AisinoBTN btnSelect;
        private IContainer components;
        private string djType;
        public int FPleftnum;
        private FPTKdal fptkBLL;
        private InvType fpType;
        private InvoiceType FPType_Card;
        private GenerateInvoice generBL;
        private bool IsNewJDC;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private AisinoLBL label3;
        private AisinoLBL label4;
        private AisinoLBL label5;
        private AisinoLBL label6;
        private AisinoLBL labelSYZS;
        private AisinoLBL labelXZDJ;
        private AisinoLBL lableFPDM;
        private AisinoLBL lableFPSH;
        private AisinoLBL lableFPZL;
        private ILog log;
        private int NPCType;
        private SelectDanJuForFP selectDJ;
        private TaxCard taxCard;
        private XmlComponentLoader xmlComponentLoader1;

        public GenerateFP(InvType typeFP)
        {
            this.log = LogUtil.GetLogger<GenerateFP>();
            this.fptkBLL = new FPTKdal();
            this.billBL = SaleBillCtrl.Instance;
            this.generBL = GenerateInvoice.Instance;
            this.taxCard = TaxCardFactory.CreateTaxCard();
            this.IsNewJDC = false;
            this.NPCType = 0;
            this.components = null;
            this.FPleftnum = 0;
            this.Initialize();
            base.StartPosition = FormStartPosition.CenterScreen;
            this.fpType = typeFP;
            this.FPType_Card = (InvoiceType) this.fpType;
            this.djType = CommonTool.GetInvTypeStr(typeFP);
            this.lableFPZL.Text = ShowString.ShowFPZL(this.djType);
            this.FPleftnum = (int) this.taxCard.GetInvNumber(this.FPType_Card);
        }

        public GenerateFP(InvType typeFP, int Type)
        {
            this.log = LogUtil.GetLogger<GenerateFP>();
            this.fptkBLL = new FPTKdal();
            this.billBL = SaleBillCtrl.Instance;
            this.generBL = GenerateInvoice.Instance;
            this.taxCard = TaxCardFactory.CreateTaxCard();
            this.IsNewJDC = false;
            this.NPCType = 0;
            this.components = null;
            this.FPleftnum = 0;
            this.Initialize();
            base.StartPosition = FormStartPosition.CenterScreen;
            this.fpType = typeFP;
            this.FPType_Card = (InvoiceType) this.fpType;
            this.djType = CommonTool.GetInvTypeStr(typeFP);
            if (Type == 1)
            {
                this.lableFPZL.Text = "农产品销售发票";
                this.NPCType = 1;
            }
            else if (Type == 2)
            {
                this.lableFPZL.Text = "收购发票";
                this.NPCType = 2;
            }
            if (Type == 3)
            {
                this.lableFPZL.Text = ShowString.ShowFPZL(this.djType) + "(新)";
                this.IsNewJDC = true;
            }
            else if (Type == 4)
            {
                this.lableFPZL.Text = ShowString.ShowFPZL(this.djType) + "(旧)";
                this.IsNewJDC = false;
            }
            this.FPleftnum = (int) this.taxCard.GetInvNumber(this.FPType_Card);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                int num = int.Parse(this.labelSYZS.Text);
                if (int.Parse(this.labelXZDJ.Text) > 0)
                {
                    List<FPGenerateResult> listGeneratePreview = this.generBL.GenerateInvForPreview(this.selectDJ.ListBH);
                    if (this.fpType == InvType.vehiclesales)
                    {
                        if (this.IsNewJDC)
                        {
                            foreach (FPGenerateResult result in listGeneratePreview)
                            {
                                result.ISNEWJDC = true;
                            }
                        }
                        else
                        {
                            foreach (FPGenerateResult result in listGeneratePreview)
                            {
                                result.ISNEWJDC = false;
                            }
                        }
                    }
                    if (this.fpType == InvType.Common)
                    {
                        if (this.NPCType == 1)
                        {
                            foreach (FPGenerateResult result in listGeneratePreview)
                            {
                                result.TYDH = "1";
                            }
                        }
                        else if (this.NPCType == 2)
                        {
                            foreach (FPGenerateResult result in listGeneratePreview)
                            {
                                XSDJ_MXModel model;
                                result.TYDH = "2";
                                int num3 = 0;
                                while (num3 < result.ListXSDJ_MX.Count)
                                {
                                    model = result.ListXSDJ_MX[num3];
                                    if (Math.Abs(model.SLV) > 1E-06)
                                    {
                                        result.KKPX = "不能开票";
                                        result.SXYY = "收购发票只能开具零税率发票";
                                        break;
                                    }
                                    if (Math.Abs(model.KCE) > 1E-06)
                                    {
                                        result.KKPX = "不能开票";
                                        result.SXYY = "收购发票不能开具差额税发票";
                                        break;
                                    }
                                    num3++;
                                }
                                if (CommonTool.isSPBMVersion())
                                {
                                    for (num3 = 0; num3 < result.ListXSDJ_MX.Count; num3++)
                                    {
                                        model = result.ListXSDJ_MX[num3];
                                        model.LSLVBS = "3";
                                        if (((model.XSYHSM == "免税") || (model.XSYHSM == "不征税")) || (model.XSYHSM == "出口零税"))
                                        {
                                            model.XSYH = false;
                                            model.XSYHSM = "";
                                        }
                                        else if (this.yhzc_contain_slv(model.XSYHSM, model.SLV.ToString(), false, false))
                                        {
                                            model.XSYH = true;
                                        }
                                        else
                                        {
                                            model.XSYH = false;
                                        }
                                        if (!model.XSYH)
                                        {
                                            model.XSYHSM = "";
                                        }
                                        if (model.XSYHSM == "")
                                        {
                                            model.XSYH = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    ConfirmDJKPZT mdjkpzt = new ConfirmDJKPZT(listGeneratePreview);
                    switch (mdjkpzt.ShowDialog())
                    {
                        case DialogResult.OK:
                        {
                            InvCodeNum currentInvCode = this.taxCard.GetCurrentInvCode(this.FPType_Card);
                            this.lableFPDM.Text = currentInvCode.InvTypeCode;
                            this.lableFPSH.Text = currentInvCode.InvNum;
                            this.labelSYZS.Text = this.taxCard.GetInvNumber(this.FPType_Card).ToString();
                            this.labelXZDJ.Text = "0";
                            this.selectDJ.ListBH.Clear();
                            base.Close();
                            break;
                        }
                        case DialogResult.Cancel:
                            base.Close();
                            break;
                    }
                }
                else if (this.fpType == InvType.Common)
                {
                    MessageManager.ShowMsgBox("INP-273203");
                }
                else if (this.fpType == InvType.Special)
                {
                    MessageManager.ShowMsgBox("INP-273203");
                }
                else if (this.fpType == InvType.transportation)
                {
                    MessageManager.ShowMsgBox("INP-273203");
                }
                else if (this.fpType == InvType.vehiclesales)
                {
                    MessageManager.ShowMsgBox("INP-273203");
                }
            }
            catch (Exception exception)
            {
                if (exception.ToString().Contains("超时"))
                {
                    this.log.Error(exception.ToString());
                }
                else
                {
                    HandleException.HandleError(exception);
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                int leftInvCount = int.Parse(this.labelSYZS.Text);
                this.billBL.CheckBillMonth("0", this.djType, false);
                this.selectDJ = new SelectDanJuForFP(this.fpType, leftInvCount);
                DialogResult result = this.selectDJ.ShowDialog();
                this.labelXZDJ.Text = this.selectDJ.SelectNum.ToString();
            }
            catch (Exception exception)
            {
                if (exception.ToString().Contains("超时"))
                {
                    this.log.Error(exception.ToString());
                }
                else
                {
                    HandleException.HandleError(exception);
                }
            }
        }

        public bool CanInvoice(FPLX fplx, out string code)
        {
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            try
            {
                TaxStateInfo info = TaxCardFactory.CreateTaxCard().get_StateInfo();
                if ((fplx == null) || (fplx == 2))
                {
                    flag = info.IsLockReached == 1;
                    flag2 = info.IsRepReached == 1;
                    flag3 = info.IsInvEmpty == 1;
                }
                else
                {
                    InvTypeInfo info2 = new InvTypeInfo();
                    List<InvTypeInfo> invTypeInfo = info.InvTypeInfo;
                    if (fplx == 11)
                    {
                        foreach (InvTypeInfo info3 in invTypeInfo)
                        {
                            if (info3.InvType == 11)
                            {
                                info2 = info3;
                                break;
                            }
                        }
                    }
                    else if (fplx == 12)
                    {
                        foreach (InvTypeInfo info3 in invTypeInfo)
                        {
                            if (info3.InvType == 12)
                            {
                                info2 = info3;
                                break;
                            }
                        }
                    }
                    else if (fplx == 0x33)
                    {
                        foreach (InvTypeInfo info3 in invTypeInfo)
                        {
                            if (info3.InvType == 0x33)
                            {
                                info2 = info3;
                                break;
                            }
                        }
                    }
                    else if (fplx == 0x29)
                    {
                        foreach (InvTypeInfo info3 in invTypeInfo)
                        {
                            if (info3.InvType == 0x29)
                            {
                                info2 = info3;
                                break;
                            }
                        }
                    }
                    flag = info2.IsLockTime == 1;
                    flag2 = info2.IsRepTime == 1;
                    flag3 = info.IsInvEmpty == 1;
                }
                if (flag)
                {
                    code = "INP-242101";
                    return false;
                }
                if (flag2)
                {
                    code = "INP-242102";
                    return false;
                }
                if (flag3)
                {
                    code = "INP-242103";
                    return false;
                }
                code = "000000";
                return true;
            }
            catch (Exception exception)
            {
                this.log.Error("单据管理开票前读取金税卡状态时异常:" + exception.ToString());
                code = "9999";
                return false;
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

        private void GenerateFP_Load(object sender, EventArgs e)
        {
            try
            {
                InvCodeNum currentInvCode = this.taxCard.GetCurrentInvCode(this.FPType_Card);
                if (this.taxCard.get_RetCode() > 0)
                {
                    if ((this.taxCard.get_RetCode() == 0x2bd) || (this.taxCard.get_RetCode() == 0x30d))
                    {
                        MessageManager.ShowMsgBox("INP-242102");
                    }
                    else if (this.taxCard.get_RetCode() == 0x2be)
                    {
                        MessageManager.ShowMsgBox("INP-242101");
                    }
                    else if ((this.taxCard.get_RetCode() == 0x300) || (this.taxCard.get_RetCode() == 0x301))
                    {
                        FormMain.CallUpload();
                        MessageManager.ShowMsgBox(this.taxCard.get_ErrCode());
                    }
                    else
                    {
                        MessageManager.ShowMsgBox(this.taxCard.get_ErrCode());
                    }
                    base.Close();
                }
                else if (this.FPleftnum == 0)
                {
                    MessageManager.ShowMsgBox("INP-242103");
                    base.Close();
                }
                else
                {
                    this.lableFPDM.Text = currentInvCode.InvTypeCode;
                    this.lableFPSH.Text = currentInvCode.InvNum;
                    this.labelSYZS.Text = this.taxCard.GetInvNumber(this.FPType_Card).ToString();
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        public string[] GetCurrent(FPLX fplx)
        {
            string str = "";
            try
            {
                TaxCard card = TaxCardFactory.CreateTaxCard();
                int num = int.Parse(Enum.Format(typeof(FPLX), fplx, "d"));
                InvCodeNum currentInvCode = card.GetCurrentInvCode(fplx);
                if ((currentInvCode.InvTypeCode == null) || (currentInvCode.InvNum == null))
                {
                    str = card.get_ErrCode();
                    if (str.StartsWith("TCD_768") || str.StartsWith("TCD_769"))
                    {
                        FormMain.CallUpload();
                    }
                    return null;
                }
                if (currentInvCode.InvTypeCode.Equals("0000000000"))
                {
                    str = "INP-242104";
                    return null;
                }
                str = "000000";
                return new string[] { currentInvCode.InvTypeCode, currentInvCode.InvNum };
            }
            catch (Exception exception)
            {
                this.log.Error("读取当前发票代码号码时异常:" + exception.ToString());
                str = "9999";
                return null;
            }
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.lableFPSH = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lableFPSH");
            this.lableFPDM = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lableFPDM");
            this.label4 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label4");
            this.label3 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label3");
            this.lableFPZL = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lableFPZL");
            this.label2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label2");
            this.label1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label1");
            this.labelXZDJ = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelXZDJ");
            this.labelSYZS = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelSYZS");
            this.label6 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label6");
            this.label5 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label5");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.btnSelect = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnSelect");
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.btnSelect.Click += new EventHandler(this.btnSelect_Click);
            this.lableFPSH.Font = new Font("宋体", 12f, FontStyle.Bold);
            this.lableFPSH.TabIndex = 15;
            this.lableFPDM.Font = new Font("宋体", 12f, FontStyle.Bold);
            this.lableFPDM.TabIndex = 14;
            this.label4.Font = new Font("宋体", 11f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label4.TabIndex = 13;
            this.label3.Font = new Font("宋体", 11f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label3.TabIndex = 12;
            this.lableFPZL.Font = new Font("宋体", 12f, FontStyle.Bold);
            this.lableFPZL.TabIndex = 11;
            this.label2.Font = new Font("宋体", 11f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label2.TabIndex = 10;
            this.label1.Font = new Font("宋体", 11f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label1.TabIndex = 9;
            this.labelXZDJ.Font = new Font("宋体", 12f, FontStyle.Bold);
            this.labelXZDJ.TabIndex = 0x15;
            this.labelSYZS.Font = new Font("宋体", 12f, FontStyle.Bold);
            this.labelSYZS.TabIndex = 20;
            this.label6.Font = new Font("宋体", 11f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label6.TabIndex = 0x13;
            this.label5.Font = new Font("宋体", 11f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label5.TabIndex = 0x12;
            this.btnOK.TabIndex = 0x16;
            this.btnCancel.TabIndex = 0x17;
            this.btnSelect.FlatAppearance.BorderSize = 0;
            this.btnSelect.FlatStyle = FlatStyle.Flat;
            this.btnSelect.TabIndex = 0x18;
            this.btnSelect.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.StartPosition = FormStartPosition.CenterScreen;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(GenerateFP));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x176, 0x120);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Wbjk.GenerateFP\Aisino.Fwkp.Wbjk.GenerateFP.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x176, 0x120);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(0, 0);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "GenerateFP";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "选择填开发票的单据";
            base.Load += new EventHandler(this.GenerateFP_Load);
            base.ResumeLayout(false);
        }

        public bool yhzc_contain_slv(string yhzc, string slv, bool flag, bool isTsfp)
        {
            string str = "aisino.fwkp.Wbjk.SelectYhzcs";
            IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            ArrayList list = baseDAOSQLite.querySQL(str, dictionary);
            if (isTsfp)
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
            foreach (Dictionary<string, object> dictionary2 in list)
            {
                string[] source = dictionary2["SLV"].ToString().Split(new string[] { "、", ",", "，" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < source.Length; i++)
                {
                    if (source[i] == "1.5%_5%")
                    {
                        source[i] = "1.5%";
                    }
                }
                if ((dictionary2["YHZCMC"].ToString() == yhzc) && (dictionary2["SLV"].ToString() == ""))
                {
                    return true;
                }
                if ((dictionary2["YHZCMC"].ToString() == yhzc) && source.Contains<string>(slv))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

