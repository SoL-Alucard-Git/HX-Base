namespace Aisino.Fwkp.Fplygl.Form.FJFPGL_2
{
    using Aisino.Fwkp.Fplygl.Form.AbsForms;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public class ChooseReclaimMedium : ChooseMedium
    {
        private IContainer components;

        public ChooseReclaimMedium()
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
            this.Text = "收回剩余发票介质选择";
            base.groupBox.Text = "请选择收回剩余发票的分机介质";
        }
    }
}

