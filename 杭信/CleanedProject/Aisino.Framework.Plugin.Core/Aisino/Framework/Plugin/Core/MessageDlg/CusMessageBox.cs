namespace Aisino.Framework.Plugin.Core.MessageDlg
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class CusMessageBox : MessageBaseForm
    {
        private AisinoBTN btnCancel;
        private AisinoBTN btnNo;
        private AisinoBTN btnSend;
        private AisinoBTN btnYes;
        private CustomInfoText customInfoText1;
        private CustomInfoText customInfoText2;
        private IContainer icontainer_1;
        private ImageList imageList_0;
        private AisinoLBL label2;
        private AisinoLBL label3;
        private AisinoLBL label4;
        private AisinoLBL lblDate;
        private AisinoLBL lblEventCode;
        private AisinoLBL lblFunCaption;
        private AisinoLBL lblFunction;
        private AisinoLBL lblOperate;
        private Panel panel1;
        private AisinoPIC picEventIco;
        private Panel pnlBodyRect;
        private AisinoRTX rtbDescript;
        private AisinoRTX rtbSolution;

        public CusMessageBox(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5, string string_6, MessageBoxButtons messageBoxButtons_0, MessageBoxIcon messageBoxIcon_0, MessageBoxDefaultButton messageBoxDefaultButton_0)
        {
            
            this.InitializeComponent_1();
            base.lblTitle.Text = string_0;
            this.lblDate.Text = string_1;
            this.lblOperate.Text = string_2;
            this.lblEventCode.Text = this.method_1(string_3);
            this.rtbDescript.Text = string_4.Replace(@"\n", Environment.NewLine);
            this.rtbSolution.Text = string_5.Replace(@"\n", Environment.NewLine);
            if (!string.Empty.Equals(string_6))
            {
                this.lblFunction.Visible = true;
                this.lblFunCaption.Visible = true;
                this.lblFunction.Text = string_6;
            }
            else
            {
                this.lblFunction.Visible = false;
                this.lblFunCaption.Visible = false;
            }
            this.method_2(messageBoxButtons_0);
            if (messageBoxIcon_0 != MessageBoxIcon.Hand)
            {
                this.btnSend.Visible = false;
            }
            this.method_3(messageBoxIcon_0);
            this.method_4(messageBoxDefaultButton_0);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            base.Visible = false;
            new SendErrMail(this.lblOperate.Text, this.lblEventCode.Text, this.lblFunction.Text, this.rtbDescript.Text, this.rtbSolution.Text).ShowDialog();
            base.Dispose();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            base.Dispose();
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
            this.icontainer_1 = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(CusMessageBox));
            this.imageList_0 = new ImageList(this.icontainer_1);
            this.pnlBodyRect = new Panel();
            this.picEventIco = new AisinoPIC();
            this.customInfoText2 = new CustomInfoText();
            this.lblFunction = new AisinoLBL();
            this.rtbSolution = new AisinoRTX();
            this.label4 = new AisinoLBL();
            this.rtbDescript = new AisinoRTX();
            this.lblFunCaption = new AisinoLBL();
            this.customInfoText1 = new CustomInfoText();
            this.label2 = new AisinoLBL();
            this.lblEventCode = new AisinoLBL();
            this.panel1 = new Panel();
            this.btnNo = new AisinoBTN();
            this.btnCancel = new AisinoBTN();
            this.btnYes = new AisinoBTN();
            this.btnSend = new AisinoBTN();
            this.label3 = new AisinoLBL();
            this.lblOperate = new AisinoLBL();
            this.lblDate = new AisinoLBL();
            base.pnlForm.SuspendLayout();
            base.TitleArea.SuspendLayout();
            base.BodyBounds.SuspendLayout();
            base.BodyClient.SuspendLayout();
            this.pnlBodyRect.SuspendLayout();
            ((ISupportInitialize) this.picEventIco).BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            base.pnlForm.Size = new Size(400, 410);
            base.BodyBounds.Size = new Size(400, 380);
            base.BodyClient.Controls.Add(this.pnlBodyRect);
            base.BodyClient.Size = new Size(0x184, 0x176);
            this.imageList_0.ImageStream = (ImageListStreamer) manager.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Transparent;
            this.imageList_0.Images.SetKeyName(0, "i.png");
            this.imageList_0.Images.SetKeyName(1, "2.png");
            this.imageList_0.Images.SetKeyName(2, "3.png");
            this.imageList_0.Images.SetKeyName(3, "4.png");
            this.pnlBodyRect.BackColor = Color.Transparent;
            this.pnlBodyRect.Controls.Add(this.picEventIco);
            this.pnlBodyRect.Controls.Add(this.customInfoText2);
            this.pnlBodyRect.Controls.Add(this.lblFunction);
            this.pnlBodyRect.Controls.Add(this.rtbSolution);
            this.pnlBodyRect.Controls.Add(this.label4);
            this.pnlBodyRect.Controls.Add(this.rtbDescript);
            this.pnlBodyRect.Controls.Add(this.lblFunCaption);
            this.pnlBodyRect.Controls.Add(this.customInfoText1);
            this.pnlBodyRect.Controls.Add(this.label2);
            this.pnlBodyRect.Controls.Add(this.lblEventCode);
            this.pnlBodyRect.Controls.Add(this.panel1);
            this.pnlBodyRect.Controls.Add(this.label3);
            this.pnlBodyRect.Controls.Add(this.lblOperate);
            this.pnlBodyRect.Controls.Add(this.lblDate);
            this.pnlBodyRect.Dock = DockStyle.Fill;
            this.pnlBodyRect.Location = new Point(0, 0);
            this.pnlBodyRect.Name = "pnlBodyRect";
            this.pnlBodyRect.Size = new Size(0x184, 0x176);
            this.pnlBodyRect.TabIndex = 2;
            this.picEventIco.Location = new Point(0x23, 30);
            this.picEventIco.Name = "picEventIco";
            this.picEventIco.Size = new Size(0x20, 0x20);
            this.picEventIco.TabIndex = 1;
            this.picEventIco.TabStop = false;
            this.customInfoText2.BorderLineColor = Color.FromArgb(11, 0x51, 130);
            this.customInfoText2.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
            this.customInfoText2.ForeColor = Color.FromArgb(11, 0x51, 130);
            this.customInfoText2.Location = new Point(0x13, 0xc9);
            this.customInfoText2.Name = "customInfoText2";
            this.customInfoText2.ShowIcon = false;
            this.customInfoText2.Size = new Size(350, 0x17);
            this.customInfoText2.TabIndex = 10;
            this.customInfoText2.Text = "可能原因及解决办法";
            this.lblFunction.BackColor = Color.Transparent;
            this.lblFunction.Font = new Font("微软雅黑", 9f);
            this.lblFunction.Location = new Point(0x121, 0x3b);
            this.lblFunction.Name = "lblFunction";
            this.lblFunction.Size = new Size(0x44, 0x13);
            this.lblFunction.TabIndex = 0x10;
            this.lblFunction.TextAlign = ContentAlignment.MiddleLeft;
            this.rtbSolution.BackColor = Color.White;
            this.rtbSolution.BorderStyle = BorderStyle.None;
            this.rtbSolution.ForeColor = Color.FromArgb(11, 0x51, 130);
            this.rtbSolution.Location = new Point(0x13, 0xe7);
            this.rtbSolution.Name = "rtbSolution";
            this.rtbSolution.ReadOnly = true;
            this.rtbSolution.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.rtbSolution.Size = new Size(350, 0x55);
            this.rtbSolution.TabIndex = 0;
            this.rtbSolution.Text = "";
            this.label4.BackColor = Color.Transparent;
            this.label4.Font = new Font("微软雅黑", 9f);
            this.label4.Location = new Point(0x4f, 0x3b);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x4d, 0x13);
            this.label4.TabIndex = 5;
            this.label4.Text = "事件代码：";
            this.label4.TextAlign = ContentAlignment.MiddleLeft;
            this.rtbDescript.BackColor = Color.White;
            this.rtbDescript.BorderStyle = BorderStyle.None;
            this.rtbDescript.ForeColor = Color.FromArgb(11, 0x51, 130);
            this.rtbDescript.Location = new Point(0x13, 0x7d);
            this.rtbDescript.Name = "rtbDescript";
            this.rtbDescript.ReadOnly = true;
            this.rtbDescript.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.rtbDescript.Size = new Size(350, 70);
            this.rtbDescript.TabIndex = 0;
            this.rtbDescript.Text = "";
            this.lblFunCaption.BackColor = Color.Transparent;
            this.lblFunCaption.Font = new Font("微软雅黑", 9f);
            this.lblFunCaption.Location = new Point(0xe8, 0x3b);
            this.lblFunCaption.Name = "lblFunCaption";
            this.lblFunCaption.Size = new Size(0x39, 0x13);
            this.lblFunCaption.TabIndex = 15;
            this.lblFunCaption.Text = "功能码：";
            this.lblFunCaption.TextAlign = ContentAlignment.MiddleLeft;
            this.lblFunCaption.Visible = false;
            this.customInfoText1.BorderLineColor = Color.FromArgb(11, 0x51, 130);
            this.customInfoText1.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
            this.customInfoText1.ForeColor = Color.FromArgb(11, 0x51, 130);
            this.customInfoText1.Location = new Point(0x13, 0x5b);
            this.customInfoText1.Name = "customInfoText1";
            this.customInfoText1.ShowIcon = false;
            this.customInfoText1.Size = new Size(350, 0x17);
            this.customInfoText1.TabIndex = 9;
            this.customInfoText1.Text = "事件描述";
            this.label2.BackColor = Color.Transparent;
            this.label2.Font = new Font("微软雅黑", 9f);
            this.label2.Location = new Point(0x4f, 0x10);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x4d, 0x13);
            this.label2.TabIndex = 3;
            this.label2.Text = "日期时间：";
            this.label2.TextAlign = ContentAlignment.MiddleLeft;
            this.lblEventCode.BackColor = Color.Transparent;
            this.lblEventCode.Font = new Font("微软雅黑", 9f);
            this.lblEventCode.Location = new Point(0x98, 0x3b);
            this.lblEventCode.Name = "lblEventCode";
            this.lblEventCode.Size = new Size(0x47, 0x13);
            this.lblEventCode.TabIndex = 12;
            this.lblEventCode.TextAlign = ContentAlignment.MiddleLeft;
            this.panel1.BackColor = Color.FromArgb(0xe4, 0xe7, 0xe9);
            this.panel1.Controls.Add(this.btnNo);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnYes);
            this.panel1.Controls.Add(this.btnSend);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x13f);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x184, 0x37);
            this.panel1.TabIndex = 2;
            this.btnNo.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnNo.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnNo.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnNo.DialogResult = DialogResult.No;
            this.btnNo.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btnNo.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnNo.ForeColor = Color.White;
            this.btnNo.ImageKey = "(无)";
            this.btnNo.Location = new Point(0xd6, 14);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new Size(0x4b, 0x1d);
            this.btnNo.TabIndex = 0x12;
            this.btnNo.Text = "否";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new EventHandler(this.btnYes_Click);
            this.btnCancel.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnCancel.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnCancel.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btnCancel.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.ImageKey = "(无)";
            this.btnCancel.Location = new Point(0x126, 14);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x1d);
            this.btnCancel.TabIndex = 0x11;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnYes_Click);
            this.btnYes.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnYes.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnYes.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnYes.DialogResult = DialogResult.OK;
            this.btnYes.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btnYes.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnYes.ForeColor = Color.White;
            this.btnYes.ImageKey = "(无)";
            this.btnYes.Location = new Point(0x86, 14);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new Size(0x4b, 0x1d);
            this.btnYes.TabIndex = 15;
            this.btnYes.Text = "是";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new EventHandler(this.btnYes_Click);
            this.btnSend.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnSend.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnSend.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnSend.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btnSend.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnSend.ForeColor = Color.White;
            this.btnSend.Location = new Point(0x36, 14);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new Size(0x4b, 0x1d);
            this.btnSend.TabIndex = 0x10;
            this.btnSend.Text = "提交报告";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new EventHandler(this.btnSend_Click);
            this.label3.BackColor = Color.Transparent;
            this.label3.Font = new Font("微软雅黑", 9f);
            this.label3.Location = new Point(0x4f, 0x25);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x4d, 0x13);
            this.label3.TabIndex = 4;
            this.label3.Text = "操  作 项：";
            this.label3.TextAlign = ContentAlignment.MiddleLeft;
            this.lblOperate.BackColor = Color.Transparent;
            this.lblOperate.Font = new Font("微软雅黑", 9f);
            this.lblOperate.Location = new Point(0x98, 0x25);
            this.lblOperate.Name = "lblOperate";
            this.lblOperate.Size = new Size(0xb9, 0x13);
            this.lblOperate.TabIndex = 11;
            this.lblOperate.TextAlign = ContentAlignment.MiddleLeft;
            this.lblDate.BackColor = Color.Transparent;
            this.lblDate.Font = new Font("微软雅黑", 9f);
            this.lblDate.Location = new Point(0x98, 0x10);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new Size(0xb9, 0x13);
            this.lblDate.TabIndex = 10;
            this.lblDate.TextAlign = ContentAlignment.MiddleLeft;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(400, 410);
            base.Name = "CusMessageBox";
            this.Text = "CusMessageBox";
            base.TopMost = true;
            base.pnlForm.ResumeLayout(false);
            base.TitleArea.ResumeLayout(false);
            base.BodyBounds.ResumeLayout(false);
            base.BodyClient.ResumeLayout(false);
            this.pnlBodyRect.ResumeLayout(false);
            ((ISupportInitialize) this.picEventIco).EndInit();
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private string method_1(string string_0)
        {
            if (string_0.IndexOf("-") > -1)
            {
                return string_0.Substring(string_0.IndexOf("-") + 1);
            }
            return string_0;
        }

        private void method_2(MessageBoxButtons messageBoxButtons_0)
        {
            int x = 0;
            int y = 0;
            switch (messageBoxButtons_0)
            {
                case MessageBoxButtons.OK:
                    this.btnYes.Text = "确认";
                    this.btnYes.DialogResult = DialogResult.OK;
                    this.btnYes.Location = this.btnCancel.Location;
                    this.btnNo.Visible = false;
                    this.btnCancel.Visible = false;
                    x = (this.btnYes.Location.X - this.btnSend.Width) - 5;
                    y = this.btnYes.Location.Y;
                    this.btnSend.Location = new Point(x, y);
                    return;

                case MessageBoxButtons.OKCancel:
                    this.btnYes.Text = "确认";
                    this.btnYes.DialogResult = DialogResult.OK;
                    this.btnNo.Visible = false;
                    this.btnCancel.Text = "取消";
                    this.btnCancel.DialogResult = DialogResult.Cancel;
                    x = (this.btnCancel.Location.X - this.btnYes.Width) - 5;
                    y = this.btnCancel.Location.Y;
                    this.btnYes.Location = new Point(x, y);
                    x = (this.btnYes.Location.X - this.btnSend.Width) - 5;
                    y = this.btnYes.Location.Y;
                    this.btnSend.Location = new Point(x, y);
                    return;

                case MessageBoxButtons.AbortRetryIgnore:
                    this.btnYes.Text = "终止";
                    this.btnYes.DialogResult = DialogResult.Abort;
                    this.btnNo.Text = "重试";
                    this.btnNo.DialogResult = DialogResult.Retry;
                    this.btnCancel.Text = "忽略";
                    this.btnCancel.DialogResult = DialogResult.Ignore;
                    return;

                case MessageBoxButtons.YesNoCancel:
                    this.btnYes.Text = "是";
                    this.btnYes.DialogResult = DialogResult.OK;
                    this.btnNo.Text = "否";
                    this.btnNo.DialogResult = DialogResult.No;
                    this.btnCancel.Text = "取消";
                    this.btnCancel.DialogResult = DialogResult.Cancel;
                    return;

                case MessageBoxButtons.YesNo:
                    this.btnYes.Text = "是";
                    this.btnYes.DialogResult = DialogResult.Yes;
                    this.btnNo.Text = "否";
                    this.btnNo.DialogResult = DialogResult.No;
                    x = this.btnCancel.Location.X;
                    y = this.btnCancel.Location.Y;
                    this.btnNo.Location = new Point(x, y);
                    this.btnCancel.Visible = false;
                    x = (this.btnNo.Location.X - this.btnYes.Width) - 5;
                    y = this.btnNo.Location.Y;
                    this.btnYes.Location = new Point(x, y);
                    x = (this.btnYes.Location.X - this.btnSend.Width) - 5;
                    y = this.btnYes.Location.Y;
                    this.btnSend.Location = new Point(x, y);
                    return;

                case MessageBoxButtons.RetryCancel:
                    this.btnNo.Text = "重试";
                    this.btnNo.DialogResult = DialogResult.Retry;
                    this.btnCancel.Text = "取消";
                    this.btnCancel.DialogResult = DialogResult.Cancel;
                    this.btnYes.Visible = false;
                    x = (this.btnCancel.Location.X - this.btnNo.Width) - 5;
                    y = this.btnCancel.Location.Y;
                    this.btnNo.Location = new Point(x, y);
                    x = (this.btnNo.Location.X - this.btnSend.Width) - 5;
                    y = this.btnNo.Location.Y;
                    this.btnSend.Location = new Point(x, y);
                    return;
            }
        }

        private void method_3(MessageBoxIcon messageBoxIcon_0)
        {
            MessageBoxIcon icon = messageBoxIcon_0;
            if (icon <= MessageBoxIcon.Hand)
            {
                switch (icon)
                {
                    case MessageBoxIcon.None:
                        this.picEventIco.Image = null;
                        break;

                    case MessageBoxIcon.Hand:
                        this.picEventIco.Image = this.imageList_0.Images[2];
                        break;
                }
            }
            else
            {
                switch (icon)
                {
                    case MessageBoxIcon.Question:
                        this.picEventIco.Image = this.imageList_0.Images[3];
                        return;

                    case MessageBoxIcon.Exclamation:
                        this.picEventIco.Image = this.imageList_0.Images[1];
                        return;
                }
                if (icon == MessageBoxIcon.Asterisk)
                {
                    this.picEventIco.Image = this.imageList_0.Images[0];
                }
            }
        }

        private void method_4(MessageBoxDefaultButton messageBoxDefaultButton_0)
        {
            if (messageBoxDefaultButton_0 == MessageBoxDefaultButton.Button1)
            {
                if (this.btnYes.Visible)
                {
                    this.btnYes.Focus();
                }
                else if (this.btnNo.Visible)
                {
                    this.btnNo.Focus();
                }
                else if (this.btnCancel.Visible)
                {
                    this.btnCancel.Focus();
                }
            }
            if (messageBoxDefaultButton_0 == MessageBoxDefaultButton.Button2)
            {
                if (this.btnYes.Visible)
                {
                    if (this.btnNo.Visible)
                    {
                        this.btnNo.Focus();
                    }
                    else if (this.btnCancel.Visible)
                    {
                        this.btnCancel.Focus();
                    }
                    else
                    {
                        this.btnYes.Focus();
                    }
                }
                else if (this.btnNo.Visible)
                {
                    if (this.btnCancel.Visible)
                    {
                        this.btnCancel.Focus();
                    }
                    else
                    {
                        this.btnNo.Focus();
                    }
                }
                else if (this.btnCancel.Visible)
                {
                    this.btnCancel.Focus();
                }
            }
            if (messageBoxDefaultButton_0 == MessageBoxDefaultButton.Button3)
            {
                if (this.btnCancel.Visible)
                {
                    this.btnCancel.Focus();
                }
                else if (this.btnNo.Visible)
                {
                    this.btnNo.Focus();
                }
                else if (this.btnYes.Visible)
                {
                    this.btnYes.Focus();
                }
            }
        }
    }
}

