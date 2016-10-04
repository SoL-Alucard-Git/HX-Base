namespace Aisino.Framework.Plugin.Core.Controls
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.FTaxBase;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    public class BaseForm : SkinForm
    {
        protected ContextMenuStrip _menu;
        public string FormTagFlag;
        private IContainer icontainer_1;
        protected TaxCard TaxCardInstance;

        public BaseForm()
        {
            
            this.TaxCardInstance = TaxCardFactory.CreateTaxCard();
            this.FormTagFlag = string.Empty;
        }

        public BaseForm(bool bool_0)
        {
            
            this.TaxCardInstance = TaxCardFactory.CreateTaxCard();
            this.FormTagFlag = string.Empty;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_1 != null))
            {
                this.icontainer_1.Dispose();
            }
            base.Dispose(disposing);
        }

        private void method_0()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(BaseForm));
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0xe3, 0xee, 0xf9);
            base.ClientSize = new Size(0x29d, 0x1b1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(0, 0);
            base.Name = "BaseForm";
            this.Text = "BaseForm";
            base.ResumeLayout(false);
        }
    }
}

