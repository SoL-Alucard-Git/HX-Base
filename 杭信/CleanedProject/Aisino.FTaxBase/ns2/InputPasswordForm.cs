namespace ns2
{
    using Aisino.FTaxBase;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    internal class InputPasswordForm : FormBase
    {
        private Button button1;
        private Button button2;
        private IDisposable idisposable_0;
        private Label label1;
        private Panel panel1;
        private TextBox textBoxPassWord;

        public InputPasswordForm()
        {
            
            this.InitializeComponent_1();
            base.CloseBtnVisible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBoxPassWord.Text.Trim() != "")
            {
                base.DialogResult = DialogResult.OK;
            }
            else if ((this.textBoxPassWord.Text.Trim().Length < 1) || (this.textBoxPassWord.Text.Trim().Length > 8))
            {
                MessageBox.Show("口令长度1-8位，请正确输入口令。", "口令输入");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.idisposable_0 != null))
            {
                this.idisposable_0.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent_1()
        {
            this.textBoxPassWord = new TextBox();
            this.button1 = new Button();
            this.label1 = new Label();
            this.panel1 = new Panel();
            this.button2 = new Button();
            base.BodyBounds.SuspendLayout();
            base.BodyClient.SuspendLayout();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            base.lblTitle.Size = new Size(0x10b, 30);
            base.lblTitle.Text = "金税设备登录";
            base.TitleArea.Size = new Size(0x123, 30);
            base.BodyBounds.Size = new Size(0x123, 170);
            base.BodyClient.Controls.Add(this.panel1);
            base.BodyClient.Controls.Add(this.label1);
            base.BodyClient.Controls.Add(this.textBoxPassWord);
            base.BodyClient.Size = new Size(0x117, 0xa4);
            this.textBoxPassWord.Location = new Point(0x12, 0x3f);
            this.textBoxPassWord.MaxLength = 8;
            this.textBoxPassWord.Name = "textBoxPassWord";
            this.textBoxPassWord.PasswordChar = '*';
            this.textBoxPassWord.Size = new Size(0xf4, 0x15);
            this.textBoxPassWord.TabIndex = 0;
            this.textBoxPassWord.KeyPress += new KeyPressEventHandler(this.textBoxPassWord_KeyPress);
            this.button1.Location = new Point(0x60, 10);
            this.button1.Name = "button1";
            this.button1.Size = new Size(80, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "确认";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.label1.AutoSize = true;
            this.label1.BackColor = Color.Transparent;
            this.label1.Font = new Font("宋体", 10f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = SystemColors.ControlText;
            this.label1.Location = new Point(20, 0x16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x8e, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "请输入金税设备口令";
            this.panel1.BackColor = Color.FromArgb(0xe4, 0xe7, 0xe9);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x74);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x117, 0x30);
            this.panel1.TabIndex = 3;
            this.button2.Location = new Point(0xb6, 10);
            this.button2.Name = "button2";
            this.button2.Size = new Size(80, 30);
            this.button2.TabIndex = 2;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x123, 200);
            base.CloseBtnVisible = true;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "InputPasswordForm";
            this.Text = "输入金税设备一级口令";
            base.BodyBounds.ResumeLayout(false);
            base.BodyClient.ResumeLayout(false);
            base.BodyClient.PerformLayout();
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public void method_1(string string_0)
        {
            this.label1.Text = string_0;
        }

        public string method_2()
        {
            return this.textBoxPassWord.Text.Trim();
        }

        public void method_3(string string_0)
        {
            this.textBoxPassWord.Text = string_0;
        }

        private void textBoxPassWord_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((this.textBoxPassWord.SelectionLength < 1) && (this.textBoxPassWord.Text.Length >= 8)) && (e.KeyChar.ToString() != "\b"))
            {
                e.Handled = true;
            }
            if (e.KeyChar == '\r')
            {
                base.DialogResult = DialogResult.OK;
            }
        }
    }
}

