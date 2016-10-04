namespace Aisino.Fwkp.Fpkj.Form.FPCX
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fpkj.Common;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class HeDui : BaseForm
    {
        private AisinoBTN btn_OK;
        private IContainer components;
        private AisinoLBL lbl_BSZT;
        private AisinoLBL lbl_FpDm;
        private AisinoLBL lbl_FpHm;
        private AisinoLBL lbl_GfSh;
        private AisinoLBL lbl_JE;
        private AisinoLBL lbl_KpRq;
        private AisinoLBL lbl_SE;
        private AisinoLBL lbl_XfSh;
        private AisinoLBL lbl_ZfBz;
        private ILog loger = LogUtil.GetLogger<HeDui>();
        private AisinoPNL panel1;
        private AisinoPNL panel2;
        private AisinoPIC pictureBox1;
        private XmlComponentLoader xmlComponentLoader1;

        public HeDui()
        {
            try
            {
                this.Initialize();
                this.btn_OK.Click += new EventHandler(this.btnOK_Click);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                base.Close();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
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

        private void Initialize()
        {
            this.InitializeComponent();
            this.panel1 = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel1");
            this.panel2 = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel2");
            this.btn_OK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_OK");
            this.lbl_FpHm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_FpHm");
            this.lbl_FpDm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_FpDm");
            this.pictureBox1 = this.xmlComponentLoader1.GetControlByName<AisinoPIC>("pictureBox1");
            this.lbl_JE = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_JE");
            this.lbl_XfSh = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_XfSh");
            this.lbl_GfSh = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_GfSh");
            this.lbl_KpRq = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_KpRq");
            this.lbl_ZfBz = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_ZfBz");
            this.lbl_SE = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_SE");
            this.lbl_BSZT = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL_BSZT");
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(HeDui));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x15f, 290);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Fpkj.Form.FPCX.HeDui\Aisino.Fwkp.Fpkj.Form.FPCX.HeDui.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x15f, 290);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "HeDui";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "核对窗口";
            base.ResumeLayout(false);
        }

        public bool setValue(Dictionary<string, object> dict)
        {
            try
            {
                this.lbl_FpDm.Text = "发票代码：";
                this.lbl_FpHm.Text = "发票号码：";
                this.lbl_KpRq.Text = "开票日期：";
                this.lbl_GfSh.Text = "购方税号：";
                this.lbl_XfSh.Text = "销方税号：";
                this.lbl_JE.Text = "金    额：";
                this.lbl_SE.Text = "税    额：";
                this.lbl_ZfBz.Text = "作废标志：";
                this.lbl_BSZT.Text = "报送状态：";
                if (base.TaxCardInstance.QYLX.ISTDQY)
                {
                    this.lbl_BSZT.Visible = false;
                }
                if (dict.Count <= 0)
                {
                    base.Close();
                    this.loger.Error("发票种类、发票代码、发票号码传入失败。");
                    MessageManager.ShowMsgBox("FPCX-000022");
                    return false;
                }
                if ((!dict.ContainsKey("lbl_FpHm") || !dict.ContainsKey("lbl_FpDm")) || ((!dict.ContainsKey("lbl_DZSYH") || !dict.ContainsKey("lbl_KPSXH")) || !dict.ContainsKey("lbl_KpRq")))
                {
                    base.Close();
                    this.loger.Error("发票种类、发票代码、发票号码传入失败。");
                    MessageManager.ShowMsgBox("FPCX-000022");
                    return false;
                }
                InvDetail detail = base.TaxCardInstance.QueryInvInfo(dict["lbl_FpDm"].ToString(), Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(dict["lbl_FpHm"]), dict["lbl_DZSYH"].ToString(), dict["lbl_KPSXH"].ToString(), Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDateTime(dict["lbl_KpRq"]));
                if (base.TaxCardInstance.RetCode != 0)
                {
                    MessageManager.ShowMsgBox(base.TaxCardInstance.ErrCode);
                    return false;
                }
                Fpxx fpxx = null;
                if (base.TaxCardInstance.SoftVersion != "FWKP_V2.0_Svr_Client")
                {
                    fpxx = new Fpxx();
                    fpxx.RepairInv(detail, -1);
                }
                else if (base.TaxCardInstance.SubSoftVersion != "Linux")
                {
                    fpxx = (Fpxx) SerializeUtil.Deserialize(ToolUtil.FromBase64String(detail.OldInvNo));
                }
                else
                {
                    fpxx = Fpxx.DeSeriealize_Linux(ToolUtil.FromBase64String(detail.OldInvNo));
                }
                if ((detail.TypeCode == "") && (detail.InvNo == 0))
                {
                    MessageManager.ShowMsgBox("FPCX-000031", new string[] { dict["lbl_FpDm"].ToString(), ShareMethods.FPHMTo8Wei(dict["lbl_FpHm"].ToString()) });
                    return false;
                }
                if (!string.IsNullOrEmpty(detail.TypeCode))
                {
                    this.lbl_FpDm.Text = this.lbl_FpDm.Text + detail.TypeCode.Trim();
                }
                this.lbl_FpHm.Text = this.lbl_FpHm.Text + ShareMethods.FPHMTo8Wei(detail.InvNo);
                if (((int)fpxx.fplx == 2) && ((int)fpxx.Zyfplx == 9))
                {
                    if (!string.IsNullOrEmpty(detail.SaleTaxCode))
                    {
                        this.lbl_GfSh.Text = this.lbl_GfSh.Text + detail.SaleTaxCode.Trim();
                    }
                    if (!string.IsNullOrEmpty(detail.BuyTaxCode))
                    {
                        this.lbl_XfSh.Text = this.lbl_XfSh.Text + detail.BuyTaxCode.Trim();
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(detail.BuyTaxCode))
                    {
                        if ((detail.InvType == 12) && !fpxx.isNewJdcfp)
                        {
                            if (fpxx.sfzhm.Length > 0)
                            {
                                this.lbl_GfSh.Text = "身份证号码/组织机构代码：" + fpxx.sfzhm;
                            }
                            else
                            {
                                this.lbl_GfSh.Text = "身份证号码/组织机构代码：" + new string('0', 15);
                            }
                        }
                        else
                        {
                            this.lbl_GfSh.Text = this.lbl_GfSh.Text + detail.BuyTaxCode.Trim();
                        }
                    }
                    if (!string.IsNullOrEmpty(detail.SaleTaxCode))
                    {
                        this.lbl_XfSh.Text = this.lbl_XfSh.Text + detail.SaleTaxCode.Trim();
                    }
                }
                DateTime date = detail.Date;
                this.lbl_KpRq.Text = this.lbl_KpRq.Text + detail.Date.ToString("yyyy年MM月dd日");
                this.lbl_JE.Text = this.lbl_JE.Text + Convert.ToString(detail.Amount.ToString("0.00"));
                this.lbl_SE.Text = this.lbl_SE.Text + Convert.ToString(detail.Tax.ToString("0.00"));
                if (detail.CancelFlag)
                {
                    this.lbl_ZfBz.Text = this.lbl_ZfBz.Text + "是";
                }
                else
                {
                    this.lbl_ZfBz.Text = this.lbl_ZfBz.Text + "否";
                }
                string str = "未报送";
                switch (fpxx.bszt)
                {
                    case 0:
                        str = "未报送";
                        break;

                    case 1:
                        str = "已报送";
                        break;

                    case 2:
                        str = "报送失败";
                        break;

                    case 3:
                        str = "报送中";
                        break;

                    case 4:
                        str = "验签失败";
                        break;

                    default:
                        str = "未报送";
                        break;
                }
                this.lbl_BSZT.Text = this.lbl_BSZT.Text + str;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
                return false;
            }
            return true;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x4e:
                case 13:
                case 14:
                case 20:
                    base.WndProc(ref m);
                    return;

                case 0x84:
                    this.DefWndProc(ref m);
                    if (m.Result.ToInt32() != 1)
                    {
                        break;
                    }
                    m.Result = new IntPtr(2);
                    return;

                case 0xa3:
                    Console.WriteLine(base.WindowState);
                    return;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }
    }
}

