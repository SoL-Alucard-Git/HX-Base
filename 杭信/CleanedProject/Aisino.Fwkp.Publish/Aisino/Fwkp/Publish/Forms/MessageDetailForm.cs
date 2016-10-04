namespace Aisino.Fwkp.Publish.Forms
{
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using Aisino.Framework.PubData.Message_S2C;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class MessageDetailForm : MessageBaseForm
    {
        private IContainer components;
        private WebBrowser messageDetailTxt;

        internal MessageDetailForm(HtmlMessage mess)
        {
            this.InitializeComponent();
            base._IsMoveFlag = false;
            if (mess != null)
            {
                this.messageDetailTxt.DocumentText = mess.Message;
                this.Text = new HtmlParser(mess.Title).Text();
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(MessageDetailForm));
            this.messageDetailTxt = new WebBrowser();
            base.BodyBounds.SuspendLayout();
            base.BodyClient.SuspendLayout();
            base.SuspendLayout();
            base.lblTitle.Size = new Size(0x300, 30);
            base.lblTitle.Text = "公告详细信息";
            base.TitleArea.Size = new Size(0x318, 30);
            base.BodyBounds.Size = new Size(0x318, 0x220);
            base.BodyClient.Controls.Add(this.messageDetailTxt);
            base.BodyClient.Size = new Size(780, 0x21a);
            this.messageDetailTxt.AllowWebBrowserDrop = false;
            this.messageDetailTxt.Dock = DockStyle.Fill;
            this.messageDetailTxt.Location = new Point(0, 0);
            this.messageDetailTxt.MinimumSize = new Size(20, 20);
            this.messageDetailTxt.Name = "messageDetailTxt";
            this.messageDetailTxt.Size = new Size(780, 0x21a);
            this.messageDetailTxt.TabIndex = 1;
            this.messageDetailTxt.WebBrowserShortcutsEnabled = false;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.GradientActiveCaption;
            base.ClientSize = new Size(0x318, 0x23e);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "MessageDetailForm";
            this.Text = "公告";
            base.Load += new EventHandler(this.MessageDetailForm_Load);
            base.BodyBounds.ResumeLayout(false);
            base.BodyClient.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void MessageDetailForm_Load(object sender, EventArgs e)
        {
            int x = (Screen.PrimaryScreen.WorkingArea.Size.Width - base.Width) / 2;
            int y = (Screen.PrimaryScreen.WorkingArea.Size.Height - base.Height) / 2;
            base.SetDesktopLocation(x, y);
        }
    }
}

