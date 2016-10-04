namespace Aisino.Fwkp.Hzfp.Form
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ChaE_Tax : BaseForm
    {
        protected AisinoButton btn_Cencel;
        protected AisinoButton btn_Confirm;
        private IContainer components;
        private GroupBox groupBox1;
        public double kce;
        private Label lab_Kce;
        private TextBox tb_Kce;

        public ChaE_Tax()
        {
            this.InitializeComponent();
        }

        private void btn_Cencel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Close();
        }

        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(this.tb_Kce.Text, out this.kce))
            {
                MessageBox.Show("输入信息格式有误！");
            }
            else if (this.kce > 10000000000)
            {
                MessageBox.Show("输入扣除额过大！");
            }
            else
            {
                base.DialogResult = DialogResult.OK;
            }
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ChaE_Tax));
            this.tb_Kce = new TextBox();
            this.lab_Kce = new Label();
            this.btn_Confirm = new AisinoButton();
            this.btn_Cencel = new AisinoButton();
            this.groupBox1 = new GroupBox();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.tb_Kce.Location = new Point(0x44, 0x1f);
            this.tb_Kce.Margin = new Padding(3, 4, 3, 4);
            this.tb_Kce.Name = "tb_Kce";
            this.tb_Kce.Size = new Size(0x9b, 0x17);
            this.tb_Kce.TabIndex = 3;
            this.tb_Kce.KeyPress += new KeyPressEventHandler(this.tb_Kce_KeyPress);
            this.lab_Kce.AutoSize = true;
            this.lab_Kce.Location = new Point(6, 0x25);
            this.lab_Kce.Name = "lab_Kce";
            this.lab_Kce.Size = new Size(0x38, 0x11);
            this.lab_Kce.TabIndex = 2;
            this.lab_Kce.Text = "扣除额：";
            this.btn_Confirm.BackColorActive=Color.FromArgb(0x19, 0x76, 210);
            this.btn_Confirm.ColorDefaultA=Color.FromArgb(0, 0xac, 0xfb);
            this.btn_Confirm.ColorDefaultB=Color.FromArgb(0, 0x91, 0xe0);
            this.btn_Confirm.Font = new Font("宋体", 9f);
            this.btn_Confirm.FontColor=Color.FromArgb(0xff, 0xff, 0xff);
            this.btn_Confirm.Imagesize=0x19;
            this.btn_Confirm.Location = new Point(0x43, 0x44);
            this.btn_Confirm.Name = "btn_Confirm";
            this.btn_Confirm.Size = new Size(0x4b, 0x17);
            this.btn_Confirm.TabIndex = 4;
            this.btn_Confirm.Text = "确认";
            this.btn_Confirm.UseVisualStyleBackColor = true;
            this.btn_Confirm.Click += new EventHandler(this.btn_Confirm_Click);
            this.btn_Cencel.BackColorActive=Color.FromArgb(0x19, 0x76, 210);
            this.btn_Cencel.ColorDefaultA=Color.FromArgb(0, 0xac, 0xfb);
            this.btn_Cencel.ColorDefaultB=Color.FromArgb(0, 0x91, 0xe0);
            this.btn_Cencel.Font = new Font("宋体", 9f);
            this.btn_Cencel.FontColor=Color.FromArgb(0xff, 0xff, 0xff);
            this.btn_Cencel.Imagesize=0x19;
            this.btn_Cencel.Location = new Point(0x94, 0x44);
            this.btn_Cencel.Name = "btn_Cencel";
            this.btn_Cencel.Size = new Size(0x4b, 0x17);
            this.btn_Cencel.TabIndex = 5;
            this.btn_Cencel.Text = "取消";
            this.btn_Cencel.UseVisualStyleBackColor = true;
            this.btn_Cencel.Click += new EventHandler(this.btn_Cencel_Click);
            this.groupBox1.Controls.Add(this.btn_Confirm);
            this.groupBox1.Controls.Add(this.btn_Cencel);
            this.groupBox1.Controls.Add(this.lab_Kce);
            this.groupBox1.Controls.Add(this.tb_Kce);
            this.groupBox1.Location = new Point(9, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xf2, 0x61);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "友情提示：以下信息输入后无法修改！";
            base.AutoScaleDimensions = new SizeF(7f, 17f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x109, 0x79);
            base.Controls.Add(this.groupBox1);
            this.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Margin = new Padding(3, 4, 3, 4);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ChaE_Tax";
            this.Text = "输入扣除额";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void tb_Kce_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar != '\b') && !char.IsDigit(e.KeyChar)) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}

