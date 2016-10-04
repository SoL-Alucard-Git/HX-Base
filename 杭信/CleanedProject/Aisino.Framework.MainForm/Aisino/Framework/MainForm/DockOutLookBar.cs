namespace Aisino.Framework.MainForm
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Controls.OutlookBar;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class DockOutLookBar : DockForm
    {
        internal bool bool_0;
        private IContainer icontainer_3;
        private Aisino.Framework.Plugin.Core.Controls.OutlookBar.OutlookBar outlookBar1;

        public DockOutLookBar(object object_0)
        {
            
            this.InitializeComponent_1();
            this.BackColor = Color.FromArgb(0xf3, 0xf3, 0xf3);
            TreeLoader.Load(this.outlookBar1, object_0, "/Aisino/Tree", false);
            this.bool_0 = this.outlookBar1.Nodes.Count > 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_3 != null))
            {
                this.icontainer_3.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent_1()
        {
            this.outlookBar1 = new Aisino.Framework.Plugin.Core.Controls.OutlookBar.OutlookBar();
            base.SuspendLayout();
            this.outlookBar1.AutoScroll = true;
            this.outlookBar1.BackColor = Color.FromArgb(0xe3, 0xee, 0xf9);
            this.outlookBar1.Dock = DockStyle.Fill;
            this.outlookBar1.Location = new Point(0, 0);
            this.outlookBar1.Name = "outlookBar1";
            this.outlookBar1.Size = new Size(0xb3, 0x1d0);
            this.outlookBar1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0xb3, 0x1d0);
            base.ControlBox = false;
            base.Controls.Add(this.outlookBar1);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "DockOutLookBar";
            base.ShowIcon = false;
            this.Text = "导航栏";
            base.ResumeLayout(false);
        }

        internal void method_3(object object_0)
        {
            this.outlookBar1.Nodes.Clear();
            TreeLoader.Load(this.outlookBar1, object_0, "/Aisino/Tree", false);
            this.bool_0 = this.outlookBar1.Nodes.Count > 0;
        }

        public bool HasChild
        {
            get
            {
                return this.bool_0;
            }
        }
    }
}

