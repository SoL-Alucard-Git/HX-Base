namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.MainForm;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class JSKPassword : DockForm
    {
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private IContainer components;
        private AisinoGRP groupBox1;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private AisinoLBL label3;
        private AisinoPNL panel1;
        private AisinoPNL panel2;
        private AisinoPIC pictureBox1;
        private TextBoxRegex txtNewPwd;
        private TextBoxRegex txtOldPwd;
        private TextBoxRegex txtReNewPwd;
        private XmlComponentLoader xmlComponentLoader1;

        public JSKPassword()
        {
            this.Initialize();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!this.checkOldPW())
            {
                return;
            }
            TaxStateInfo stateInfo = base.TaxCardInstance.GetStateInfo(false);
            ushort isLockTime = 0;
            ushort num2 = 0;
            foreach (InvTypeInfo info2 in stateInfo.InvTypeInfo)
            {
                if (info2.InvType == 11)
                {
                    isLockTime = info2.IsLockTime;
                }
                if (info2.InvType == 12)
                {
                    num2 = info2.IsLockTime;
                }
            }
            if (((stateInfo.IsLockReached != 0) || (isLockTime != 0)) || (num2 != 0))
            {
                MessageManager.ShowMsgBox("INP-252201");
            }
            else
            {
                switch (base.TaxCardInstance.SetCardPassword(this.txtOldPwd.Text, this.txtNewPwd.Text))
                {
                    case 0:
                        MessageManager.ShowMsgBox("TCD_9113_", new List<KeyValuePair<string, string>>(), new string[] { "" }, new string[] { "" });
                        goto Label_0171;

                    case 1:
                        MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                        this.txtOldPwd.Focus();
                        this.txtOldPwd.SelectAll();
                        return;

                    case 2:
                        MessageManager.ShowMsgBox("INP-252203");
                        goto Label_0171;
                }
                MessageManager.ShowMsgBox("TCD_9114_", new List<KeyValuePair<string, string>>(), new string[] { "" }, new string[] { "" });
            }
        Label_0171:
            FormMain.RefreashStatus();
            base.Close();
        }

        private bool checkOldPW()
        {
            bool flag = true;
            try
            {
                if (string.IsNullOrEmpty(this.txtOldPwd.Text))
                {
                    MessageManager.ShowMsgBox("TCD_511_");
                    flag = false;
                }
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Initialize()
        {
            this.InitializeComponent();
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.txtNewPwd = this.xmlComponentLoader1.GetControlByName<TextBoxRegex>("txtNewPwd");
            this.txtOldPwd = this.xmlComponentLoader1.GetControlByName<TextBoxRegex>("txtOldPwd");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.groupBox1 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox1");
            this.label2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label2");
            this.label1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label1");
            this.panel1 = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel1");
            this.panel2 = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel2");
            this.txtReNewPwd = this.xmlComponentLoader1.GetControlByName<TextBoxRegex>("txtReNewPwd");
            this.label3 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label3");
            this.pictureBox1 = this.xmlComponentLoader1.GetControlByName<AisinoPIC>("pictureBox1");
            this.txtOldPwd.set_RegexText("^[A-Za-z0-9]*$");
            this.txtNewPwd.set_RegexText("^[A-Za-z0-9]*$");
            this.txtReNewPwd.set_RegexText("^[A-Za-z0-9]{0,8}$");
            this.txtOldPwd.MaxLength = 8;
            this.txtNewPwd.MaxLength = 8;
            this.txtOldPwd.set_IsEmpty(false);
            this.txtNewPwd.set_IsEmpty(false);
            this.txtReNewPwd.set_IsEmpty(false);
            this.txtOldPwd.PasswordChar = '*';
            this.txtNewPwd.PasswordChar = '*';
            this.txtReNewPwd.PasswordChar = '*';
            this.btnOK.Enabled = false;
            this.txtReNewPwd.TextChanged += new EventHandler(this.txtReNewPwd_TextChanged);
            this.txtNewPwd.TextChanged += new EventHandler(this.txtNewPwd_TextChanged);
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(JSKPassword));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(450, 0x1b0);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Bsgl.JSKPassword\Aisino.Fwkp.Bsgl.JSKPassword.xml");
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(450, 0x1b0);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "JSKPassword";
            base.set_TabText("金税设备口令设置");
            this.Text = "金税设备口令设置";
            base.ResumeLayout(false);
        }

        private void txtNewPwd_TextChanged(object sender, EventArgs e)
        {
            if (((this.txtNewPwd.Text.Length > 0) && (this.txtNewPwd.Text.Length < 9)) && (this.txtNewPwd.Text == this.txtReNewPwd.Text))
            {
                this.btnOK.Enabled = true;
            }
            else
            {
                this.btnOK.Enabled = false;
            }
        }

        private void txtReNewPwd_TextChanged(object sender, EventArgs e)
        {
            if (((this.txtReNewPwd.Text.Length > 0) && (this.txtReNewPwd.Text.Length < 9)) && (this.txtReNewPwd.Text == this.txtNewPwd.Text))
            {
                this.btnOK.Enabled = true;
            }
            else
            {
                this.btnOK.Enabled = false;
            }
        }
    }
}

