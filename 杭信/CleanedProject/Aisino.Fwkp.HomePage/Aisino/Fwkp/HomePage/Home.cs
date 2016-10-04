namespace Aisino.Fwkp.HomePage
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Startup.Login;
    using Aisino.Fwkp.HomePage.AisinoDock;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Home : DockForm
    {
        private IContainer components;
        private ContextMenuStrip contextMenuStrip1;
        private PageControl pageControl1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;

        public Home()
        {
            this.InitializeComponent();
            this.pageControl1.init(UserInfo.get_Yhmc());
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
            this.pageControl1 = new PageControl();
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new ToolStripMenuItem();
            this.toolStripMenuItem3 = new ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.pageControl1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.pageControl1.BackColor = Color.White;
            this.pageControl1.IsEdit = false;
            this.pageControl1.Location = new Point(-4, -1);
            this.pageControl1.MetorColor = Color.FromArgb(0xbf, 0xcf, 0xd8);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.Size = new Size(0x256, 0x1b9);
            this.pageControl1.TabIndex = 0;
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripMenuItem2, this.toolStripMenuItem3 });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0x99, 70);
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new Size(0x98, 0x16);
            this.toolStripMenuItem2.Text = "111";
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new Size(0x98, 0x16);
            this.toolStripMenuItem3.Text = "222";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x250, 0x1b7);
            base.Controls.Add(this.pageControl1);
            base.Name = "Home";
            base.set_TabText("首页");
            this.Text = "首页";
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
        }
    }
}

