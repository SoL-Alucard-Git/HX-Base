namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Wbjk.BLL;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ExcelPassword : BaseForm
    {
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private IContainer components = null;
        private AisinoGRP groupBox1;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private string passWord = "";
        private AisinoRDO rdbtnB;
        private AisinoRDO rdBtnDrag;
        private AisinoTXT textBox1;
        private AisinoTXT textBox2;

        public ExcelPassword()
        {
            this.InitializeComponent();
            this.textBox1.Text = TaxCardValue.taxCard.GetCardClock().Year.ToString();
            this.passWord = "Excel" + this.textBox1.Text;
            this.textBox1.ReadOnly = true;
            this.btnOK.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.textBox2.Text != this.passWord)
            {
                MessageManager.ShowMsgBox("INP-271206");
            }
            else if (this.rdBtnDrag.Checked)
            {
                base.DialogResult = DialogResult.Yes;
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ExcelPassword));
            this.btnCancel = new AisinoBTN();
            this.btnOK = new AisinoBTN();
            this.groupBox1 = new AisinoGRP();
            this.textBox2 = new AisinoTXT();
            this.textBox1 = new AisinoTXT();
            this.label2 = new AisinoLBL();
            this.label1 = new AisinoLBL();
            this.rdbtnB = new AisinoRDO();
            this.rdBtnDrag = new AisinoRDO();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0xd3, 0x85);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "放弃";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnOK.Location = new Point(130, 0x85);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.groupBox1.BackColor = Color.Transparent;
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(0x18, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xf7, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Excel维护设置口令";
            this.textBox2.Location = new Point(0x5d, 0x42);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(100, 0x15);
            this.textBox2.TabIndex = 0;
            this.textBox2.UseSystemPasswordChar = true;
            this.textBox2.TextChanged += new EventHandler(this.textBox2_TextChanged);
            this.textBox2.KeyPress += new KeyPressEventHandler(this.textBoxPassWord_KeyPress);
            this.textBox1.Location = new Point(0x5d, 0x1c);
            this.textBox1.MaxLength = 4;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(100, 0x15);
            this.textBox1.TabIndex = 1;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x16, 0x47);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "授权口令：";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x16, 0x1f);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "年    份：";
            this.rdbtnB.AutoSize = true;
            this.rdbtnB.BackColor = Color.Transparent;
            this.rdbtnB.Location = new Point(0x18, 0x76);
            this.rdbtnB.Name = "rdbtnB";
            this.rdbtnB.Size = new Size(0x47, 0x10);
            this.rdbtnB.TabIndex = 3;
            this.rdbtnB.Text = "标准设置";
            this.rdbtnB.UseVisualStyleBackColor = false;
            this.rdbtnB.Visible = false;
            this.rdBtnDrag.AutoSize = true;
            this.rdBtnDrag.BackColor = Color.Transparent;
            this.rdBtnDrag.Checked = true;
            this.rdBtnDrag.Location = new Point(0x18, 140);
            this.rdBtnDrag.Name = "rdBtnDrag";
            this.rdBtnDrag.Size = new Size(0x47, 0x10);
            this.rdBtnDrag.TabIndex = 4;
            this.rdBtnDrag.TabStop = true;
            this.rdBtnDrag.Text = "拖拉设置";
            this.rdBtnDrag.UseVisualStyleBackColor = false;
            this.rdBtnDrag.Visible = false;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x125, 0xa5);
            base.Controls.Add(this.rdBtnDrag);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.rdbtnB);
            base.Controls.Add(this.groupBox1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(0, 0);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ExcelPassword";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "请输入授权口令";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (this.passWord == this.textBox2.Text)
            {
                this.btnOK.Enabled = true;
            }
            else
            {
                this.btnOK.Enabled = false;
            }
        }

        private void textBoxPassWord_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.btnOK_Click(sender, new EventArgs());
            }
        }
    }
}

