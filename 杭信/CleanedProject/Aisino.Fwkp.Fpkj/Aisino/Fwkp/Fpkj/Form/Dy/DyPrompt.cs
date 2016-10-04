namespace Aisino.Fwkp.Fpkj.Form.Dy
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Fpkj.Common;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class DyPrompt : BaseForm
    {
        private bool _bAllDaYin;
        private AisinoBTN but_AllDaYin;
        private AisinoBTN but_DaYin;
        private AisinoBTN but_quxiao;
        private AisinoBTN but_TiaoGuo;
        private IContainer components;
        private AisinoGRP groupBox1;
        private AisinoLBL lab_Fpdm;
        private AisinoLBL lab_Fphm;
        private AisinoLBL lab_Fpzl;
        private AisinoLBL lab_Msg;
        private AisinoRTX lab_Msg2;
        private ILog loger = LogUtil.GetLogger<DyPrompt>();
        private XmlComponentLoader xmlComponentLoader1;

        public DyPrompt()
        {
            try
            {
                this.Initialize();
                this._bAllDaYin = false;
                this.groupBox1.Visible = true;
                base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                base.FormBorderStyle = FormBorderStyle.FixedDialog;
                base.ControlBox = false;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        private void btnAllDaYin_Click(object sender, EventArgs e)
        {
            try
            {
                this._bAllDaYin = true;
                base.DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        private void btnDaYin_Click(object sender, EventArgs e)
        {
            try
            {
                this._bAllDaYin = false;
                base.DialogResult = DialogResult.Yes;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        private void btnQuxiao_Click(object sender, EventArgs e)
        {
            try
            {
                this._bAllDaYin = false;
                base.DialogResult = DialogResult.Cancel;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        private void btnTiaoGuo_Click(object sender, EventArgs e)
        {
            try
            {
                this._bAllDaYin = false;
                base.DialogResult = DialogResult.Abort;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
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
            this.lab_Fpzl = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_Fpzl");
            this.but_quxiao = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_quxiao");
            this.but_TiaoGuo = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_TiaoGuo");
            this.groupBox1 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox1");
            this.lab_Msg2 = this.xmlComponentLoader1.GetControlByName<AisinoRTX>("lab_Msg2");
            this.lab_Msg = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_Msg");
            this.lab_Fpdm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_Fpdm");
            this.lab_Fphm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_Fphm");
            this.but_AllDaYin = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_AllDaYin");
            this.but_DaYin = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_DaYin");
            this.but_quxiao.Click += new EventHandler(this.btnQuxiao_Click);
            this.but_TiaoGuo.Click += new EventHandler(this.btnTiaoGuo_Click);
            this.but_AllDaYin.Click += new EventHandler(this.btnAllDaYin_Click);
            this.but_DaYin.Click += new EventHandler(this.btnDaYin_Click);
            this.but_AllDaYin.TabIndex = 0;
            this.lab_Msg2.BorderStyle = BorderStyle.None;
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(420, 0x109);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Fpkj.Form.Dy.DyPrompt\Aisino.Fwkp.Fpkj.Form.Dy.DyPrompt.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(420, 0x109);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Name = "DyPrompt";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "打印提示";
            base.ResumeLayout(false);
        }

        public bool setValue(Dictionary<string, object> dict)
        {
            try
            {
                this.lab_Fpzl.Text = "发票种类：";
                this.lab_Fpdm.Text = "开票代码：";
                this.lab_Fphm.Text = "发票号码：";
                if (dict.Count <= 0)
                {
                    base.Close();
                    this.loger.Error("发票种类、发票代码、发票号码传入失败。");
                    MessageManager.ShowMsgBox("FPCX-000022");
                    return false;
                }
                if ((!dict.ContainsKey("lbl_Fpzl") || !dict.ContainsKey("lbl_Fpdm")) || !dict.ContainsKey("lbl_Fphm"))
                {
                    base.Close();
                    this.loger.Error("发票种类、发票代码、发票号码传入失败。");
                    MessageManager.ShowMsgBox("FPCX-000022");
                    return false;
                }
                this.lab_Fpzl.Text = this.lab_Fpzl.Text + dict["lbl_Fpzl"];
                this.lab_Fpdm.Text = this.lab_Fpdm.Text + dict["lbl_Fpdm"];
                this.lab_Fphm.Text = this.lab_Fphm.Text + ShareMethods.FPHMTo8Wei(dict["lbl_Fphm"].ToString());
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
            return true;
        }

        public bool setValue(Dictionary<string, object> dict, string Msg, bool ISQD = false)
        {
            try
            {
                this.setValue(dict);
                this.lab_Msg2.Text = Msg;
                if (!ISQD)
                {
                    this.lab_Msg.Text = "正在打印发票......";
                }
                else
                {
                    this.lab_Msg.Text = "正在打印清单......";
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
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

        public bool AllDaYin
        {
            get
            {
                return this._bAllDaYin;
            }
        }
    }
}

