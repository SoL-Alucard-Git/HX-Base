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
    using System.Windows.Forms;

    public class DyQueRen : BaseForm
    {
        private AisinoBTN but_queding;
        private AisinoBTN but_quxiao;
        private IContainer components;
        private AisinoLBL lab_Fpdm;
        private AisinoLBL lab_Fphm;
        private AisinoLBL lab_Fpzl;
        private ILog loger = LogUtil.GetLogger<DyQueRen>();
        private XmlComponentLoader xmlComponentLoader1;

        public DyQueRen()
        {
            try
            {
                this.Initialize();
                base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                base.FormBorderStyle = FormBorderStyle.FixedDialog;
                base.ControlBox = false;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        private void btnQueding_Click(object sender, EventArgs e)
        {
            try
            {
                base.DialogResult = DialogResult.OK;
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
                base.DialogResult = DialogResult.Cancel;
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
            this.lab_Fpzl = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_fpzl");
            this.lab_Fpdm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_fpdm");
            this.lab_Fphm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_fphm");
            this.but_quxiao = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_quxiao");
            this.but_queding = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_queding");
            this.but_quxiao.Click += new EventHandler(this.btnQuxiao_Click);
            this.but_queding.Click += new EventHandler(this.btnQueding_Click);
            this.but_queding.TabIndex = 0;
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
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Fpkj.Form.Dy.DyQueRen\Aisino.Fwkp.Fpkj.Form.Dy.DyQueRen.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(420, 0x109);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Name = "DyQueRen";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "确认对话框";
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
                this.lab_Fpzl.Text = (string) dict["lbl_Fpzl"];
                this.lab_Fpdm.Text = (string) dict["lbl_Fpdm"];
                this.lab_Fphm.Text = ShareMethods.FPHMTo8Wei(dict["lbl_Fphm"].ToString());
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
    }
}

