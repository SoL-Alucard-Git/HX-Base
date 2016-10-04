namespace Aisino.Fwkp.Fplygl.Form.FJFPGL_2
{
    using Aisino.Fwkp.Fplygl.Form.AbsForms;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public class ChooseAllocateMedium : ChooseMedium
    {
        private IContainer components;

        public ChooseAllocateMedium()
        {
            this.Initialize();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public override string GetDeviceType()
        {
            if (!base.isJinshuipan && base.isBaoshuipan)
            {
                return "3";
            }
            return "1";
        }

        private void Initialize()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.Font;
            this.Text = "分配介质选择";
            base.groupBox.Text = "请选择分配发票的分机介质";
        }
    }
}

