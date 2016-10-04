namespace Aisino.Fwkp.Fplygl.Form.FPLYGL_1
{
    using Aisino.Fwkp.Fplygl.Form.AbsForms;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public class ChooseReadMedium : ChooseMedium
    {
        private IContainer components;

        public ChooseReadMedium()
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

        private void Initialize()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.Font;
            this.Text = "读入介质选择";
            base.groupBox.Text = "请选择读入发票的来源介质";
        }
    }
}

