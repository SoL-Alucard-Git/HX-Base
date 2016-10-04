namespace InvAutomation
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class UpdateDescForm : Form
    {
        private Button btnSave;
        private IContainer icontainer_0;
        private Label label2;
        private RichTextBox rtbDesc;

        public UpdateDescForm()
        {
            
            this.InitializeComponent();
        }

        public UpdateDescForm(string text)
        {
            
            this.InitializeComponent();
            this.rtbDesc.Text = text;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            base.Close();
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
            this.label2 = new Label();
            this.btnSave = new Button();
            this.rtbDesc = new RichTextBox();
            base.SuspendLayout();
            this.label2.AutoSize = true;
            this.label2.Font = new Font("宋体", 11f);
            this.label2.ForeColor = Color.Maroon;
            this.label2.Location = new Point(0x12, 13);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x52, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "版本说明：";
            this.btnSave.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnSave.Location = new Point(0x8d, 0x113);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(0x4b, 0x17);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "关  闭";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);
            this.rtbDesc.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.rtbDesc.BorderStyle = BorderStyle.FixedSingle;
            this.rtbDesc.Location = new Point(13, 0x27);
            this.rtbDesc.Name = "rtbDesc";
            this.rtbDesc.ReadOnly = true;
            this.rtbDesc.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.rtbDesc.Size = new Size(330, 220);
            this.rtbDesc.TabIndex = 6;
            this.rtbDesc.Text = "";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x164, 0x133);
            base.Controls.Add(this.rtbDesc);
            base.Controls.Add(this.btnSave);
            base.Controls.Add(this.label2);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "UpdateDescForm";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "版本说明";
            base.TopMost = true;
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

