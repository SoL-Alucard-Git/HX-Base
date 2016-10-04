namespace Aisino.Fwkp.Xtsz.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Xtsz.BLL;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class InitialForm : DockForm
    {
        private AisinoBTN btnCancel = new AisinoBTN();
        private AisinoBTN btnOK = new AisinoBTN();
        private IContainer components;
        private TaxEntityBLL m_taxEntityBLL = new TaxEntityBLL();
        private string managerName = "";
        private int nMonth;
        private AisinoTXT textBoxMonth = new AisinoTXT();
        private AisinoTXT textBoxName = new AisinoTXT();
        private AisinoTXT textBoxUserPasswd = new AisinoTXT();
        private AisinoTXT textBoxYear = new AisinoTXT();
        private XmlComponentLoader xmlComponentLoaderInitial;

        public InitialForm()
        {
            this.Initial();
            this.m_taxEntityBLL.GetLoginName(ref this.managerName);
            this.textBoxName.Text = this.managerName;
            this.textBoxYear.Text = base.TaxCardInstance.GetCardClock().Year.ToString();
            this.textBoxMonth.Text = base.TaxCardInstance.GetCardClock().Month.ToString();
            this.nMonth = base.TaxCardInstance.GetCardClock().Month;
            base.AcceptButton = this.btnOK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.textBoxName.Text.ToString().Trim() != "")
                {
                    string managerName = this.managerName;
                    this.managerName = this.textBoxName.Text.ToString();
                    if (!this.m_taxEntityBLL.DataBack())
                    {
                        MessageManager.ShowMsgBox("INP-231104");
                        return;
                    }
                    if (!this.CleanupData())
                    {
                        MessageManager.ShowMsgBox("INP-231102");
                        return;
                    }
                    if (managerName != this.managerName)
                    {
                        if (!this.UpdateUser(managerName, this.managerName))
                        {
                            MessageManager.ShowMsgBox("INP-231103");
                            return;
                        }
                        this.m_taxEntityBLL.SetManagerName(this.managerName);
                    }
                    this.InvoiceRepair(this.nMonth);
                    string text = this.textBoxYear.Text;
                    if (this.textBoxMonth.Text.Trim().Length < 2)
                    {
                        text = text + "0" + this.textBoxMonth.Text.ToString();
                    }
                    else
                    {
                        text = text + this.textBoxMonth.Text.ToString();
                    }
                    PropertyUtil.SetValue("开帐月份", text);
                    PropertyUtil.Save();
                    base.Close();
                }
                this.textBoxName.Focus();
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-231101", new string[] { exception.Message });
            }
        }

        private bool CleanupData()
        {
            try
            {
                return this.m_taxEntityBLL.CleanTable();
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            return false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Initial()
        {
            this.InitializeComponent();
            base.MinimizeBox = false;
            base.MaximizeBox = false;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.btnOK = this.xmlComponentLoaderInitial.GetControlByName<AisinoBTN>("btnOK");
            this.btnOK.Enabled = false;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel = this.xmlComponentLoaderInitial.GetControlByName<AisinoBTN>("btnCancel");
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.textBoxName = this.xmlComponentLoaderInitial.GetControlByName<AisinoTXT>("textBoxName");
            this.textBoxUserPasswd = this.xmlComponentLoaderInitial.GetControlByName<AisinoTXT>("textBoxUserPasswd");
            this.textBoxUserPasswd.PasswordChar = '*';
            this.textBoxUserPasswd.TextChanged += new EventHandler(this.textBoxUserPasswd_TextChanged);
            this.textBoxYear = this.xmlComponentLoaderInitial.GetControlByName<AisinoTXT>("textBoxYear");
            this.textBoxYear.Enabled = false;
            this.textBoxMonth = this.xmlComponentLoaderInitial.GetControlByName<AisinoTXT>("textBoxMonth");
            this.textBoxMonth.Enabled = false;
            base.Load += new EventHandler(this.InitialForm_Load);
        }

        private void InitialForm_Load(object sender, EventArgs e)
        {
            string text = this.textBoxYear.Text;
            if (this.textBoxMonth.Text.Trim().Length < 2)
            {
                text = text + "0" + this.textBoxMonth.Text.ToString();
            }
            else
            {
                text = text + this.textBoxMonth.Text.ToString();
            }
            PropertyUtil.SetValue("开帐月份", text);
            PropertyUtil.Save();
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(InitialForm));
            this.xmlComponentLoaderInitial = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoaderInitial.BackColor = Color.Transparent;
            this.xmlComponentLoaderInitial.Dock = DockStyle.Fill;
            this.xmlComponentLoaderInitial.Location = new Point(0, 0);
            this.xmlComponentLoaderInitial.Name = "xmlComponentLoaderInitial";
            this.xmlComponentLoaderInitial.Size = new Size(0x1c4, 0x16e);
            this.xmlComponentLoaderInitial.TabIndex = 0;
            this.xmlComponentLoaderInitial.Tag = manager.GetObject("xmlComponentLoaderInitial.Tag");
            this.xmlComponentLoaderInitial.Text = "系统初始化";
            this.xmlComponentLoaderInitial.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Xtsz.InitialForm\Aisino.Fwkp.Xtsz.InitialForm.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1c4, 0x16e);
            base.Controls.Add(this.xmlComponentLoaderInitial);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(0, 0);
            base.Name = "InitialForm";
            base.set_TabText("系统初始化");
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "系统初始化";
            base.ResumeLayout(false);
        }

        private bool InvoiceRepair(int _nMonth)
        {
            try
            {
                return this.m_taxEntityBLL.InvoiceRepair(_nMonth);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
                return false;
            }
        }

        private void textBoxUserPasswd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.m_taxEntityBLL.IsAdmin())
                {
                    if (this.m_taxEntityBLL.IsMatch(this.managerName, this.textBoxUserPasswd.Text.Trim()))
                    {
                        this.btnOK.Enabled = true;
                    }
                    else
                    {
                        this.btnOK.Enabled = false;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-231101", new string[] { exception.Message });
            }
        }

        private bool UpdateUser(string strOldName, string strNewName)
        {
            try
            {
                return this.m_taxEntityBLL.UpdateManager(strOldName, strNewName);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            return false;
        }
    }
}

