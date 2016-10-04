namespace Aisino.Fwkp.HomePage.AisinoDock.Docks
{
    using Aisino.Fwkp.HomePage.AisinoDock;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    public class TongjiDock : IDock
    {
        private static JScardDock _dock;
        private IContainer components;

        public TongjiDock()
        {
            this.InitializeComponent();
        }

        public TongjiDock(PageControl page) : base(page)
        {
            this.InitializeComponent();
        }

        public static IDock CreateDock(PageControl page)
        {
            if ((_dock == null) || _dock.IsDisposed)
            {
                _dock = new JScardDock(page);
            }
            return _dock;
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
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.Name = "TongjiDock";
            base.Size = new Size(0x18c, 0x182);
            base.Title = "统计信息";
            base.ResumeLayout(false);
        }
    }
}

