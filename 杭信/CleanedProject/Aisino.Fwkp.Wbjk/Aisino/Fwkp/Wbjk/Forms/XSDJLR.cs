namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Registry;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.DAL;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class XSDJLR : BaseForm
    {
        private SaleBillCtrl billBL = SaleBillCtrl.Instance;
        private AisinoBTN btnOK;
        private AisinoBTN button2;
        private AisinoCHK checkBox_JiaoYan;
        private AisinoCMB comboBoxDJZL;
        private AisinoCMB comboBoxYF;
        private IContainer components = null;
        private AisinoGRP groupBox1;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private AisinoLBL label3;
        private List<string> listrMonths = new List<string>();
        private ILog log = LogUtil.GetLogger<XSDJLR>();
        private bool UseYear = true;

        public XSDJLR()
        {
            this.InitializeComponent();
            this.comboBoxYF.DropDownStyle = ComboBoxStyle.DropDown;
            this.comboBoxDJZL.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxYF.TextChanged += new EventHandler(this.comboBoxYF_TextChanged);
            if (this.UseYear)
            {
                this.btnOK.Click += new EventHandler(this.btnOK2_Click);
                this.comboBoxDJZL.SelectedIndexChanged += new EventHandler(this.comboBoxDJZL_SelectedIndexChanged);
            }
            else
            {
                this.btnOK.Click += new EventHandler(this.btnOK_Click);
            }
            this.button2.Click += new EventHandler(this.button2_Click);
            base.Load += new EventHandler(this.XSDJLR_Load);
            this.listrMonths.AddRange(new string[] { 
                "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "01", "02", "03", 
                "04", "05", "06", "07", "08", "09"
             });
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                PropValue.IsJiaoYan = this.checkBox_JiaoYan.Checked;
                int result = 0;
                bool flag = int.TryParse(this.comboBoxYF.Text, out result);
                string dJmonth = result.ToString();
                string dJtype = this.comboBoxDJZL.SelectedValue.ToString();
                switch (dJtype)
                {
                    case "a":
                    case "c":
                    case "s":
                    {
                        TaxCard taxCard = TaxCardFactory.CreateTaxCard();
                        if (!this.CanXTInv(taxCard))
                        {
                            MessageManager.ShowMsgBox("INP-242132");
                            return;
                        }
                        break;
                    }
                }
                AisinoDataSet set = new DJXGdal().QueryXSDJ(dJmonth, dJtype);
                if ((dJmonth != "0") && (set.get_Data().Rows.Count == 0))
                {
                    MessageManager.ShowMsgBox("没有符合条件的数据");
                }
                else
                {
                    this.billBL.XGDJZL = dJtype;
                    new DJXG(dJmonth, dJtype, this.checkBox_JiaoYan.Checked).ShowDialog();
                    base.Close();
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void btnOK2_Click(object sender, EventArgs e)
        {
            try
            {
                PropValue.IsJiaoYan = this.checkBox_JiaoYan.Checked;
                int result = 0;
                int num2 = 0;
                string s = "0";
                string str2 = "";
                string str3 = this.comboBoxYF.Text.Trim();
                if (str3 != "0")
                {
                    string[] strArray = str3.Split(new char[] { '-' });
                    if (strArray.Length == 2)
                    {
                        str2 = strArray[0].Trim();
                        s = strArray[1].Trim();
                    }
                }
                bool flag = int.TryParse(s, out result);
                bool flag2 = int.TryParse(str2, out num2);
                string dJmonth = result.ToString();
                string dJYear = "";
                if (num2 != 0)
                {
                    dJYear = num2.ToString();
                }
                string dJtype = this.comboBoxDJZL.SelectedValue.ToString();
                switch (dJtype)
                {
                    case "a":
                    case "c":
                    case "s":
                    {
                        TaxCard taxCard = TaxCardFactory.CreateTaxCard();
                        if (!this.CanXTInv(taxCard))
                        {
                            MessageManager.ShowMsgBox("INP-242132");
                            return;
                        }
                        break;
                    }
                }
                AisinoDataSet set = new DJXGdal().QueryXSDJ(dJYear, dJmonth, dJtype);
                if ((dJmonth != "0") && (set.get_Data().Rows.Count == 0))
                {
                    MessageManager.ShowMsgBox("没有符合条件的数据");
                }
                else
                {
                    this.billBL.XGDJZL = dJtype;
                    new DJXG(dJYear, dJmonth, dJtype, this.checkBox_JiaoYan.Checked).ShowDialog();
                    base.Close();
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private bool CanXTInv(TaxCard taxCard)
        {
            try
            {
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMIsNeedImportXTSP", null);
                if ((((objArray != null) && (objArray.Length > 0)) && (objArray[0] != null)) && Convert.ToBoolean(objArray[0]))
                {
                    ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMImportXTSP", null);
                }
                if (taxCard.get_QYLX().ISXT)
                {
                    object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMCheckXTSP", null);
                    if (!(((objArray2 != null) && (objArray2[0] is bool)) && Convert.ToBoolean(objArray2[0])))
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private void comboBoxDJZL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.UseYear)
            {
                string str = "";
                string str2 = "";
                string str3 = this.comboBoxDJZL.SelectedValue.ToString();
                if (str3 == "a")
                {
                    str = "s";
                    str2 = "c";
                }
                else
                {
                    str = str3;
                    str2 = str3;
                }
                this.comboBoxYF.Items.Clear();
                this.comboBoxYF.Items.AddRange(this.billBL.SaleBillYearMonth(str, str2));
                this.comboBoxYF.Text = "0";
            }
        }

        private void comboBoxYF_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int num = 0;
                bool flag = false;
                string item = this.comboBoxYF.Text.Trim();
                if (this.UseYear && (item != "0"))
                {
                    string[] strArray = item.Split(new char[] { '-' });
                    if (strArray.Length == 2)
                    {
                        item = strArray[1].Trim();
                    }
                }
                if (this.listrMonths.Contains(item))
                {
                    flag = true;
                }
                if (!flag)
                {
                    this.btnOK.Enabled = false;
                }
                else if ((num < 0) || (num > 12))
                {
                    this.btnOK.Enabled = false;
                }
                else
                {
                    this.btnOK.Enabled = true;
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
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

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(XSDJLR));
            this.label3 = new AisinoLBL();
            this.groupBox1 = new AisinoGRP();
            this.label1 = new AisinoLBL();
            this.label2 = new AisinoLBL();
            this.comboBoxYF = new AisinoCMB();
            this.comboBoxDJZL = new AisinoCMB();
            this.checkBox_JiaoYan = new AisinoCHK();
            this.button2 = new AisinoBTN();
            this.btnOK = new AisinoBTN();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.label3.AutoSize = true;
            this.label3.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label3.Location = new Point(0x3f, 0x1f);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x11f, 0x38);
            this.label3.TabIndex = 10;
            this.label3.Text = "【注意事项】\r\n1.修改仅限于本月尚未开具发票的销售单据\r\n2.输入月份为零时，可查询修改所有尚未全部\r\n开具发票的销售单据";
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBoxYF);
            this.groupBox1.Controls.Add(this.comboBoxDJZL);
            this.groupBox1.Controls.Add(this.checkBox_JiaoYan);
            this.groupBox1.Location = new Point(0x3f, 0x71);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x11f, 0x66);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择条件";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x1d, 0x1c);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "月份";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(5, 0x39);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "单据种类";
            this.comboBoxYF.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxYF.FormattingEnabled = true;
            this.comboBoxYF.Location = new Point(0x40, 0x19);
            this.comboBoxYF.Name = "comboBoxYF";
            this.comboBoxYF.Size = new Size(0x84, 20);
            this.comboBoxYF.TabIndex = 2;
            this.comboBoxDJZL.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxDJZL.FormattingEnabled = true;
            this.comboBoxDJZL.Location = new Point(0x40, 0x36);
            this.comboBoxDJZL.Name = "comboBoxDJZL";
            this.comboBoxDJZL.Size = new Size(0xce, 20);
            this.comboBoxDJZL.TabIndex = 3;
            this.checkBox_JiaoYan.AutoSize = true;
            this.checkBox_JiaoYan.Location = new Point(7, 80);
            this.checkBox_JiaoYan.Name = "checkBox_JiaoYan";
            this.checkBox_JiaoYan.Size = new Size(0x9c, 0x10);
            this.checkBox_JiaoYan.TabIndex = 4;
            this.checkBox_JiaoYan.Text = "退出窗口时进行单据校验";
            this.checkBox_JiaoYan.UseVisualStyleBackColor = true;
            this.button2.set_BackColorActive(Color.FromArgb(0x19, 0x76, 210));
            this.button2.set_ColorDefaultA(Color.FromArgb(0, 0xac, 0xfb));
            this.button2.set_ColorDefaultB(Color.FromArgb(0, 0x91, 0xe0));
            this.button2.Font = new Font("宋体", 9f);
            this.button2.set_FontColor(Color.FromArgb(0xff, 0xff, 0xff));
            this.button2.Location = new Point(0xea, 0xef);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 7;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.btnOK.set_BackColorActive(Color.FromArgb(0x19, 0x76, 210));
            this.btnOK.set_ColorDefaultA(Color.FromArgb(0, 0xac, 0xfb));
            this.btnOK.set_ColorDefaultB(Color.FromArgb(0, 0x91, 0xe0));
            this.btnOK.Font = new Font("宋体", 9f);
            this.btnOK.set_FontColor(Color.FromArgb(0xff, 0xff, 0xff));
            this.btnOK.Location = new Point(110, 0xef);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1a2, 0x123);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.btnOK);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "XSDJLR";
            this.Text = "销售单据录入查询";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void XSDJLR_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.UseYear)
                {
                    this.comboBoxDJZL.SelectedIndexChanged -= new EventHandler(this.comboBoxDJZL_SelectedIndexChanged);
                }
                this.comboBoxYF.Items.AddRange(this.billBL.SaleBillMonth());
                this.comboBoxYF.SelectedIndex = 0;
                this.comboBoxDJZL.DataSource = CbbXmlBind.ReadXmlNode("InvType", true);
                this.comboBoxDJZL.DisplayMember = "Value";
                this.comboBoxDJZL.ValueMember = "Key";
                this.checkBox_JiaoYan.Checked = PropValue.IsJiaoYan;
                if (this.UseYear)
                {
                    string str = "";
                    string str2 = "";
                    string str3 = this.comboBoxDJZL.SelectedValue.ToString();
                    if (str3 == "a")
                    {
                        str = "s";
                        str2 = "c";
                    }
                    else
                    {
                        str = str3;
                    }
                    this.comboBoxYF.Items.Clear();
                    this.comboBoxYF.Items.AddRange(this.billBL.SaleBillYearMonth(str, str2));
                }
                if (RegisterManager.CheckRegFile("JIJS"))
                {
                    TaxCard card = TaxCardFactory.CreateTaxCard();
                    if (card.get_QYLX().ISPTFP && card.get_QYLX().ISZYFP)
                    {
                        this.comboBoxDJZL.SelectedIndex = 2;
                        this.comboBoxDJZL.Enabled = false;
                    }
                    else if (card.get_QYLX().ISPTFP)
                    {
                        this.btnOK.Enabled = false;
                    }
                    else if (card.get_QYLX().ISZYFP)
                    {
                        this.comboBoxDJZL.SelectedIndex = 1;
                        this.comboBoxDJZL.Enabled = false;
                    }
                    else
                    {
                        this.btnOK.Enabled = false;
                    }
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
            finally
            {
                if (this.UseYear)
                {
                    this.comboBoxDJZL.SelectedIndexChanged += new EventHandler(this.comboBoxDJZL_SelectedIndexChanged);
                }
            }
        }
    }
}

