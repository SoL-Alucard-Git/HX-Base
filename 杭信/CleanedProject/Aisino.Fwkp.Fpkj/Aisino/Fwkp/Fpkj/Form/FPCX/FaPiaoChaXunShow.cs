namespace Aisino.Fwkp.Fpkj.Form.FPCX
{
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Framework.Plugin.Core.Util;
    public class FaPiaoChaXunShow : FaPiaoChaXun
    {
        private IContainer components;
        private ILog loger = LogUtil.GetLogger<FaPiaoChaXunShow>();

        public FaPiaoChaXunShow()
        {
            try
            {
                this.InitializeComponent();
                base.TabText = "选择发票号码查询";
                this.Text = "选择发票号码查询";
            }
            catch (Exception exception)
            {
                this.loger.Error("FaPiaoChaXunShow" + exception.Message);
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
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.Font;
            this.Text = "FaPiaoChaXunShow";
        }
    }
}

