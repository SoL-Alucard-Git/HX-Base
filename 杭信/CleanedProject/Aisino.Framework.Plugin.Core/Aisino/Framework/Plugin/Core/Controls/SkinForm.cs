namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class SkinForm : Form
    {
        private ToolStripMenuItem aToolStripMenuItem;
        private ToolStripMenuItem bToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip1;
        private IContainer icontainer_0;
        private ToolStripMenuItem toolStripMenuItem1;

        public SkinForm()
        {
            
            this.InitializeComponent();
            base.Shown += new EventHandler(this.SkinForm_Shown);
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
            this.icontainer_0 = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(SkinForm));
            this.contextMenuStrip1 = new ContextMenuStrip(this.icontainer_0);
            this.toolStripMenuItem1 = new ToolStripMenuItem();
            this.aToolStripMenuItem = new ToolStripMenuItem();
            this.bToolStripMenuItem = new ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripMenuItem1, this.aToolStripMenuItem, this.bToolStripMenuItem });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0x55, 70);
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new Size(0x54, 0x16);
            this.toolStripMenuItem1.Text = "c";
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Size = new Size(0x54, 0x16);
            this.aToolStripMenuItem.Text = "a";
            this.bToolStripMenuItem.Name = "bToolStripMenuItem";
            this.bToolStripMenuItem.Size = new Size(0x54, 0x16);
            this.bToolStripMenuItem.Text = "b";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x198, 0x153);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "SkinFormDemo";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "skin";
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void SkinForm_Shown(object sender, EventArgs e)
        {
            base.Icon = Class131.smethod_16();
            this.BackColor = Color.White;
        }
    }
}

