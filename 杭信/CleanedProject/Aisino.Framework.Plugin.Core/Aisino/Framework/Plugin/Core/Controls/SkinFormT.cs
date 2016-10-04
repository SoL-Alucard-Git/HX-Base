namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class SkinFormT : Form
    {
        private IContainer icontainer_0;

        public SkinFormT()
        {
            
            this.InitializeComponent();
            base.Shown += new EventHandler(this.SkinFormT_Shown);
        }

        public void AddToolButton(ToolButton toolButton_0)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(SkinFormT));
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x25b, 0x177);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "SkinForm";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Form2";
            base.ResumeLayout(false);
        }

        private void SkinFormT_Shown(object sender, EventArgs e)
        {
            base.Icon = Class131.smethod_16();
            this.BackColor = Color.White;
        }

        public class ToolButton
        {
            private bool bool_0;
            private Image image_0;
            private Image image_1;
            private System.Drawing.Size size_0;

            public event EventHandler Click;

            public event EventHandler StartInit;

            public ToolButton(System.Drawing.Size size_1, Image image_2, Image image_3)
            {
                
                this.size_0 = new System.Drawing.Size(0x12, 0x12);
                this.bool_0 = true;
                this.size_0 = size_1;
                this.image_0 = image_2;
                this.image_1 = image_3;
            }

            public void OnClick()
            {
                if (this.Click != null)
                {
                    this.Click(this, null);
                }
            }

            public void OnStartInit()
            {
                if (this.StartInit != null)
                {
                    this.StartInit(this, null);
                }
            }

            public Image Default
            {
                get
                {
                    return this.image_0;
                }
            }

            public Image Move
            {
                get
                {
                    return this.image_1;
                }
            }

            public System.Drawing.Size Size
            {
                get
                {
                    if (this.bool_0)
                    {
                        return this.size_0;
                    }
                    return System.Drawing.Size.Empty;
                }
            }

            public bool Start
            {
                get
                {
                    return this.bool_0;
                }
                set
                {
                    this.bool_0 = value;
                }
            }
        }
    }
}

