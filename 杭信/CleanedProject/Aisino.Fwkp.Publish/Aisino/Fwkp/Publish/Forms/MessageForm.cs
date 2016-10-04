namespace Aisino.Fwkp.Publish.Forms
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using Aisino.Framework.PubData.Message_S2C;
    using Aisino.Fwkp.Publish.Entry;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class MessageForm : MessageBaseForm
    {
        private IContainer components;
        private MessageDetailForm mdf;
        private HtmlMessage mess;
        private WebBrowser messText;
        private PubClient parent;
        private Timer reTimer;
        private AisinoLBL showBt;
        private int showSec;

        internal MessageForm(PubClient parent, HtmlMessage mess)
        {
            this.InitializeComponent();
            base._IsMoveFlag = false;
            this.parent = parent;
            this.mess = mess;
            this.messText.DocumentText = mess.Title;
            this.showSec = mess.TipShowSec;
            if (mess.Id.Equals("000000"))
            {
                this.showBt.Hide();
            }
            this.reTimer = new Timer();
            this.reTimer.Interval = 0x3e8;
            this.reTimer.Tick += new EventHandler(this.reTimer_Tick);
            this.reTimer.Start();
        }

        private void closeBt_Click(object sender, EventArgs e)
        {
            this.CloseWin();
        }

        private void CloseWin()
        {
            base.Close();
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
            this.showBt = new AisinoLBL();
            this.messText = new WebBrowser();
            base.BodyBounds.SuspendLayout();
            base.BodyClient.SuspendLayout();
            base.SuspendLayout();
            base.lblTitle.Size = new Size(0x178, 30);
            base.lblTitle.Text = "最新信息";
            base.TitleArea.Size = new Size(400, 30);
            base.BodyBounds.Size = new Size(400, 0x9b);
            base.BodyClient.Controls.Add(this.messText);
            base.BodyClient.Controls.Add(this.showBt);
            base.BodyClient.Size = new Size(0x184, 0x95);
            this.showBt.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.showBt.AutoSize = true;
            this.showBt.BackColor = SystemColors.Desktop;
            this.showBt.BorderStyle = BorderStyle.FixedSingle;
            this.showBt.Cursor = Cursors.Hand;
            this.showBt.ForeColor = Color.White;
            this.showBt.Location = new Point(0x156, 0x84);
            this.showBt.Name = "showBt";
            this.showBt.Size = new Size(0x1f, 14);
            this.showBt.TabIndex = 3;
            this.showBt.Text = "查看";
            this.showBt.Click += new EventHandler(this.showBt_Click);
            this.messText.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.messText.Location = new Point(6, 6);
            this.messText.MinimumSize = new Size(20, 20);
            this.messText.Name = "messText";
            this.messText.ScrollBarsEnabled = false;
            this.messText.Size = new Size(0x178, 0x7b);
            this.messText.TabIndex = 4;
            this.messText.WebBrowserShortcutsEnabled = false;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0x33, 0x57, 0xa6);
            base.ClientSize = new Size(400, 0xb9);
            base.Name = "MessageForm";
            this.Text = "MessageForm";
            base.Load += new EventHandler(this.MessageForm_Load);
            base.BodyBounds.ResumeLayout(false);
            base.BodyClient.ResumeLayout(false);
            base.BodyClient.PerformLayout();
            base.ResumeLayout(false);
        }

        private void MessageForm_Load(object sender, EventArgs e)
        {
            int x = (Screen.PrimaryScreen.WorkingArea.Size.Width - base.Width) - 10;
            int y = (Screen.PrimaryScreen.WorkingArea.Size.Height - base.Height) - 10;
            base.SetDesktopLocation(x, y);
        }

        private void reTimer_Stop()
        {
            if (this.reTimer != null)
            {
                this.reTimer.Enabled = false;
                this.reTimer.Stop();
                this.reTimer.Dispose();
                this.reTimer = null;
            }
        }

        private void reTimer_Tick(object sender, EventArgs e)
        {
            this.showSec--;
            if ((this.parent != null) && this.parent.hasExit)
            {
                if (this.mdf != null)
                {
                    this.mdf.Close();
                }
                this.reTimer_Stop();
                this.CloseWin();
            }
            if (this.showSec == 0)
            {
                this.reTimer_Stop();
                this.CloseWin();
            }
        }

        private void showBt_Click(object sender, EventArgs e)
        {
            this.CloseWin();
            this.mdf = new MessageDetailForm(this.mess);
            this.mdf.ShowDialog();
            this.mdf = null;
        }
    }
}

