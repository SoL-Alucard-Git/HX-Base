namespace ns8
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class ErroEndForm : Form
    {
        private AisinoBTN CLE_BT;
        private IDisposable idisposable_0;
        private AisinoTXT MESS_TXT;
        private AisinoPIC pictureBox1;

        internal ErroEndForm()
        {
            
            this.InitializeComponent();
        }

        internal ErroEndForm(string string_0)
        {
            
            this.InitializeComponent();
            this.MESS_TXT.Text = string_0;
        }

        private void CLE_BT_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.idisposable_0 != null))
            {
                this.idisposable_0.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pictureBox1 = new AisinoPIC();
            this.MESS_TXT = new AisinoTXT();
            this.CLE_BT = new AisinoBTN();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            base.SuspendLayout();
            this.pictureBox1.Location = new Point(2, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x70, 0x71);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.MESS_TXT.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.MESS_TXT.BackColor = SystemColors.MenuBar;
            this.MESS_TXT.BorderStyle = BorderStyle.None;
            this.MESS_TXT.Location = new Point(120, 12);
            this.MESS_TXT.Multiline = true;
            this.MESS_TXT.Name = "MESS_TXT";
            this.MESS_TXT.ReadOnly = true;
            this.MESS_TXT.ScrollBars = ScrollBars.Vertical;
            this.MESS_TXT.Size = new Size(0x128, 0x71);
            this.MESS_TXT.TabIndex = 3;
            this.CLE_BT.Anchor = AnchorStyles.Bottom;
            this.CLE_BT.DialogResult = DialogResult.Cancel;
            this.CLE_BT.Location = new Point(0xac, 0x88);
            this.CLE_BT.Name = "CLE_BT";
            this.CLE_BT.Size = new Size(0x4b, 0x17);
            this.CLE_BT.TabIndex = 0;
            this.CLE_BT.Text = "确定";
            this.CLE_BT.UseVisualStyleBackColor = true;
            this.CLE_BT.Click += new EventHandler(this.CLE_BT_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CausesValidation = false;
            base.ClientSize = new Size(0x1ac, 0xab);
            base.ControlBox = false;
            base.Controls.Add(this.MESS_TXT);
            base.Controls.Add(this.pictureBox1);
            base.Controls.Add(this.CLE_BT);
            this.ForeColor = SystemColors.ControlText;
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ErroEndForm";
            base.SizeGripStyle = SizeGripStyle.Hide;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "错误信息";
            base.TopMost = true;
            ((ISupportInitialize) this.pictureBox1).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

