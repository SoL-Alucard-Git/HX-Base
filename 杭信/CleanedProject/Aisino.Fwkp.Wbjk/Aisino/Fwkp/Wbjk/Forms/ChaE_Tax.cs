namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ChaE_Tax : Form
    {
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private IContainer components = null;
        private Label lab_Hsxse;
        private Label lab_Kce;
        private TextBox tb_Hsxse;
        private TextBox tb_Kce;

        public ChaE_Tax()
        {
            this.InitializeComponent();
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
            this.lab_Hsxse = new Label();
            this.tb_Hsxse = new TextBox();
            this.tb_Kce = new TextBox();
            this.lab_Kce = new Label();
            this.btnOK = new AisinoBTN();
            this.btnCancel = new AisinoBTN();
            base.SuspendLayout();
            this.lab_Hsxse.AutoSize = true;
            this.lab_Hsxse.Location = new Point(6, 30);
            this.lab_Hsxse.Name = "lab_Hsxse";
            this.lab_Hsxse.Size = new Size(80, 0x11);
            this.lab_Hsxse.TabIndex = 0;
            this.lab_Hsxse.Text = "含税销售额：";
            this.tb_Hsxse.Location = new Point(0x55, 0x19);
            this.tb_Hsxse.Margin = new Padding(3, 4, 3, 4);
            this.tb_Hsxse.Name = "tb_Hsxse";
            this.tb_Hsxse.Size = new Size(0x9b, 0x17);
            this.tb_Hsxse.TabIndex = 1;
            this.tb_Hsxse.KeyPress += new KeyPressEventHandler(this.tb_Hsxse_KeyPress);
            this.tb_Kce.Location = new Point(0x55, 0x3e);
            this.tb_Kce.Margin = new Padding(3, 4, 3, 4);
            this.tb_Kce.Name = "tb_Kce";
            this.tb_Kce.Size = new Size(0x9b, 0x17);
            this.tb_Kce.TabIndex = 3;
            this.tb_Kce.KeyPress += new KeyPressEventHandler(this.tb_Kce_KeyPress);
            this.lab_Kce.AutoSize = true;
            this.lab_Kce.Location = new Point(30, 0x44);
            this.lab_Kce.Name = "lab_Kce";
            this.lab_Kce.Size = new Size(0x38, 0x11);
            this.lab_Kce.TabIndex = 2;
            this.lab_Kce.Text = "扣除额：";
            this.btnOK.set_BackColorActive(Color.FromArgb(0x19, 0x76, 210));
            this.btnOK.set_ColorDefaultA(Color.FromArgb(0, 0xac, 0xfb));
            this.btnOK.set_ColorDefaultB(Color.FromArgb(0, 0x91, 0xe0));
            this.btnOK.Font = new Font("宋体", 9f);
            this.btnOK.set_FontColor(Color.FromArgb(0xff, 0xff, 0xff));
            this.btnOK.Location = new Point(0xf6, 0x19);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnCancel.set_BackColorActive(Color.FromArgb(0x19, 0x76, 210));
            this.btnCancel.set_ColorDefaultA(Color.FromArgb(0, 0xac, 0xfb));
            this.btnCancel.set_ColorDefaultB(Color.FromArgb(0, 0x91, 0xe0));
            this.btnCancel.Font = new Font("宋体", 9f);
            this.btnCancel.set_FontColor(Color.FromArgb(0xff, 0xff, 0xff));
            this.btnCancel.Location = new Point(0xf6, 0x3e);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(7f, 17f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x169, 0x71);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.tb_Kce);
            base.Controls.Add(this.lab_Kce);
            base.Controls.Add(this.tb_Hsxse);
            base.Controls.Add(this.lab_Hsxse);
            this.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.Margin = new Padding(3, 4, 3, 4);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ChaE_Tax";
            this.Text = "输入含税销售额、扣除额";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void tb_Hsxse_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar == '\b') || char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void tb_Kce_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar == '\b') || char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
        }
    }
}

