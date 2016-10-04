namespace Aisino.Framework.MainForm
{
    using Aisino.Framework.MainForm.Properties;
    using Aisino.Framework.Plugin.Core.Util;
    using ns5;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class SysModifyNavForm : BaseNavForm
    {
        private Button btnJSGL;
        private Button btnSJBF;
        private Button btnSJQY;
        private Button btnYHGL;
        private IContainer icontainer_1;
        private List<string> list_0;
        private List<string> list_1;

        public SysModifyNavForm()
        {
            
            this.InitializeComponent_1();
            base.FormTagFlag = "NavForm";
            this.list_0 = new List<string>();
            this.list_1 = new List<string>();
            base.Load += new EventHandler(this.SysModifyNavForm_Load);
            this.list_1.Add("Aisino.Fwkp.DataMigrationTool.Entry.DataMigrationToolEntry");
            this.list_1.Add("Aisino.Fwkp.Sjbf.RunCopy_Execution");
            this.list_1.Add("Menu.Xtgl.Qxsz.Jsgl");
            this.list_1.Add("Menu.Xtgl.Qxsz.Yhgl");
        }

        private void btnJSGL_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Xtgl.Qxsz.Jsgl");
        }

        private void btnSJBF_Click(object sender, EventArgs e)
        {
            this.method_1("Aisino.Fwkp.Sjbf.RunCopy_Execution");
        }

        private void btnSJQY_Click(object sender, EventArgs e)
        {
            this.method_1("Aisino.Fwkp.DataMigrationTool.Entry.DataMigrationToolEntry");
        }

        private void btnYHGL_Click(object sender, EventArgs e)
        {
            this.method_1("Menu.Xtgl.Qxsz.Yhgl");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_1 != null))
            {
                this.icontainer_1.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent_1()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(SysModifyNavForm));
            this.btnSJBF = new Button();
            this.btnYHGL = new Button();
            this.btnJSGL = new Button();
            this.btnSJQY = new Button();
            base.NavPanle.SuspendLayout();
            base.SuspendLayout();
            base.NavPanle.BackgroundImage = Resources.smethod_44();
            base.NavPanle.Controls.Add(this.btnSJBF);
            base.NavPanle.Controls.Add(this.btnYHGL);
            base.NavPanle.Controls.Add(this.btnJSGL);
            base.NavPanle.Controls.Add(this.btnSJQY);
            this.btnSJBF.Image = Resources.smethod_37();
            this.btnSJBF.Location = new Point(480, 0x85);
            this.btnSJBF.Name = "btnSJBF";
            this.btnSJBF.Size = new Size(0x41, 0x4b);
            this.btnSJBF.TabIndex = 20;
            this.btnSJBF.UseVisualStyleBackColor = true;
            this.btnSJBF.Click += new EventHandler(this.btnSJBF_Click);
            this.btnYHGL.Image = Resources.smethod_47();
            this.btnYHGL.Location = new Point(480, 0x141);
            this.btnYHGL.Name = "btnYHGL";
            this.btnYHGL.Size = new Size(0x41, 0x4b);
            this.btnYHGL.TabIndex = 0x15;
            this.btnYHGL.UseVisualStyleBackColor = true;
            this.btnYHGL.Click += new EventHandler(this.btnYHGL_Click);
            this.btnJSGL.Image = Resources.smethod_48();
            this.btnJSGL.Location = new Point(0xf3, 0x141);
            this.btnJSGL.Name = "btnJSGL";
            this.btnJSGL.Size = new Size(0x41, 0x4b);
            this.btnJSGL.TabIndex = 0x12;
            this.btnJSGL.UseVisualStyleBackColor = true;
            this.btnJSGL.Click += new EventHandler(this.btnJSGL_Click);
            this.btnSJQY.Image = Resources.smethod_38();
            this.btnSJQY.Location = new Point(0xf3, 0x85);
            this.btnSJQY.Name = "btnSJQY";
            this.btnSJQY.Size = new Size(0x41, 0x4b);
            this.btnSJQY.TabIndex = 0x13;
            this.btnSJQY.UseVisualStyleBackColor = true;
            this.btnSJQY.Click += new EventHandler(this.btnSJQY_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(800, 530);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "SysModifyNavForm";
            this.Text = "系统维护";
            base.Load += new EventHandler(this.SysModifyNavForm_Load);
            base.NavPanle.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void method_0()
        {
            this.btnSJQY.Enabled = this.list_0.Contains("Aisino.Fwkp.DataMigrationTool.Entry.DataMigrationToolEntry");
            this.btnSJBF.Enabled = this.list_0.Contains("Aisino.Fwkp.Sjbf.RunCopy_Execution");
            this.btnJSGL.Enabled = this.list_0.Contains("Menu.Xtgl.Qxsz.Jsgl");
            this.btnYHGL.Enabled = this.list_0.Contains("Menu.Xtgl.Qxsz.Yhgl");
        }

        private void method_1(string string_0)
        {
            ToolUtil.RunFuction(string_0);
        }

        private void SysModifyNavForm_Load(object sender, EventArgs e)
        {
            Class82.smethod_0(base.MdiParent.MainMenuStrip, this.list_1, this.list_0);
            this.method_0();
        }
    }
}

